using UnityEngine;

public class Walk : StateMachineBehaviour
{
   private CharacterMovement CharacterMovement;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Testing.Instance.GoToNextPosition();
        //CharacterMovement = animator.GetComponent<CharacterMovement>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetBool("IsLastPointReached")==false)
        {
            Testing.Instance.GoToNextPosition();
        }
        
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //CharacterMovement.Movement();
    }

}
