using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndManagerScript : MonoBehaviour
{
    private int financialScore;
    private int teamMoralScore; 
    private int moralityScore1;
    private int moralityScore2;
    private int moralityScore3;
    private int moralityScore4;
    private int moralityScore5;
    private int moralityScore6;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        int FinancialScoreBiggestLossDecisionIndexFinal,
        int TeamMoralScoreBiggestLossDecisionIndexFinal,
        int MoralityScore1BiggestLossDecisionIndexFinal,
        int MoralityScore2BiggestLossDecisionIndexFinal,
        int MoralityScore3BiggestLossDecisionIndexFinal,
        int MoralityScore4BiggestLossDecisionIndexFinal,
        int MoralityScore5BiggestLossDecisionIndexFinal,
        int MoralityScore6BiggestLossDecisionIndexFinal)
    {
        financialScore = financialScoreFinal;
        teamMoralScore = teamMoralScoreFinal;
        moralityScore1 = moralityScore1Final;
        moralityScore2 = moralityScore2Final;
        moralityScore3 = moralityScore3Final;
        moralityScore4 = moralityScore4Final;
        moralityScore5 = moralityScore5Final;
        moralityScore6 = moralityScore6Final;
        FinancialScoreBiggestLossDecisionIndex = FinancialScoreBiggestLossDecisionIndexFinal;
        TeamMoralScoreBiggestLossDecisionIndex = TeamMoralScoreBiggestLossDecisionIndexFinal;
        MoralityScore1BiggestLossDecisionIndex = MoralityScore1BiggestLossDecisionIndexFinal;
        MoralityScore2BiggestLossDecisionIndex = MoralityScore2BiggestLossDecisionIndexFinal;
        MoralityScore3BiggestLossDecisionIndex = MoralityScore3BiggestLossDecisionIndexFinal;
        MoralityScore4BiggestLossDecisionIndex = MoralityScore4BiggestLossDecisionIndexFinal;
        MoralityScore5BiggestLossDecisionIndex = MoralityScore5BiggestLossDecisionIndexFinal;
        MoralityScore6BiggestLossDecisionIndex = MoralityScore6BiggestLossDecisionIndexFinal;
    }
}
