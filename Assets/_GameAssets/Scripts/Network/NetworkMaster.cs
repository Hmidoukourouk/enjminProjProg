using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkMaster : NetworkManager
{
    public static NetworkMaster instance;

    public override void Awake()
    {
        base.Awake();
        instance = this;
    }

    public void ConnectionWithIP()
    {
        
    }
}
