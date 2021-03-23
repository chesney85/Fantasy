using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class UI_Control : MonoBehaviour
{
    #region Variables

    //Public
    [Header("Cameras")] 
    public Camera minimapCam;
    public RenderTexture minimapTexture;
    [Header("Other Game Objects")]
    public GameObject playerUI;
    public GameObject uiCamera;
    public GameObject mapCanvas;
    
    //Private
    private bool _uiIsActive;
    private UI_Settings settings;
    
    //Events
    public delegate void OnUiIsActive();
    public static OnUiIsActive onUiIsActive;
    
    #endregion

    #region Monobeahaviour

    private void Awake()
    {
        _uiIsActive = false;
        StartCoroutine(nameof(SetupUI));
        SetupEvents();
    }

    private void OnEnable()
    {
        playerUI.SetActive(false);
        mapCanvas.SetActive(false);
        uiCamera.SetActive(true);
        uiCamera.SetActive(false);
        uiCamera.SetActive(true);
        ChangeCursorSetting();
        if (settings)
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

        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMap();
        }
    }

    #endregion
    
    #region Quit Menu
    
    public delegate void OnQuitMenuOpened();
    public delegate void OnQuitMenuClosed();
    public static OnQuitMenuOpened onQuitMenuOpened;
    public static OnQuitMenuClosed onQuitMenuClosed;
    private GameObject playerQuitMenu;
    [Header("Quit Menu")]
    public GameObject QuitMenuPrefab;
    public GameObject QuitMenuContainer;
    void OpenQuitMenu()
    {
        playerQuitMenu.SetActive(true);
        onUiIsActive.Invoke();
    }

    void CloseQuitMenu()
    {
        playerQuitMenu.SetActive(false);
        onUiIsActive.Invoke();
    }
    
    void ToggleQuitMenu()
    {
        if (playerQuitMenu)
        {
            if (playerQuitMenu.activeSelf)
                onQuitMenuClosed.Invoke();
            else
                onQuitMenuOpened.Invoke();
        }
    }

    #endregion

    #region Main Menu

    public delegate void OnOpenMainMenu();
    public delegate void OnCloseMainMenu();
    public static OnCloseMainMenu onCloseMainMenu;
    public static OnOpenMainMenu onOpenMainMenu;

    void OpenMainMenu()
    {
        playerUI.SetActive(true);
        onUiIsActive.Invoke();
    }

    void CloseMainMenu()
    {
        playerUI.SetActive(false);
        onUiIsActive.Invoke();
    }
    
    public void ToggleMainMenu()
    {
        if (playerUI.activeSelf)
            onCloseMainMenu.Invoke();
        else
            onOpenMainMenu.Invoke();
    }

    #endregion
    
    #region Map

    void OpenMap()
    {
        mapCanvas.SetActive(true);
        minimapCam.targetTexture = null;
        onUiIsActive.Invoke();
    }

    void CloseMap()
    {
        mapCanvas.SetActive(false);
        minimapCam.targetTexture = minimapTexture;
        onUiIsActive.Invoke();
    }
    void ToggleMap()
    {
        CloseMainMenu();
        CloseQuitMenu();
        if (mapCanvas.activeSelf)
            CloseMap();
        else
            OpenMap();
    }
    #endregion

    #region Menu Selection Buttons
    
    [InfoBox("Choose Panels To Be Added To The UI - Drag Into List In Order Wanted")]
    [Header("Menu Setup")]
    public List<GameObject> menuPanelList;
    private List<GameObject> panelList = new List<GameObject>();
    public GameObject menuButtonsContainer;
    public GameObject panelContainer;
    public GameObject menuButtonPrefab;

    public delegate void OnMenuButtonPressed(int index);
    public static OnMenuButtonPressed onMenuButtonPressed;
    void SetupMenuButtons()
    {
        if (menuPanelList.Count < 1)
        {
            return;
        }
        //for every panel add a button to container
        onMenuButtonPressed += MenuButtonPressed;
        for (int i = 0; i < menuPanelList.Count; i++)
        {
            GameObject go = Instantiate(menuButtonPrefab, menuButtonsContainer.transform);
            go.transform.SetParent(menuButtonsContainer.transform);
            go.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = menuPanelList[i].name;
            UI_MenuSelection_Button ms = go.GetComponent<UI_MenuSelection_Button>();
            ms.index = i;
        }
    }
    void SetupPanels()
    {
        //determine how many panels add to list
        if (menuPanelList.Count < 1)
        {
            return ;
        }
        for (int i = 0; i < menuPanelList.Count; i++)
        {
            GameObject go = Instantiate(menuPanelList[i],panelContainer.transform);
            go.transform.SetParent(panelContainer.transform);
            if (go.GetComponent<UI_Settings>())
            {
                settings = go.GetComponent<UI_Settings>();
            }
        }

        for (int i = 0; i < panelContainer.transform.childCount; i++)
        {
            panelList.Add(panelContainer.transform.GetChild(i).gameObject);
        }

        if (QuitMenuPrefab)
        {
            GameObject go = Instantiate(QuitMenuPrefab, QuitMenuContainer.transform);
            go.transform.SetParent(QuitMenuContainer.transform);
            playerQuitMenu = go;
        }
    }
    
    public void MenuButtonPressed(int panelIndex)
    {
        for (int i = 0; i < panelList.Count; i++)
        {
            panelList[i].SetActive(false);
        }
        panelList[panelIndex].SetActive(true);
    }

    IEnumerator SetupUI()
    {
         SetupPanels();
         yield return new WaitForSeconds(0.1f);
         SetupMenuButtons();
    }

    #endregion
    
    #region UI Prompts
    
    [Header("UI Prompts")]
    public Text infoTextCentre;
    public float textFlashTime;
    public delegate void OnPlayerNeedsInfo(string infoText);
    public static OnPlayerNeedsInfo onPlayerNeedsInfo;
    void FlashTextOnScreen(string infoText)
    {
        StartCoroutine(FlashText(infoText));
    }
    IEnumerator FlashText(string _infoString)
    {
        infoTextCentre.text = "";
        infoTextCentre.text = _infoString;
        yield return new WaitForSeconds(textFlashTime);
        infoTextCentre.text = "";
    }

    #endregion

    #region Class Methods
    void SetupEvents()
    {
        //Open Quit Menu Events
        onQuitMenuOpened += OpenQuitMenu;
        onQuitMenuOpened += CloseMap;
        onQuitMenuOpened += CloseMainMenu;
        //Close Quit Menu Events
        onQuitMenuClosed += CloseQuitMenu; 
        //Open Main Menu Events
        onOpenMainMenu += CloseMap;
        onOpenMainMenu += CloseQuitMenu;
        onOpenMainMenu += OpenMainMenu;
        //Close Main Menu Events
        onCloseMainMenu += CloseMainMenu;
        //On UI is Active Events
        onUiIsActive += ChangeCursorSetting;
        //Info Text Events
        onPlayerNeedsInfo += FlashTextOnScreen;
    }
    void ChangeCursorSetting()
    {
        if (playerQuitMenu)
        {
            _uiIsActive = playerQuitMenu.activeSelf;
        }
        if (playerUI.activeSelf || mapCanvas.activeSelf)
            _uiIsActive = true;
        else
            _uiIsActive = false;
        
        Time.timeScale = _uiIsActive ? 0 : 1;
        Cursor.visible = _uiIsActive;
        Cursor.lockState = _uiIsActive ? CursorLockMode.None : CursorLockMode.Locked;
    }

    #endregion
    
}
