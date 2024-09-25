using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ContextManagerScript : MonoBehaviour
{
    private gameManagerScript gameManager;
    private TMP_Text title;
    private TMP_Text body;
    private string titleText = "";
    private string bodyText = "";
    private bool inInitialContext = false;

    // Start is called before the first frame update
    void Start()
    {
        //Updates all the text feilds and the manager
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<gameManagerScript>();
        title = GameObject.FindGameObjectWithTag("Title").GetComponent<TMP_Text>();
        body = GameObject.FindGameObjectWithTag("Body").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (titleText != ""){
            title.text = titleText;
        }
        if (bodyText != ""){
            body.text = bodyText;
        }
    }

    public void SetUpContext(string txtFilePath, int choiceNumber)
    {
        if (choiceNumber != 0){
            titleText = "Scenario " + choiceNumber.ToString();
            var reader = new StreamReader(txtFilePath);
            bodyText = reader.ReadLine();
        }
        else {
            inInitialContext = true;
        }
    }

    public void ContinueToChoices(){
        if (inInitialContext){
            gameManager.GoToContext();
        }
        else{
            gameManager.GoToChoices();
        }
    }
}
