using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseStats : MonoBehaviour
{
    #region Variables

    private float health = 100f;
    private float stamina = 100f;
    private float armour = 100f;
    
    public List<StatModifier> StatBaseModifiers;

    public UnityEvent OnModifierChanged;

    #endregion


    #region Getters/Setters

    public float Get_health()
    {
        return health;
    }

    public void Set_Health(float _health)
    {
        health = _health;
    }

    public float Get_stamina()
    {
        return stamina;
    }

    public void Set_Stamina(float _stamina)
    {
        stamina = _stamina;
    }

    public float Get_armour()
    {
        return armour;
    }

    public void Set_Armour(float _armour)
    {
        armour = _armour;
    }

    #endregion

    #region Class Methods

    private void CalculateFinalValues()
    {
        var baseHealth = health;
        var hModVal = 0f;
        var hModPercent = 0f;
        var baseArmour = armour;
        var aModVal = 0f;
        var aModPercent = 0f;
        var baseStamina = stamina;
        var sModVal = 0f;
        var sModPercent = 0f;
        
        
        foreach (var stat in StatBaseModifiers)
        {
            if (stat.modifierType == StatModifier.ModifierType.value)
            {
                switch (stat.modifyTarget)
                {
                    case StatModifier.ModifyTarget.armour:
                        aModVal += stat.modifierValue;
                        break;
                    case StatModifier.ModifyTarget.health:
                        hModVal += stat.modifierValue;
                        break;
                    case StatModifier.ModifyTarget.stamina:
                        sModVal += stat.modifierValue;
                        break;
                    default:
                        Debug.Log("Type Not Found");
                        break;
                }
            }
            else if (stat.modifierType == StatModifier.ModifierType.Percentage)
            {
                switch (stat.modifyTarget)
                {
                    case StatModifier.ModifyTarget.armour:
                        aModPercent += stat.modifierValue;
                        break;
                    case StatModifier.ModifyTarget.health:
                        hModPercent += stat.modifierValue;
                        break;
                    case StatModifier.ModifyTarget.stamina:
                        sModPercent += stat.modifierValue;
                        break;
                    default:
                        Debug.Log("Type Not Found");
                        break;
                }
            }
        }
        
        Debug.Log(baseArmour + "\n" + baseHealth + "\n" + baseStamina);
        Debug.Log(aModVal + "\n" + hModVal + "\n" +sModVal);

        baseArmour += aModVal;
        baseArmour *= 1 + aModPercent;
        baseHealth += hModVal;
        baseHealth *= 1 + aModPercent;
        baseStamina += sModVal;
        baseStamina *= 1 + sModPercent;
        Debug.Log(baseArmour + "\n" + baseHealth + "\n" + baseStamina);
        
        Set_Armour(baseArmour);
        Set_Stamina(baseStamina);
        Set_Health(baseHealth);
        
        Debug.Log(armour + "\n" + health + "\n" + stamina);
    }

    public void AddModifier(StatModifier _mod)
    {
        StatBaseModifiers.Add(_mod);
        OnModifierChanged?.Invoke();
    }

    public void RemoveModifier(StatModifier _mod)
    {
        StatBaseModifiers.Remove(_mod);
       OnModifierChanged?.Invoke();
    }

    public void InvokeEventss()
    {
        OnModifierChanged?.Invoke();
    }

    #endregion


}
