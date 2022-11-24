using UnityEngine;

[CreateAssetMenu(fileName = "NewTankConfig", menuName = "ScriptableObjects/TankClasesScriptableObject", order = 1)]
public class TankClases : ScriptableObject
{
    public string tankName = "NewTankConfig";

    [Header("0 classic 1 mortar 2 saw")]
    public int shootMode;
    public float forwardSpeed = 5f;
    public float turnSpeed = 200f;
}