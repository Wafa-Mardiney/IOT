using UnityEngine;
using System.Threading;
using System.IO;
using System;
using UnityEngine.UI;

public class ArduinoManager : MonoBehaviour
{
    public static ArduinoManager instance;
    public GameObject rotatableObject;
    public Vector3 rotatableObjectRotation;
    float previous, current;
    public float speed = 0;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            ReadCOMandBaud();
        }
        DontDestroyOnLoad(this);
        rotatableObjectRotation = rotatableObject.transform.localRotation.eulerAngles;
    }
    string lastSerialMessage = null;
    public string LastSerialMessage { get { return lastSerialMessage; } }
    void ReadMessages()
    {
        if (!StartReading) { return; }
        lastSerialMessage = ReadSerialMessage();
        if (lastSerialMessage == null)
            return;
        if (float.TryParse(LastSerialMessage, out current))
        {
            current = float.Parse(LastSerialMessage);
            float delta = Mathf.Abs(previous - current);
            bool isClockwise = (previous > current);
            if (delta > 10)
                delta = 0;
            delta = (isClockwise ? 1 : -1) * delta;
            rotatableObject.transform.RotateAroundLocal(rotatableObject.transform.up, delta * speed);
            previous = float.Parse(LastSerialMessage);
        }
    }

    #region ReadWriteOperations

    [Serializable]
    public class ArduinoJSON
    {
        public string COM;
        public string BaudRate;
    }

    ArduinoJSON jsonFile;

    bool StartReading = false;

    void ReadCOMandBaud()
    {
        string path = Application.dataPath + "/../Arduino.json";
        string json = File.ReadAllText(path);

        jsonFile = JsonUtility.FromJson<ArduinoJSON>(json);

        portName = jsonFile.COM;
        baudRate = int.Parse(jsonFile.BaudRate);

        ConnectArduino();
        StartReading = true;
    }

    #endregion

    #region ArduinoSerialOperations

    string portName;
    public string PortName { get { return portName; } }
    int baudRate = 9600;
    public int BaudRate { get { return baudRate; } }
    GameObject messageListener;

    [Tooltip("After an error in the serial communication, or an unsuccessful " + "connect, how many milliseconds we should wait.")]
    [SerializeField] int reconnectionDelay = 1000;

    [Tooltip("Maximum number of unread data messages in the queue. " + "New messages will be discarded.")]
    [SerializeField] int maxUnreadMessages = 1;

    const string SERIAL_DEVICE_CONNECTED = "Connected";
    const string SERIAL_DEVICE_DISCONNECTED = "Disconnected";

    protected Thread thread;
    protected SerialThreadLines serialThread;

    public void ConnectArduino()
    {
        serialThread = new SerialThreadLines(portName,
                                             baudRate,
                                             reconnectionDelay,
                                             maxUnreadMessages);
        thread = new Thread(new ThreadStart(serialThread.RunForever));
        thread.Start();
    }

    public void OnDisable()
    {
        if (userDefinedTearDownFunction != null)
            userDefinedTearDownFunction();

        if (serialThread != null)
        {
            serialThread.RequestStop();
            serialThread = null;
        }

        if (thread != null)
        {
            thread.Join();
            thread = null;
        }
    }

    public delegate void TearDownFunction();
    private TearDownFunction userDefinedTearDownFunction;
    public void SetTearDownFunction(TearDownFunction userFunction)
    {
        this.userDefinedTearDownFunction = userFunction;
    }

    public string ReadSerialMessage()
    {
        // Read the next message from the queue
        return (string)serialThread.ReadMessage();
    }

    public void SendSerialMessage(string message)
    {
        serialThread.SendMessage(message);
    }

    void HandleSerialMessageQueue()
    {
        // If the user prefers to poll the messages instead of receiving them
        // via SendMessage, then the message listener should be null.
        if (messageListener == null)
            return;

        // Read the next message from the queue
        string message = (string)serialThread.ReadMessage();
        if (message == null)
            return;

        // Check if the message is plain data or a connect/disconnect event.
        if (ReferenceEquals(message, SERIAL_DEVICE_CONNECTED))
            messageListener.SendMessage("OnConnectionEvent", true);
        else if (ReferenceEquals(message, SERIAL_DEVICE_DISCONNECTED))
        {
            print("Disconnected");
        }
        else
            messageListener.SendMessage("OnMessageArrived", message);
    }

    #endregion

    #region ArduinoDebugUI
    [Header("UI Elements Settings")]
    [SerializeField] GameObject ADU;
    [SerializeField] Text COM;
    [SerializeField] Text Baud;
    [SerializeField] Text Message;
    [SerializeField] InputField input;


    bool allowEnter;

    void UpdateDebugUI()
    {
        if (Input.GetKeyDown(KeyCode.A) && Input.GetKey(KeyCode.LeftControl))
        { ADU.gameObject.SetActive(!ADU.gameObject.activeSelf); }

        if (allowEnter && (input.text.Length > 0) && (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter)))
        { allowEnter = false; SendSerialMessage(input.text); }
        else
        { allowEnter = input.isFocused; }

        COM.text = portName;
        Baud.text = baudRate.ToString();
        if (LastSerialMessage == null)
        { return; }
        else
        { Message.text = LastSerialMessage; }
    }

    public void SendSerialMessageFromDebugInput()
    {
        SendSerialMessage(input.text);
    }

    #endregion

    private void Update()
    {
        HandleSerialMessageQueue();
        ReadMessages();
        UpdateDebugUI();
    }

}
