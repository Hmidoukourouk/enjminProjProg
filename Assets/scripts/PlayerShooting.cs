using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public MainBullet bullet;
    public List<MainBullet> bullets = new List<MainBullet>();
    public List<MainBullet> bulletsInactive = new List<MainBullet>();
    public float poolnumber = 20;
    public int playerNumber;


    private void Start()
    {
        for (int i = 0; i < poolnumber; i++)
        {
            MainBullet bulletTemp = Instantiate(bullet);
            bulletTemp.Init(this);
            bulletsInactive.Add(bulletTemp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    public void UnregisterBullet(MainBullet self)
    {
        bullets.Remove(self);
        bulletsInactive.Add(self);
        self.gameObject.SetActive(false);
    }

    void Shoot()
    {
        MainBullet bulletobj;
        if (bulletsInactive.Count>0)
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
        bulletobj.Respawn(transform.position, transform.rotation);
    }
}
