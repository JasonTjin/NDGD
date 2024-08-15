using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using TMPro;
using System.Net.WebSockets;

public class OptionManager : MonoBehaviour
{   
    public TextAsset csvFile;
    public Animator optionAnimator;
    public Animator promptAnimator;
    public TMP_Text option1text; 
    public TMP_Text option2text;
    public TMP_Text option3text;
    public TMP_Text prompt;

        // Start is called before the first frame update
    void Start()
    {
        LoadOption(csvFile);
        DisplayOption();
    }

    public void LoadOption(TextAsset file)
    {
        StringReader reader = new StringReader(file.text);
        bool headerSkipped = false;

        while (reader.Peek() != -1)
        {
            var line = reader.ReadLine();
            // Skipping csv header line
            if (!headerSkipped)
            {
                headerSkipped = true;
                continue;
            }

            var values = line.Split(",");
            prompt.text = values[0].Replace('|', ',');
            option1text.text = values[1].Replace('|', ',');
            option2text.text = values[2].Replace('|', ',');
            option3text.text = values[3].Replace('|', ',');
        }


    }

    public void DisplayOption(){
        optionAnimator.SetBool("isOpen", true);
        promptAnimator.SetBool("isOpen", true);
    }
}