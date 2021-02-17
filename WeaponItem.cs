using UnityEngine;

namespace TauntGames
{
    

    public class WeaponItem : Item
    {
        private float weaponDamage;
        public enum WeaponType
        {
            Projectile,
            Melee,
            Throwable
        }

        private WeaponType weaponType;
            
        #region Constructor

        public WeaponItem(int _playerLevel,WeaponType _weaponType)
        {
            itemType = ItemType.Weapon;
            weaponType = _weaponType;
            weaponDamage = CalculateDamage(_playerLevel);
        }
        
        //Create methods for determining each stat.
        public float CalculateDamage(int _playerLevel)
        {
            float damage = 0;
            return damage;
        }
       

        #endregion
    }
    
}
