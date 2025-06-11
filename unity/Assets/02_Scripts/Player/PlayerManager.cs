using System.Collections;
using System.Text;
using _02_Scripts.Player;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerManager : Singleton<PlayerManager>
{
    public PlayerData playerData;
    public PlayerStatData playerStatData;
    public PlayerStatData playerItemStatData;
    public void Init(PlayerData data)
    {
        playerData = data;
        playerStatData = new PlayerStatData();
        playerItemStatData = new PlayerStatData();
        Debug.Log($"로그인된 유저: {data.name}");
    }
}