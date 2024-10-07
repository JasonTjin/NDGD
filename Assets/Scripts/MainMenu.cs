using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public gameManagerScript gameManager;
    
    private void Update(){
        if (!gameManager){
            try{
                gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<gameManagerScript>();
            }
            catch{}
        }
    }
    public void PlayGame () {
        gameManager.GoToDialogue();
    }

    public void GoToAcknowledgements () {
        gameManager.GoToAcknowledgements();
    }
}
