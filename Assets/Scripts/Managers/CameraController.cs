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
//using UnityEngine;

//public class CameraController : MonoBehaviour
//{
//    [SerializeField] private float panSpeed = 0.01f;
//    [SerializeField] private float zoomSpeed = 0.5f;
//    [SerializeField] private float minZoom = 3f;
//    [SerializeField] private float maxZoom = 12f;
//    [SerializeField] private float gridWidth = 20f;
//    [SerializeField] private float gridHeight = 20f;

//    public bool IsDragging => isDragging;

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
//        if (Input.GetMouseButtonDown(0))
//        {
//            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
//            isDragging = false;
//        }

//        if (Input.GetMouseButton(0))
//        {
//            Vector3 currentPos = cam.ScreenToWorldPoint(Input.mousePosition);
//            Vector3 delta = dragOrigin - currentPos;

//            if (delta.magnitude > 0.01f)
//                isDragging = true;

//            if (isDragging)
//            {
//                transform.position += delta * panSpeed * cam.orthographicSize;
//                ClampPosition();
//            }
//        }
//    }

//    void HandleZoom()
//    {
//        float scroll = Input.GetAxis("Mouse ScrollWheel");
//        if (Mathf.Abs(scroll) < 0.001f)
//            return;

//        cam.orthographicSize -= scroll * zoomSpeed * cam.orthographicSize;
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
#region Phase 1 Sprint 7 - Camera Controls with New Input System and Clamping
//using UnityEngine;

//public class CameraController : MonoBehaviour
//{
//    [SerializeField] private float panSpeed = 0.01f;
//    [SerializeField] private float zoomSpeed = 0.5f;
//    [SerializeField] private float minZoom = 3f;
//    [SerializeField] private float maxZoom = 12f;
//    [SerializeField] private float gridWidth = 20f;
//    [SerializeField] private float gridHeight = 20f;

//    public bool IsDragging => isDragging;

//    private Vector3 dragOrigin;
//    private bool isDragging;
//    private Camera cam;

//    void Start()
//    {
//        cam = GetComponent<Camera>();
//        transform.position = new Vector3(0f, gridHeight * 0.25f, transform.position.z);
//    }

//    void Update()
//    {
//        HandlePan();
//        HandleZoom();
//    }

//    void HandlePan()
//    {
//        if (Input.GetMouseButtonDown(0))
//        {
//            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
//            isDragging = false;
//        }

//        if (Input.GetMouseButton(0))
//        {
//            Vector3 currentPos = cam.ScreenToWorldPoint(Input.mousePosition);
//            Vector3 delta = dragOrigin - currentPos;

//            if (delta.magnitude > 0.01f)
//                isDragging = true;

//            if (isDragging)
//            {
//                transform.position += delta * panSpeed * cam.orthographicSize;
//                ClampPosition();
//            }
//        }
//    }

//    void HandleZoom()
//    {
//        float scroll = Input.GetAxis("Mouse ScrollWheel");
//        if (Mathf.Abs(scroll) < 0.001f)
//            return;

//        cam.orthographicSize -= scroll * zoomSpeed * cam.orthographicSize;
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
#region Phase 1 Sprint 7a - Camera Controls with New Input System, Clamping, and Initial Position
//using UnityEngine;

//public class CameraController : MonoBehaviour
//{
//    [SerializeField] private float panSpeed = 0.01f;
//    [SerializeField] private float zoomSpeed = 0.5f;
//    [SerializeField] private float minZoom = 3f;
//    [SerializeField] private float maxZoom = 12f;
//    [SerializeField] private float gridWidth = 20f;
//    [SerializeField] private float gridHeight = 20f;

//    public bool IsDragging => isDragging;

//    private Vector3 dragOrigin;
//    private bool isDragging;
//    private Camera cam;

//    void Start()
//    {
//        cam = GetComponent<Camera>();
//        transform.position = new Vector3(0f, gridHeight * 0.25f, transform.position.z);
//    }

//    void Update()
//    {
//        HandlePan();
//        HandleZoom();
//    }

//    void HandlePan()
//    {
//        if (Input.GetMouseButtonDown(0))
//        {
//            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
//            isDragging = false;
//        }

//        if (Input.GetMouseButton(0))
//        {
//            Vector3 currentPos = cam.ScreenToWorldPoint(Input.mousePosition);
//            Vector3 delta = dragOrigin - currentPos;

//            if (delta.magnitude > 0.01f)
//                isDragging = true;

//            if (isDragging)
//            {
//                transform.position += delta * panSpeed * cam.orthographicSize;
//                ClampPosition();
//            }
//        }
//    }

//    void HandleZoom()
//    {
//        float scroll = Input.GetAxis("Mouse ScrollWheel");
//        if (Mathf.Abs(scroll) < 0.001f)
//            return;

//        cam.orthographicSize -= scroll * zoomSpeed * cam.orthographicSize;
//        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
//        ClampPosition();
//    }

//    void ClampPosition()
//    {
//        float halfW = gridWidth * 0.5f;
//        float minY = 0f;
//        float maxY = gridHeight * 0.5f;

//        Vector3 pos = transform.position;
//        pos.x = Mathf.Clamp(pos.x, -halfW, halfW);
//        pos.y = Mathf.Clamp(pos.y, minY, maxY);
//        transform.position = pos;
//    }
//}
#endregion
#region Phase 1 Sprint 9 - Camera Controller Rewrite (Modular, Inertia, Viewport-Aware)
//using UnityEngine;

//public class CameraController : MonoBehaviour
//{
//    [Header("Zoom")]
//    [SerializeField] private float zoomSpeed = 0.1f;
//    [SerializeField] private float minZoom = 2f;
//    [SerializeField] private float maxZoom = 15f;

//    [Header("Smoothing")]
//    [SerializeField] private float smoothing = 10f;

//    public bool IsDragging => isDragging;

//    private Camera cam;
//    private TileGridRenderer gridRenderer;

//    private Vector3 dragOrigin;
//    private bool isDragging;

//    private Vector3 targetPosition;
//    private float targetZoom;

//    // World bounds calculated from the actual grid at startup
//    private float boundsMinX;
//    private float boundsMaxX;
//    private float boundsMinY;
//    private float boundsMaxY;

//    void Start()
//    {
//        cam = GetComponent<Camera>();
//        gridRenderer = FindFirstObjectByType<TileGridRenderer>();

//        CalculateBounds();

//        // Start centered on the grid
//        float centerX = (boundsMinX + boundsMaxX) * 0.5f;
//        float centerY = (boundsMinY + boundsMaxY) * 0.5f;
//        transform.position = new Vector3(centerX, centerY, transform.position.z);

//        targetPosition = transform.position;
//        targetZoom = cam.orthographicSize;
//    }

//    void Update()
//    {
//        HandlePan();
//        HandleZoom();
//        ApplySmoothing();
//    }

//    void CalculateBounds()
//    {
//        if (gridRenderer == null)
//        {
//            // Fallback if no grid found
//            boundsMinX = -10f;
//            boundsMaxX = 10f;
//            boundsMinY = 0f;
//            boundsMaxY = 10f;
//            return;
//        }

//        // Use GridToWorld to get the actual world extents of the grid
//        Vector3 bottomTip = gridRenderer.GridToWorld(0, 0);
//        Vector3 topTip = gridRenderer.GridToWorld(gridRenderer.GridWidth - 1, gridRenderer.GridHeight - 1);
//        Vector3 leftTip = gridRenderer.GridToWorld(0, gridRenderer.GridHeight - 1);
//        Vector3 rightTip = gridRenderer.GridToWorld(gridRenderer.GridWidth - 1, 0);

//        boundsMinX = leftTip.x;
//        boundsMaxX = rightTip.x;
//        boundsMinY = bottomTip.y;
//        boundsMaxY = topTip.y;
//    }

//    void HandlePan()
//    {
//        if (Input.GetMouseButtonDown(0))
//        {
//            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
//            isDragging = false;
//        }

//        if (Input.GetMouseButton(0))
//        {
//            Vector3 currentWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
//            Vector3 delta = dragOrigin - currentWorldPos;

//            if (delta.magnitude > 0.01f)
//                isDragging = true;

//            if (isDragging)
//            {
//                targetPosition += delta;
//                ClampTarget();
//            }
//        }

//        if (Input.GetMouseButtonUp(0))
//            isDragging = false;
//    }

//    void HandleZoom()
//    {
//        float scroll = Input.GetAxis("Mouse ScrollWheel");
//        if (Mathf.Abs(scroll) < 0.001f)
//            return;

//        // Zoom speed scales with current zoom so it feels consistent at all levels
//        targetZoom -= scroll * zoomSpeed * targetZoom;
//        targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
//        ClampTarget();
//    }

//    void ApplySmoothing()
//    {
//        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothing);
//        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * smoothing);
//    }

//    void ClampTarget()
//    {
//        // Account for camera viewport edges so you can never pan outside the grid
//        float camHalfH = targetZoom;
//        float camHalfW = targetZoom * cam.aspect;

//        float clampedX = Mathf.Clamp(targetPosition.x, boundsMinX + camHalfW, boundsMaxX - camHalfW);
//        float clampedY = Mathf.Clamp(targetPosition.y, boundsMinY + camHalfH, boundsMaxY - camHalfH);

//        // If the grid is smaller than the camera view, just center on the grid
//        if (boundsMaxX - boundsMinX < camHalfW * 2f)
//            clampedX = (boundsMinX + boundsMaxX) * 0.5f;
//        if (boundsMaxY - boundsMinY < camHalfH * 2f)
//            clampedY = (boundsMinY + boundsMaxY) * 0.5f;

//        targetPosition = new Vector3(clampedX, clampedY, targetPosition.z);
//    }
//}
#endregion

#region Phase 2 Sprint 2 - Camera With Padding and HUD Offset
//using UnityEngine;

//public class CameraController : MonoBehaviour
//{
//    [Header("Zoom")]
//    [SerializeField] private float zoomSpeed = 0.1f;
//    [SerializeField] private float minZoom = 2f;
//    [SerializeField] private float maxZoom = 20f;
//    [SerializeField] private float startZoom = 8f;

//    [Header("Smoothing")]
//    [SerializeField] private float smoothing = 10f;

//    [Header("HUD Offset")]
//    [SerializeField] private float hudOffsetY = -0.5f;

//    [Header("Bounds Padding")]
//    [SerializeField] private float paddingX = 5f;
//    [SerializeField] private float paddingY = 3f;

//    public bool IsDragging => isDragging;

//    private Camera cam;
//    private TileGridRenderer gridRenderer;
//    private Vector3 dragOrigin;
//    private bool isDragging;
//    private Vector3 targetPosition;
//    private float targetZoom;

//    private float boundsMinX;
//    private float boundsMaxX;
//    private float boundsMinY;
//    private float boundsMaxY;

//    void Start()
//    {
//        cam = GetComponent<Camera>();
//        gridRenderer = FindFirstObjectByType<TileGridRenderer>();

//        CalculateBounds();

//        float centerX = (boundsMinX + boundsMaxX) * 0.5f;
//        float centerY = (boundsMinY + boundsMaxY) * 0.5f + hudOffsetY;

//        transform.position = new Vector3(centerX, centerY, transform.position.z);
//        targetPosition = transform.position;
//        targetZoom = startZoom;
//        cam.orthographicSize = startZoom;
//    }

//    void Update()
//    {
//        HandlePan();
//        HandleZoom();
//        ApplySmoothing();
//    }

//    void CalculateBounds()
//    {
//        if (gridRenderer == null)
//        {
//            boundsMinX = -10f;
//            boundsMaxX = 10f;
//            boundsMinY = 0f;
//            boundsMaxY = 10f;
//            return;
//        }

//        Vector3 bottomTip = gridRenderer.GridToWorld(0, 0);
//        Vector3 topTip = gridRenderer.GridToWorld(gridRenderer.GridWidth - 1, gridRenderer.GridHeight - 1);
//        Vector3 leftTip = gridRenderer.GridToWorld(0, gridRenderer.GridHeight - 1);
//        Vector3 rightTip = gridRenderer.GridToWorld(gridRenderer.GridWidth - 1, 0);

//        // Add padding so the camera can pan slightly beyond the grid edges
//        boundsMinX = leftTip.x - paddingX;
//        boundsMaxX = rightTip.x + paddingX;
//        boundsMinY = bottomTip.y - paddingY;
//        boundsMaxY = topTip.y + paddingY;
//    }

//    void HandlePan()
//    {
//        if (Input.GetMouseButtonDown(0))
//        {
//            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
//            isDragging = false;
//        }

//        if (Input.GetMouseButton(0))
//        {
//            Vector3 currentWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
//            Vector3 delta = dragOrigin - currentWorldPos;

//            if (delta.magnitude > 0.01f)
//                isDragging = true;

//            if (isDragging)
//            {
//                targetPosition += delta;
//                ClampTarget();
//            }
//        }

//        if (Input.GetMouseButtonUp(0))
//            isDragging = false;
//    }

//    void HandleZoom()
//    {
//        float scroll = Input.GetAxis("Mouse ScrollWheel");
//        if (Mathf.Abs(scroll) < 0.001f) return;

//        targetZoom -= scroll * zoomSpeed * targetZoom;
//        targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
//        ClampTarget();
//    }

//    void ApplySmoothing()
//    {
//        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothing);
//        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * smoothing);
//    }

//    void ClampTarget()
//    {
//        float camHalfH = targetZoom;
//        float camHalfW = targetZoom * cam.aspect;

//        float clampedX = Mathf.Clamp(targetPosition.x, boundsMinX + camHalfW, boundsMaxX - camHalfW);
//        float clampedY = Mathf.Clamp(targetPosition.y, boundsMinY + camHalfH, boundsMaxY - camHalfH);

//        if (boundsMaxX - boundsMinX < camHalfW * 2f)
//            clampedX = (boundsMinX + boundsMaxX) * 0.5f;

//        if (boundsMaxY - boundsMinY < camHalfH * 2f)
//            clampedY = (boundsMinY + boundsMaxY) * 0.5f;

//        targetPosition = new Vector3(clampedX, clampedY, targetPosition.z);
//    }
//}
#endregion

#region Phase 2 Sprint 2 - Camera With Padding and HUD Offset
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Zoom")]
    [SerializeField] private float zoomSpeed = 0.1f;
    [SerializeField] private float minZoom = 2f;
    [SerializeField] private float maxZoom = 20f;
    [SerializeField] private float startZoom = 8f;

    [Header("Smoothing")]
    [SerializeField] private float smoothing = 10f;

    [Header("HUD Offset")]
    [SerializeField] private float hudOffsetY = -0.5f;

    [Header("Bounds Padding")]
    [SerializeField] private float paddingX = 5f;
    [SerializeField] private float paddingY = 3f;

    public bool IsDragging => isDragging;

    private Camera cam;
    private TileGridRenderer gridRenderer;
    private Vector3 dragOrigin;
    private bool isDragging;
    private Vector3 targetPosition;
    private float targetZoom;

    private float boundsMinX;
    private float boundsMaxX;
    private float boundsMinY;
    private float boundsMaxY;

    void Start()
    {
        cam = GetComponent<Camera>();
        gridRenderer = FindFirstObjectByType<TileGridRenderer>();

        CalculateBounds();

        float centerX = (boundsMinX + boundsMaxX) * 0.5f;
        float centerY = (boundsMinY + boundsMaxY) * 0.5f + hudOffsetY;

        transform.position = new Vector3(centerX, centerY, transform.position.z);
        targetPosition = transform.position;
        targetZoom = startZoom;
        cam.orthographicSize = startZoom;
    }

    void Update()
    {
        HandlePan();
        HandleZoom();
        ApplySmoothing();
    }

    void CalculateBounds()
    {
        if (gridRenderer == null)
        {
            boundsMinX = -10f;
            boundsMaxX = 10f;
            boundsMinY = 0f;
            boundsMaxY = 10f;
            return;
        }

        Vector3 bottomTip = gridRenderer.GridToWorld(0, 0);
        Vector3 topTip = gridRenderer.GridToWorld(gridRenderer.GridWidth - 1, gridRenderer.GridHeight - 1);
        Vector3 leftTip = gridRenderer.GridToWorld(0, gridRenderer.GridHeight - 1);
        Vector3 rightTip = gridRenderer.GridToWorld(gridRenderer.GridWidth - 1, 0);

        // Add padding so the camera can pan slightly beyond the grid edges
        boundsMinX = leftTip.x - paddingX;
        boundsMaxX = rightTip.x + paddingX;
        boundsMinY = bottomTip.y - paddingY;
        boundsMaxY = topTip.y + paddingY;
    }

    void HandlePan()
    {
        if (Mouse.current == null) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            dragOrigin = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            isDragging = false;
        }

        if (Mouse.current.leftButton.isPressed)
        {
            Vector3 currentWorldPos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector3 delta = dragOrigin - currentWorldPos;

            if (delta.magnitude > 0.01f)
                isDragging = true;

            if (isDragging)
            {
                targetPosition += delta;
                ClampTarget();
            }
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
            isDragging = false;
    }

    void HandleZoom()
    {
        if (Mouse.current == null) return;

        float scroll = Mouse.current.scroll.ReadValue().y;
        if (Mathf.Abs(scroll) < 0.001f) return;

        targetZoom -= scroll * zoomSpeed * targetZoom * 0.01f;
        targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
        ClampTarget();
    }

    void ApplySmoothing()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothing);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * smoothing);
    }

    void ClampTarget()
    {
        float camHalfH = targetZoom;
        float camHalfW = targetZoom * cam.aspect;

        float clampedX = Mathf.Clamp(targetPosition.x, boundsMinX + camHalfW, boundsMaxX - camHalfW);
        float clampedY = Mathf.Clamp(targetPosition.y, boundsMinY + camHalfH, boundsMaxY - camHalfH);

        if (boundsMaxX - boundsMinX < camHalfW * 2f)
            clampedX = (boundsMinX + boundsMaxX) * 0.5f;

        if (boundsMaxY - boundsMinY < camHalfH * 2f)
            clampedY = (boundsMinY + boundsMaxY) * 0.5f;

        targetPosition = new Vector3(clampedX, clampedY, targetPosition.z);
    }
}
#endregion