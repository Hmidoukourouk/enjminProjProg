using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerShooting : NetworkBehaviour
{
    public MainBullet bullet;
    public List<MainBullet> bullets = new List<MainBullet>();
    public List<MainBullet> bulletsInactive = new List<MainBullet>();
    public float poolnumber = 20;
    public int playerNumber;
    [SerializeField] Transform spawnPoint;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        playerNumber = (int)netId;
        SpawnBullets();

        if (!isLocalPlayer) return;
        GameManager.input.Tank.Fire.performed += shootValue => Shoot(shootValue.ReadValue<float>()); //L ctx c'est context on se'en fou du nom en gros ça va read la valeur shoot
    }

    [Command]
    void SpawnBullets()
    {
        for (int i = 0; i < poolnumber; i++)
        {
            MainBullet bulletTemp = Instantiate(bullet);
            bulletTemp.Init(this); //le add bullets inactive est dans le script main bullet
            NetworkServer.Spawn(bulletTemp.gameObject);
        }
    }

    //unregister bullet est dans le MainBullet

    [Command]
    void Shoot(float cc)
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
        bulletobj.gameObject.SetActive(true);
        bulletobj.Respawn(spawnPoint.position, spawnPoint.rotation);

    }
    //shoot call

}
