using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SCR_ENEMIE : MonoBehaviour
{
    //Importa o script NavMeshAgent
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private SCR_ENEMIE_ANIMATION animControl;
    //Importa o script SCR_PLAYER
    private SCR_PLAYER player;

    void Start()
    {
        //Procura um objeto com o script SCR_PLAYER
        player = FindObjectOfType<SCR_PLAYER>();
        

        //Não atualiza a rotação
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        //Define o alvo como a posição do player
        agent.SetDestination(player.transform.position);

        //Se a distancia entre o objeto e o player for menor ou igual ao valor da distância de parada
        if(Vector2.Distance(transform.position, player.transform.position) <= agent.stoppingDistance)
        {
            animControl.PlayAnim(2);
        }
        else
        {
            animControl.PlayAnim(1);
        }

        float posX = player.transform.position.x - transform.position.x;
        if (posX > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }
}
