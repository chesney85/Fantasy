using UnityEngine;

public class UI_MenuSelection_Button : MonoBehaviour
{
    public int index;
    public void OnClickThisButton()
    {
        UI_Control.onMenuButtonPressed.Invoke(index);
    }
}
