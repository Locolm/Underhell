using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_01_run : StateMachineBehaviour
{
    public float speed = 2.5f;
    public float attackRange = 3f;

    Transform player;
    Rigidbody2D rb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
        
        if (Vector2.Distance(player.position,rb.position)<= attackRange)
        {
            int randomAttack = Random.Range(1, 4); // G�n�re un nombre al�atoire entre 1 et 3 (inclus)

            switch (randomAttack)
            {
                case 1:
                    animator.SetTrigger("Attack");
                    break;
                case 2:
                    animator.SetTrigger("Attack2");
                    break;
                case 3:
                    animator.SetTrigger("Attack3");
                    break;
                default:
                    animator.SetTrigger("Attack1");
                    break;
            }
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Attack2");
        animator.ResetTrigger("Attack3");
    }

}
