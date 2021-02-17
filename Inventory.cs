using System.Collections.Generic;
using UnityEngine;

namespace TauntGames
{

    public class Inventory : MonoBehaviour
    {
        #region Singleton

        public static Inventory instance;
        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }
        }

        #endregion

        #region OnEnable

        private void OnEnable()
        {
            onInventoryAddResult += AddToInventory;
        }

        #endregion

        #region Class Variables

        public int inventorySize = 2;
        public int slotsTaken;
        public List<Item> playerResources;
        
        #endregion

        #region Events


        public delegate void OnInventoryAddResult(Item item);
        public OnInventoryAddResult onInventoryAddResult;

        #endregion

        #region Class Methods

        public void AddToInventory(Item item)
        {
            playerResources.Add(item);
            slotsTaken++;
        }

        public void RemoveFromInventory(Item item)
        {
            playerResources.Remove(item);
            slotsTaken--;
        }

        void InventoryAdd(Item item)
        {
            InventoryResults results = new InventoryResults(item);

            if (results._quantityToAdd > 0)
            {
               for (int i = 0; i < results._quantityToAdd; i++)
                   {
                       AddToInventory(results._itemToAdd);
                   } 
            }
        }

        #endregion
        
    }

    public class InventoryResults
    {

        #region Class Variables

        public readonly Item _itemToAdd;
        public readonly int _slotsLeft;
        public readonly int _quantityLost;
        public readonly int _quantityToAdd;
        public readonly string _infoString;
        public readonly Color32 _textColor;

        #endregion

        #region Constructor

        public InventoryResults(Item _item)
        {
            _slotsLeft = Inventory.instance.inventorySize - Inventory.instance.slotsTaken;
            _itemToAdd = _item;

            if (_slotsLeft == 0)
            {
                _quantityLost = _item.itemQuantity;
                _quantityToAdd = 0;
                _textColor = new Color32(229, 137, 137, 255);
                _infoString = "Inventory Full";
            }
            else if (_item.itemQuantity <= _slotsLeft)
            {
                _quantityLost = 0;
                _quantityToAdd = _item.itemQuantity;
                _textColor = new Color32(124, 233, 135, 255);
                _infoString = _quantityToAdd + " - Added To Inventory";
            }
            else
            {
                _quantityLost = _item.itemQuantity - _slotsLeft;
                _quantityToAdd = _item.itemQuantity - _quantityLost;
                _textColor = new Color32(124, 233, 135, 255);
                _infoString = _quantityToAdd + " - Added To Inventory";
            }
        }

        #endregion
        
    }
}