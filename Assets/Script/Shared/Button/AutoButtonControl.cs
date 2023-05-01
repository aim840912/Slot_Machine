using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class AutoButtonControl : ValueControl
{
    [SerializeField] private Button[] _button;
    [SerializeField] private BetButtonControl _betControl;

    private void Start()
    {
        for (int i = 0; i < _button.Length; i++)
        {
            _button[i].onClick.AddListener(() => ValueCheck());
        }
    }

    public override void Add()
    {
        if (CurrentValue <= MaxMultiple())
            CurrentValue++;
    }

    public override void Minus()
    {
        if (CurrentValue > 0)
            CurrentValue--;
    }

    public override void Max()
    {
        CurrentValue = MaxMultiple();
    }

    public override void ValueCheck()
    {
        if (CurrentValue > MaxMultiple())
            Max();

        ValueText.text = $"{CurrentValue}";
    }

    public override void SetZero()
    {
        CurrentValue = 0;
        ValueText.text = $"{CurrentValue}";
    }

    private int MaxMultiple()
    {
        int betMoney = _betControl.CurrentValue;
        int playerMoney = PlayerManager.instance.PlayerData.Money;

        if (playerMoney == 0 || betMoney == 0)
        {
            return 0;
        }

        return (int)playerMoney / betMoney;
    }
}