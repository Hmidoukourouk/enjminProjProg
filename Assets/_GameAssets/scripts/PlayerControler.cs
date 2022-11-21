using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerControler : MonoBehaviour
{
    public float forwardSpeed = 1f;
    public float turnSpeed = 1f;
    public bool canShoot;
    public PlayerShooting refShooting;
    public int playerNumber = 0;

    Rigidbody rg;
    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();
        refShooting.playerNumber = playerNumber;
        CameraControler.instance.players.Add(transform);
        Debug.Log(CameraControler.instance);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float vertical = Input.GetAxis("Vertical") * Time.deltaTime * forwardSpeed;
        float horizontal = Input.GetAxis("Horizontal") * Time.deltaTime * turnSpeed;
        rg.transform.position = rg.transform.position + (rg.transform.forward * vertical);
        rg.rotation = Quaternion.Euler(rg.rotation.eulerAngles + new Vector3(0, horizontal, 0));
    }

    private void Update()
    {
        
    }
}
