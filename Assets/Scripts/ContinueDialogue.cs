using UnityEngine;

public class ContinueDialogue : MonoBehaviour
{
    public DialogueManager2 dialogueManager;

    void Update(){
        if (!dialogueManager){
            try{
                dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager2>();
            }
            catch{}
        }
    }
    public void Continue (){
        dialogueManager.GoToNextDialogue();
    }
}
