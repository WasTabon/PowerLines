using UnityEngine;

public class Consumer : Building
{
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
                UIController.Instance.ShowTooMuch();
            }
            else
            {
                UIController.Instance.ShowNo();
            }
        }
    }
}
