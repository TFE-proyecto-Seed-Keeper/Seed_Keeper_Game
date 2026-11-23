using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "SightCheck", story: "Check [Player] with Sight [Detector]", category: "Conditions", id: "8c5c2916fcc04906dca6d2edab382230")]
public partial class SightCheckCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Player;
    [SerializeReference] public BlackboardVariable<SightDetector> Detector;

    public override bool IsTrue()
    {
        return Detector.Value.PerformDetection(Player.Value) != null;
    }
}
