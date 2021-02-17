using UnityEngine;

namespace TauntGames
{
[CreateAssetMenu(menuName = "Taunt Games/fuckingItem")]
    public class Item : ScriptableObject
    {
        #region Class Variables
        
        public ItemType itemType;
        public string itemName;
        public string itemDescription;
        public int itemQuantityMin;
        public int itemQuantityMax;
        public int itemQuantity;
        public bool isQuestItem;
        public Sprite itemIcon;
        
        public enum ItemType
        {
            Armour,
            Consumable,
            Weapon,
            
        }

        

        #endregion
        
    }

}
