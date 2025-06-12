using UnityEngine;

public class Consumer : Building
{
    [SerializeField] private GameObject _tooMuchParticle;
    [SerializeField] private GameObject _notParticle;
    
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.TryGetComponent(out Building building))
        {
            if (building.Volt == 5)
            {
                UIController.Instance.ShowWinPanel();
            }
            else if (building.Volt > 5)
            {
                Instantiate(_tooMuchParticle, transform.position, Quaternion.identity);
                UIController.Instance.ShowTooMuch();
            }
            else
            {
                Instantiate(_notParticle, transform.position, Quaternion.identity);
                UIController.Instance.ShowNo();
            }
        }
    }
}
