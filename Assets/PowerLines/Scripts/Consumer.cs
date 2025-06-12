using UnityEngine;

public class Consumer : Building
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.TryGetComponent(out Building building))
        {
            UIController.Instance.ShowWinPanel();
            if (building.Volt == 5)
            {
                //UIController.Instance.ShowWinPanel();
            }
        }
    }
}
