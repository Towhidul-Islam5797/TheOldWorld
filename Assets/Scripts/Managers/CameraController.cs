#region Summary
/// <summary>
/// Controls the camera movement and zoom within the game.
/// Allows the player to pan the camera by dragging and zoom in/out using the mouse scroll wheel.
/// The camera's position is clamped to ensure it stays within the bounds of the tile grid, preventing the player from moving the camera too far away from the playable area.
/// The pan speed and zoom speed can be adjusted via the inspector, as well as the minimum and maximum zoom levels. The grid width and height are also configurable to match the size of the tile grid in the game.
/// This script should be attached to the main camera in the Unity scene to enable these controls.
/// Note: The camera is assumed to be orthographic for this implementation, which is common for 2D games. If using a perspective camera, additional adjustments may be needed to handle zooming and panning correctly.
/// Overall, this script provides a simple and intuitive way for players to navigate the game world by controlling the camera, enhancing the user experience and allowing for better exploration of the tile-based environment.
/// </summary>
#endregion
#region Phase 1 Sprint 2 - Camera Controls
//using UnityEngine;
//using UnityEngine.InputSystem;

//public class CameraController : MonoBehaviour
//{
//    [SerializeField] private float panSpeed = 0.01f;
//    [SerializeField] private float zoomSpeed = 0.5f;
//    [SerializeField] private float minZoom = 3f;
//    [SerializeField] private float maxZoom = 12f;
//    [SerializeField] private float gridWidth = 20f;
//    [SerializeField] private float gridHeight = 20f;

//    private Vector3 dragOrigin;
//    private bool isDragging;
//    private Camera cam;

//    void Start()
//    {
//        cam = GetComponent<Camera>();
//    }

//    void Update()
//    {
//        HandlePan();
//        HandleZoom();
//    }

//    void HandlePan()
//    {
//        Vector2 mousePos = Mouse.current.position.ReadValue();
//        Vector3 worldMousePos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0));

//        if (Mouse.current.leftButton.wasPressedThisFrame)
//        {
//            dragOrigin = worldMousePos;
//            isDragging = false;
//        }

//        if (Mouse.current.leftButton.isPressed)
//        {
//            Vector3 delta = dragOrigin - worldMousePos;

//            if (delta.magnitude > 0.01f)
//                isDragging = true;

//            if (isDragging)
//            {
//                transform.position += delta * panSpeed * cam.orthographicSize;
//                ClampPosition();
//            }
//        }

//        if (Mouse.current.leftButton.wasReleasedThisFrame)
//            isDragging = false;
//    }

//    void HandleZoom()
//    {
//        float scroll = Mouse.current.scroll.ReadValue().y;
//        if (Mathf.Abs(scroll) < 0.001f)
//            return;

//        cam.orthographicSize -= scroll * zoomSpeed;
//        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
//        ClampPosition();
//    }

//    void ClampPosition()
//    {
//        float halfW = gridWidth * 0.5f;
//        float halfH = gridHeight * 0.25f;

//        Vector3 pos = transform.position;
//        pos.x = Mathf.Clamp(pos.x, -halfW, halfW);
//        pos.y = Mathf.Clamp(pos.y, -halfH, halfH);
//        transform.position = pos;
//    }
//}
#endregion
#region Phase 1 Sprint 3 - Camera Controls with New Input System
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float panSpeed = 0.01f;
    [SerializeField] private float zoomSpeed = 0.5f;
    [SerializeField] private float minZoom = 3f;
    [SerializeField] private float maxZoom = 12f;
    [SerializeField] private float gridWidth = 20f;
    [SerializeField] private float gridHeight = 20f;

    public bool IsDragging => isDragging;

    private Vector3 dragOrigin;
    private bool isDragging;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        HandlePan();
        HandleZoom();
    }

    void HandlePan()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
            isDragging = false;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 currentPos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 delta = dragOrigin - currentPos;

            if (delta.magnitude > 0.01f)
                isDragging = true;

            if (isDragging)
            {
                transform.position += delta * panSpeed * cam.orthographicSize;
                ClampPosition();
            }
        }
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) < 0.001f)
            return;

        cam.orthographicSize -= scroll * zoomSpeed * cam.orthographicSize;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
        ClampPosition();
    }

    void ClampPosition()
    {
        float halfW = gridWidth * 0.5f;
        float halfH = gridHeight * 0.25f;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -halfW, halfW);
        pos.y = Mathf.Clamp(pos.y, -halfH, halfH);
        transform.position = pos;
    }
}
#endregion  