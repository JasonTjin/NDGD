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

[Table("RESULTS")]
public class Results : BaseModel
{
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
}
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
    private summaryScript summaryManager;
    public int currentDialogue; //The number of the current dialogue file
    private int currentChoice; //The number of the current choice file
    private int currentResult; //The number of the current result from the current choice
    private int FinancialScore = 0; 
    private int TeamMoralScore = 0;
    private int MoralityScore1 = 0; //The primacy of the public interest
    private int MoralityScore2 = 0; //The enhancement of quality of life
    private int MoralityScore3 = 0; //Honesty
    private int MoralityScore4 = 0; //Compentancy
    private int MoralityScore5 = 0; //Professional development
    private int MoralityScore6 = 0; //Professionalism
    private string currentScene; //The name of the current scene
    private int[] Decisions = {1,2,3,4,5,6,7,8,9,10,11};
    private Supabase.Client  supabase;

    async void Awake()
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
                        dialogueManager.SetUpDialogue(DIALOGUE_FILE_PATH + currentDialogue.ToString() + FILE_EXTENSION, TYPING_DELAY);
                    }
                    catch{}
                }
                break;
            case "Results":
                if (!dialogueManager){
                    try{
                        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager2>();
                        dialogueManager.SetUpDialogue(RESULTS_FILE_PATH + currentChoice.ToString() + "-" + currentResult.ToString() + FILE_EXTENSION, TYPING_DELAY);
                    }
                    catch{}
                }
                break;
            case "Summary":
                if (!summaryManager){
                    try{
                        summaryManager = GameObject.FindGameObjectWithTag("Summary").GetComponent<summaryScript>();
                        summaryManager.UpdateScores(FinancialScore, TeamMoralScore * 25, ((MoralityScore1 + MoralityScore2 + MoralityScore3 + MoralityScore4 + MoralityScore5 + MoralityScore6)/6) * 25);
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

    public void GoToSumary(){
        //Changes the scene, updates the score
        SceneManager.LoadScene("Summary");
        currentScene = "Summary";
    }

    public void goToMainMenu(){
        SceneManager.LoadScene("Menu");
    }

    public void GoToAcknowledgements () {
        SceneManager.LoadScene("Acknowledgements");
    }

    public async void test()
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
            Decision11 = Decisions[10]
        };
        await supabase.From<Results>().Insert(model);
    }
}
