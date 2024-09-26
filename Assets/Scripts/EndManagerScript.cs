using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndManagerScript : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {
        currentSlide = 1;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<gameManagerScript>();
        title = GameObject.FindGameObjectWithTag("Title").GetComponent<TMP_Text>();
        body = GameObject.FindGameObjectWithTag("Body").GetComponent<TMP_Text>();
        nextButton = GameObject.FindGameObjectWithTag("EndNextButton");
        backButton = GameObject.FindGameObjectWithTag("EndBackButton");
        nextButtonText = GameObject.FindGameObjectWithTag("EndNextButton").GetComponent<TMP_Text>();
        score = GameObject.FindGameObjectWithTag("FinalScore").GetComponent<TMP_Text>();
        backButton.SetActive(false);
        updated = false;
    }

    private string GetRecomendations(int percentageScore, string ethicsName){
        var recomendations = "";
        if ((moralityScore1 * 100 / moralityScore1Max) >= 90){
            recomendations = "  Your adherance to " + ethicsName + " was very good and needs little improvemnt.";
        }
        else if ((moralityScore1 * 100 / moralityScore1Max) >= 80){
            recomendations = "  Your adherance to " + ethicsName + " was good, but you could probably touch up on your undestanding.";
        }
        else if ((moralityScore1 * 100 / moralityScore1Max) >= 70){
            recomendations = "  Your adherance to " + ethicsName + " was okay, however you can certainly improve your understanding.";
        }
        else if ((moralityScore1 * 100 / moralityScore1Max) >= 60){
            recomendations = "  Your adherance to " + ethicsName + " was mediocre and you should read up on the ACS code of ethics.";
        }
        else if ((moralityScore1 * 100 / moralityScore1Max) >= 50){
            recomendations = "  Your adherance to " + ethicsName + " was not good and suggests that you need to do some serious study of the ACS code of ethics.";
        }
        else if ((moralityScore1 * 100 / moralityScore1Max) >= 40){
            recomendations = "  Your adherance to " + ethicsName + " was bad and suggests a missunderstanding in the key concepts involved.  We highly recomend reading up on " + ethicsName + " in the ACS code of ethics.";
        }
        else if ((moralityScore1 * 100 / moralityScore1Max) >= 30){
            recomendations = "  Your adherance to " + ethicsName + " was very bad, nearly every decision relating to " + ethicsName + " was wrong.  It is a necessity that you read the ACS code of ethics to understand where you went wrong.";
        }
        else if ((moralityScore1 * 100 / moralityScore1Max) >= 20){
            recomendations = "  Your adherance to " + ethicsName + " was non-existent and suggests a deliberate attempt to be unethical, however you could have been worse.";
        }
        else {
            recomendations = "  Congratulations you have been so unethical that it is possible " + '"' + ethicsName + '"' + " isn't a part of your vocabulary.";
        }
        return recomendations;
    }

    // Update is called once per frame
    void Update()
    {
        if (!updated){
            var bodyText = "";
            var success = true;
            switch(currentSlide){
                case 1:
                    title.text = "Finances";
                    score.text = (financialScore * 100 / financialScoreMax).ToString() + "%";
                    bodyText = ("Your biggest loss in finances was in scenario " 
                        +  FinancialScoreBiggestLossDecisionIndex.ToString() 
                        + " when you decided to " 
                        + "");
                    break;
                case 2:
                    title.text = "Team Morale";
                    score.text = (teamMoralScore * 100 / teamMoralScoreMax).ToString() + "%";
                    bodyText = ("Your biggest loss in team morale was in scenario " 
                        +  TeamMoralScoreBiggestLossDecisionIndex.ToString() 
                        + " when you decided to " 
                        + "");
                    break;
                case 3:
                    title.text = "The Primacy of the Public Interest";
                    score.text = (moralityScore1 * 100 / moralityScore1Max).ToString() + "%";
                    if ((moralityScore1 * 100 / moralityScore1Max) == 100){

                    }
                    bodyText = ("Your decision that least adheared to the primacy of the public interest was in scenario " 
                        +  MoralityScore1BiggestLossDecisionIndex.ToString() 
                        + " when you decided to " 
                        + "");
                    
                    break;
                case 4:
                    title.text = "The Enhancement of Quality of Life";
                    score.text = (moralityScore2 * 100 / moralityScore2Max).ToString() + "%";
                    bodyText = ("Your decision that least adheared to the enhancement of quality of life was in scenario " 
                        +  MoralityScore2BiggestLossDecisionIndex.ToString() 
                        + " when you decided to " 
                        + "");
                    break;
                case 5:
                    title.text = "Honesty";
                    score.text = (moralityScore3 * 100 / moralityScore3Max).ToString() + "%";
                    bodyText = ("Your decision that least adheared to honesty was in scenario " 
                        +  MoralityScore3BiggestLossDecisionIndex.ToString() 
                        + " when you decided to " 
                        + "");
                    break;
                case 6:
                    title.text = "Competence";
                    score.text = (moralityScore4 * 100 / moralityScore4Max).ToString() + "%";
                    bodyText = ("Your decision that least adheared to competence was in scenario " 
                        +  MoralityScore4BiggestLossDecisionIndex.ToString() 
                        + " when you decided to " 
                        + "");
                    break;
                case 7:
                    title.text = "Professional Development";
                    score.text = (moralityScore5 * 100 / moralityScore5Max).ToString() + "%";
                    bodyText = ("Your decision that least adheared to professional development was in scenario " 
                        +  MoralityScore5BiggestLossDecisionIndex.ToString() 
                        + " when you decided to " 
                        + "");
                    break;
                case 8:
                    title.text = "Professionalism";
                    score.text = (moralityScore6 * 100 / moralityScore6Max).ToString() + "%";
                    bodyText = ("Your decision that least adheared to professionalism was in scenario " 
                        +  MoralityScore6BiggestLossDecisionIndex.ToString() 
                        + " when you decided to " 
                        + "");
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
    public void InitiateEndSequence(
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
        int[] decisionsListFinal)
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
    }

    public void Next(){
        if (currentSlide != 8){
            if (currentSlide == 0){
                backButton.SetActive(true);
            }
            currentSlide++;
            updated = false;
            if (currentSlide == 9){
                nextButtonText.text = "Finish";
            }
        }
    }

    public void Back(){
        if (currentSlide != 0){
            if (currentSlide == 8){
                nextButtonText.text = "Next";
            }
            currentSlide--;
            updated = false;
            if (currentSlide == 0){
                backButton.SetActive(false);
            }
        }
    }
}
