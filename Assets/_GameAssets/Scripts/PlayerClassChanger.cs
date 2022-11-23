using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerClassChanger : NetworkBehaviour
{
    public TankClases newClass;
    public SkinnedMeshRenderer tourelle;
    public MeshFilter corp;
    public MeshFilter chenilles;
    // Start is called before the first frame update

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        ChangeClass(newClass);
    }

    [Command]
    void ChangeClass(TankClases newClass)
    {
        tourelle.sharedMesh = newClass.tourelle;
        corp.sharedMesh = newClass.corp;
        chenilles.sharedMesh = newClass.chenilles;
    }
}
