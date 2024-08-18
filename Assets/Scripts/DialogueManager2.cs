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
    private int typingDelay;
    private TMP_Text dialogueText;
    private TMP_Text speakerText;
    private gameManagerScript gameManager;
    private List<int> nodes;
    private List<string> speakers;
    private List<string> prompts;
    private List<int> nextNodes;
    private int currentNode;
    private bool inDialogue;
    private bool typingSpeaker;
    private int speakerTextIndex;
    private int speakerTextLength;
    private string speakerTextTyping;
    private bool typingDialogue;
    private int dialogueTextIndex;
    private int dialogueTextLength;
    private string dialogueTextTyping;
    private bool initialUpdate;
    private int typingDelayCounter;

    private void Start(){
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<gameManagerScript>();
        dialogueText = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<TMP_Text>();
        speakerText = GameObject.FindGameObjectWithTag("Speaker").GetComponent<TMP_Text>();
        typingSpeaker = false;
        initialUpdate = false;
        typingDelayCounter = 0;
    }

    private void Update(){
        if (currentNode == 1 && prompts != null && !initialUpdate){
            UpdateDialogue();
            initialUpdate = true;
        }
        typingDelayCounter++;
        if (typingSpeaker && typingDelayCounter >= typingDelay){
            speakerTextTyping = speakerTextTyping + speakers[currentNode-1][speakerTextIndex];
            speakerText.text = speakerTextTyping;
            speakerTextIndex++;
            if (speakerTextIndex == speakerTextLength){
                typingSpeaker = false;
            }
        }
        if (typingDialogue && typingDelayCounter >= typingDelay){
            dialogueTextTyping = dialogueTextTyping + prompts[currentNode-1][dialogueTextIndex];
            dialogueText.text = dialogueTextTyping;
            dialogueTextIndex++;
            typingDelayCounter = 0;
            if (dialogueTextIndex == dialogueTextLength){
                typingDialogue = false;
            }
        }
    }

    public void SetUpDialogue(string csvFilePath, bool setInDialogue, int setTypingDelay)
    {
        currentNode = 1;
        nodes = new();
        speakers = new();
        prompts = new();
        nextNodes = new();
        inDialogue = setInDialogue;
        typingDelay = setTypingDelay;
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
    }

    private void UpdateDialogue(){
        if (typingDialogue || typingSpeaker){
            typingDialogue = false;
            typingSpeaker = false;
            speakerText.text = speakers[currentNode-1];
            dialogueText.text = prompts[currentNode-1];
        }
        else{
            speakerTextIndex = 0;
            speakerTextLength = speakers[currentNode-1].Length;
            speakerTextTyping = "";
            typingSpeaker = true;
            dialogueTextIndex = 0;
            dialogueTextLength = prompts[currentNode-1].Length;
            dialogueTextTyping = "";
            typingDialogue = true;
            typingDelayCounter = 10000;
        }
    }

    public void GoToNextDialogue(){
        if (!(typingDialogue || typingSpeaker)){
            currentNode = nextNodes[currentNode-1];
        }
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
