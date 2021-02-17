using UnityEngine;
[CreateAssetMenu (menuName = "Taunt Games/Stats/New Modifier")]
public class StatModifier : ScriptableObject
{
    public enum ModifierType
    {
        value,
        Percentage
    }

    public ModifierType modifierType;

    public enum ModifyTarget
    {
        health,
        stamina,
        armour,
        GunDamage,
        GunAccuracy,
        GunStability,
        
    }

    public ModifyTarget modifyTarget;

    public float modifierValue;
}
