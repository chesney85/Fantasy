using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Armour : MonoBehaviour
{
    public List<StatModifier> armourStatModifiers;
    public string armourName;
    public string description;
    public UnityEvent On_Equip;
    public UnityEvent On_Holster;

    
}
