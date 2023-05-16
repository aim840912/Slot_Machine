using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AutoSpin : SpinBase
{
    private bool _isCrRunning = false;

    Coroutine _coroutine = null;

    public AutoSpin(Button spinBtn, UiManager uiManager, BoardManager boardManager, IGameMode gameMode, MonoBehaviour mono)
    : base(spinBtn, uiManager, boardManager, gameMode, mono)
    { }

    public override void SpinHandler()
    {
        if (_isCrRunning == false)
            _coroutine = _mono.StartCoroutine(NormalAuto());
        else
        {
            _mono.StopCoroutine(_coroutine);
            _mono.StartCoroutine(StopAutoAndRestart());
        }
    }

    private IEnumerator NormalAuto()
    {
        if (GetAutoTime() < 1)
            yield break;
        _isCrRunning = true;

        if (_SpinBool == true)
        {
            Rotate();
            yield return new WaitForSeconds(3);
        }

        Stop();

        yield return new WaitUntil(() => _boardManager.IsOver == true);


        _isCrRunning = false;

        _coroutine = _mono.StartCoroutine(NormalAuto());
    }


    private IEnumerator StopAutoAndRestart()
    {
        Stop();

        yield return new WaitUntil(() => _boardManager.IsOver == true);

        _isCrRunning = false;

        _coroutine = _mono.StartCoroutine(NormalAuto());

    }




    private int GetAutoTime() => _uiManager._autoControl.CurrentValue;


}
