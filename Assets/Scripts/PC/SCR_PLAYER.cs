using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_PLAYER : MonoBehaviour
{
    [SerializeField] private float speed; //Velocidade de Caminhada
    [SerializeField] private float runSpeed; //Velocidade de Corrida

    private Rigidbody2D rig; //Armazena informa��es da colis�o

    private float initialSpeed; //Armazena o valor da velocidade incial
    private bool isRunning; //Armazena se esta correndo ou n�o
    private bool isRolling; //Armazena se esta esquivando ou n�o
    private bool isSwording; //Armazena se esta usando a espada ou n�o
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
        rig = GetComponent<Rigidbody2D>(); //Referencia a colis�o
        initialSpeed = speed; //Atribui a velocidade de caminhada como a velocidade inicial
    }

    private void Update()
    {
        OnInput();
        OnRun();
        OnRoll();
        OnSword();
    }

    //M�todo que atualiza de forma fixa
    private void FixedUpdate()
    {
        OnMove();
    }

    #region Movement
    void OnInput()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); //Define a dire��o incrementando/decrementando o x caso aperte os bot�es horizontais, e a mesmca coisa com o y quando aperta os bot�es verticais
    }
    void OnMove()
    {
        rig.MovePosition(rig.position + direction * speed * Time.fixedDeltaTime); //Movimenta o personagem pegando a posi��o atual mais a dire��o multiplicado pela velocidade e pelo delta time
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
            isRunning = false; //N�o esta correndo
        }
    }
    void OnRoll()
    {
        //Se apertar bot�o direito do mouse (1)
        if (Input.GetMouseButtonDown(1))
        {
            isRolling = true; //Esta esquivando
        }
        //Se soltar bot�o direito do mouse (1)
        if (Input.GetMouseButtonUp(1))
        {
            isRolling = false; //N�o esta esquivando
        }
    }

    void OnSword()
    {
        //Se apertar bot�o direito do mouse (1)
        if (Input.GetMouseButtonDown(0))
        {
            isSwording = true; //Esta esquivando
        }
        //Se soltar bot�o direito do mouse (1)
        if (Input.GetMouseButtonUp(0))
        {
            isSwording = false; //N�o esta esquivando
        }
    }
    #endregion
}
