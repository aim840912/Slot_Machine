using System.Collections;
using UnityEngine;

public class SingleGame : MonoBehaviour, IGameMode
{
    public BackendData BackendData { get; set; } = new BackendData();
    private SingleGameHandler _singleGameHandler = new SingleGameHandler();
    private FileDataHandler _fileDataHandler;
    private CalcMultiple _calcMultiple = new CalcMultiple();

    [SerializeField] private string _fileName = "slotMachine";
    [SerializeField] private bool _encryptData = true;
    [SerializeField] private string _filePath = "idbfs/aim841104fsdfsdfsdagf";

    #region Data Modify

    [ContextMenu("Delete save file")]
    private void DeleteSavedData()
    {
        _fileDataHandler = new FileDataHandler(Application.persistentDataPath, _fileName, _encryptData);
        _fileDataHandler.Delete();
    }

    [ContextMenu("Reset file")]
    private void ResetSavedData()
    {
        PlayerManager.instance.PlayerData.Money = 10000;
        _singleGameHandler.SaveGame(PlayerManager.instance.PlayerData, _fileDataHandler);
    }

    #endregion


    private void Start()
    {
        if (GameManager.Instance.GameMode == GameMode.SINGLE_GAME)
        {
            // _fileDataHandler = new FileDataHandler(Application.persistentDataPath, _fileName, _encryptData);
            _fileDataHandler = new FileDataHandler(_filePath, _fileName, _encryptData);
            _singleGameHandler.LoadGame(_fileDataHandler);
        }
    }

    public IEnumerator GetServerData(int betInputValue)
    {
        GenerateGameBoard(0, 10);

        CalcMoney(betInputValue);

        yield return null;
    }

    private void GenerateGameBoard(int min, int max)
    {
        int[] slotNumber = new int[9];

        for (var i = 0; i < slotNumber.Length; i++)
        {
            slotNumber[i] = Random.Range(min, max);
        }

        BackendData.BoardNum = slotNumber;
    }

    private void CalcMoney(int betInputValue)
    {
        int winMoney = 0;
        int currentMoney = PlayerManager.instance.PlayerData.Money;

        winMoney = GetMultiple(BackendData.BoardNum) * betInputValue / 8 - betInputValue;

        currentMoney += winMoney;


        BackendData.WinMoney = winMoney;
        BackendData.Money = currentMoney;

        PlayerManager.instance.PlayerData.Money = currentMoney;

        _singleGameHandler.SaveGame(PlayerManager.instance.PlayerData, _fileDataHandler);
    }


    private void OnApplicationQuit()
    {
        if (GameManager.Instance.GameMode == GameMode.SINGLE_GAME)
            _singleGameHandler.SaveGame(PlayerManager.instance.PlayerData, _fileDataHandler);
    }

    private int GetMultiple(int[] boardNum)
    {
        int multiple = _calcMultiple.GetMultiples(boardNum);

        return multiple;
    }
}
