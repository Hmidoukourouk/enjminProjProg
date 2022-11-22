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

    private void Start()
    {
        
        if (!isLocalPlayer) enabled = false;

        GameManager.input.Tank.Fire.performed += ctx => Shoot(ctx.ReadValue<float>()); //L ctx c'est context on se'en fou du nom
        for (int i = 0; i < poolnumber; i++)
        {
            MainBullet bulletTemp = Instantiate(bullet);
            bulletTemp.Init(this);
            bulletsInactive.Add(bulletTemp);
            NetworkServer.Spawn(bulletTemp.gameObject);
        }

    }

    public void UnregisterBullet(MainBullet self)
    {
        bullets.Remove(self);
        bulletsInactive.Add(self);
        self.gameObject.SetActive(false);
    }


    void Shoot(float cc)
    {
        MainBullet bulletobj;
        if (bulletsInactive.Count > 0)
        {
            bulletobj = (MainBullet)bulletsInactive.ToArray().GetValue(0);
            bulletsInactive.Remove(bulletobj);
            bullets.Add(bulletobj);
        }
        else
        {
            bulletobj = (MainBullet)bullets.ToArray().GetValue(0);
            bullets.Remove(bulletobj);
            bullets.Add(bulletobj);
        }
        bulletobj.gameObject.SetActive(true);
        bulletobj.Respawn(spawnPoint.position, spawnPoint.rotation);
    }
}
