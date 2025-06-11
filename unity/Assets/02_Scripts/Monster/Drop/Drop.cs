using System.Collections;
using System.Text;
using _02_Scripts.Player;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace _02_Scripts.Monster.Drop
{
    public class Drop
    {
        private string dropUrl = "http://localhost:3000/api/drop/drop"; 
        
        private IEnumerator SendDropRequest(int monsterId, int userId, float dropRate)
        {
            string jsonData = JsonConvert.SerializeObject(new DropData(monsterId, userId, dropRate));

            UnityWebRequest request = new UnityWebRequest(dropUrl, "POST");
            byte[] jsonToSend = new UTF8Encoding().GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
            }
            else
            {
                Debug.LogError("서버 요청 실패: " + request.error);
            }
        }
    }
}