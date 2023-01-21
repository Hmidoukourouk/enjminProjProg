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
    [SyncVar]public float forwardSpeed = 1f;
    [SyncVar]public float turnSpeed = 1f;
    [SerializeField] Image imgHeath;
    public PlayerShooting refShooting;

    public Rigidbody rb;
    bool isNotControlable = true;

    [SerializeField] bool isServ;
    [SerializeField] bool isCli;
    [SerializeField] bool autho;
    [SerializeField] bool owneris;
    [SerializeField] bool localPlay;



    Vector3 tempPos;
    Quaternion tempRot;

    private void Awake()
    {
        stretchMax = imgHeath.transform.localScale.x;
        healthMax = health;
    }
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        
        if (isLocalPlayer) CameraControler.instance.players.Add(transform);

        GameManager.GM.players.Add(this);

        isNotControlable = false;

        AssignAuth(connectionToClient);

        //if (!isLocalPlayer) enabled = false; //pas besoin
    }

    public int playerNumber()
    {
        return refShooting.playerNumber;
    }


    [Command]
    void AssignAuth(NetworkConnectionToClient con) {
        netIdentity.AssignClientAuthority(con);
    }


    public void TakeDmg(float damage)
    {
        Debug.Log(health);
        health -= damage;

        ActualizeHUD(health);//sinon ça prend la valeur d'avant pour la display donc cringe pas synchro
    }

   
    [ClientRpc]
    void ActualizeHUD(float h)
    {
        imgHeath.transform.localScale = new Vector2(h/healthMax, imgHeath.transform.localScale.y);
        if (h <= 5)
        {
            imgHeath.color = Color.red;
        }
    }

    void FixedUpdate()
    {
        isServ = isServer;
        autho = authority;
        isCli = isClient;
        owneris = isOwned;
        localPlay = isLocalPlayer;


        if (!isLocalPlayer || isNotControlable) { return; };

        //Debug.Log(isServ + " " + autho + " " + isCli + " " + owneris + " " + localPlay);

        Vector2 input = GameManager.input.Tank.Movement.ReadValue<Vector2>();

        Moving(input);
    }

    void Moving(Vector2 input)
    {
        if (rb == null)
        {
            Debug.Log("bordel de rigidbody"); 
            rb = GetComponent<Rigidbody>(); 
            return;
        }

        tempPos = rb.transform.position;
        tempRot = rb.transform.rotation;

        float vertical = input.y * Time.deltaTime * forwardSpeed;
        float horizontal = input.x * Time.deltaTime * turnSpeed;
        tempPos = tempPos + (rb.transform.forward * vertical);
        tempRot = Quaternion.Euler(tempRot.eulerAngles + new Vector3(0, horizontal, 0));
        rb.transform.position = tempPos;
        rb.transform.rotation = tempRot;
        //MovingServeur(rb.transform.position, rb.transform.rotation);
    }

    //en gros ça clip dans les mur comme on bouge plus le rb mais une valeur hardcodée
[Command]
    void MovingServeur(Vector3 pos, Quaternion rot)
    {
        rb.transform.position = pos;
        rb.transform.rotation = rot;
    }
}
