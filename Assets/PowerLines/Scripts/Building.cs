using UnityEngine;

public abstract class Building : MonoBehaviour
{
    protected int _volt;
    protected int _amper;

    public int Volt => _volt;
    public int Amper => _amper;

    protected bool _isActive;

    public void SetActive()
    {
        _isActive = true;
    }
}