using UnityEngine;
public class UI_Map : MonoBehaviour
{

    public GameObject minimapCamera;
    public UI_MinimapCamera mmc;
    public Camera cam;
    [Range(2,6)]
    public float dragSpeed;
    [Range(20,50)]
    public float zoomSpeed;
    private bool canDrag;
    private bool canZoom;
    private Vector3 camPosition;

    private void OnEnable()
    {
        canDrag = false;
        canZoom = true;
        mmc.isMapModeEnabled = true;
        camPosition = minimapCamera.transform.position;
    }

    private void OnDisable()
    {
        canZoom = false;
        minimapCamera.transform.position = camPosition;
        canDrag = false;
        mmc.isMapModeEnabled = false;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            canDrag = true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            canDrag = false;
        }
        Vector3 pos = minimapCamera.transform.position;
        if (canDrag)
        {
            pos.x -= Input.GetAxis("Mouse X") * dragSpeed;
            pos.x = Mathf.Clamp(pos.x,0f,500f);
            pos.z -= Input.GetAxis("Mouse Y") * dragSpeed;
            pos.z = Mathf.Clamp(pos.z,0f,500f);
            minimapCamera.transform.position = pos;
        }
        if (canZoom)
        {
            float orthoSize = cam.orthographicSize;
            orthoSize -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            cam.orthographicSize = Mathf.Clamp(orthoSize,20f,75f);
        }
    }
    
}
