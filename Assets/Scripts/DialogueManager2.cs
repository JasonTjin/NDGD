using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class DialogueManager2 : MonoBehaviour
{
    private TMP_Text dialogueText;
    private TMP_Text speakerText;
    private gameManagerScript gameManager;
    public List<int> nodes;
    public List<string> speakers;
    public List<string> prompts;
    public List<int> nextNodes;
    public int currentNode;
    public bool inDialogue;

    private void Start(){
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<gameManagerScript>();
        dialogueText = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<TMP_Text>();
        speakerText = GameObject.FindGameObjectWithTag("Speaker").GetComponent<TMP_Text>();
    }

    private void Update(){
        if (currentNode == 1 && prompts != null){
            UpdateDialogue();
        }
    }

    public void SetUpDialogue(string csvFilePath, bool setInDialogue)
    {
        currentNode = 1;
        nodes = new();
        speakers = new();
        prompts = new();
        nextNodes = new();
        inDialogue = setInDialogue;
        var reader = new StreamReader(csvFilePath);
        reader.ReadLine(); //skip the titles
        while (!reader.EndOfStream)
        {
            var values = reader.ReadLine().Split(",");
            nodes.Add(Convert.ToInt32(values[0]));
            speakers.Add(values[1].Replace('|', ','));
            prompts.Add(values[2].Replace('|', ','));
            nextNodes.Add(Convert.ToInt32(values[3]));
        }
        UpdateDialogue();
    }

    private void UpdateDialogue(){
        speakerText.text = speakers[currentNode-1];
        dialogueText.text = prompts[currentNode-1];
    }

    public void GoToNextDialogue(){
        currentNode = nextNodes[currentNode-1];
        if (currentNode != 0){
            UpdateDialogue();
        }
        else{
            if(inDialogue){
                gameManager.GoToChoices();
            }
            else{
                gameManager.GoToDialogue();
            }
        }
    }    
}
