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
        //if (!isLocalPlayer) enabled = false;

        cam = Camera.main;
    }
    void Update()
    {
        if (!isLocalPlayer) return;

        Vector2 input = GameManager.input.Tank.Aim.ReadValue<Vector2>();
        Ray ray = cam.ScreenPointToRay(input);
        RotateTurretServeur(ray);
    }

    [Command]
    void RotateTurretServeur(Ray ray)
    {
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            storedTransform.LookAt(new Vector3(hit.point.x, turret.transform.position.y, hit.point.z), storedTransform.up);

            turret.transform.rotation = Quaternion.Lerp(turret.transform.rotation, storedTransform.rotation, Time.deltaTime * turnSpeed);
        }
    }
}
