using UnityEngine;

namespace TauntGames
{
    public class ItemInteract : Interactable
    {
        public Item SO_item;
        public Item item;

        private void OnEnable()
        {
            item = null;
            item = Instantiate(SO_item);
            item.itemQuantity = Random.Range(item.itemQuantityMin, item.itemQuantityMax);
            item.itemName = "jobbies";
        }

        public override void Interact()
        {
            // InventoryResults result = new InventoryResults(item);
            // if (result._quantityToAdd > 0)
            // {
            //     for (int i = 0; i < result._quantityToAdd; i++)
            //     {
            //         Inventory.instance.AddToInventory(item);
            //     }
            //     switchHighlighted(false);
            //     interactionText.text = "";
            //     canInteract = false;
            // }
            // ShowInformation(result._infoString,result._textColor);
            // QuestManager.instance.Invoke_PlayerPickedUpItem(item);
        }
        
        
    }
}