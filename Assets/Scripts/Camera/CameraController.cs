using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 20f;
    public float panBorderThickness = 10f;
    public float xLimit, yLimit;
    public float scrollSpeed = 20f;
    //private float zoom;
    //private float zoomMultiplier = 4f;
    private float minZ = -5f;
    private float maxZ = -40f;
    //private float velocity = 0f;
    //private float smoothTime = 0.25f;

    UnityEngine.Vector3 lastDragPosition;


    private Camera myCamera;

    void Start()
    {
        myCamera = Camera.main;
        //zoom = cam.ortographicSize;
    }

    // Update is called once per frame
    void Update()
    {     
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

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.z += scroll * scrollSpeed * 100f * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, -xLimit, xLimit);
        pos.y = Mathf.Clamp(pos.y, -yLimit, yLimit);
        pos.z = Mathf.Clamp(pos.z, maxZ, minZ);

        transform.position = pos;

        UpdateDrag();    
    }

    void UpdateDrag()
    {
        if (Input.GetMouseButtonDown(2))
            lastDragPosition = Input.mousePosition;
        if (Input.GetMouseButton(2))
        {
            var delta = lastDragPosition - Input.mousePosition;
            transform.Translate(delta * Time.deltaTime * 1f);
            lastDragPosition = Input.mousePosition;
        }
    }

    private UnityEngine.Vector3 GetMousePosition => myCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
}
