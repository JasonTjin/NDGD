using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class EndManagerScript : MonoBehaviour
{
    const string FILE_PATH = "CSVs/ChoiceSummaries/ChoiceSummaries";
    const string CSV_EXTENSION = ".csv";
    private int[] decisionsList;
    private GameObject nextButton;
    private GameObject backButton;
    private TMP_Text nextButtonText;
    private gameManagerScript gameManager;
    private TMP_Text title;
    private TMP_Text body;
    private TMP_Text score;
    private bool updated;
    private int currentSlide;
    private bool narrativeIncluded;
    private int financialScore;
    private int teamMoralScore; 
    private int moralityScore1;
    private int moralityScore2;
    private int moralityScore3;
    private int moralityScore4;
    private int moralityScore5;
    private int moralityScore6;
    private int financialScoreMax;
    private int teamMoralScoreMax; 
    private int moralityScore1Max;
    private int moralityScore2Max;
    private int moralityScore3Max;
    private int moralityScore4Max;
    private int moralityScore5Max;
    private int moralityScore6Max;
    private int FinancialScoreBiggestLossDecisionIndex;
    private int TeamMoralScoreBiggestLossDecisionIndex;
    private int MoralityScore1BiggestLossDecisionIndex;
    private int MoralityScore2BiggestLossDecisionIndex;
    private int MoralityScore3BiggestLossDecisionIndex;
    private int MoralityScore4BiggestLossDecisionIndex;
    private int MoralityScore5BiggestLossDecisionIndex;
    private int MoralityScore6BiggestLossDecisionIndex;
    private List<List<string>> summaries;


    private async Task<List<string>> GetSummary(int summaryNumber){
        var output = new List<string>();
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, FILE_PATH + summaryNumber.ToString() + CSV_EXTENSION);
        UnityWebRequest request = UnityWebRequest.Get(filePath);
        UnityWebRequestAsyncOperation operation = request.SendWebRequest();
        while (!operation.isDone)
        {
            await Task.Yield();
        }
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Cannot load file at " + filePath);
            return output;
        }
        
        var lines = request.downloadHandler.text.Split('\n');
        for(var nodeNum = 1; nodeNum < lines.Length; nodeNum++){
            var values = lines[nodeNum].Split(",");
            if (narrativeIncluded){
                output.Add(values[0].Replace('|', ','));
            }
            else{
                output.Add(values[1].Replace('|', ','));
            }
        }
        return output;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentSlide = 1;
        backButton.SetActive(false);
        updated = false;
    }

    private string GetRecomendations(int percentageScore, string ethicsName){
        var recomendations = "";
        if (percentageScore >= 90){
            recomendations = "  Your adherance to " + ethicsName + " was very good and needs little improvemnt.";
        }
        else if (percentageScore >= 80){
            recomendations = "  Your adherance to " + ethicsName + " was good, but you could probably touch up on your understanding.";
        }
        else if (percentageScore >= 70){
            recomendations = "  Your adherance to " + ethicsName + " was okay, however you can certainly improve your understanding.";
        }
        else if (percentageScore >= 60){
            recomendations = "  Your adherance to " + ethicsName + " was mediocre and you should read up on the ACS code of ethics.";
        }
        else if (percentageScore >= 50){
            recomendations = "  Your adherance to " + ethicsName + " was not good and suggests that you need to do some serious study of the ACS code of ethics.";
        }
        else if (percentageScore >= 40){
            recomendations = "  Your adherance to " + ethicsName + " was bad and suggests a missunderstanding in the key concepts involved.  We highly recomend reading up on " + ethicsName + " in the ACS code of ethics.";
        }
        else if (percentageScore >= 30){
            recomendations = "  Your adherance to " + ethicsName + " was very bad, nearly every decision relating to " + ethicsName + " was wrong.  It is a necessity that you read the ACS code of ethics to understand where you went wrong.";
        }
        else if (percentageScore >= 20){
            recomendations = "  Your adherance to " + ethicsName + " was non-existent and suggests a deliberate attempt to be unethical, however you could have been worse.";
        }
        else {
            recomendations = "  Congratulations you achieved one of the most unethical scores possible.";
        }
        return recomendations;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager){
            try{
                gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<gameManagerScript>();
            }
            catch{}
        }
        if (!title){
            try{
                title = GameObject.FindGameObjectWithTag("Title").GetComponent<TMP_Text>();
            }
            catch{}
        }
        if (!body){
            try{
                body = GameObject.FindGameObjectWithTag("Body").GetComponent<TMP_Text>();
            }
            catch{}
        }
        if (!nextButton){
            try{
                nextButton = GameObject.FindGameObjectWithTag("EndNextButton");
            }
            catch{}
        }
        if (!backButton){
            try{
                backButton = GameObject.FindGameObjectWithTag("EndBackButton");
            }
            catch{}
        }
        if (!nextButtonText){
            try{
                nextButtonText = GameObject.FindGameObjectWithTag("EndNextButtonText").GetComponent<TMP_Text>();
            }
            catch{}
        }
        if (!score){
            try{
                score = GameObject.FindGameObjectWithTag("FinalScore").GetComponent<TMP_Text>();
            }
            catch{}
        }
        if (!updated){
            var bodyText = "";
            var success = true;
            switch(currentSlide){
                case 1:
                    title.text = "Finances";
                    score.text = (financialScore * 100 / financialScoreMax).ToString() + "%";
                    score.color = scoreColor(financialScore * 100 / financialScoreMax);
                    bodyText += "Your biggest loss in finances was in scenario ";
                    bodyText += FinancialScoreBiggestLossDecisionIndex.ToString() ;
                    bodyText += " when you " ;
                    bodyText += summaries[FinancialScoreBiggestLossDecisionIndex][decisionsList[FinancialScoreBiggestLossDecisionIndex] - 1];
                    break;
                case 2:
                    title.text = "Team Morale";
                    score.text = (teamMoralScore * 100 / teamMoralScoreMax).ToString() + "%";
                    score.color = scoreColor(teamMoralScore * 100 / teamMoralScoreMax);
                    bodyText = (
                        "Your biggest loss in team morale was in scenario " 
                        +  TeamMoralScoreBiggestLossDecisionIndex.ToString() 
                        + " when you " 
                        + summaries[TeamMoralScoreBiggestLossDecisionIndex][decisionsList[TeamMoralScoreBiggestLossDecisionIndex] - 1]);
                    break;
                case 3:
                    title.text = "The Primacy of the Public Interest";
                    score.text = (moralityScore1 * 100 / moralityScore1Max).ToString() + "%";
                    score.color = scoreColor(moralityScore1 * 100 / moralityScore1Max);
                    if ((moralityScore1 * 100 / moralityScore1Max) == 100){

                    }
                    bodyText = (
                        "Your decision that least adheared to the primacy of the public interest was in scenario " 
                        +  MoralityScore1BiggestLossDecisionIndex.ToString() 
                        + " when you " 
                        + summaries[MoralityScore1BiggestLossDecisionIndex][decisionsList[MoralityScore1BiggestLossDecisionIndex] - 1]
                        + GetRecomendations(moralityScore1 * 100 / moralityScore1Max, "the primacy of the public interest"));
                    
                    break;
                case 4:
                    title.text = "The Enhancement of Quality of Life";
                    score.text = (moralityScore2 * 100 / moralityScore2Max).ToString() + "%";
                    score.color = scoreColor(moralityScore2 * 100 / moralityScore2Max);
                    bodyText = (
                        "Your decision that least adheared to the enhancement of quality of life was in scenario " 
                        +  MoralityScore2BiggestLossDecisionIndex.ToString() 
                        + " when you " 
                        + summaries[MoralityScore2BiggestLossDecisionIndex][decisionsList[MoralityScore2BiggestLossDecisionIndex] - 1]
                        + GetRecomendations(moralityScore2 * 100 / moralityScore2Max, "the enhancement of quality of life"));
                    break;
                case 5:
                    title.text = "Honesty";
                    score.text = (moralityScore3 * 100 / moralityScore3Max).ToString() + "%";
                    score.color = scoreColor(moralityScore3 * 100 / moralityScore3Max);
                    bodyText = (
                        "Your decision that least adheared to honesty was in scenario " 
                        +  MoralityScore3BiggestLossDecisionIndex.ToString() 
                        + " when you " 
                        + summaries[MoralityScore3BiggestLossDecisionIndex][decisionsList[MoralityScore3BiggestLossDecisionIndex] - 1]
                        + GetRecomendations(moralityScore3 * 100 / moralityScore3Max, "honesty"));
                    break;
                case 6:
                    title.text = "Competence";
                    score.text = (moralityScore4 * 100 / moralityScore4Max).ToString() + "%";
                    score.color = scoreColor(moralityScore4 * 100 / moralityScore4Max);
                    bodyText = (
                        "Your decision that least adheared to competence was in scenario " 
                        +  MoralityScore4BiggestLossDecisionIndex.ToString() 
                        + " when you " 
                        + summaries[MoralityScore4BiggestLossDecisionIndex][decisionsList[MoralityScore4BiggestLossDecisionIndex] - 1]
                        + GetRecomendations(moralityScore4 * 100 / moralityScore4Max, "competence"));
                    break;
                case 7:
                    title.text = "Professional Development";
                    score.text = (moralityScore5 * 100 / moralityScore5Max).ToString() + "%";
                    score.color = scoreColor(moralityScore5 * 100 / moralityScore5Max);
                    bodyText = (
                        "Your decision that least adheared to professional development was in scenario " 
                        +  MoralityScore5BiggestLossDecisionIndex.ToString() 
                        + " when you " 
                        + summaries[MoralityScore5BiggestLossDecisionIndex][decisionsList[MoralityScore5BiggestLossDecisionIndex] - 1]
                        + GetRecomendations(moralityScore5 * 100 / moralityScore5Max, "professional development"));
                    break;
                case 8:
                    title.text = "Professionalism";
                    score.text = (moralityScore6 * 100 / moralityScore6Max).ToString() + "%";
                    score.color = scoreColor(moralityScore6 * 100 / moralityScore6Max);
                    bodyText = (
                        "Your decision that least adheared to professionalism was in scenario " 
                        +  MoralityScore6BiggestLossDecisionIndex.ToString() 
                        + " when you " 
                        + summaries[MoralityScore6BiggestLossDecisionIndex][decisionsList[MoralityScore6BiggestLossDecisionIndex] - 1]
                        + GetRecomendations(moralityScore6 * 100 / moralityScore6Max, "professionalism"));
                    break;
                default:
                    success = false;
                    break;
            }
            if (success){
                body.text = bodyText;
                updated = true; 
            }
            
        }
    }
    public async void InitiateEndSequence(
        int financialScoreFinal, 
        int teamMoralScoreFinal, 
        int moralityScore1Final, 
        int moralityScore2Final, 
        int moralityScore3Final, 
        int moralityScore4Final, 
        int moralityScore5Final, 
        int moralityScore6Final, 
        int financialScoreMaxFinal, 
        int teamMoralScoreMaxFinal, 
        int moralityScore1MaxFinal, 
        int moralityScore2MaxFinal, 
        int moralityScore3MaxFinal, 
        int moralityScore4MaxFinal, 
        int moralityScore5MaxFinal, 
        int moralityScore6MaxFinal, 
        int FinancialScoreBiggestLossDecisionIndexFinal,
        int TeamMoralScoreBiggestLossDecisionIndexFinal,
        int MoralityScore1BiggestLossDecisionIndexFinal,
        int MoralityScore2BiggestLossDecisionIndexFinal,
        int MoralityScore3BiggestLossDecisionIndexFinal,
        int MoralityScore4BiggestLossDecisionIndexFinal,
        int MoralityScore5BiggestLossDecisionIndexFinal,
        int MoralityScore6BiggestLossDecisionIndexFinal,
        int[] decisionsListFinal,
        bool setNarrativeIncluded)
    {
        financialScore = financialScoreFinal;
        teamMoralScore = teamMoralScoreFinal;
        moralityScore1 = moralityScore1Final;
        moralityScore2 = moralityScore2Final;
        moralityScore3 = moralityScore3Final;
        moralityScore4 = moralityScore4Final;
        moralityScore5 = moralityScore5Final;
        moralityScore6 = moralityScore6Final;
        financialScoreMax = financialScoreMaxFinal;
        teamMoralScoreMax = teamMoralScoreMaxFinal;
        moralityScore1Max = moralityScore1MaxFinal;
        moralityScore2Max = moralityScore2MaxFinal;
        moralityScore3Max = moralityScore3MaxFinal;
        moralityScore4Max = moralityScore4MaxFinal;
        moralityScore5Max = moralityScore5MaxFinal;
        moralityScore6Max = moralityScore6MaxFinal;
        FinancialScoreBiggestLossDecisionIndex = FinancialScoreBiggestLossDecisionIndexFinal;
        TeamMoralScoreBiggestLossDecisionIndex = TeamMoralScoreBiggestLossDecisionIndexFinal;
        MoralityScore1BiggestLossDecisionIndex = MoralityScore1BiggestLossDecisionIndexFinal;
        MoralityScore2BiggestLossDecisionIndex = MoralityScore2BiggestLossDecisionIndexFinal;
        MoralityScore3BiggestLossDecisionIndex = MoralityScore3BiggestLossDecisionIndexFinal;
        MoralityScore4BiggestLossDecisionIndex = MoralityScore4BiggestLossDecisionIndexFinal;
        MoralityScore5BiggestLossDecisionIndex = MoralityScore5BiggestLossDecisionIndexFinal;
        MoralityScore6BiggestLossDecisionIndex = MoralityScore6BiggestLossDecisionIndexFinal;
        decisionsList = decisionsListFinal;
        narrativeIncluded = setNarrativeIncluded;
        summaries = new();
        for (var i = 1; i <= decisionsList.Length; i++){
            summaries.Add(await GetSummary(i));
        }
    }

    public void Next(){
        if (currentSlide != 8){
            backButton.SetActive(true);
            currentSlide++;
            updated = false;
            if (currentSlide == 8){
                nextButtonText.text = "Finish";
            }
        }
        else{
            gameManager.GoToSurveyDetails();
        }
    }

    public void Back(){
        if (currentSlide != 1){
            if (currentSlide == 8){
                nextButtonText.text = "Next";
            }
            currentSlide--;
            updated = false;
            if (currentSlide == 1){
                backButton.SetActive(false);
            }
        }
    }
    public Color scoreColor(int score){
        Color darkGreen, green, yellow, red;
        UnityEngine.ColorUtility.TryParseHtmlString("#006124", out darkGreen);  
        UnityEngine.ColorUtility.TryParseHtmlString("#03E054", out green); 
        UnityEngine.ColorUtility.TryParseHtmlString("#DED700", out yellow);   
        UnityEngine.ColorUtility.TryParseHtmlString("#AE0800", out red);  

        if (score >= 90)
        {
            return darkGreen;
        }
        else if (score >= 70)
        {
            return green;
        }
        else if (score >= 50)
        {
            return yellow;
        }
        else
        {
            return red;
        }
    }
}
