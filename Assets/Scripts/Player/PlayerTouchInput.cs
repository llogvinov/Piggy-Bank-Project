using UnityEngine;

public class PlayerTouchInput : PlayerInput
{
    [SerializeField] private float _deadZoneX = 0.3f;

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;

            float deltaX = mouseWorldPos.x - transform.position.x;

            if (Mathf.Abs(deltaX) > _deadZoneX)
            {
                HorizontalInput = deltaX < 0 ? -1f : 1f;
            }
            else
            {
                HorizontalInput = 0f;
            }
        }
        else
        {
            HorizontalInput = 0f;
        }
#else
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchWorldPos = Camera.main.ScreenToWorldPoint(touch.position);
            touchWorldPos.z = 0;

            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                float deltaX = touchWorldPos.x - transform.position.x;

                if (Mathf.Abs(deltaX) > _deadZoneX)
                {
                    HorizontalInput = deltaX < 0 ? -1f : 1f;
                }
                else
                {
                    HorizontalInput = 0f;
                }
            }
        }
        else
        {
            HorizontalInput = 0f;
        }
#endif
    }
}