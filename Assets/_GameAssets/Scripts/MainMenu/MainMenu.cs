using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public TMP_InputField input;
    public void OnClickJoin()
    {
        NetworkMaster.instance.networkAddress = input.text;
        NetworkMaster.instance.StartClient();
    }
    public void OnClickHost() => NetworkMaster.instance.StartHost();
}
