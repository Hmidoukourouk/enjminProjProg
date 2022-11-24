using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerShooting : NetworkBehaviour
{
    public MainBullet baseBullet;
    public MortarBullet mortarBullet;
    public List<MainBullet> bullets = new List<MainBullet>();
    public List<MainBullet> bulletsInactive = new List<MainBullet>();
    public float poolnumber = 20;
    public int playerNumber;

    public int shootMode;

    public PlayerTurret turretRef;

    public bool canShoot = true;

    public Transform spawnPoint;

    [SerializeField] Animator feedback;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        playerNumber = (int)netId;

        // SpawnBullets appelé dans playerClass

        if (!isLocalPlayer) return;
        GameManager.input.Tank.Fire.performed += shootValue => Shoot(shootValue.ReadValue<float>()); //L ctx c'est context on se'en fou du nom en gros ça va read la valeur shoot
    }

    public void SpawnBullets()//elle est appelée par une fonction qui a deja le [Command]
    {
        bullets.Clear();
        bulletsInactive.Clear();
        if (canShoot && isServer)
        {
            for (int i = 0; i < poolnumber; i++)
            {
                switch (shootMode)
                {
                    case 0:
                        MainBullet bulletTemp = Instantiate(baseBullet);
                        bulletTemp.Init(this);
                        NetworkServer.Spawn(bulletTemp.gameObject);
                        break;
                    case 1:
                        MortarBullet mortarTemp = Instantiate(mortarBullet);
                        mortarTemp.Init(this);
                        NetworkServer.Spawn(mortarTemp.gameObject);
                        break;
                    default:
                        return;//pas besion de spanw si c'est saw
                }
            }
        }
    }

    //unregister bullet est dans le MainBullet

    [Command]
    void Shoot(float cc)
    {
        if (canShoot)
        {
            MainBullet bulletobj;
            if (bulletsInactive.Count > 0)
            {
                bulletobj = (MainBullet)bulletsInactive.ToArray().GetValue(0);
                bulletsInactive.Remove(bulletobj);
                bullets.Add(bulletobj);
            }
            else if (bullets.Count > 0)
            {
                bulletobj = (MainBullet)bullets.ToArray().GetValue(0);
                bullets.Remove(bulletobj);
                bullets.Add(bulletobj);
            }
            else
            {
                Debug.LogWarning("y a pas de bullets a utiliser !");

                return;
            }
            bulletobj.clickedArea = turretRef.hitLocation;
            bulletobj.gameObject.SetActive(true);
            bulletobj.Respawn(spawnPoint.position, spawnPoint.rotation);
            feedback.SetTrigger("Shot");
        }


    }
    //shoot call

}
