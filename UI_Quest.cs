using System.Collections.Generic;
using UnityEngine;

public class UI_Quest : MonoBehaviour
{
    //viewports determine which menu option selected
    public List<GameObject> questStatusViewports;


    #region Class Methods
    public void QuestStatusChoiceButton(int index)
    {
        //disable all viewports
        for (int i = 0; i < questStatusViewports.Count; i++)
        {
            questStatusViewports[i].SetActive(false);
        }
        //enable selected viewports
        questStatusViewports[index].SetActive(true);
        //set the cursor to visible
        Cursor.visible = true;
    }
    
    #endregion
    
}
