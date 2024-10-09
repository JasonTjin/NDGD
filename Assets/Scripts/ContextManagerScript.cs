using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class ContextManagerScript : MonoBehaviour
{
    private gameManagerScript gameManager;
    private TMP_Text title;
    private TMP_Text body;
    private string titleText = "";
    private string bodyText = "";
    private bool inInitialContext = false;

    // Update is called once per frame
    void Update()
    {
        //Updates all the text feilds and the manager
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
        if (titleText != ""){
            title.text = titleText;
        }
        if (bodyText != ""){
            body.text = bodyText;
        }
    }

    public async void SetUpContext(string txtFilePath, int choiceNumber)
    {
        if (choiceNumber != 0){
            titleText = "Scenario " + choiceNumber.ToString();
            string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, txtFilePath);
            UnityWebRequest request = UnityWebRequest.Get(filePath);
            UnityWebRequestAsyncOperation operation = request.SendWebRequest();
            while (!operation.isDone)
            {
                await Task.Yield();
            }
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Cannot load file at " + filePath);
                return;
            }
            bodyText = request.downloadHandler.text;
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
