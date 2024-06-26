using System;
using UnityEngine;

[Serializable]
public class PlayerAnimationData
{
    [field: SerializeField] public string idleParameterName = "Idle";
    [field: SerializeField] public string attackParameterName = "Attack";
    [field: SerializeField] public string dieParameterName = "Die";

    public int IdleParameterHash { get; private set; }
    public int AttackParameterHash { get; private set; }
    public int DieParameterHash { get; private set; }

    public void Initialize()
    {
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        AttackParameterHash = Animator.StringToHash(attackParameterName);
        DieParameterHash = Animator.StringToHash(dieParameterName);
    }
}
