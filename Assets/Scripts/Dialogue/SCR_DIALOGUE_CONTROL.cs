using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCR_DIALOGUE_CONTROL : MonoBehaviour
{
    [System.Serializable] //Exibe no inspector
    public enum Idioma
    {
        ptbr,
        eng,
        spa
    }
    public Idioma language;

    [Header("Components")] //Separa as vari�veis no inspector
    public GameObject dialogueObject; //Janela do Dialogo
    public Image profileSprite; //Imagem do Ator
    public Text speechText; //Texto do Ator dentro do Canvas
    public Text actorNameText; //Nome do Ator

    [Header("Settings")] //Separa as vari�veis no inspector
    public float typingSpeed; //Velocidade que o texto vai aparecer em segundos
    //Vari�veis de controle
    public bool isShowing; //Janela esta vis�vel
    private int index; //Index que vai percorrer as palavras
    private string[] sentences; //Array com as senten�as do Ator

    public static SCR_DIALOGUE_CONTROL instance; //Instancia a classe para usar em outros scripts

    //M�todo chamado antes de todos os m�todos start
    private void Awake()
    {
        instance = this; //instancia antes de todos os m�todos start
    }
    //Faz o efeito de mostrar letra por letra na caixa de dialogo
    IEnumerator TypeSentence()
    {
        //Para cada Letra na Senten�a executa o c�digo
        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter; //Adiciona uma letra ao campo texto do canvas
            yield return new WaitForSeconds(typingSpeed); //Define quanto tempo vai esperar para executar o foreach
        }
    }
    //Vai passar para a pr�xima senten�a
    public void NextSentence()
    {
        //Se a fala que esta na caixa estiver igual a senten�a, executa
        if(speechText.text == sentences[index])
        {
            // Se o index for menor que a quantidade de senten�as menos 1, executa
            if(index < sentences.Length - 1)
            {
                index++;
                speechText.text = ""; //Apaga o texto para receber outro
                StartCoroutine(TypeSentence()); //chama o m�todo que coloca letra por letra
            }
            else // quando termina as senten�as
            {
                speechText.text = ""; //Apaga o texto para receber outro
                index = 0; //Reinicia o index para come�ar o dialogo do inicio
                dialogueObject.SetActive(false); //Desativa a caixa de dialogo
                sentences = null; //Apaga o hist�rico do dialogo para receber outro
                isShowing = false; //Define que a janela n�o esta sendo exibida
            }
        }
    }
    //Vai mostrar o dialogo do Ator quando poss�vel
    public void Speech(string[] txt)
    {
        //Se n�o estiver mostrando a frase executa o c�digo
        if(!isShowing)
        {
            dialogueObject.SetActive(true); //Define a janela do dialogo como ativa
            sentences = txt; //Passa a lista de palavras para a lista que vai mostrar elas
            StartCoroutine(TypeSentence()); //Inicia a courotine que vai percorrer as letras das palavras
            isShowing = true; //Define que a janela esta sendo exibida
        }
    }
}
