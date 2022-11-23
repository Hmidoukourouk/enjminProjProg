using UnityEngine;

[CreateAssetMenu(fileName = "NewTankConfig", menuName = "ScriptableObjects/TankClasesScriptableObject", order = 1)]
public class TankClases : ScriptableObject
{
    public string tankName = "NewTankConfig";

    
    public Mesh tourelle;
    public Mesh corp;
    public Mesh chenilles;
}