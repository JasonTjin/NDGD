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

    // Update is called once per frame
    void Update()
    {
        if (!updated){
            switch(currentSlide){
                case 1:
                    title.text = "Finances";
                    score.text = (financialScore * 100 / financialScoreMax).ToString() + "%";
                    updated = true;
                    break;
                case 2:
                    title.text = "Team Morale";
                    score.text = (teamMoralScore * 100 / teamMoralScoreMax).ToString() + "%";
                    updated = true;
                    break;
                case 3:
                    title.text = "The Primacy of the Public Interest";
                    score.text = (moralityScore1 * 100 / moralityScore1Max).ToString() + "%";
                    updated = true;
                    break;
                case 4:
                    title.text = "The Enhancement of Quality of Life";
                    score.text = (moralityScore2 * 100 / moralityScore2Max).ToString() + "%";
                    updated = true;
                    break;
                case 5:
                    title.text = "Honesty";
                    score.text = (moralityScore3 * 100 / moralityScore3Max).ToString() + "%";
                    updated = true;
                    break;
                case 6:
                    title.text = "Competence";
                    score.text = (moralityScore4 * 100 / moralityScore4Max).ToString() + "%";
                    updated = true;
                    break;
                case 7:
                    title.text = "Professional Development";
                    score.text = (moralityScore5 * 100 / moralityScore5Max).ToString() + "%";
                    updated = true;
                    break;
                case 8:
                    title.text = "Professionalism";
                    score.text = (moralityScore6 * 100 / moralityScore6Max).ToString() + "%";
                    updated = true;
                    break;
                default:
                    break;
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
