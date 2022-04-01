using System.Collections;
using System.Collections.Generic;
using UnityEngine;





[CreateAssetMenu(fileName ="Reward",menuName ="Task/Reward")]
public class SO_BaseReward : ScriptableObject
{
    public int ID;
    public GameObject reward;
    public int quanity;
}
