using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManagerScript : MonoBehaviour
{
    const string DIALOGUE_FILE_PATH = "./assets/CSVs/Dialogues/Dialogue";
    const string CHOICES_FILE_PATH = "./assets/CSVs/Choices/Choices";
    const string RESULTS_FILE_PATH = "./assets/CSVs/Results/Results";
    const string FILE_EXTENSION = ".csv";
    public ChoiceManager choiceManager;
    public DialogueManager2 dialogueManager;
    public GameObject thisObject;
    private int currentDialogue;
    private int currentChoice;
    private int currentResult;
    private int FinancialScore;
    private int TeamMoralScore;
    private int MoralityScore1;
    private int MoralityScore2;
    private int MoralityScore3;
    private int MoralityScore4;
    private int MoralityScore5;
    private int MoralityScore6;
    private string currentScene;

    void Awake()
    {
        currentDialogue = 0;
        currentChoice = 0;
        DontDestroyOnLoad(thisObject);
    }

    void Update(){
        switch (currentScene){
            case "Choices":
                if (!choiceManager){
                    try{
                        choiceManager = GameObject.FindGameObjectWithTag("ChoiceManager").GetComponent<ChoiceManager>();
                        choiceManager.SetUpChoices(CHOICES_FILE_PATH + currentChoice.ToString() + FILE_EXTENSION);
                    }
                    catch{}
                }
                break;
            case "Dialogue":
                if (!dialogueManager){
                    try{
                        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager2>();
                        dialogueManager.SetUpDialogue(DIALOGUE_FILE_PATH + currentDialogue.ToString() + FILE_EXTENSION, true);
                    }
                    catch{}
                }
                break;
            case "Results":
                if (!dialogueManager){
                    try{
                        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager2>();
                        dialogueManager.SetUpDialogue(RESULTS_FILE_PATH + currentChoice.ToString() + "-" + currentResult.ToString() + FILE_EXTENSION, false);
                    }
                    catch{}
                }
                break;
            default:
                break;
        }
    }

    public void UpdateScores(int FinancialChange, int TeamMoralChange, int MoralityScore1Change, int MoralityScore2Change, int MoralityScore3Change, int MoralityScore4Change, int MoralityScore5Change, int MoralityScore6Change){
        FinancialScore += FinancialChange;
        TeamMoralScore += TeamMoralChange;
        MoralityScore1 += MoralityScore1Change;
        MoralityScore2 += MoralityScore2Change;
        MoralityScore3 += MoralityScore3Change;
        MoralityScore4 += MoralityScore4Change;
        MoralityScore5 += MoralityScore5Change;
        MoralityScore6 += MoralityScore6Change;
    }

    public void GoToDialogue(){
        SceneManager.LoadScene("ContextTest");
        currentDialogue += 1;
        currentScene = "Dialogue";
    }
    public void GoToChoices(){
        SceneManager.LoadScene("OptionTest");
        currentChoice += 1;
        currentScene = "Choices";
    }
    public void GoToResults(int resultNumber){
        SceneManager.LoadScene("ContextTest");
        currentResult = resultNumber;
        currentScene = "Results";
    }
}
