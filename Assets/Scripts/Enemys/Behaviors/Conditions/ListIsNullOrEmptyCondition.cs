using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "ListIsNullOrEmpty", story: "[Data] is null or empty", category: "Conditions", id: "0fd6c0ec2384711b944128f0f28ba3eb")]
public partial class ListIsNullOrEmptyCondition : Condition
{
    [SerializeReference] public BlackboardVariable<List<GameObject>> Data;

    public override bool IsTrue()
    {
        return Data == null || Data.Value == null || Data.Value.Count == 0;
    }
}
