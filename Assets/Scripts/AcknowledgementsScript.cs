using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcknowledgementsScript : MonoBehaviour
{
    private gameManagerScript gameManager;
   
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<gameManagerScript>();
    }

    public void goToMainMenu(){
        gameManager.goToMainMenu();
    }
}
