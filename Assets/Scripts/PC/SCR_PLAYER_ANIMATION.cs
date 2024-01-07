using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_PLAYER_ANIMATION : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask enemyLayer;

    private SCR_PLAYER player; //Variável que pode armazenar conteudos do Script Player
    private Animator anim; //Variável que pode armazenar conteudos do Animator

    private bool isHitted;
    private float timeCount;
    [SerializeField] private float recoveryTime = 1f;

    void Start()
    {
        player = GetComponent<SCR_PLAYER>(); //Recebe o componente do Script Player
        anim = GetComponent<Animator>(); //Recebe o componente do Animator
    }

    void Update()
    {
        OnMove();
        OnTools();
        OnRun();

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

    void OnMove()
    {
        //Se a direção do player esta com o valor maior que 0
        if (player.prop_direction.sqrMagnitude > 0)
        {
            if(player.prop_isRolling)
            {
                anim.SetTrigger("isRolling"); //Marca o gatilho isRolling no Animator
            }
            else
            {
                anim.SetInteger("param_transition", 1); //Muda o parâmetro de transição para 1 (Walking)
            }

            if (player.prop_isSwording)
            {
                anim.SetTrigger("isSwording"); //Marca o gatilho isRolling no Animator
            }
            else
            {
                anim.SetInteger("param_transition", 1); //Muda o parâmetro de transição para 1 (Walking)
            }
        }
        else
        {
            anim.SetInteger("param_transition", 0);//Muda o parâmetro de transição para 0 (Idle)
        }

        //Se o X for maior que 0
        if (player.prop_direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0); //Mantem o angulo do sprite como o padrão
        }
        //Se o X for menor que 0
        if (player.prop_direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180); //Inverte o angulo do sprite
        }

    }
    void OnRun()
    {
        if (player.prop_isRunning)
        {
            anim.SetInteger("param_transition", 2);//Muda o parâmetro de transição para 2 (Run)
        }
    }
    
    void OnTools()
    {

        if (player.prop_isCutting)
        {
            anim.SetInteger("param_transition", 3);
        }

        if (player.prop_isDigging)
        {
            anim.SetInteger("param_transition", 4);
        }

        if (player.prop_isWatering)
        {
            anim.SetInteger("param_transition", 5);
        }
    }

    public void OnHit()
    {
        if (!isHitted)
        {
            anim.SetTrigger("isHitted");
            isHitted = true;
        }
    }

    public void OnAttack()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, enemyLayer);

        if (hit != null)
        {
            hit.GetComponentInChildren<SCR_ENEMIE_ANIMATION>().OnHit();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }
}
