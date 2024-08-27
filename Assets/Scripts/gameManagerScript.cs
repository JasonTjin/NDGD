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
    const int TYPING_DELAY = 10; //Controlls the delay between each letter showing up
    public ChoiceManager choiceManager;
    public DialogueManager2 dialogueManager;
    public GameObject thisObject; //Used to make this object not destroy on load
    private int currentDialogue; //The number of the current dialogue file
    private int currentChoice; //The number of the current choice file
    private int currentResult; //The number of the current result from the current choice
    private int FinancialScore; 
    private int TeamMoralScore;
    private int MoralityScore1; //The primacy of the public interest
    private int MoralityScore2; //The enhancement of quality of life
    private int MoralityScore3; //Honesty
    private int MoralityScore4; //Compentancy
    private int MoralityScore5; //Professional development
    private int MoralityScore6; //Professionalism
    private string currentScene; //The name of the current scene

    void Awake()
    {
        currentDialogue = 0;
        currentChoice = 0;
        DontDestroyOnLoad(thisObject); //Stops this object from being unloaded 
    }

    void Update(){ 
        //This makes sure that the choice manager or dialogue manager are correctly stored, then sets them up for when a scene change happens
        switch (currentScene){
            case "Choices":
                if (!choiceManager){
                    try{
                        choiceManager = GameObject.FindGameObjectWithTag("ChoiceManager").GetComponent<ChoiceManager>();
                        choiceManager.SetUpChoices(CHOICES_FILE_PATH + currentChoice.ToString() + FILE_EXTENSION, TYPING_DELAY);
                    }
                    catch{}
                }
                break;
            case "Dialogue":
                if (!dialogueManager){
                    try{
                        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager2>();
                        dialogueManager.SetUpDialogue(DIALOGUE_FILE_PATH + currentDialogue.ToString() + FILE_EXTENSION, false, TYPING_DELAY);
                    }
                    catch{}
                }
                break;
            case "Results":
                if (!dialogueManager){
                    try{
                        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager2>();
                        dialogueManager.SetUpDialogue(RESULTS_FILE_PATH + currentChoice.ToString() + "-" + currentResult.ToString() + FILE_EXTENSION, false, TYPING_DELAY);
                    }
                    catch{}
                }
                break;
            default:
                break;
        }
    }

    public void UpdateScores(int FinancialChange, int TeamMoralChange, int MoralityScore1Change, int MoralityScore2Change, int MoralityScore3Change, int MoralityScore4Change, int MoralityScore5Change, int MoralityScore6Change){
        //Updates all the scores
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
        //Changes the scene, updates the dialogue
        currentDialogue ++;
        SceneManager.LoadScene("Assets/Scenes/Chapters/Dialogue"+ currentDialogue.ToString() + ".unity"); 
        currentScene = "Dialogue";
    }
    public void GoToChoices(){
        //Changes the scene, updates the dialogue
        SceneManager.LoadScene("OptionScene");
        currentChoice ++;
        currentScene = "Choices";
    }
    public void GoToResults(int resultNumber){
        //Changes the scene, updates the dialogue
        SceneManager.LoadScene("Assets/Scenes/Chapters/Dialogue"+ currentDialogue.ToString() + ".unity");
        currentResult = resultNumber;
        currentScene = "Results";
    }
}
