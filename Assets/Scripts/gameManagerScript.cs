using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManagerScript : MonoBehaviour
{
    const string DIALOGUE_FILE_PATH = "./assets/CSVs/Dialogues/Dialogue";
    const string CHOICES_FILE_PATH = "./assets/CSVs/Choices/Choices";
    const string RESULTS_FILE_PATH = "./assets/CSVs/Results/Results";
    const string FILE_EXTENSION = ".csv";
    private ChoiceManager choiceManager;
    private DialogueManager2 dialogueManager;
    private int currentDialogue;
    private int currentChoice;
    private int FinancialScore;
    private int TeamMoralScore;
    private int MoralityScore1;
    private int MoralityScore2;
    private int MoralityScore3;
    private int MoralityScore4;
    private int MoralityScore5;
    private int MoralityScore6;

    void Awake()
    {
        currentDialogue = 0;
        currentChoice = 0;
    }
    void Start()
    {
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("GameManager"));
        GoToDialogue();
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
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager2>();
        currentDialogue += 1;
        dialogueManager.SetUpDialogue(DIALOGUE_FILE_PATH + currentDialogue.ToString() + FILE_EXTENSION, true);
    }
    public void GoToChoices(){
        SceneManager.LoadScene("OptionTest");
        choiceManager = GameObject.FindGameObjectWithTag("ChoiceManager").GetComponent<ChoiceManager>();
        currentChoice += 1;
        choiceManager.SetUpChoices(CHOICES_FILE_PATH + currentChoice.ToString() + FILE_EXTENSION);
    }
    public void GoToResults(int resultNumber){
        SceneManager.LoadScene("ContextTest");
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager2>();
        dialogueManager.SetUpDialogue(RESULTS_FILE_PATH + currentChoice.ToString() + "-" + resultNumber.ToString() + FILE_EXTENSION, false);
    }
}
