using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ChoiceManager : MonoBehaviour
{
    public TMP_Text choice1TextObject;
    public TMP_Text choice2TextObject;
    public TMP_Text choice3TextObject;
    public TMP_Text promptTextObject;
    private gameManagerScript gameManager;
    private int previousNode = 1;
    private int currentNode = 1;
    private List<int> nodes = new();
    private List<string> prompts = new();
    private List<string> choice1Text = new();
    private List<string> choice2Text = new();
    private List<string> choice3Text = new();
    private List<int> choice1NextNode = new();
    private List<int> choice2NextNode = new();
    private List<int> choice3NextNode = new();
    private List<int> FinancialChanges = new();
    private List<int> TeamMoralChanges = new();
    private List<int> MoralityScore1Changes = new();
    private List<int> MoralityScore2Changes = new();
    private List<int> MoralityScore3Changes = new();
    private List<int> MoralityScore4Changes = new();
    private List<int> MoralityScore5Changes = new();
    private List<int> MoralityScore6Changes = new();
    private int resultsStart;

    private void Start(){
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<gameManagerScript>();
    }
    public void HandleChoiceSelection(int selectedOption){
        previousNode = currentNode;
        switch (selectedOption){
            case 0:
                currentNode = choice1NextNode[currentNode - 1];
                break;
            case 1:
                currentNode = choice2NextNode[currentNode - 1];
                break;
            case 2:
                currentNode = choice3NextNode[currentNode - 1];
                break;
            default:
                break;
        }
        if (choice1NextNode[currentNode - 1] == 0){
            ResolveChoiceResults();
        }
        else{
            UpdateChoices();
        }
    }

    public void SetUpChoices(string csvFilePath){
        var reader = new StreamReader(csvFilePath);
        reader.ReadLine(); //skip the titles
        while (!reader.EndOfStream)
        {
            var values = reader.ReadLine().Split(",");
            nodes.Add(Convert.ToInt32(values[0]));
            prompts.Add(values[1].Replace('|', ','));
            choice1Text.Add(values[2].Replace('|', ','));
            choice2Text.Add(values[3].Replace('|', ','));
            choice3Text.Add(values[4].Replace('|', ','));
            choice1NextNode.Add(Convert.ToInt32(values[5]));
            choice2NextNode.Add(Convert.ToInt32(values[6]));
            choice3NextNode.Add(Convert.ToInt32(values[7]));
            FinancialChanges.Add(Convert.ToInt32(values[8]));
            TeamMoralChanges.Add(Convert.ToInt32(values[9]));
            MoralityScore1Changes.Add(Convert.ToInt32(values[10]));
            MoralityScore2Changes.Add(Convert.ToInt32(values[11]));
            MoralityScore3Changes.Add(Convert.ToInt32(values[12]));
            MoralityScore4Changes.Add(Convert.ToInt32(values[13]));
            MoralityScore5Changes.Add(Convert.ToInt32(values[14]));
            MoralityScore6Changes.Add(Convert.ToInt32(values[15]));
        }
        UpdateChoices();
        resultsStart = 0;
        while (prompts[resultsStart] != ""){
            resultsStart += 1;
        }

    }

    private void UpdateChoices(){
        promptTextObject.text = prompts[currentNode - 1];
        choice1TextObject.text = choice1Text[currentNode - 1];
        choice2TextObject.text = choice2Text[currentNode - 1];
        choice3TextObject.text = choice3Text[currentNode - 1];
    }

    private void ResolveChoiceResults(){
        gameManager.UpdateScores(FinancialChanges[previousNode - 1], TeamMoralChanges[previousNode - 1], MoralityScore1Changes[previousNode - 1], MoralityScore2Changes[previousNode - 1], MoralityScore3Changes[previousNode - 1], MoralityScore4Changes[previousNode - 1], MoralityScore5Changes[previousNode - 1], MoralityScore6Changes[previousNode - 1]);
        gameManager.GoToResults(previousNode - resultsStart);
    }
}
