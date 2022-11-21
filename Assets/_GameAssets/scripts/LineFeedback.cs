using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

public class LineFeedback : MonoBehaviour
{
    LineRenderer line;
    
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);
            Vector3[] positionArray = new[] { transform.position, hit.point };
            line.SetPositions(positionArray);
            line.enabled = true;
        }
        else
        {
            line.enabled = false;
        }
    }
}
