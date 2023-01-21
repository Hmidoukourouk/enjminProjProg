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

    [SerializeField] [SyncVar] Quaternion rotaTemp;

    [SyncVar][HideInInspector] public Vector3 hitLocation;

    private void Start()
    {
        if (!isLocalPlayer) enabled = false;

         // donner le controle au client

        cam = Camera.main;
    }
    void Update()
    {
        if (!isLocalPlayer) return;

        Vector2 input = GameManager.input.Tank.Aim.ReadValue<Vector2>();
        Ray ray = cam.ScreenPointToRay(input);
        RotateTurret(ray);
    }
    void RotateTurret(Ray ray)
    {
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            storedTransform.LookAt(new Vector3(hit.point.x, turret.transform.position.y, hit.point.z), storedTransform.up);
            storedTransform.localRotation = Quaternion.Euler(0, storedTransform.localRotation.eulerAngles.y, 0);

            //turret.transform.localRotation = Quaternion.Lerp(turret.transform.localRotation, storedTransform.localRotation, Time.deltaTime * turnSpeed);
            
            rotaTemp = Quaternion.Lerp(rotaTemp, storedTransform.localRotation, Time.deltaTime * turnSpeed);

            CastServ(rotaTemp);

            hitLocation = hit.point;
        }
    }

[Command]
    void CastServ(Quaternion rota)
    {
        turret.transform.localRotation = rota;
    }

}
