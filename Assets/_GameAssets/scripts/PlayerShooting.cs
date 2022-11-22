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

        if (authority)
        {
            SpawnBulletsServeur();
            SpawnExec();
        }
        else
        {
            SpawnBulletsClient();
        }

        GameManager.input.Tank.Fire.performed += ctx => Shoot(ctx.ReadValue<float>()); //L ctx c'est context on se'en fou du nom en gros ça va read la valeur shoot
    }

    [Command]
    void SpawnBulletsClient()
    {
        SpawnExec();
    }

    [ClientRpc]
    void SpawnBulletsServeur()
    {
        SpawnExec();
    }

    void SpawnExec()
    {
        for (int i = 0; i < poolnumber; i++)
        {
            MainBullet bulletTemp = Instantiate(bullet);
            bulletTemp.Init(this); //le add bullets inactive est dans le script main bullet
            NetworkServer.Spawn(bulletTemp.gameObject);
        }
    }

    public void UnregisterBullet(MainBullet self)
    {
        int index = bullets.IndexOf(self);
        if (authority)
        {
            UnregiterBulletServeur(index);
            UnregiterBulletEXEC(index);
        }
        else
        {
            UnregiterBulletClient(index);
        }
    }

    [ClientRpc]
    void UnregiterBulletServeur(int index)
    {
        UnregiterBulletEXEC(index);
    }

    [Command]
    void UnregiterBulletClient(int index)
    {
        UnregiterBulletEXEC(index);
    }

    void UnregiterBulletEXEC(int index)
    {
        MainBullet bulletRef = (MainBullet)bullets.ToArray().GetValue(index);
        bullets.Remove(bulletRef);
        bulletsInactive.Add(bulletRef);
        bulletRef.gameObject.SetActive(false);
    }
    void ShootEXEC()
    {
        MainBullet bulletobj;
        if (bulletsInactive.Count > 0)
        {
            bulletobj = (MainBullet)bulletsInactive.ToArray().GetValue(0);
            bulletsInactive.Remove(bulletobj);
            bullets.Add(bulletobj);
        }
        else if(bullets.Count>0)
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

    void Shoot(float cc)
    {
        if (authority)
        {
            ShootServeur();
            ShootEXEC();
        }
        else
        {
            ShootClient();
        }

    }
    //shoot call
    [ClientRpc]
    void ShootServeur()
    {
        ShootEXEC();
    }

    [Command]
    void ShootClient()
    {
        ShootEXEC();
    }
}
