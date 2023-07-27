using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class DataStore : MonoBehaviour
{
    float TimePlaying = 0f;
    private void Awake()
    {
        //kieemr tra xem bo nho co key nao ko
        if (PlayerPrefs.HasKey(GameConstants.StartGameKey) && PlayerPrefs.HasKey(GameConstants.ExitGameKey))
        {
            DateTime timStart = DateTime.Parse(PlayerPrefs.GetString(GameConstants.StartGameKey));
            DateTime timExit = DateTime.Parse(PlayerPrefs.GetString(GameConstants.ExitGameKey));
            TimePlaying = (float)(timExit - timStart).TotalMinutes;
            Debug.Log("TimePlay" + TimePlaying);
        }
        PlayerPrefs.SetString(GameConstants.StartGameKey, DateTime.Now.ToString());
        //WritePlayerDataToJson();
        PlayerConfig playerData = GetPlayerDataFromJson();
        PlatfromController controller = GetComponent<PlatfromController>();
        //controller.moveSpeed = playerData.moveSpeed;
    }
    private PlayerConfig GetPlayerDataFromJson()
    {
        string json = File.ReadAllText("Assets/Script/Config/playerData.json");
        PlayerConfig data = JsonUtility.FromJson<PlayerConfig>(json);
        return data;
    }
    private void WritePlayerDataToJson()
    {
        PlayerConfig playerData = new PlayerConfig("untitledSaS",18,450,100,10);
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText("Assets/Script/Config/playerData.json",json);
        Debug.Log("write player data complete");
    }
    private void OnDestroy()
    {
        PlayerPrefs.SetString(GameConstants.ExitGameKey, DateTime.Now.ToString());
    }
}
