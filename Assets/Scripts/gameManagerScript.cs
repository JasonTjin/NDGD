using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Supabase.Postgrest.Requests;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
using TMPro;

[Table("RESULTS")]
public class Results : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }
    public Results() { } // Parameterless constructor
    [Column("Decision1")]
    public int Decision1 { get; set; }
    [Column("Decision2")]
    public int Decision2 { get; set; }
    [Column("Decision3")]
    public int Decision3 { get; set; }
    [Column("Decision4")]
    public int Decision4 { get; set; }
    [Column("Decision5")]
    public int Decision5 { get; set; }
    [Column("Decision6")]
    public int Decision6 { get; set; }
    [Column("Decision7")]
    public int Decision7 { get; set; }
    [Column("Decision8")]
    public int Decision8 { get; set; }
    [Column("Decision9")]
    public int Decision9 { get; set; }
    [Column("Decision10")]
    public int Decision10 { get; set; }
    [Column("Decision11")]
    public int Decision11 { get; set; }
    [Column("Narrative_Included")]
    public bool Narrative_Included {get; set;}
}
public class gameManagerScript : MonoBehaviour
{
    const string DIALOGUE_FILE_PATH = "CSVs/Dialogues/Dialogue";
    const string CHOICES_FILE_PATH = "CSVs/Choices/Choices";
    const string RESULTS_FILE_PATH = "CSVs/Results/Results";
    const string CONTEXT_FILE_PATH = "CSVs/Contexts/Context";
    const string CONCLUSION_FILE_PATH = "CSVs/Conclusions/Conclusion";
    const string CSV_EXTENSION = ".csv";
    const string TXT_EXTENSION = ".txt";
    const int TYPING_DELAY = 2; //Controlls the delay between each letter showing up
    const int TARGET_FRAMERATE = 60;
    private ChoiceManager choiceManager;
    private DialogueManager2 dialogueManager;
    public GameObject thisObject; //Used to make this object not destroy on load
    private summaryScript summaryManager;
    private ContextManagerScript contextManager;
    private EndManagerScript endManager;
    private TMP_Text sceneTransitionText;
    private TMP_Text gameNumberText;
    private bool narrativeIncluded;
    private int currentDialogue; //The number of the current dialogue file
    private int currentChoice; //The number of the current choice file
    private int currentResult; //The number of the current result from the current choice
    private int FinancialScore = 1; 
    private int TeamMoralScore = 1;
    private int MoralityScore1 = 1; //The primacy of the public interest
    private int MoralityScore2 = 1; //The enhancement of quality of life
    private int MoralityScore3 = 1; //Honesty
    private int MoralityScore4 = 1; //Compentancy
    private int MoralityScore5 = 1; //Professional development
    private int MoralityScore6 = 1; //Professionalism
    private int FinancialScoreMax = 1; 
    private int TeamMoralScoreMax = 1;
    private int MoralityScore1Max = 1; //The primacy of the public interest
    private int MoralityScore2Max = 1; //The enhancement of quality of life
    private int MoralityScore3Max = 1; //Honesty
    private int MoralityScore4Max = 1; //Compentancy
    private int MoralityScore5Max = 1; //Professional development
    private int MoralityScore6Max = 1; //Professionalism
    private int FinancialScoreBiggestLoss = 0; 
    private int TeamMoralScoreBiggestLoss = 0;
    private int MoralityScore1BiggestLoss = 0; //The primacy of the public interest
    private int MoralityScore2BiggestLoss = 0; //The enhancement of quality of life
    private int MoralityScore3BiggestLoss = 0; //Honesty
    private int MoralityScore4BiggestLoss = 0; //Compentancy
    private int MoralityScore5BiggestLoss = 0; //Professional development
    private int MoralityScore6BiggestLoss = 0; //Professionalism
    private int FinancialScoreBiggestLossDecisionIndex = -1; 
    private int TeamMoralScoreBiggestLossDecisionIndex = -1;
    private int MoralityScore1BiggestLossDecisionIndex = -1; //The primacy of the public interest
    private int MoralityScore2BiggestLossDecisionIndex = -1; //The enhancement of quality of life
    private int MoralityScore3BiggestLossDecisionIndex = -1; //Honesty
    private int MoralityScore4BiggestLossDecisionIndex = -1; //Compentancy
    private int MoralityScore5BiggestLossDecisionIndex = -1; //Professional development
    private int MoralityScore6BiggestLossDecisionIndex = -1; //Professionalism
    private string currentScene; //The name of the current scene
    private int[] Decisions = new int[11];
    private bool initialContextGiven = false;
    private int transitionTimer;
    private bool transitionDone = false;
    private int conclusionNumber;
    private bool conclusionDone = false;
    private int gameNumber;
    private Supabase.Client  supabase;

    async void Start()
    {
        currentDialogue = 0;
        currentChoice = 0;
        DontDestroyOnLoad(thisObject); //Stops this object from being unloaded 
        SceneManager.LoadScene("Menu");

        var options = new Supabase.SupabaseOptions
        {
            AutoConnectRealtime = true
        };

        supabase = new Supabase.Client("https://okzgvpnnqwecacqrppgn.supabase.co", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im9remd2cG5ucXdlY2FjcXJwcGduIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MjU0Mzc1MzYsImV4cCI6MjA0MTAxMzUzNn0.gwTxDDZnFrdIOljsdaDbeKxx9xJqAtQGflZvhWD_syQ", options);
        await supabase.InitializeAsync();
        Application.targetFrameRate = TARGET_FRAMERATE;
        if (UnityEngine.Random.value >= 0.5){
            narrativeIncluded = true;
        }
        else{
            narrativeIncluded = false;
        }
        
    }
    void Update(){ 
        //This makes sure that the choice manager or dialogue manager are correctly stored, then sets them up for when a scene change happens
        switch (currentScene){
            case "Choices":
                if (!choiceManager){
                    try{
                        choiceManager = GameObject.FindGameObjectWithTag("ChoiceManager").GetComponent<ChoiceManager>();
                        if (narrativeIncluded){
                            choiceManager.SetUpChoices(CHOICES_FILE_PATH + currentChoice.ToString() + CSV_EXTENSION, TYPING_DELAY);
                        }
                        else{
                            choiceManager.SetUpChoices(CHOICES_FILE_PATH + "A" + currentChoice.ToString() + CSV_EXTENSION, TYPING_DELAY);
                        }
                    }
                    catch{}
                }
                break;
            case "Dialogue":
                if (!dialogueManager){
                    try{
                        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager2>();
                        dialogueManager.SetUpDialogue(DIALOGUE_FILE_PATH + currentDialogue.ToString() + CSV_EXTENSION, TYPING_DELAY);
                    }
                    catch{}
                }
                break;
            case "Results":
                if (!dialogueManager){
                    try{
                        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager2>();
                        dialogueManager.SetUpDialogue(RESULTS_FILE_PATH + currentChoice.ToString() + "-" + currentResult.ToString() + CSV_EXTENSION, TYPING_DELAY);
                    }
                    catch{}
                }
                break;
            case "Summary":
                if (!summaryManager){
                    try{
                        summaryManager = GameObject.FindGameObjectWithTag("Summary").GetComponent<summaryScript>();
                        summaryManager.UpdateScores(FinancialScore * 100/FinancialScoreMax, TeamMoralScore * 100/TeamMoralScoreMax, (MoralityScore1 * 100/MoralityScore1Max + MoralityScore2 * 100/MoralityScore2Max + MoralityScore3 * 100/MoralityScore3Max + MoralityScore4 * 100/MoralityScore4Max + MoralityScore5 * 100/MoralityScore5Max + MoralityScore6 * 100/MoralityScore6Max)/6);
                    }
                    catch{}
                }
                break;
            case "Context":
                if (!contextManager){
                    try{
                        contextManager = GameObject.FindGameObjectWithTag("ContextManager").GetComponent<ContextManagerScript>();
                        if (!initialContextGiven && currentChoice == 0){
                            initialContextGiven = true;
                            contextManager.SetUpContext("", currentChoice);
                        }
                        else{
                            contextManager.SetUpContext(CONTEXT_FILE_PATH + (currentChoice + 1).ToString() + TXT_EXTENSION, currentChoice + 1);
                        }
                    }
                    catch{}
                }
                break;
            case "ScenarioTransition":
                if (!sceneTransitionText){
                    try{
                        sceneTransitionText = GameObject.FindGameObjectWithTag("ScenarioTitle").GetComponent<TMP_Text>();
                        if (currentChoice != 11){
                            sceneTransitionText.text = "Scenario " + (currentChoice + 1).ToString();
                        }
                        else{
                            sceneTransitionText.text = "The End";
                        }
                        
                        transitionTimer = 0;
                    }
                    catch{}
                }
                else{
                    transitionTimer++;
                    if (transitionTimer > 100){
                        GoToDialogue();
                    }
                }
                break;
            case "Conclusion":
                if (!dialogueManager){
                    try{
                        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager2>();
                        dialogueManager.SetUpDialogue(CONCLUSION_FILE_PATH + conclusionNumber.ToString() + CSV_EXTENSION, TYPING_DELAY);
                        conclusionDone = true;
                    }
                    catch{}
                }
                break;
            case "End":
                if (!endManager){
                    try{
                        endManager = GameObject.FindGameObjectWithTag("EndManager").GetComponent<EndManagerScript>();
                        endManager.InitiateEndSequence(
                            FinancialScore,
                            TeamMoralScore, 
                            MoralityScore1, 
                            MoralityScore2, 
                            MoralityScore3, 
                            MoralityScore4, 
                            MoralityScore5, 
                            MoralityScore6, 
                            FinancialScoreMax,
                            TeamMoralScoreMax,
                            MoralityScore1Max,
                            MoralityScore2Max,
                            MoralityScore3Max,
                            MoralityScore4Max,
                            MoralityScore5Max,
                            MoralityScore6Max,
                            FinancialScoreBiggestLossDecisionIndex,
                            TeamMoralScoreBiggestLossDecisionIndex,
                            MoralityScore1BiggestLossDecisionIndex,
                            MoralityScore2BiggestLossDecisionIndex,
                            MoralityScore3BiggestLossDecisionIndex,
                            MoralityScore4BiggestLossDecisionIndex,
                            MoralityScore5BiggestLossDecisionIndex,
                            MoralityScore6BiggestLossDecisionIndex,
                            Decisions,
                            narrativeIncluded);
                    }
                    catch {}
                }
                break;
            case "SurveyDetails":
                if (!gameNumberText){
                    try{
                        gameNumberText = GameObject.FindGameObjectWithTag("GameNumberScore").GetComponent<TMP_Text>();
                        gameNumberText.text = gameNumber.ToString();
                    }
                    catch{}
                }
                break;
            default:
                break;
        }
    }

    public void UpdateScores(int FinancialChange, int TeamMoralChange, int MoralityScore1Change, int MoralityScore2Change, int MoralityScore3Change, int MoralityScore4Change, int MoralityScore5Change, int MoralityScore6Change, int FinancialChangeMax, int TeamMoralChangeMax, int MoralityScore1ChangeMax, int MoralityScore2ChangeMax, int MoralityScore3ChangeMax, int MoralityScore4ChangeMax, int MoralityScore5ChangeMax, int MoralityScore6ChangeMax){
        //Updates all the scores
        FinancialScore += FinancialChange;
        TeamMoralScore += TeamMoralChange;
        MoralityScore1 += MoralityScore1Change;
        MoralityScore2 += MoralityScore2Change;
        MoralityScore3 += MoralityScore3Change;
        MoralityScore4 += MoralityScore4Change;
        MoralityScore5 += MoralityScore5Change;
        MoralityScore6 += MoralityScore6Change;
        FinancialScoreMax += FinancialChangeMax;
        TeamMoralScoreMax += TeamMoralChangeMax;
        MoralityScore1Max += MoralityScore1ChangeMax;
        MoralityScore2Max += MoralityScore2ChangeMax;
        MoralityScore3Max += MoralityScore3ChangeMax;
        MoralityScore4Max += MoralityScore4ChangeMax;
        MoralityScore5Max += MoralityScore5ChangeMax;
        MoralityScore6Max += MoralityScore6ChangeMax;
        if (FinancialChange - FinancialChangeMax < FinancialScoreBiggestLoss){
            FinancialScoreBiggestLoss = FinancialChange - FinancialChangeMax;
            FinancialScoreBiggestLossDecisionIndex = currentChoice;
        }
        if (TeamMoralChange - TeamMoralChangeMax < TeamMoralScoreBiggestLoss){
            TeamMoralScoreBiggestLoss = TeamMoralChange - TeamMoralChangeMax;
            TeamMoralScoreBiggestLossDecisionIndex = currentChoice;
        }
        if (MoralityScore1Change - MoralityScore1ChangeMax < MoralityScore1BiggestLoss){
            MoralityScore1BiggestLoss = MoralityScore1Change - MoralityScore1ChangeMax;
            MoralityScore1BiggestLossDecisionIndex = currentChoice;
        }
        if (MoralityScore2Change - MoralityScore2ChangeMax < MoralityScore2BiggestLoss){
            MoralityScore2BiggestLoss = MoralityScore2Change - MoralityScore2ChangeMax;
            MoralityScore2BiggestLossDecisionIndex = currentChoice;
        }
        if (MoralityScore3Change - MoralityScore3ChangeMax < MoralityScore3BiggestLoss){
            MoralityScore3BiggestLoss = MoralityScore3Change - MoralityScore3ChangeMax;
            MoralityScore3BiggestLossDecisionIndex = currentChoice;
        }
        if (MoralityScore4Change - MoralityScore4ChangeMax < MoralityScore4BiggestLoss){
            MoralityScore4BiggestLoss = MoralityScore4Change - MoralityScore4ChangeMax;
            MoralityScore4BiggestLossDecisionIndex = currentChoice;
        }
        if (MoralityScore5Change - MoralityScore5ChangeMax < MoralityScore5BiggestLoss){
            MoralityScore5BiggestLoss = MoralityScore5Change - MoralityScore5ChangeMax;
            MoralityScore5BiggestLossDecisionIndex = currentChoice;
        }
        if (MoralityScore6Change - MoralityScore6ChangeMax < MoralityScore6BiggestLoss){
            MoralityScore6BiggestLoss = MoralityScore6Change - MoralityScore6ChangeMax;
            MoralityScore6BiggestLossDecisionIndex = currentChoice;
        }
    }

    public void GoToTransition(){
        SceneManager.LoadScene("Assets/Scenes/ScenarioTransition.unity");
        currentScene = "ScenarioTransition";
    }

    public void GoToDialogue(){
        if (currentChoice >= Decisions.Length){
            if (!transitionDone){
                GoToTransition();
                transitionDone = true;
                return;
            }
            else{
                GoToEnd();
                return;
            } 
        }

        //Changes the scene, updates the dialogue
        if (narrativeIncluded){
            if (currentDialogue >= 3 && !transitionDone){
                GoToTransition();
                transitionDone = true;
                return;
            }
            transitionDone = false;
            currentDialogue ++;
            SceneManager.LoadScene("Assets/Scenes/Chapters/Dialogue"+ currentDialogue.ToString() + ".unity"); 
            currentScene = "Dialogue";
        }
        else{
            GoToContext();
        }
    }
    public void GoToChoices(){
        //Changes the scene, updates the dialogue
        SceneManager.LoadScene("OptionScene");
        currentChoice ++;
        currentScene = "Choices";
    }
    public void GoToResults(int resultNumber){
        //Changes the scene, updates the dialogue
        Decisions[currentChoice - 1] = resultNumber;
        if (narrativeIncluded){
            SceneManager.LoadScene("Assets/Scenes/Chapters/Dialogue"+ currentDialogue.ToString() + ".unity");
            currentResult = resultNumber;
            currentScene = "Results";
        }
        else{
            GoToContext();
        }
    }

    public void GoToSumary(){
        //Changes the scene, updates the score
        SceneManager.LoadScene("Summary");
        currentScene = "Summary";
    }
    public void GoToContext(){
        if (currentChoice >= Decisions.Length){
            GoToEnd();
            return;
        }
        //Changes the scene, updates the dialogue
        SceneManager.LoadScene("Context");
        currentScene = "Context";
    }

    public void GoToEnd(){
        //Changes the scene to the end
        if (narrativeIncluded && !conclusionDone){

            GoToConclusion();
            return;
        }
        SceneManager.LoadScene("End");
        currentScene = "End";
        submitResultsToSupabase();
    }

    public void GoToConclusion(){
        if (FinancialScore * 100 / FinancialScoreMax >= 70){
            conclusionNumber = 3;
        }
        else if (TeamMoralScore * 100 / TeamMoralScoreMax >= 70){
            conclusionNumber = 2;
        }
        else{
            conclusionNumber = 1;
        }
        SceneManager.LoadScene("Assets/Scenes/Conclusion" + conclusionNumber.ToString() + ".unity");
        currentScene = "Conclusion";
    }

    public void goToMainMenu(){
        SceneManager.LoadScene("Menu");
    }

    public void GoToAcknowledgements () {
        SceneManager.LoadScene("Acknowledgements");
    }

    public void GoToSurveyDetails(){
        SceneManager.LoadScene("SurveyDetails");
        currentScene = "SurveyDetails";
    }

    public async void GetGameNumber(){
        var result = await supabase.From<Results>().Get();
        gameNumber = result.Models[result.Models.Count - 1].Id;
    }

    public async void submitResultsToSupabase()
    {
        var model = new Results
        {
            Decision1 = Decisions[0],
            Decision2 = Decisions[1],
            Decision3 = Decisions[2],
            Decision4 = Decisions[3],
            Decision5 = Decisions[4],
            Decision6 = Decisions[5],
            Decision7 = Decisions[6],
            Decision8 = Decisions[7],
            Decision9 = Decisions[8],
            Decision10 = Decisions[9],
            Decision11 = Decisions[10],
            Narrative_Included = narrativeIncluded
        };
        await supabase.From<Results>().Insert(model);
        GetGameNumber();
    }
}
