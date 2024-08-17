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
    public TMP_Text dialogueText;
    public TMP_Text speakerText;
    private gameManagerScript gameManager;
    private List<int> nodes = new();
    private List<string> speakers = new();
    private List<string> prompts = new();
    private List<int> nextNodes = new();
    private int currentNode = 1;
    private bool inDialogue;

    private void Start(){
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<gameManagerScript>();
    }

    public void SetUpDialogue(string csvFilePath, bool setInDialogue)
    {
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
