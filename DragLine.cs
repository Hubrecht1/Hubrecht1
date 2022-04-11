using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragLine : MonoBehaviour
{
    LineRenderer _lineRenderer;
    private Bird _bird;

    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>(); 
        _bird = FindObjectOfType<Bird>();
        Vector3 lineZeroPosition = new Vector3(_bird.transform.position.x, transform.position.y, -0.1f);
        _lineRenderer.SetPosition(0, _bird.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (_bird.IsDragging)
        {
            float distancebird = Vector2.Distance(_lineRenderer.transform.position, _bird.transform.position);
            _lineRenderer.enabled = true;
            _lineRenderer.SetPosition(1, _bird.transform.position);
        }
        else
        {
            _lineRenderer.enabled = false;
        }
    }
}
