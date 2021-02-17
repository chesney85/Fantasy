using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerManager.onPlayerShoots += Shoot;
    }

    private void OnDisable()
    {
        PlayerManager.onPlayerShoots -= Shoot;
    }

    void Shoot()
    {
        Debug.Log("shooting");
    }
}
