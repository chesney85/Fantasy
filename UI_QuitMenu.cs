using UnityEngine;

public class UI_QuitMenu : MonoBehaviour
{
    

    public GameObject areYouSurePanel;
    public GameObject quitPanel;

    private void OnEnable()
    {
        UI_Control.onUiIsActive.Invoke();
    }

    private void OnDisable()
    {
        UI_Control.onUiIsActive.Invoke();
    }

    public void QuitGame()
    {
        Application.Quit(0);
    }

    public void ReturnToGame()
    {
        UI_Control.onQuitMenuClosed.Invoke();
    }
    
    public void AreYouSure()
    {
        quitPanel.SetActive(!quitPanel.activeSelf);
        areYouSurePanel.SetActive(!areYouSurePanel.activeSelf);
    }
    
    public void SaveThenQuit()
    {
        //debug.log i have saved then quit
        Application.Quit(0);
    }

}
