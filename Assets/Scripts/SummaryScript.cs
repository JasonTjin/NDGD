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

    // Update is called once per frame
    public void UpdateScores(int newFinanceScore, int newTeamMorale, int newMoralityScore)
    {
        financeScore.text = newFinanceScore.ToString();
        teamMorale.text = newTeamMorale.ToString();
        moralityScore.text = newMoralityScore.ToString();
    }

    void Start(){
        financeScore = GameObject.FindGameObjectWithTag("FinanceScore").GetComponent<TMP_Text>();
        teamMorale = GameObject.FindGameObjectWithTag("TeamMorale").GetComponent<TMP_Text>();
        moralityScore = GameObject.FindGameObjectWithTag("MoralityScore").GetComponent<TMP_Text>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<gameManagerScript>();
    }

    public void Continue(){
        gameManager.GoToDialogue();
    }
}
