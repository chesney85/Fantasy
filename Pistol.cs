

using UnityEngine;

public class Pistol : Weapon
{
    
    
    public enum FireMode
    {
        single,
        Burst,
        Auto
    }
    public FireMode fireMode;
    [Range(0, 1)] public float fireRate;
    public void OnEnable()
    {
        On_Equip?.Invoke();
    }

    public void OnDisable()
    {
        On_Holster?.Invoke();
    }
}
