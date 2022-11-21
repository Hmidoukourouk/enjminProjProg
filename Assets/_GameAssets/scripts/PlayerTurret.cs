using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurret : MonoBehaviour
{
    public float turnSpeed = 2f;
    [SerializeField] bool usingMouse = true;
    [SerializeField] Camera cam;
    [SerializeField] Transform storedTransform;

    private void Start()
    {
        cam = Camera.main;
    }
    void Update()
    {
        if (!usingMouse)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, Input.GetAxis("TurretTurn"), 0) * Time.deltaTime * turnSpeed + transform.rotation.eulerAngles);
        }
        else
        {
            RaycastHit hit;

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                storedTransform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z), storedTransform.up);
                Debug.Log(storedTransform.rotation.eulerAngles);
                transform.rotation = Quaternion.Lerp(transform.rotation, storedTransform.rotation, Time.deltaTime * turnSpeed);
            }
        }
        
    }
}
