using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerStat")]
public class PlayerStat : ScriptableObject
{
    [Header("Stat")]
    public int PlayerHP = 100;
    public int PlayerAttack = 40;
    public int PlayerSpeed = 1;
    public float CriticalRate = 0.3f;
    public float CriticalDamage = 1.1f;
    public int PlayerDefense = 10;  
}
