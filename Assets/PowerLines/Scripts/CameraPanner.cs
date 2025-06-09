using UnityEngine;

public class CameraPanner : MonoBehaviour
{
    [Header("Movement")]
    public float panSpeed = 0.02f;
    public float inertiaDamping = 0.9f;

    [Header("Boundary Transforms")]
    public Transform leftLimit;
    public Transform rightLimit;
    public Transform topLimit;
    public Transform bottomLimit;

    private Vector3 inertiaVelocity;
    private bool isDragging;
    private Camera cam;

    private void OnEnable()
    {
        InputHandler.OnDrag += HandleDrag;
        InputHandler.OnTap += StopInertia;
    }

    private void OnDisable()
    {
        InputHandler.OnDrag -= HandleDrag;
        InputHandler.OnTap -= StopInertia;
    }

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (!isDragging && inertiaVelocity.magnitude > 0.01f)
        {
            MoveCamera(inertiaVelocity * Time.deltaTime);
            inertiaVelocity *= inertiaDamping;
        }
    }

    private void HandleDrag(Vector2 dragDelta)
    {
        isDragging = true;

        Vector3 screenDelta = new Vector3(-dragDelta.x, -dragDelta.y, 0) * panSpeed;
        Vector3 worldDelta = cam.ScreenToWorldPoint(cam.transform.position + screenDelta) 
                           - cam.ScreenToWorldPoint(cam.transform.position);

        MoveCamera(worldDelta);
        inertiaVelocity = worldDelta / Time.deltaTime;
    }

    private void StopInertia(Vector2 _)
    {
        isDragging = false;
    }

    private void MoveCamera(Vector3 delta)
    {
        Vector3 newPos = cam.transform.position + delta;

        float vertExtent = cam.orthographicSize;
        float horzExtent = vertExtent * cam.aspect;

        float leftBound = leftLimit.position.x + horzExtent;
        float rightBound = rightLimit.position.x - horzExtent;
        float bottomBound = bottomLimit.position.y + vertExtent;
        float topBound = topLimit.position.y - vertExtent;

        newPos.x = Mathf.Clamp(newPos.x, leftBound, rightBound);
        newPos.y = Mathf.Clamp(newPos.y, bottomBound, topBound);

        cam.transform.position = new Vector3(newPos.x, newPos.y, cam.transform.position.z);
    }
}
