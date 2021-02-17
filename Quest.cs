using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Taunt Games/Create New Quest")]
public class Quest : ScriptableObject
{
   public enum QuestType
   {
      Main,
      Side,
      Misc
   }

   public QuestType questType;
   public string QuestName = "woohaytyas";
   [TextArea] public string questDescription;
   public Reward questReward;
   public List<Objective> objectives;
}
