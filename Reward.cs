using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Taunt Games/Rewards/Quest Reward")]
public class Reward : ScriptableObject
{
    public int goldReward;
    public float experienceReward;
    public List<ItemReward> itemRewards;
    
    
}
