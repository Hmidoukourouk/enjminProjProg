using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MainBullet : NetworkBehaviour
{
    public float speed = 2f;
    public bool alive;
    [HideInInspector] public bool isNotBaseBullet;
    public PlayerShooting owner;

    public float damage = 5f;


    //valeurs pour mortar bullet
    [HideInInspector] public Vector3 clickedArea;
    [HideInInspector] public Vector3 basePos;
    [HideInInspector] public float t;
    [HideInInspector] public float speedOffset = 0;

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
                        shootingRef.TakeDamage(damage);
                        Unregister();
                    }
                }
            }
        }

    }

    private void Awake()
    {

    }


    public void Unregister()
    {
        alive = false;
        owner.bullets.Remove(this);
        owner.bulletsInactive.Add(this);
        gameObject.SetActive(false);
    }

    public void Respawn(Vector3 pos, Quaternion rota)
    {
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

    public void Init(PlayerShooting playerShootingRef)
    {
        owner = playerShootingRef;
        owner.bulletsInactive.Add(this);
        gameObject.SetActive(false);
        SoftReset();

        InitCast(playerShootingRef);
    }

    [Command]
    void InitCast(PlayerShooting playerShootingRef)
    {
        owner = playerShootingRef;
        owner.bulletsInactive.Add(this);
        gameObject.SetActive(false);
        SoftReset();
    }

    void SoftReset()
    {
        speedOffset = Vector3.Distance(basePos, clickedArea);
        t = 0;
        basePos = transform.position;
    }



}
