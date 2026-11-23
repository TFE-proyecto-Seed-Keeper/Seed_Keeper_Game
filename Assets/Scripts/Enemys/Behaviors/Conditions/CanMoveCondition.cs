using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "CanMove", story: "[Self] canMoveToTarget", category: "Conditions", id: "35bc21a0c1ef4464544dd76d9889d6ab")]
public partial class CanMoveCondition : Condition
{
    [SerializeReference] public BlackboardVariable<EnemySystem> Self;

    public override bool IsTrue()
    {
        return Self.Value.CanMove;
    }
}
