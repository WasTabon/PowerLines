using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private int _powerSourceVolt;
    
    [SerializeField] private PowerSource _powerSource;

    private void Awake()
    {
        _powerSource.SetVolt(_powerSourceVolt);
    }
}
