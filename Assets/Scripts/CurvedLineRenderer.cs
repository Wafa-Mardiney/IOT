using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CurvedLineRenderer : MonoBehaviour
{
    //PUBLIC
    public float lineSegmentSize = 0.15f;
    public float lineWidth = 0.1f;
    [Header("Gizmos")]
    public bool showGizmos = true;
    public float gizmoSize = 0.1f;
    public Color gizmoColor = new Color(1, 0, 0, 0.5f);
    //PRIVATE
    private CurvedLinePoint[] linePoints = new CurvedLinePoint[0];
    private Vector3[] linePositions = new Vector3[0];
    private Vector3[] linePositionsOld = new Vector3[0];
    LineRenderer line;
    Material material;
    float offset;
    Vector3[] smoothedPoints;
    private void Awake()
    {
        line = this.GetComponent<LineRenderer>();
        material = line.sharedMaterial;


        //find curved points in children
        linePoints = this.GetComponentsInChildren<CurvedLinePoint>();
        linePositions = new Vector3[linePoints.Length];
        linePositionsOld = new Vector3[linePositions.Length];
        FillOldPositions();
    }
    // Update is called once per frame
    public void Update()
    {
        GetPoints();
        SetPointsToLine();
        offset = Time.time * -2;
        material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }

    void GetPoints()
    {
        //add positions
        for (int i = 0; i < linePoints.Length; i++)
            linePositions[i] = linePoints[i].transform.position;
    }
    void FillOldPositions()
    {
        for (int i = 0; i < linePositions.Length; i++)
            linePositionsOld[i] = linePositions[i];
    }
    void SetPointsToLine()
    {
        //check if line points have moved
        bool moved = false;
        for (int i = 0; i < linePositions.Length; i++)
        {
            //compare
            if (linePositions[i] != linePositionsOld[i])
                moved = true;
        }

        //update if moved
        if (moved == true)
        {
            //get smoothed values
            smoothedPoints = LineSmoother.SmoothLine(linePositions, lineSegmentSize);

            //set line settings
            line.SetVertexCount(smoothedPoints.Length);
            line.SetPositions(smoothedPoints);
            line.SetWidth(lineWidth, lineWidth);
            FillOldPositions();
        }
    }

    void OnDrawGizmosSelected()
    {
        Update();
    }

    void OnDrawGizmos()
    {
        if (linePoints.Length == 0)
        {
            GetPoints();
        }

        //settings for gizmos
        foreach (CurvedLinePoint linePoint in linePoints)
        {
            linePoint.showGizmo = showGizmos;
            linePoint.gizmoSize = gizmoSize;
            linePoint.gizmoColor = gizmoColor;
        }
    }
}
