using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider2D))]
public class ScreenBoxEditor : MonoBehaviour
{
    [SerializeField] bool showCollider;
    [SerializeField] Color color = Color.blue;

    BoxCollider2D _collider;

    // Start is called before the first frame update
    void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    private void OnDrawGizmos()
    {
        if (showCollider)
        {
            Gizmos.color = color;
            DrawBounds();
        }
    }
    void DrawBounds()
    {
        var min = _collider.bounds.min;
        var max = _collider.bounds.max;
        Vector2[] points = new Vector2[] { min, new Vector2(min.x, max.y), max, new Vector2(max.x, min.y) };
        for(int i = 0; i < points.Length-1; i++)
        {
            Gizmos.DrawLine(transform.TransformPoint(points[i]), transform.TransformPoint(points[i + 1]));
        }
        Gizmos.DrawLine(transform.TransformPoint(points[points.Length - 1]), transform.TransformPoint(points[0]));
    }
}
