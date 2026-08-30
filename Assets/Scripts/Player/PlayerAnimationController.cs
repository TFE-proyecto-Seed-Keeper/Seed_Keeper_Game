using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimationController : MonoBehaviour, IAttacks
{
   Animator animator;
    ThirdPersonController thirdPersonController;
    public InputActionReference jumpAction;



    private void Start()
    {
        thirdPersonController = GetComponentInParent<ThirdPersonController>();
        animator = GetComponent<Animator>();

    }

    public void SetAreaAttack()
    {
        animator.SetTrigger("area");
    }

    public void SetMeleeAttack()
    {
        animator.SetTrigger("melee");
    }

    public void SetRangettack()
    {
        animator.SetTrigger("range");
    }

    public void SetJumpAttack(InputAction.CallbackContext context)
    {
        if(thirdPersonController.Grounded)
            animator.SetTrigger("jump");
    }

    private void OnEnable()
    {
        jumpAction.action.started += SetJumpAttack;
        jumpAction.action.Enable();
    }

    private void OnDisable()
    {
        jumpAction.action.started -= SetJumpAttack;
    }




}
