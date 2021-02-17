using System.Collections;
using System.Collections.Generic;
using TauntGames;
using UnityEngine;
using UnityEngine.UI;

public class UI_Control : MonoBehaviour
{
    #region Variables

    public GameObject playerUI;
    public GameObject playerCameraCanvas;
    public GameObject uiCamera;
    public GameObject playerQuitMenu;
    public GameObject areYouSurePanel;
    public GameObject quitPanel;
    public Text infoText_Centre;
    public float textFlashTime;
    public List<GameObject> panelToSetActive;
    public UI_Settings settings;

    #endregion

    #region Monobeahaviour

    private void OnEnable()
    {
        playerUI.SetActive(false);
        playerQuitMenu.SetActive(false);
        uiCamera.SetActive(true);
        uiCamera.SetActive(false);
        uiCamera.SetActive(true);
        CheckCursor();
        onPlayerNeedsInfo += FlashTextOnScreen;
        settings.Startup();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleMainMenu();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleQuitMenu();
        }
    }

    #endregion

    #region Events

    public delegate void OnPlayerNeedsInfo(string infoText);
    public static OnPlayerNeedsInfo onPlayerNeedsInfo;

    #endregion

    void ToggleQuitMenu()
    {
        if (playerUI.activeSelf)
        {
            playerUI.SetActive(false);
            playerQuitMenu.SetActive(!playerQuitMenu.activeSelf);
        }
        else
        {
            // uiCamera.SetActive(!uiCamera.activeSelf);
            playerQuitMenu.SetActive(!playerQuitMenu.activeSelf);
        }
        CheckCursor();
    }
    
        

    public void ToggleMainMenu()
    {
        if (playerQuitMenu.activeSelf)
        {
            playerQuitMenu.SetActive(!playerQuitMenu.activeSelf);
            playerUI.SetActive(!playerUI.activeSelf);
        }
        else
        {
            // uiCamera.SetActive(!uiCamera.activeSelf);
            playerUI.SetActive(!playerUI.activeSelf);
        }
        CheckCursor();
    }

    public void QuitGame()
    {
        Application.Quit(0);
    }

    public void ReturnToGame()
    {
        playerUI.SetActive(false);
        playerQuitMenu.SetActive(false);
        CheckCursor();
    }
    
    void CheckCursor()
    {
        bool isOpen;
        if (playerUI.activeSelf || playerQuitMenu.activeSelf)
            isOpen = true;
        else
            isOpen = false;
        playerCameraCanvas.SetActive(!isOpen);
        Time.timeScale = isOpen ? 0 : 1;
        Cursor.visible = isOpen;
        Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void AreYouSure()
    {
        quitPanel.SetActive(!quitPanel.activeSelf);
        areYouSurePanel.SetActive(!areYouSurePanel.activeSelf);
        CheckCursor();
    }

    public void SaveThenQuit()
    {
        //debug.log i have saved then quit
        Application.Quit(0);
    }
    
    public void MenuButtonPressed(int panelIndex)
    {
        for (int i = 0; i < panelToSetActive.Count; i++)
        {
            panelToSetActive[i].SetActive(false);
        }
        panelToSetActive[panelIndex].SetActive(true);
        CheckCursor();
    }

    void FlashTextOnScreen(string infoText)
    {
        infoText_Centre.text = infoText;
    }
    

    IEnumerator FlashText(InventoryResults results)
    {
        infoText_Centre.color = results._textColor;
        infoText_Centre.text = results._infoString;
        yield return new WaitForSeconds(textFlashTime);
        infoText_Centre.text = "";
    }
}
