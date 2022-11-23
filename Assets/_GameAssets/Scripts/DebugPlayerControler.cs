using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class DebugPlayerControler : NetworkBehaviour
{
    [SerializeField] float speed = 2f;
    // Start is called before the first frame update
    bool waitover;
    [SerializeField] bool isClientes;
    [SyncVar]Vector3 pos;
    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        //if (!isLocalPlayer) enabled = false;//aaa

        CameraControler.instance.players.Add(transform);
        waitover = true;
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        isClientes = isClient;
        if (!waitover) return;
        if (!isLocalPlayer) return;//aaa
        
        Vector2 input = GameManager.input.Tank.Movement.ReadValue<Vector2>();

        MovingServeur(input);
    }

    [Command]
    void MovingServeur(Vector2 input)
    {
        transform.position += new Vector3(input.x, 0, input.y) * Time.deltaTime * speed;
    }

    
}
