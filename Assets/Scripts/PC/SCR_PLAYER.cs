using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_PLAYER : MonoBehaviour
{
    [SerializeField] private float speed; //Velocidade de Caminhada
    [SerializeField] private float runSpeed; //Velocidade de Corrida

    private Rigidbody2D rig; //Armazena informa��es da colis�o
    private SCR_PLAYER_ITEMS playerItems;

    private float initialSpeed; //Armazena o valor da velocidade incial
    private bool isRunning;
    private bool isRolling;
    private bool isCutting;
    private bool isDigging;
    private bool isWatering;
    private bool isSwording;
    private Vector2 direction; //Armazena um vetor x e y

    private int handlingObj;

    public int propHandlingObj { get => handlingObj; set => handlingObj = value; }

    //Propriedades para acessar fora desse script
    #region properties
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
    public bool prop_isCutting
    {
        get { return isCutting; }
        set { isCutting = value; }
    }
    public bool prop_isDigging
    {
        get { return isDigging; }
        set { isDigging = value; }
    }
    public bool prop_isWatering
    {
        get { return isWatering; }
        set { isWatering = value; }
    }
    public bool prop_isSwording
    {
        get { return isSwording; }
        set { isSwording = value; }
    }
    #endregion

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>(); //Referencia a colis�o
        playerItems = GetComponent<SCR_PLAYER_ITEMS>();
        initialSpeed = speed; //Atribui a velocidade de caminhada como a velocidade inicial
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            handlingObj = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            handlingObj = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            handlingObj = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            handlingObj = 3;
        }

        OnInput();
        OnRun();
        OnRoll();

        //Usando Ferramentas
        OnCutting();
        OnDigging();
        OnWatering();
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
    #endregion

    #region Tools
    void OnCutting()
    {
        if (handlingObj == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isCutting = true;
                speed = 0f;
            }
            if (Input.GetMouseButtonUp(0))
            {
                isCutting = false;
                speed = initialSpeed;
            }
        }
    }

    void OnDigging()
    {
        if (handlingObj == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDigging = true;
                speed = 0f;
            }
            if (Input.GetMouseButtonUp(0))
            {
                isDigging = false;
                speed = initialSpeed;
            }
        }
    }

    void OnWatering()
    {
        if (handlingObj == 2)
        {
            if (Input.GetMouseButtonDown(0) && playerItems.propCurrentWater > 0)
            {
                
                isWatering = true;
                speed = 0f;
            }
            if (Input.GetMouseButtonUp(0) || playerItems.propCurrentWater < 0)
            {
                isWatering = false;
                speed = initialSpeed;
            }
            if (isWatering)
            {
                playerItems.propCurrentWater -= 0.01f;
            }
        }
    }

    void OnSword()
    {
        if (handlingObj == 3)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isSwording = true;
                speed = 0f;
            }
            if (Input.GetMouseButtonUp(0))
            {
                isSwording = false;
                speed = initialSpeed;
            }
        }
    }
    #endregion
}
