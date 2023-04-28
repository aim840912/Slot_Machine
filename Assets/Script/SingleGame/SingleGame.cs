using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SingleGame : MonoBehaviour, IGameMode
{
    public int WinMoney { get; set; }
    public int[] SlotNumber { get; set; } = new int[9];
    public int PlayerMoney { get; set; }
    public ServerReturnData ServerReturnData { get; set; }
    public bool GetData { get; set; }

    readonly int _minBoardNumber = 0;
    readonly int _maxBoardNumber = 10;
    private CalcMultiple _calcMultiple = new CalcMultiple();


    public IEnumerator GetServerData(int betInputValue)
    {
        GenerateGameBoard();

        CalcMoney(betInputValue);

        yield return null;

        SaveData();
    }

    void GenerateGameBoard()
    {
        for (var i = 0; i < SlotNumber.Length; i++)
        {
            SlotNumber[i] = Random.Range(_minBoardNumber, _maxBoardNumber);
        }
    }

    void CalcMoney(int betInputValue)
    {
        int _currentMoney = PlayerManager.instance.PlayerData.Money;

        WinMoney = GetMultiple() * betInputValue / 8 - betInputValue;

        _currentMoney += WinMoney;

        SaveManager.instance.gameData.Money = _currentMoney;

        PlayerManager.instance.PlayerData.Money = _currentMoney;

    }

    int GetMultiple()
    {
        int multiple = _calcMultiple.GetMultiples(SlotNumber);
        Debug.Log($"{multiple}");

        return multiple;
    }

    void SaveData()
    {
        SaveManager.instance.gameData.BoardNum = this.SlotNumber;
        SaveManager.instance.SaveGame();
    }


}
