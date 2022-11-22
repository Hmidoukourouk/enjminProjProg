using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class GameManager : MonoBehaviour
{
    public bool hostConnected;

    public static InputsActions input;
    public List<PlayerControler> players = new List<PlayerControler>();
    public static GameManager GM;
    private void OnEnable()
    {
        input = new();
        input.Enable();
    }
}
