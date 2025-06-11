using System.Collections.Generic;
using _02_Scripts.Item;
using Newtonsoft.Json;
using UnityEngine;

public class ItemMasterLoader : MonoBehaviour
{
    public static Dictionary<int, ItemMasterData> itemMasterDict = new();

    void Awake()
    {
        LoadItemMaster();
    }

    private void LoadItemMaster()
    {
        TextAsset jsonText = Resources.Load<TextAsset>("Data/itemMasterData");
        if (jsonText == null)
        {
            Debug.LogError("itemMasterData.json 파일을 찾을 수 없습니다.");
            return;
        }

        List<ItemMasterData> items = JsonConvert.DeserializeObject<List<ItemMasterData>>(jsonText.text);
        foreach (var item in items)
        {
            itemMasterDict[item.seq] = item;
        }
    }

}