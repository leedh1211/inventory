using System.Collections;
using System.Collections.Generic;
using _02_Scripts.Item;
using _02_Scripts.Player.Inventory.Request;
using _02_Scripts.Player.Status.Request;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace _02_Scripts.Player.Status
{
    public class StatusManager : MonoBehaviour
    {
        [SerializeField]
        private StatusUIManager statusUIManager;
        public void Start()
        {
            StartCoroutine(SetStat(1));
        }
        
        public IEnumerator GetStat(int userSeq)
        {
            Debug.Log(userSeq);
            string url = "http://localhost:3000/api/status/Get";

            StatusRequest requestData = new StatusRequest
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
                Debug.Log("서버 응답 JSON: " + json);
                PlayerStatData statData = JsonConvert.DeserializeObject<PlayerStatData>(json);
                PlayerManager.Instance.playerStatData = statData;
            }
            else
            {
                Debug.LogError("에러 발생: " + request.error);
            }
        }
        
        public IEnumerator GetItemStat(int userSeq)
        {
            Debug.Log(userSeq);
            string url = "http://localhost:3000/api/status/GetItemStat";

            StatusRequest requestData = new StatusRequest
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
                Debug.Log("서버 응답 JSON: " + json);
                PlayerStatData statData = JsonConvert.DeserializeObject<PlayerStatData>(json);
                PlayerManager.Instance.playerItemStatData = statData;
            }
            else
            {
                Debug.LogError("에러 발생: " + request.error);
            }
        }

        public IEnumerator SetStat(int userSeq)
        {
            yield return StartCoroutine(GetStat(userSeq));
            yield return StartCoroutine(GetItemStat(userSeq));
            statusUIManager.MakeStatusRow();
        }
    }
}