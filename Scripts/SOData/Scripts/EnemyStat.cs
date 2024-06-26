using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStat")]
public class EnemyStat : ScriptableObject
{
    public string EnemyName;
    public List<string> EnemyNames;
    public int a = 10;
    public float EnemyHP = 50;
    public float EnemyAttack = 2;
    public float EnemyDefence = 5;

    public int HpRate = 10;
    public int AttackRate = 1;
    public int DefenceRate = 1;
}
