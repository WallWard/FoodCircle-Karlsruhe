using UnityEngine;
using UnityEngine.UI;

public class GraphController : MonoBehaviour
{
    private float bio;
    private float emissionen;
    private float müll;

    public float updateInterval = 1f;  // Update the graph every second

    public LineRenderer lineRenderer;

    public GameObject abfallcontroller;

    void Start()
    {
        // Assuming the LineRenderer is attached to the child Image representing the line
        //lineRenderer = GetComponentInChildren<LineRenderer>();
        InvokeRepeating("UpdateGraph", 0f, updateInterval);
    }

    void UpdateGraph()
    {
        var abfContrl = abfallcontroller.GetComponent<AbfallController>();


        // Update your values here
        bio = abfContrl.biomasse / 10;
        emissionen = abfContrl.emission / 1000;
        müll = abfContrl.müll / 1000;

        // Add new points to the line renderer
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, new Vector3(Time.time, bio, 0.0f));
    }
}
