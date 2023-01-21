using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MainBullet : NetworkBehaviour
{
    public float speed = 2f;
    public bool alive;
    [SerializeField] GameObject displayed;
    [HideInInspector] public bool isNotBaseBullet;
    public PlayerShooting owner;

    public float damage = 5f;


    //valeurs pour mortar bullet
    [HideInInspector] public Vector3 clickedArea;
    [HideInInspector] public Vector3 basePos;
    [HideInInspector] public float t;
    [HideInInspector] public float speedOffset = 0;
    NetworkConnectionToClient con;

    void Update()
    {
        if (alive && !isNotBaseBullet)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (alive)
        {
            if (other.CompareTag("Wall"))
            {
                //Debug.Log("mur");
                Unregister();

            }
            if (other.CompareTag("Player"))
            {
                other.gameObject.TryGetComponent(out PlayerControler shootingRef);
                if (shootingRef != null)
                {
                    if (shootingRef.playerNumber() != owner.playerNumber)
                    {
                        Debug.Log(shootingRef);
                        CastDmg(shootingRef);
                        Unregister();
                    }
                }
            }
        }

    }

    [Command]
    void CastDmg(PlayerControler shootingRef)
    {
        shootingRef.TakeDmg(damage);
    }

    public override void OnStartServer()
    {
        base.OnStartServer();

        AssignAuth(con);
        HideCast();
    }

    public void Unregister()
    {
        alive = false;
        owner.bullets.Remove(this);
        owner.bulletsInactive.Add(this);
        displayed.SetActive(false);
        HideCast();
    }

    [ClientRpc]
    void HideCast()
    {
        displayed.SetActive(false);
    }

    [ClientRpc]
    void ShowCast()
    {
        displayed.SetActive(true);
    }


    public void Respawn(Vector3 pos, Quaternion rota) //deja apelée depuis une command
    {
        ShowCast();
        displayed.SetActive(true);
        transform.position = pos;
        transform.rotation = rota;
        alive = true;
        StopCoroutine(UnregisterOnDelay());
        StartCoroutine(UnregisterOnDelay());
        SoftReset();
    }

    IEnumerator UnregisterOnDelay()
    {
        yield return new WaitForSeconds(4f);
        Unregister();
    }

    public void Init(PlayerShooting playerShootingRef, NetworkConnectionToClient conec)
    {
        owner = playerShootingRef;
        owner.bulletsInactive.Add(this);
        displayed.SetActive(false);
        SoftReset();

        InitCast(playerShootingRef);

        con = conec;
    }

    void AssignAuth(NetworkConnectionToClient con)
    {
        netIdentity.AssignClientAuthority(con);
    }

    void InitCast(PlayerShooting playerShootingRef)
    {
        //HideCast();
        owner = playerShootingRef;
        owner.bulletsInactive.Add(this);
        displayed.SetActive(false);
        SoftReset();
    }


    void SoftReset()
    {
        speedOffset = Vector3.Distance(basePos, clickedArea);
        t = 0;
        basePos = transform.position;
    }



}
