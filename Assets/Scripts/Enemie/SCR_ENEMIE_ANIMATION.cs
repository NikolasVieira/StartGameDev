using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_ENEMIE_ANIMATION : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerLayer;

    private SCR_PLAYER_ANIMATION player;
    private Animator anim;

    private bool isHitted;
    private float timeCount;
    [SerializeField] private float recoveryTime = 1f;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<SCR_PLAYER_ANIMATION>();

        if (isHitted)
        {
            timeCount += Time.deltaTime;
            if (timeCount >= recoveryTime)
            {
                isHitted = false;
                timeCount = 0f;
            }
        }
    }

    public void PlayAnim(int value)
    {
        anim.SetInteger("param_transition", value);
    }
   
    public void Attack()
    {
        // Overlap Circle Cria um Circulo
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, playerLayer);

        if (hit != null)
        {
            player.OnHit();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }
    public void OnHit()
    {
        anim.SetTrigger("hit");
    }

}
