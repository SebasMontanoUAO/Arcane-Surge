using UnityEngine;

public class AuraRangeVisual : MonoBehaviour
{
    public int segments = 50; 
    public float width = 0.1f;
    public Color color = new Color(1, 0, 0, 0.5f); 

    private LineRenderer lineRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;

        DrawCircle(GetComponent<AuraAbility>().Range);
    }

    void DrawCircle(float radius)
    {
        lineRenderer.positionCount = segments + 1;
        float angle = 0f;

        for (int i = 0; i < segments + 1; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            float z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            lineRenderer.SetPosition(i, new Vector3(x, 0.1f, z));
            angle += 360f / segments;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
