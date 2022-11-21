using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public void OnClickJoin() => NetworkMaster.instance.StartClient();
    public void OnClickHost() => NetworkMaster.instance.StartHost();
}
