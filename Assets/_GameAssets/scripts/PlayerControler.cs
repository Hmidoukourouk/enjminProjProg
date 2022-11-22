using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

[RequireComponent(typeof(Rigidbody))]

public class PlayerControler : NetworkBehaviour
{
    [SyncVar] public float health = 20f;
    public float forwardSpeed = 1f;
    public float turnSpeed = 1f;
    public bool canShoot;
    public PlayerShooting refShooting;
    public int playerNumber = 0;
    Rigidbody rb;
    bool isNotControlable;

    [SerializeField] bool isServ;
    [SerializeField] bool isCli;
    [SerializeField] bool autho;
    [SerializeField] bool owneris;
    [SerializeField] bool localPlay;

    private void Awake()
    {
        MainBullet.onPlayerHit += TakeDamage;
    }

    void Start()
    {
        if (!isLocalPlayer) enabled = false;

        rb = GetComponent<Rigidbody>();
        refShooting.playerNumber = playerNumber;

        CameraControler.instance.players.Add(transform);

        GameManager.GM.players.Add(this);
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
    }

    [Command]
    void AssignClientAuth()
    {
        netIdentity.AssignClientAuthority(connectionToClient);
    }

    private void TakeDamage(PlayerControler playerControler)
    {
        if (playerControler == this)
            return;

        TakeDamageCmd(5f);
    }

    [Command]
    //[ClientRpc]
    private void TakeDamageCmd(float damage)
    {
        health -= damage;
    }

    void FixedUpdate()
    {
        isServ = isServer;
        autho = authority;
        isCli = isClient;
        owneris = isOwned;
        localPlay = isLocalPlayer;


        if (!isLocalPlayer || isNotControlable) { return; };

        Debug.Log(isServ + " " + autho + " " + isCli + " " + owneris + " " + localPlay);

        Vector2 input = GameManager.input.Tank.Movement.ReadValue<Vector2>();
        float vertical = input.y * Time.deltaTime * forwardSpeed;
        float horizontal = input.x * Time.deltaTime * turnSpeed;
        rb.transform.position = rb.transform.position + (rb.transform.forward * vertical);
        rb.rotation = Quaternion.Euler(rb.rotation.eulerAngles + new Vector3(0, horizontal, 0));


    }


}
