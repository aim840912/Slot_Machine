using UnityEngine;
using UnityEngine.UI;

public abstract class SpinBase : MonoBehaviour
{
    [SerializeField] private Toggle _spinToggle;

    public abstract void StartSpin();
    public abstract void StopSpin();
}