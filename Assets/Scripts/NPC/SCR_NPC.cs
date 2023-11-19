using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_NPC : MonoBehaviour
{
    public bool randomPath;
    public float speed; //Velocidade que o NPC vai andar
    private float initialSpeed; //Velocidade Inicial do NPC
    private int index; //Variável de Controle
    public List<Transform> paths = new List<Transform>(); //Armazena uma lista de posições

    private Animator anim; //Variável que pode armazenar conteudos do Animator

    private void Start()
    {
        initialSpeed = speed;
        anim = GetComponent<Animator>(); //Recebe o componente do Animator
    }
    void Update()
    {
        PathWalk();
    }

    //Método para o NPC percorrer um caminho
    void PathWalk()
    {
        //Se o NPC estiver conversando
        if(SCR_DIALOGUE_CONTROL.instance.isShowing)
        {
            
            speed = 0f; // Zera a velocidade
            anim.SetBool("isWalking", false); //Muda a animação para idle
        }
        else //Se o NPC parar de conversar
        {

            speed = initialSpeed; //Volta a velocidade
            anim.SetBool("isWalking", true); //Muda a animação para walking
        }
        // Move Towards retorna uma posição em uma direção baseado na posição, direção e velocidade 
        transform.position = Vector2.MoveTowards(transform.position, paths[index].position, speed * Time.deltaTime);

        //Verifica se o NPC esta em cima do alvo
        if (Vector2.Distance(transform.position, paths[index].position) < 0.1f)
        {
            // Verifica se ainda há posições
            if (index < paths.Count - 1)
            {
                if (randomPath)
                {
                    index = Random.Range(0, paths.Count - 1); //Muda o index de forma aleatória no intervalo de 0 e o maximo de caminhos
                }
                else
                {
                    index++; //Incrementa o index de 1 em 1
                }
            }
            else
            {
                index = 0;
            }
        }
        // Recebe a direção subtraindo a posição do alvo com a posição do NPC
        Vector2 direction = paths[index].position - transform.position;

        // Se o X for maior que zero, ele esta indo para a direita
        if (direction.x > 0)
        {
            //Gira o sprite para a direita
            transform.eulerAngles = new Vector2(0, 0);
        }
        // Se o X for menor que zero, ele esta indo para a esquerda
        if (direction.x < 0)
        {
            //Gira o sprite para a direita
            transform.eulerAngles = new Vector2(0, 180);
        }
    }
}
