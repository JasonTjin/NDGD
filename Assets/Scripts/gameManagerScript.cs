using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManagerScript : MonoBehaviour
{
    const string DIALOGUE_FILE_PATH = "./assets/CSVs/Dialogues/Dialogue";
    const string CHOICES_FILE_PATH = "./assets/CSVs/Choices/Choices";
    const string RESULTS_FILE_PATH = "./assets/CSVs/Results/Results";
    const string FILE_EXTENSION = ".csv";
    public GameObject gameManagerObject;
    public ChoiceManager choiceManager;
    public string currentDialogue;
    public string currentChoice;
    public int FinancialScore;
    public int TeamMoralScore;
    public int MoralityScore1;
    public int MoralityScore2;
    public int MoralityScore3;
    public int MoralityScore4;
    public int MoralityScore5;
    public int MoralityScore6;
    public string resultsCSVName;

    void Awake()
    {
        DontDestroyOnLoad(gameManagerObject);
        currentDialogue = "Intro";
        currentChoice = "0";
    }
    void Start()
    {
        GoToChoices();
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

    }
    public void GoToChoices(){
        //SceneManager.LoadScene();
        choiceManager.SetUpChoices(CHOICES_FILE_PATH + (Convert.ToInt32(currentChoice) + 1).ToString() + FILE_EXTENSION);
    }
    public void GoToResults(int resultNumber){
        
    }
}
