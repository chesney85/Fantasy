using System.Collections.Generic;
using UnityEngine;

public class QuestLog : MonoBehaviour
{
    private string questTitle;
    [TextArea]
    private string questDescription;
    private List<QuestObjective> objectives;
    private bool isactive;
    
}
