using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AnimationTrigger", story: "Play [animation] in [animator]", category: "Action", id: "3252ab7257623363b87810dfd7065908")]
public partial class AnimationTriggerAction : Action
{
    [SerializeReference] public BlackboardVariable<string> Animation;
    [SerializeReference] public BlackboardVariable<Animator> Animator;

    private float clipTime;
    private float timer;

    protected override Status OnStart()
    {
        timer = 0f;
        Animator.Value.SetTrigger(Animation.Value);

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        timer += Time.deltaTime;
        clipTime = Animator.Value.GetCurrentAnimatorStateInfo(0).length;

        return timer >= clipTime ? Status.Success: Status.Running;
    }
}

