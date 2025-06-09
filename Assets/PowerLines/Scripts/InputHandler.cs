using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class InputHandler : MonoBehaviour
{
    public static event Action<Vector2> OnTap;
    public static event Action<Vector2> OnDrag;

    private Vector2 startPos;
    private Vector2 lastPos;
    private bool isTouching = false;
    private float maxTapDistance = 20f;

    private void Update()
    {
        if (IsPointerOverUI()) return;

#if UNITY_EDITOR || UNITY_STANDALONE
        HandleMouse();
#else
        HandleTouch();
#endif
    }

    private void HandleMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isTouching = true;
            startPos = lastPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0) && isTouching)
        {
            Vector2 currentPos = Input.mousePosition;
            Vector2 delta = currentPos - lastPos;
            lastPos = currentPos;

            OnDrag?.Invoke(delta);
        }

        if (Input.GetMouseButtonUp(0) && isTouching)
        {
            Vector2 endPos = Input.mousePosition;
            float distance = Vector2.Distance(startPos, endPos);

            if (distance <= maxTapDistance)
            {
                OnTap?.Invoke(endPos);
            }

            isTouching = false;
        }
    }

    private void HandleTouch()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                isTouching = true;
                startPos = lastPos = touch.position;
                break;

            case TouchPhase.Moved:
                Vector2 delta = touch.position - lastPos;
                lastPos = touch.position;

                OnDrag?.Invoke(delta);
                break;

            case TouchPhase.Ended:
                float distance = Vector2.Distance(startPos, touch.position);
                if (distance <= maxTapDistance)
                {
                    OnTap?.Invoke(touch.position);
                }
                isTouching = false;
                break;
        }
    }

    private bool IsPointerOverUI()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
#else
        if (Input.touchCount > 0)
        {
            return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
        }
        return false;
#endif
    }
}
