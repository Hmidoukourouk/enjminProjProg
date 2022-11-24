using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Mirror;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] TMP_InputField tankNumberField;
    public int tankNumber;
    public string ipAdress;
    public static MainMenu menuValues;
    public void OnClickJoin()
    {
        tankNumber = int.Parse(tankNumberField.text);
        Debug.Log(tankNumberField.text);// le parse fail et il detecte r ici non plus sdfsdfsdf
        ipAdress = inputField.text;

        menuValues = this;
        DontDestroyOnLoad(this.gameObject);

        NetworkMaster.instance.networkAddress = ipAdress;
        NetworkMaster.instance.StartClient();
    }

    private void Start()
    {
        menuValues = this;
    }

    public void OnClickHost() => NetworkMaster.instance.StartHost();
}
