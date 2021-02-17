using System.Collections;
using TauntGames;
using UnityEngine;
using UnityEngine.UI;

public class UI_QuestButton : MonoBehaviour
{
    #region Class Variables
    //------public-------
    //this is set on instantiation from UI-QuesTypeButton --> Expand(void CreateListItem())
    public Quest quest;
    //this is set on instantiation from UI-QuesTypeButton --> Expand(void CreateListItem())
    public GameObject objectiveButton;
    //this is set on instantiation from UI-QuesTypeButton --> Expand(void CreateListItem())
    public GameObject rewardButton;
    //this is set on instantiation from UI-QuesTypeButton --> Expand(void CreateListItem())
    public Transform objectiveContent;
    //this is set on instantiation from UI-QuesTypeButton --> Expand(void CreateListItem())
    public Transform rewardContent;
    //this is set on instantiation from UI-QuesTypeButton --> Expand(void CreateListItem())
    public Text questDescription;
    //this is set in inspector as this does not change
    public Sprite incomplete;
    //this is set in inspector as this does not change
    public Sprite complete;
    //this is set in inspector as this does not change
    public Sprite gold;
    //this is set in inspector as this does not change
    public Sprite xp;
    
    //-----private-----
    //used to stop repeated method invokes while deleting UI elements
    private bool _isDeletingElements;
    
    #endregion
    
    #region Show Quest Details
    //Button Click Method
    public void ShowQuestDetails()
    {
        //if deleting UI elements cancel this method call
        if (_isDeletingElements)
            return;
        StartCoroutine(nameof(ClearContent));
        
    }

    IEnumerator ClearContent()
    {
        yield return StartCoroutine(DeleteUIElements(objectiveContent, -1));
        yield return StartCoroutine(DeleteUIElements(rewardContent, -1));
        yield return StartCoroutine(nameof(ShowInfo));
    }

    IEnumerator ShowInfo()
    {
        yield return new WaitForEndOfFrame();
        //if not continue and show objective and rewards info
        ShowQuestInfo();
        ShowRewardsInfo();
    }
    
    //show quest information
    void ShowQuestInfo()
    {
        //Update UI
        RepaintViewPort(objectiveContent,true);
        //change quest description text to match selected quest
        questDescription.text = quest.questDescription;
        //for each quest objective in quest object
        for (int i = 0; i < quest.objectives.Count; i++)
        {
            //create a new button object to hold objective
            GameObject objective = Instantiate(objectiveButton, objectiveContent);
            //set the button parent to the objective content container
            objective.transform.SetParent(objectiveContent);
            //set Text element t as the text component attached to button
            Text t = objective.transform.GetChild(0).GetComponent<Text>();
            //set objBut to the UI_ObjectiveButton component attached to button
            UI_ObjectiveButton objBut = objective.GetComponent<UI_ObjectiveButton>();
            //set objBut objective equal to the quest objective from the list inside quest
            objBut.objective = quest.objectives[i];
            //set t equal to the title of the objective
            t.text = objBut.objective.objectiveTitle;
            //if the objective is complete
            if (objBut.objective.isComplete)
                //set the objective icon to complete
                objective.transform.GetChild(1).GetComponent<Image>().sprite = complete;
            //if not complete
            else
                //set icon to incomplete
                objective.transform.GetChild(1).GetComponent<Image>().sprite = incomplete;
            
        }
        //Update UI
        RepaintViewPort(objectiveContent,false);
    }
    //show quest reward info
    void ShowRewardsInfo()
    {
        //if UI elements are still deleting, cancel this method call
        if (_isDeletingElements)
            return;
        //if there is no quest reward cancel the rest of this method call
        if (!quest.questReward)
            return;
        //if there is an experience reward greater than zero
        if (quest.questReward.experienceReward > 0)
        {
            //create a button for the experience to be shown
            GameObject xpReward = Instantiate(rewardButton, rewardContent);
            //set xp parent to reward content container
            xpReward.transform.SetParent(rewardContent);
            //set xp image to pre set experience icon
            xpReward.transform.GetChild(1).GetComponent<Image>().sprite = xp;
            //set the text of the button to match the experience of the reward
            xpReward.transform.GetChild(0).GetComponent<Text>().text = "" +
                                                                       quest.questReward.experienceReward;
        }
        //if there is an experience reward greater than zero
        if (quest.questReward.goldReward > 0)
        {
            //create a button for the gold to be shown
            GameObject goldReward = Instantiate(rewardButton, rewardContent);
            //set gold reward parent to reward content container
            goldReward.transform.SetParent(rewardContent);
            //set gold image to pre set experience icon
            goldReward.transform.GetChild(1).GetComponent<Image>().sprite = gold;
            //set the text of the button to match the gold amount of the reward
            goldReward.transform.GetChild(0).GetComponent<Text>().text = "" +
                                                                         quest.questReward.goldReward;
        }
        //for each item reward
        for (int i = 0; i < quest.questReward.itemRewards.Count; i++)
        {
            //if there are no items then cancel method call
            if (quest.questReward.itemRewards.Count < 1)
                return;
            //if there is an item reward
            if (quest.questReward.itemRewards.Count > 0)
            {
                //create a button for the item to be shown
                GameObject itemReward = Instantiate(rewardButton, rewardContent);
                //set item reward parent to reward content container
                itemReward.transform.SetParent(rewardContent);
                //create a new item and set it to match current selected reward item from list in quest
                Item item = quest.questReward.itemRewards[i].item;
                //set item icon image to match reward item icon image
                itemReward.transform.GetChild(1).GetComponent<Image>().sprite = item.itemIcon;
                //set the text of the button to equal the name of the item reward
                itemReward.transform.GetChild(0).GetComponent<Text>().text = "" +
                                                                             item.itemName;
            }
        }
        RepaintViewPort(rewardContent,false);
    }
    
    private IEnumerator DeleteUIElements(Transform t, int indexToStopAt)
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
        if (_viewPort.childCount < 1)
        {
            return;
        }

        if (isOpen)
        {
            RectTransform rect = _viewPort.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, 0f);
        }
        else
        {
            float height = 0;
            for (int j = 0; j < _viewPort.childCount; j++)
            {
                height += 30f;
            }

            RectTransform rect = _viewPort.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, height);
        }
    }

    #endregion
}
