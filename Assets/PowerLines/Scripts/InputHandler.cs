using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class InputHandler : MonoBehaviour
{
    public static event Action<Vector2> OnTap;
    public static event Action<Vector2> OnDrag;

    [SerializeField] private GraphicRaycaster graphicRaycaster;
    [SerializeField] private EventSystem eventSystem;

    private PointerEventData pointerEventData;

    private Vector2 startPos;
    private Vector2 lastPos;
    private bool isTouching = false;
    private bool startedOverUI = false;
    private float maxTapDistance = 20f;

    private void Update()
    {
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
            startedOverUI = IsPointerOverUIObject(startPos);
        }

        if (Input.GetMouseButton(0) && isTouching)
        {
            if (!startedOverUI)
            {
                Vector2 currentPos = Input.mousePosition;
                Vector2 delta = currentPos - lastPos;
                lastPos = currentPos;

                OnDrag?.Invoke(delta);
            }
        }

        if (Input.GetMouseButtonUp(0) && isTouching)
        {
            Vector2 endPos = Input.mousePosition;
            float distance = Vector2.Distance(startPos, endPos);

            if (distance <= maxTapDistance && !startedOverUI)
            {
                OnTap?.Invoke(endPos);
            }

            isTouching = false;
            startedOverUI = false;
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
                startedOverUI = IsPointerOverUIObject(startPos);
                break;

            case TouchPhase.Moved:
                if (!startedOverUI)
                {
                    Vector2 delta = touch.position - lastPos;
                    lastPos = touch.position;

                    OnDrag?.Invoke(delta);
                }
                break;

            case TouchPhase.Ended:
                float distance = Vector2.Distance(startPos, touch.position);
                if (distance <= maxTapDistance && !startedOverUI)
                {
                    OnTap?.Invoke(touch.position);
                }
                isTouching = false;
                startedOverUI = false;
                break;
        }
    }

    private bool IsPointerOverUIObject(Vector2 screenPosition)
    {
        if (graphicRaycaster == null || eventSystem == null)
        {
            Debug.LogWarning("Missing GraphicRaycaster or EventSystem reference.");
            return false;
        }

        pointerEventData = new PointerEventData(eventSystem)
        {
            position = screenPosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(pointerEventData, results);

        return results.Count > 0;
    }
}
