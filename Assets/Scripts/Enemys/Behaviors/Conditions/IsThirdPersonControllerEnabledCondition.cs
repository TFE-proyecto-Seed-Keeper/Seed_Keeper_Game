using StarterAssets;
using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsThirdPersonControllerEnabled", story: "Is [Target] enabled", category: "Conditions", id: "fa05f2339bec22371648921a1bda9033")]
public partial class IsThirdPersonControllerEnabledCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    public override bool IsTrue()
    {
        return Target.Value.TryGetComponent(out ThirdPersonController component) && component.enabled;
    }
}
