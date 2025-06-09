using UnityEngine;

public class Cell : MonoBehaviour
{
    private void OnEnable()
    {
        InputHandler.OnTap += HandleTap;
    }

    private void OnDisable()
    {
        InputHandler.OnTap -= HandleTap;
    }

    private void HandleTap(Vector2 screenPosition)
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPosition);
        Vector2 worldPos2D = new Vector2(worldPos.x, worldPos.y);

        Collider2D col = Physics2D.OverlapPoint(worldPos2D);

        if (col != null && col.transform == transform)
        {
            Debug.Log("Tapped on cell: " + gameObject.name);
        }
    }
}