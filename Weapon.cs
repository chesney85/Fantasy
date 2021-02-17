using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    public List<StatModifier> WeaponStatModifiers;
    public string weaponName;
    public string weaponDescription;
    public UnityEvent On_Equip;
    public UnityEvent On_Holster;

   
}
