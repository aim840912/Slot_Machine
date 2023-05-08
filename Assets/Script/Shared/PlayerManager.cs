using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string UserId;
    public string Name;
    public int Money = 10000;
}

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance = null;

    public PlayerData PlayerData { get; set; }

    public int PlayerMoney
    {
        get
        {
            return PlayerData.Money;
        }
    }

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void UpdatePlayerManager(PlayerData playerData)
    {
        this.PlayerData = playerData;
    }

    public void ResetPlayerManager()
    {
        this.PlayerData.UserId = "";
        this.PlayerData.Name = "";
        this.PlayerData.Money = 0;
    }
}
