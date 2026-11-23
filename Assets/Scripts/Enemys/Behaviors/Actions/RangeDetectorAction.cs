using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "RangeDetector", story: "Update Range [Detector] and assign [Player]", category: "Action", id: "3252ab7257623363b87810dfd7065907")]
public partial class RangeDetectorAction : Action
{
    [SerializeReference] public BlackboardVariable<RangeDetector> Detector;
    [SerializeReference] public BlackboardVariable<GameObject> Player;

    protected override Status OnUpdate()
    {
        Player.Value = Detector.Value.UpdateDetector();

        return Player.Value == null ? Status.Failure: Status.Success;
    }
}

