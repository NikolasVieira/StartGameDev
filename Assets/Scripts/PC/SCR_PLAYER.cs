using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_PLAYER : MonoBehaviour
{
    [SerializeField] private float speed; //Velocidade de Caminhada
    [SerializeField] private float runSpeed; //Velocidade de Corrida

    private Rigidbody2D rig; //Armazena informações da colisão

    private float initialSpeed; //Armazena o valor da velocidade incial
    private bool isRunning; //Armazena se esta correndo ou não
    private bool isRolling; //Armazena se esta esquivando ou não
    private bool isSwording; //Armazena se esta usando a espada ou não
    private Vector2 direction; //Armazena um vetor x e y

    //Propriedades para acessar fora desse script
    public Vector2 prop_direction
    {
        get { return direction; }
        set { direction = value; }
    }
    public bool prop_isRunning
    {
        get { return isRunning; }
        set { isRunning = value; }
    }
    public bool prop_isRolling
    {
        get { return isRolling; }
        set { isRolling = value; }
    }
    public bool prop_isSwording
    {
        get { return isSwording; }
        set { isSwording = value; }
    }

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>(); //Referencia a colisão
        initialSpeed = speed; //Atribui a velocidade de caminhada como a velocidade inicial
    }

    private void Update()
    {
        OnInput();
        OnRun();
        OnRoll();
        OnSword();
    }

    //Método que atualiza de forma fixa
    private void FixedUpdate()
    {
        OnMove();
    }

    #region Movement
    void OnInput()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); //Define a direção incrementando/decrementando o x caso aperte os botões horizontais, e a mesmca coisa com o y quando aperta os botões verticais
    }
    void OnMove()
    {
        rig.MovePosition(rig.position + direction * speed * Time.fixedDeltaTime); //Movimenta o personagem pegando a posição atual mais a direção multiplicado pela velocidade e pelo delta time
    }
    void OnRun()
    {
        //Se apertar o Shift Esquerdo
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = runSpeed; //Define a velocidade como a de corrida
            isRunning = true; //Esta correndo
        }
        //Se soltar o Shift Esquerdo
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = initialSpeed; //Define a velocidade como a inicial
            isRunning = false; //Não esta correndo
        }
    }
    void OnRoll()
    {
        //Se apertar botão direito do mouse (1)
        if (Input.GetMouseButtonDown(1))
        {
            isRolling = true; //Esta esquivando
        }
        //Se soltar botão direito do mouse (1)
        if (Input.GetMouseButtonUp(1))
        {
            isRolling = false; //Não esta esquivando
        }
    }

    void OnSword()
    {
        //Se apertar botão direito do mouse (1)
        if (Input.GetMouseButtonDown(0))
        {
            isSwording = true; //Esta esquivando
        }
        //Se soltar botão direito do mouse (1)
        if (Input.GetMouseButtonUp(0))
        {
            isSwording = false; //Não esta esquivando
        }
    }
    #endregion
}
