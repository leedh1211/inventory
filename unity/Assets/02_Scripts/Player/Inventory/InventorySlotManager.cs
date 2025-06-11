using _02_Scripts.Item;
using UnityEngine;

namespace _02_Scripts.Player.Inventory
{
    public class InventorySlotManager : MonoBehaviour
    {
        private ItemData itemData;

        public void SetItemData(ItemData itemData)
        {
            this.itemData = itemData;
        }
    }
}