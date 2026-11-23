using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "CanAttack", story: "[Self] can attack", category: "Conditions", id: "c060d2c1618fda2a0240932b8f7cec59")]
public partial class CanAttackCondition : Condition
{
    [SerializeReference] public BlackboardVariable<EnemySystem> Self;

    public override bool IsTrue()
    {
        return Self.Value.CanAttack;
    }
}
