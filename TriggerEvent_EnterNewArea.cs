using UnityEngine;

public class TriggerEvent_EnterNewArea : MonoBehaviour
{
    public SO_AreaTrigger area;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           TriggerNewArea();
        }
    }

    void TriggerNewArea()
    {
        // GlobalEvents.onPlayerEntersNewArea?.Invoke();
        Destroy(gameObject,0.1f);
    }
}
