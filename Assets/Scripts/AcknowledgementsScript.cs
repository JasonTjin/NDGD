using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcknowledgementsScript : MonoBehaviour
{
    private gameManagerScript gameManager;
   
    void Update(){
        if (!gameManager){
            try{
                gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<gameManagerScript>();
            }
            catch{}
        }
    }

    public void goToMainMenu(){
        gameManager.goToMainMenu();
    }
}
