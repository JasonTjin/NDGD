using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public gameManagerScript gameManager;

    private void Start(){
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<gameManagerScript>();
    }
    public void PlayGame () {
        gameManager.GoToDialogue();
    }

    public void GoToAcknowledgements () {
        gameManager.GoToAcknowledgements();
    }
}
