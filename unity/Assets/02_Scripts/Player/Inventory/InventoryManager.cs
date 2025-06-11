using System;
using System.Collections;
using System.Collections.Generic;
using _02_Scripts.Item;
using _02_Scripts.Player.Inventory.Request;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

namespace _02_Scripts.Player.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField]
        private InventoryUIManager inventoryUIManager;
        
        public List<ItemData> inventoryItems = new List<ItemData>();

        public void Start()
        {
            StartCoroutine(GetInventory(PlayerManager.Instance.playerData.seq));
        }

        [ContextMenu("Get Inventory")]
        private IEnumerator RunInventorySequence()
        {
            yield return StartCoroutine(AddInventory(1, 1));
            yield return StartCoroutine(AddInventory(2, 1));
            yield return StartCoroutine(AddInventory(3, 1));
            yield return StartCoroutine(AddInventory(4, 1));
        }

        public IEnumerator AddInventory(int itemSeq, int userSeq)
        {
            string url = "http://localhost:3000/api/inventory/Add";

            InventoryRequest requestData = new InventoryRequest
            {
                itemSeq = itemSeq,
                userSeq = userSeq
            };

            string jsonData = JsonConvert.SerializeObject(requestData);
            
            UnityWebRequest request = new UnityWebRequest(url, "POST");
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("추가 성공: " + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("에러 발생: " + request.error);
            }
        }

        public IEnumerator GetInventory(int userSeq)
        {
            Debug.Log(userSeq);
            string url = "http://localhost:3000/api/inventory/Get";

            InventoryRequest requestData = new InventoryRequest
            {
                userSeq = userSeq
            };

            string jsonData = JsonUtility.ToJson(requestData);

            UnityWebRequest request = new UnityWebRequest(url, "POST");
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = request.downloadHandler.text;
                Debug.Log("서버 응답: " + json);

                ParsingInventory(JsonConvert.DeserializeObject<List<ItemData>>(json)); 
                Debug.Log($"받은 아이템 개수: {inventoryItems.Count}");
                inventoryUIManager.Init(inventoryItems);
            }
            else
            {
                Debug.LogError("에러 발생: " + request.error);
            }
        }

        private void ParsingInventory(List<ItemData> itemList)
        {
            inventoryItems = new List<ItemData>();
            foreach (var item in itemList)
            {
                item.item_description = ItemMasterLoader.itemMasterDict[item.item_seq].description;
                item.item_icon = ItemMasterLoader.itemMasterDict[item.item_seq].icon;
                item.item_name = ItemMasterLoader.itemMasterDict[item.item_seq].display_name;
                inventoryItems.Add(item);
            }
        }
    }
}