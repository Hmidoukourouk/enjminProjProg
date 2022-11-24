using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerClassChanger : NetworkBehaviour
{
    public TankClases[] clases;

    [Header("0 base, 1 mortar, 2 melee")]
    public GameObject[] baseTank;
    public GameObject baseBulletRef;
    public GameObject[] mortarTank;
    public GameObject mortarBulletRef;
    public GameObject[] meleeTank;

    [SerializeField] PlayerControler player;
    [SerializeField] Transform[] shootPoints;

    List<GameObject> toHide = new List<GameObject>();
    // Start is called before the first frame update

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        Debug.Log(MainMenu.menuValues == null);
        ChangeClass(MainMenu.menuValues.tankNumber);
    }


    [Command]
    public void ChangeClass(int claN)
    {
        toHide.Clear();

        TankClases newCla = (TankClases)clases.GetValue(claN);
        Debug.Log(claN);

        GameObject[] toReveal;

        GameObject bulletToReplace;

        switch (newCla.shootMode)
        {
            case 0:
                AddToDelete(mortarTank);
                AddToDelete(meleeTank);
                toReveal = baseTank;
                bulletToReplace = baseBulletRef;
                break;
            case 1:
                AddToDelete(baseTank);
                AddToDelete(meleeTank);
                toReveal = mortarTank;
                bulletToReplace = mortarBulletRef;
                break;
            case 2:
                AddToDelete(mortarTank);
                AddToDelete(baseTank);
                toReveal = meleeTank;
                bulletToReplace = null;
                break;
            default:
                toReveal = baseTank;
                bulletToReplace = baseBulletRef;
                break;
        }

        foreach (GameObject item in toHide)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in toReveal)
        {
            item.SetActive(true);
        }

        if (bulletToReplace == null)
        {
            player.refShooting.canShoot = false;
        }

        player.refShooting.spawnPoint = (Transform)shootPoints.GetValue(newCla.shootMode); // changement point de spawn
        player.refShooting.shootMode = newCla.shootMode;
        player.refShooting.SpawnBullets();

        player.forwardSpeed = newCla.forwardSpeed;
        player.turnSpeed = newCla.turnSpeed;
    }

    void AddToDelete(GameObject[] listToAdd)
    {
        foreach (GameObject item in listToAdd)
        {
            toHide.Add(item);
        }
    }
}
