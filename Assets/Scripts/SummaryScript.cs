using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class summaryScript : MonoBehaviour
{
    private TMP_Text financeScore;
    private TMP_Text teamMorale;
    private TMP_Text moralityScore;
    private gameManagerScript gameManager;
    private string financeScoreText;
    private string teamMoraleText;
    private string moralityScoreText;
    private int financeScoreInt;
    private int teamMoraleInt;
    private int moralityScoreInt;

    // Update is called once per frame
    public void UpdateScores(int newFinanceScore, int newTeamMorale, int newMoralityScore)
    {
        financeScoreText = (newFinanceScore.ToString() + '%');
        teamMoraleText = (newTeamMorale.ToString() + '%');
        moralityScoreText = (newMoralityScore.ToString() + '%');

        financeScoreInt = newFinanceScore;
        teamMoraleInt = newTeamMorale;
        moralityScoreInt = newMoralityScore;
    }

    void Update()
    {
        if (!financeScore){
            try{
                financeScore = GameObject.FindGameObjectWithTag("FinanceScore").GetComponent<TMP_Text>();
            }
            catch{}
        }
        if (!teamMorale){
            try{
                teamMorale = GameObject.FindGameObjectWithTag("TeamMorale").GetComponent<TMP_Text>();
            }
            catch{}
        }
        if (!moralityScore){
            try{
                moralityScore = GameObject.FindGameObjectWithTag("MoralityScore").GetComponent<TMP_Text>();
            }
            catch{}
        }
        if (!gameManager){
            try{
                gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<gameManagerScript>();
            }
            catch{}
        }
        try{
            financeScore.text = financeScoreText;
            teamMorale.text = teamMoraleText;
            moralityScore.text = moralityScoreText;

            Color darkGreen, green, yellow, red;
            ColorUtility.TryParseHtmlString("#006124", out darkGreen);  
            ColorUtility.TryParseHtmlString("#03E054", out green); 
            ColorUtility.TryParseHtmlString("#DED700", out yellow);   
            ColorUtility.TryParseHtmlString("#AE0800", out red);   

            // Change colour of score based on the score
            if (financeScoreInt >= 90)
            {
                financeScore.color = darkGreen;
            }
            else if (financeScoreInt >= 70)
            {
                financeScore.color = green;
            }
            else if (financeScoreInt >= 50)
            {
                financeScore.color = yellow;
            }
            else
            {
                financeScore.color = red;
            }
            // Update Morale Score Text Color
            if (teamMoraleInt >= 90)
            {
                teamMorale.color = darkGreen;
            }
            else if (teamMoraleInt >= 70)
            {
                teamMorale.color = green;
            }
            else if (teamMoraleInt >= 50)
            {
                teamMorale.color = yellow;
            }
            else
            {
                teamMorale.color = red;
            }
            // Update Morality Score Text Color
            if (moralityScoreInt >= 90)
            {
                moralityScore.color = darkGreen;
            }
            else if (moralityScoreInt >= 70)
            {
                moralityScore.color = green;
            }
            else if (moralityScoreInt >= 50)
            {
                moralityScore.color = yellow;
            }
            else
            {
                moralityScore.color = red;
            }
        }
        catch{}
    }

    public void Continue()
    {
        gameManager.GoToDialogue();
    }
}
