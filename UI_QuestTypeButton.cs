using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_QuestTypeButton : MonoBehaviour
{
    #region Class Variables

    //------Private------
    //GameObject the button component is attached to
    private GameObject _button;
    //The icon image attached to button
    private Image _buttonImage;
    //bool is active when removing elements from viewports
    private bool _isDeletingElements;

    //-------Public-------
    //icon image to show list is expanded
    public Sprite expanded;
    //icon for when list is closed
    public Sprite closed;
    //Prefab to spawn into viewports
    public GameObject buttonPrefab;
    //viewport containing spawned objects
    public Transform _viewPort;
    //text box for quest description
    public Text questDescription;
    //content panel to spawn objectives
    public Transform objectiveContent;
    //content panel to spawn reward content
    public Transform rewardContent;
    //dropdown choice to determine quest type
    public enum QuestType { Main,Side,Misc }
    public QuestType questType; 
    //dropdown choice to determine quest status
    public enum QuestStatus { Active, Completed, Failed }
    public QuestStatus questStatus;

    #endregion

    #region Monobehaviour
    //runs when object gets enabled
    private void OnEnable()
    {
        //button object is the game object containing button component
        _button = gameObject;
        //the image attached to button game object
        _buttonImage = _button.transform.GetChild(1).GetComponent<Image>();
        //viewport is the parent of the button object
        _viewPort = _button.transform.parent;
    }
    #endregion

    #region Button Press
    //This is the button click event
    public void QuestTypeButtonPressed()
    {
        //this determines if list is expanded or not
        if (_buttonImage.sprite == closed && !_isDeletingElements)
        {
            //button image to expanded
            _buttonImage.sprite = expanded;
            //populate the quest list
            ExpandList();
            //update the UI
            RepaintViewPort(_viewPort,false);
        }
        else
        {
            //button image to closed
            _buttonImage.sprite = closed;
            //delete elements in viewport
            CloseList();
        }
    }

    #endregion

    #region Expand/Close Quests
    void ExpandList()
    {
        //selection statement based on quest status
        switch (questStatus)
        {
            //if quest status is active
            case QuestStatus.Active:
                //pass quest type into selection statement
                switch (questType)
                {
                    //if main quest
                    case QuestType.Main:
                        //create list of active main quests
                        CreateList(QuestManager.instance.activeQuests, Quest.QuestType.Main);
                        //update UI
                        RepaintViewPort(_viewPort,false);
                        break;
                    case QuestType.Side:
                        //create list of active side quests
                        CreateList(QuestManager.instance.activeQuests, Quest.QuestType.Side);
                        //update UI
                        RepaintViewPort(_viewPort,false);
                        break;
                    case QuestType.Misc:
                        //create list of active misc quests
                        CreateList(QuestManager.instance.activeQuests, Quest.QuestType.Misc);
                        //update UI
                        RepaintViewPort(_viewPort,false);
                        break;
                    default:
                        //if none of the above quest type exist, default to error log
                        Debug.Log("Error: Type Didn't Exist");
                        break;
                }
                break;
            //if quest status is Complete
            case QuestStatus.Completed:
                //pass quest type into selection statement
                switch (questType)
                {
                    case QuestType.Main:
                        //create list of complete main quests
                        CreateList(QuestManager.instance.completedQuests, Quest.QuestType.Main);
                        //update UI
                        RepaintViewPort(_viewPort,false);
                        break;
                    case QuestType.Side:
                        //create list of complete side quests
                        CreateList(QuestManager.instance.completedQuests, Quest.QuestType.Side);
                        //update UI
                        RepaintViewPort(_viewPort,false);
                        break;
                    case QuestType.Misc:
                        //create list of complete misc quests
                        CreateList(QuestManager.instance.completedQuests, Quest.QuestType.Misc);
                        //update UI
                        RepaintViewPort(_viewPort,false);
                        break;
                    default:
                        //if none of the above quest type exist, default to error log
                        Debug.Log("Error: Type Didn't Exist");
                        break;
                }
                break;
            //if quest status is Failed
            case QuestStatus.Failed:
                switch (questType)
                {
                    case QuestType.Main:
                        //create list of failed main quests
                        CreateList(QuestManager.instance.failedQuests, Quest.QuestType.Main);
                        //update UI
                        RepaintViewPort(_viewPort,false);
                        break;
                    case QuestType.Side:
                        //create list of failed side quests
                        CreateList(QuestManager.instance.failedQuests, Quest.QuestType.Side);
                        //update UI
                        RepaintViewPort(_viewPort,false);
                        break;
                    case QuestType.Misc:
                        //create list of failed misc quests
                        CreateList(QuestManager.instance.failedQuests, Quest.QuestType.Misc);
                        //update UI
                        RepaintViewPort(_viewPort,false);
                        break;
                    default:
                        //if none of the above quest type exist, default to error log
                        Debug.Log("Error: Type Didn't Exist");
                        break;
                }
                break;
            default:
                //if none of the above status exist, default to error log
                Debug.Log("Error: quest Status Didn't Exist");
                break;
        }
        //method only exists inside Expand method
        void CreateListItem(List<Quest> questList, int i)
        {
            //create a new button
            GameObject button = Instantiate(buttonPrefab, _viewPort);
            //set button parent to viewport
            button.transform.SetParent(_viewPort);
            //get button text from child object --> set it to quest name
            button.transform.GetChild(0).GetComponent<Text>().text =
                questList[i].QuestName;
            //get UI_QuestButton component from button
            UI_QuestButton q = button.GetComponent<UI_QuestButton>();
            //set quest list
            q.quest = questList[i];
            //set quest description
            q.questDescription = questDescription;
            //set quest objective content container
            q.objectiveContent = objectiveContent;
            //set quest rewards content container
            q.rewardContent = rewardContent;
        }
        //method only exists inside expand method
        void CreateList(List<Quest> questList, Quest.QuestType questType)
        {
            for (int i = 0; i < questList.Count; i++)
            {
                //for each quest run if statement 
                if (questList[i].questType == questType)
                    //if quest type from list matches quest parameter, add to viewport
                    CreateListItem(questList, i);
            }
        }
    }

    void CloseList()
    {
        //start co-routine to delete ui elements --> pass in viewport and index for, for loop
        StartCoroutine(DeleteUIElements(_viewPort, 0));
        //update UI
        RepaintViewPort(_viewPort,true);
    }

    #endregion

    #region Helper Methods

    IEnumerator DeleteUIElements(Transform t, int indexToStopAt)
    {
        //update bool to stop repeated method use
        _isDeletingElements = true;
        //set count to number of child elements
        int count = t.childCount;
        //set index to number of child elements -1 to correct out of bounds
        int index = count - 1;
        //set Rect Transform from component attached to parameter t
        RectTransform rect = t.GetComponent<RectTransform>();
        //set Rect size to same width as before, but height is equal to 30 (i.e. height of one button)
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, 30);
        //while index is greater than parameter to stop at
        while (index > indexToStopAt)
        {
            //delete child objects of parameter t (Transform)
            DestroyImmediate(t.GetChild(index).gameObject);
            //decrement index
            index--;
            //return to start of loop if if condition not met
            yield return null;
        }
        //set cursor visible on screen
        Cursor.visible = true;
        //change bool to no deleting elements allowing this to be run again
        _isDeletingElements = false;
    }

    private void RepaintViewPort(Transform _viewPort, bool isOpen)
    {
        //if there are no child object then cancel this method call
        if (_viewPort.childCount < 1)
            return;
        //if the list is already open
        if (isOpen)
        {
            //set the height of the Rect Transform to the height of one button
            RectTransform rect = _viewPort.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, 30f);
        }
        else
        {
            //if there are more than 0 child objects
            float height = 0;
            
            for (int j = 0; j < _viewPort.childCount; j++)
            {
                //add 30 height for each child objects
                height += 30f;
            }
            //set the height of Rect Transform to height of all child objects
            RectTransform rect = _viewPort.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, height);
        }
    }

    #endregion
}