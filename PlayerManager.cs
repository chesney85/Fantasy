using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public delegate void OnPlayerShoots();
    public static event OnPlayerShoots onPlayerShoots;
    public delegate void OnPlayerSecondaryShoots();
    public static event OnPlayerSecondaryShoots onPlayerSecondaryShoots;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            onPlayerShoots?.Invoke();
        }
        if (Input.GetMouseButtonDown(1))
        {
            onPlayerSecondaryShoots?.Invoke();
        }
    }
}
