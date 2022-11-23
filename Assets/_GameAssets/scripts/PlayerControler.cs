using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]

public class PlayerControler : NetworkBehaviour
{
    [SyncVar] public float health = 20f;
    float healthMax;
    float stretchMax;
    public float forwardSpeed = 1f;
    public float turnSpeed = 1f;
    [SerializeField] Image imgHeath;
    public PlayerShooting refShooting;

    public Rigidbody rb;
    bool isNotControlable = true;

    [SerializeField] bool isServ;
    [SerializeField] bool isCli;
    [SerializeField] bool autho;
    [SerializeField] bool owneris;
    [SerializeField] bool localPlay;

    private void Awake()
    {
        stretchMax = imgHeath.transform.localScale.x;
        healthMax = health;
        MainBullet.onPlayerHit += TakeDamage;
    }
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        
        if (isLocalPlayer) CameraControler.instance.players.Add(transform);

        GameManager.GM.players.Add(this);

        isNotControlable = false;
    }

    public int playerNumber()
    {
        return refShooting.playerNumber;
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
        imgHeath.transform.localScale = new Vector2(healthMax/health, imgHeath.transform.localScale.y);
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

        MovingServeur(input);

    }

    [Command]
    void MovingServeur(Vector2 input)
    {
        if (rb == null)
        {
            Debug.Log("bordel de rigidbody"); 
            rb = GetComponent<Rigidbody>(); 
            return;
        }

        float vertical = input.y * Time.deltaTime * forwardSpeed;
        float horizontal = input.x * Time.deltaTime * turnSpeed;
        rb.transform.position = rb.transform.position + (rb.transform.forward * vertical);
        rb.rotation = Quaternion.Euler(rb.rotation.eulerAngles + new Vector3(0, horizontal, 0));
    }


}
