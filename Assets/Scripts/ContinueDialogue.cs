using UnityEngine;

public class ContinueDialogue : MonoBehaviour
{
    public DialogueManager2 dialogueManager;

    private void Start(){
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager2>();
    }
    public void Continue (){
        dialogueManager.GoToNextDialogue();
    }
}
