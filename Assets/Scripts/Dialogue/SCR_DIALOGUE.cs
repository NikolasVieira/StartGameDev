using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Scriptable Objects/Dialogue")]
public class SCR_DIALOGUE : ScriptableObject
{
    [Header("Settings")]
    public GameObject actor; //Objeto do Ator - Utilizar no Futuro quando quiser fazer cutscenes

    [Header("Dialogue")]
    public Sprite speakerSprite; //Imagem do Ator
    public string sentence; //Sentença do Ator

    public List<Sentences> dialogues = new List<Sentences>();
}

[System.Serializable]
public class Sentences
{
    public string actorName; //Nome do Ator
    public Sprite profile; //Imagem do rosto do Ator
    public Languages sentenceLanguage; //Idioma da Sentença
}

[System.Serializable]
public class Languages
{
    public string portuguese;
    public string english;
    public string spanish;
}

#if UNITY_EDITOR
[CustomEditor(typeof(SCR_DIALOGUE))]
public class BuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SCR_DIALOGUE dialogue_settings = (SCR_DIALOGUE)target;

        Languages language_settings = new Languages();
        language_settings.portuguese = dialogue_settings.sentence;

        Sentences sentences_settings = new Sentences();
        sentences_settings.profile = dialogue_settings.speakerSprite;
        sentences_settings.sentenceLanguage = language_settings;

        if(GUILayout.Button("Create Dialogue"))
        {
            if(dialogue_settings.sentence != "")
            {
                dialogue_settings.dialogues.Add(sentences_settings);
                dialogue_settings.speakerSprite = null;
                dialogue_settings.sentence = "";
            }
        }
    }
}
#endif
