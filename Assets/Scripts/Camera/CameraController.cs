using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 20f;
    public float panBorderThickness = 10f;
    public float xLimit, yLimit;
    public float scrollSpeed = 2f;
    private float zoom;
    private float zoomMultiplier = 4f;
    private float minZoom = 2f;
    private float maxZoom = 8f;
    private float velocity = 0f;
    private float smoothTime = 0.25f;

    private bool _isDragging;
    private UnityEngine.Vector3 _MouseOrigin;
    private UnityEngine.Vector3 _MouseDifference;

    [SerializeField] private Camera myCamera;

    void Start()
    {
        myCamera = Camera.main;
        zoom = cam.ortographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        float scroll = Input.
        
        UnityEngine.Vector3 pos = transform.position;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.y += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            pos.y -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }
   
        
        //float GetScroll = Mouse.current.scroll.ReadValue<UnityEngine.Vector2>().normalized.y;
        //UnityEngine.Vector2 vec = Mouse.current.scroll.ReadValue();
        //float scroll = GetScroll.y;
        //pos.z -= scroll * scrollSpeed * 1f * Time.deltaTime;
        //if (scroll !=0) 
        //if (scroll > 0)
            //pos.z -= scroll * scrollSpeed * 80f * Time.deltaTime;
        //else if (scroll < 0)
            //pos.z += scroll * scrollSpeed * 80f * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, -xLimit, xLimit);
        //pos.z = Mathf.Clamp(pos.z, minZ, maxZ);
        pos.y = Mathf.Clamp(pos.y, -yLimit, yLimit);

        transform.position = pos;
    }

    public void OnDrag(InputAction.CallbackContext ctx)
    {
        if (ctx.started) _MouseOrigin = GetMousePosition;
        _isDragging = ctx.started || ctx.performed;
    }

    private void LateUpdate()
    {
        if (!_isDragging) return;

        _MouseDifference = GetMousePosition - transform.position;
        transform.position = _MouseOrigin - _MouseDifference;
    }

    private UnityEngine.Vector3 GetMousePosition => myCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
}
