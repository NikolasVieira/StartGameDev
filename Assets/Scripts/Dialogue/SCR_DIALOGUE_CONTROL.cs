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

    [Header("Components")] //Separa as variáveis no inspector
    public GameObject dialogueObject; //Janela do Dialogo
    public Image profileSprite; //Imagem do Ator
    public Text speechText; //Texto do Ator dentro do Canvas
    public Text actorNameText; //Nome do Ator

    [Header("Settings")] //Separa as variáveis no inspector
    public float typingSpeed; //Velocidade que o texto vai aparecer em segundos
    //Variáveis de controle
    public bool isShowing; //Janela esta visível
    private int index; //Index que vai percorrer as palavras
    private string[] sentences; //Array com as sentenças do Ator

    public static SCR_DIALOGUE_CONTROL instance; //Instancia a classe para usar em outros scripts

    //Método chamado antes de todos os métodos start
    private void Awake()
    {
        instance = this; //instancia antes de todos os métodos start
    }
    //Faz o efeito de mostrar letra por letra na caixa de dialogo
    IEnumerator TypeSentence()
    {
        //Para cada Letra na Sentença executa o código
        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter; //Adiciona uma letra ao campo texto do canvas
            yield return new WaitForSeconds(typingSpeed); //Define quanto tempo vai esperar para executar o foreach
        }
    }
    //Vai passar para a próxima sentença
    public void NextSentence()
    {
        //Se a fala que esta na caixa estiver igual a sentença, executa
        if(speechText.text == sentences[index])
        {
            // Se o index for menor que a quantidade de sentenças menos 1, executa
            if(index < sentences.Length - 1)
            {
                index++;
                speechText.text = ""; //Apaga o texto para receber outro
                StartCoroutine(TypeSentence()); //chama o método que coloca letra por letra
            }
            else // quando termina as sentenças
            {
                speechText.text = ""; //Apaga o texto para receber outro
                index = 0; //Reinicia o index para começar o dialogo do inicio
                dialogueObject.SetActive(false); //Desativa a caixa de dialogo
                sentences = null; //Apaga o histórico do dialogo para receber outro
                isShowing = false; //Define que a janela não esta sendo exibida
            }
        }
    }
    //Vai mostrar o dialogo do Ator quando possível
    public void Speech(string[] txt)
    {
        //Se não estiver mostrando a frase executa o código
        if(!isShowing)
        {
            dialogueObject.SetActive(true); //Define a janela do dialogo como ativa
            sentences = txt; //Passa a lista de palavras para a lista que vai mostrar elas
            StartCoroutine(TypeSentence()); //Inicia a courotine que vai percorrer as letras das palavras
            isShowing = true; //Define que a janela esta sendo exibida
        }
    }
}
