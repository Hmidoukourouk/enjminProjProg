using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerTurret : NetworkBehaviour
{
    public float turnSpeed = 2f;
    [SerializeField] Camera cam;
    [SerializeField] Transform storedTransform;
    [SerializeField] Transform turret;

    private void Start()
    {
        if (isOwned) enabled = false;
        cam = Camera.main;
    }
    void Update()
    {
        RaycastHit hit;

        Ray ray = cam.ScreenPointToRay(GameManager.input.Tank.Aim.ReadValue<Vector2>());

        if (Physics.Raycast(ray, out hit))
        {
            storedTransform.LookAt(new Vector3(hit.point.x, turret.transform.position.y, hit.point.z), storedTransform.up);
            Debug.Log(storedTransform.rotation.eulerAngles);
            turret.transform.rotation = Quaternion.Lerp(turret.transform.rotation, storedTransform.rotation, Time.deltaTime * turnSpeed);
        }
    }
}
