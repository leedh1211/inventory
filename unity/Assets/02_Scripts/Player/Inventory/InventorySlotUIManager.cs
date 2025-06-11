using _02_Scripts.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utill;

namespace _02_Scripts.Player.Inventory
{
    public class InventorySlotUIManager : MonoBehaviour
    {
        [SerializeField] 
        private Image itemIcon;
        [SerializeField] 
        private TMP_Text amountText;
        [SerializeField] private GameObject EquippedItem;

        public void SetItem(ItemData itemData)
        {
            if (itemData.is_equipped)
            {
                EquippedItem.SetActive(true);
            }
            Sprite icon = Resources.Load<Sprite>($"Icon/{itemData.item_icon}");
            itemIcon.sprite = icon;
            amountText.text = itemData.quantity > 1 ? itemData.quantity.ToString() : "";
        }
    }
}