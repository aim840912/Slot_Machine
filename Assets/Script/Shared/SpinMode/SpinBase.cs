using UnityEngine;
using UnityEngine.UI;

public abstract class SpinBase
{
    protected Button _spinBtn;
    protected Toggle _spinToggle;
    protected UiManager _uiManager;
    protected BoardManager _boardManager;
    protected IGameMode _gameMode;
    protected Text _toggleText;
    protected MonoBehaviour _mono;

    public SpinBase(Button spinBtn, Toggle toggle, UiManager uiManager, BoardManager boardManager, IGameMode gameMode, MonoBehaviour mono)
    {
        // Debug.Log("Spin base");
        this._spinBtn = spinBtn;
        this._spinToggle = toggle;
        this._uiManager = uiManager;
        this._boardManager = boardManager;
        this._gameMode = gameMode;
        this._mono = mono;

        _toggleText = _spinToggle.GetComponentInChildren<Text>();
    }

    public virtual void SpinHandler()
    {
        _uiManager.CloseAllPanel();
    }
    protected virtual void StartSpin()
    {
        _mono.StartCoroutine(_gameMode.GetServerData(GetInputValue()));

        _boardManager.Spin();

        _uiManager.TurnWinMoneyToZero();
    }
    protected virtual void StopSpin()
    {
        _mono.StartCoroutine(_boardManager.Stop(_gameMode.BackendData.BoardNum));

        _uiManager.UpdatedPlayerUI(_gameMode);
    }

    protected virtual void SetToggleText(bool isSpin)
    {
        _toggleText.text = isSpin ? "Stop" : "Spin";
    }
    protected virtual int GetInputValue() => _uiManager._betControl.CurrentValue;

}