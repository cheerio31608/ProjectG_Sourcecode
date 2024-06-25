using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossStat")]
public class BossStat : ScriptableObject
{
    public string BossName;
    public List<string> BossNames;
    public float BossHP = 150;
    public float BossAttack = 15;
    public float BossDefence = 20;

    public int HpRate = 125;
    public int AttackRate = 15;
    public int DefenceRate = 10;

}
