using System.Collections.Generic;
using _02_Scripts.Player.Status;
using Newtonsoft.Json;
using UnityEngine;

public class StatMasterLoader : MonoBehaviour
{
    public static Dictionary<int, StatMasterData> statMasterDict = new();

    void Awake()
    {
        LoadStatMaster();
    }

    private void LoadStatMaster()
    {
        TextAsset jsonText = Resources.Load<TextAsset>("Data/statMasterData");
        if (jsonText == null)
        {
            Debug.LogError("statMasterData.json 파일을 찾을 수 없습니다.");
            return;
        }

        List<StatMasterData> stats = JsonConvert.DeserializeObject<List<StatMasterData>>(jsonText.text);
        foreach (var stat in stats)
        {
            statMasterDict[stat.id] = stat;
        }
    }
}