using System.Linq;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    public string name;
    
    protected int _volt;
    protected int _amper;
    
    [SerializeField] protected Vector2 checkBoxSize = Vector2.one;

    public int Volt => _volt;
    public int Amper => _amper;

    protected bool _isActive;

    public void SetActive()
    {
        _isActive = true;
    }
    
    public void CheckBuildings()
    {
        Vector2 frontPosition = (Vector2)transform.position + (Vector2)transform.up * checkBoxSize.y;
        Vector2 backPosition  = (Vector2)transform.position - (Vector2)transform.up * checkBoxSize.y;

        var frontHits = Physics2D.OverlapBoxAll(frontPosition, checkBoxSize, transform.eulerAngles.z);
        var backHits = Physics2D.OverlapBoxAll(backPosition, checkBoxSize, transform.eulerAngles.z);

        var frontBuildings = frontHits
            .Select(hit => hit.GetComponent<Building>())
            .Where(b => b != null && b != this)
            .ToList();

        var backBuildings = backHits
            .Select(hit => hit.GetComponent<Building>())
            .Where(b => b != null && b != this)
            .ToList();

        Debug.Log($"Front Buildings: {frontBuildings.Count}");
        Debug.Log($"Back Buildings: {backBuildings.Count}");
    }
    
    private void OnDrawGizmosSelected()
    {
        Vector2 frontPosition = (Vector2)transform.position + (Vector2)transform.up * checkBoxSize.y;
        Vector2 backPosition  = (Vector2)transform.position - (Vector2)transform.up * checkBoxSize.y;

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(frontPosition, checkBoxSize);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(backPosition, checkBoxSize);
    }
}