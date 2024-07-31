using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class NarrativeControllerScript : MonoBehaviour
{
    const string NARRATIVE_FILE_PATH = "./assets/narrative.csv";
    private List<string> prompts = new();
    private List<string> options = new();
    private List<int> nextPrompt = new();
    public int currentPrompt = 2;
    public TMP_Text leftArrowText;
    public TMP_Text rightArrowText;
    public TMP_Text upArrowText;
    public TMP_Text promptText;

    private void Awake(){
        using var reader = new StreamReader(NARRATIVE_FILE_PATH);
        reader.ReadLine(); //skip the titles
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            var values = SplitCsvLine(line);

            prompts.Add(values[0]);
            options.Add(values[1]);
            nextPrompt.Add(Convert.ToInt32(values[2]));
        }
    }

    private List<string> SplitCsvLine(string s) {
        List<string> values = new ();
        bool inString = false;
        var subString = "";
        for (int i = 0; i < s.Length; i++) {
            if (inString){
                if (s[i] == '"'){
                    inString = false;
                }
                else{
                    subString += s[i];
                }
            }
            else{
                if (s[i] == '"'){
                    inString = true;
                }
                else{
                    if (s[i] == ','){
                        values.Add(subString);
                        subString = "";
                    }
                    else{
                        subString += s[i];
                    }
                }
            }
        }

        return values;
    }
    public void LeftArrowSelected(){
        Debug.Log("Left Arrow Selected");
        Debug.Log(currentPrompt);
        currentPrompt = nextPrompt[currentPrompt - 2];
        updateDecisionScene();
    }

    public void RightArrowSelected(){
        Debug.Log("Right Arrow Selected");
        currentPrompt = nextPrompt[currentPrompt - 1];
        updateDecisionScene();
    }

    public void UpArrowSelected(){
        Debug.Log("Up Arrow Selected");
        currentPrompt = nextPrompt[currentPrompt];
        updateDecisionScene();
    }

    private void updateDecisionScene(){
        promptText.text = prompts[currentPrompt - 2];
        leftArrowText.text = options[currentPrompt - 2];
        rightArrowText.text = options[currentPrompt - 1];
        upArrowText.text = options[currentPrompt];
    }
}
