using System.Collections.Generic;
using _02_Scripts.Item;
using UnityEditor;
using UnityEngine;

namespace _02_Scripts.Player.Inventory
{
    public class InventoryUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject inventoryPanel;
        [SerializeField] private GameObject content;
        [SerializeField] private GameObject SlotManager;
        [SerializeField] private GameObject inventoryItemPrefab;
        private InventorySlotUIManager slotUI;
        private InventorySlotManager slotManager;

        public void Init(List<ItemData> items)
        {
            SetItemList(items);
            inventoryPanel.SetActive(false);
        }

        public void SetItemList(List<ItemData> items)
        {
            foreach (ItemData item in items)
            {
                GameObject instance = Instantiate(inventoryItemPrefab, content.transform);
                InventorySlotUIManager slotUI = instance.GetComponent<InventorySlotUIManager>();
                InventorySlotManager slotManager = instance.GetComponent<InventorySlotManager>();
                if (slotUI != null)
                {
                    slotUI.SetItem(item);
                }

                if (slotManager != null)
                {
                    slotManager.SetItemData(item);
                }
            }
        }

        public void ShowInventory()
        {
            inventoryPanel.SetActive(true);
        }

        public void HideInventory()
        {
            inventoryPanel.SetActive(false);
        }
    }
}