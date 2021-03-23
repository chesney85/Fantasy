
using UnityEngine;
using Vector3 = UnityEngine.Vector3;


public class UI_MinimapCamera : MonoBehaviour
{

    [Header("Player To Follow")]
    public Transform player;
    [Header("Height Above Player")]
    [Range(340,440)]
    public float cameraHeight;
    [Header("How Fast The Minimap Should Update")]
    [Range(0.1f,2f)]
    public float miniMapUpdateSpeed;
    [Header("Determines If Minimap Updates")]
    public bool isMapModeEnabled;
    [Header("Minimap To Rotate")] 
    public RectTransform minimap;
    [Header("Map Letters To Rotate")]
    public RectTransform north;
    public RectTransform south;
    public RectTransform west;
    public RectTransform east;

    [Header("Stuff To Enable/Disable Map")]
    public GameObject playerUI;
    public GameObject playerCamera;
    public GameObject uiCamera;
    public Camera mapCamera;
    public RenderTexture mapTexture;
    

    private void Start()
    {
        isMapModeEnabled = false;
        InvokeRepeating(nameof(CheckPosition), 0f,miniMapUpdateSpeed);
    }

    void CheckPosition()
    {
        // if (Input.GetKeyDown(KeyCode.M))
        // {
        //     MapModeEnabled();
        // }
        
        if (!isMapModeEnabled)
        {
            //Map Position
            Vector3 playerPos = player.position;
            playerPos.y = cameraHeight;
            transform.position = playerPos;
            //Map Rotation
            Quaternion rot = player.rotation;
            rot.z = rot.y;
            rot.x = 0;
            rot.y = 0;
            minimap.rotation = rot;
            rot.z = 0;
            north.rotation = rot;
            south.rotation = rot;
            west.rotation = rot;
            east.rotation = rot;
        }

        
        
    }

    // public void MapModeEnabled()
    // {
    //     isMapModeEnabled = !isMapModeEnabled;
    // }
    
}
