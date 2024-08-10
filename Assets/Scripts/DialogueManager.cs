using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class DialogueManager : MonoBehaviour
{
    public TextAsset csvFile;
    private Dictionary<int, Dialogue> dialogueNodes;

    public ButtonManager buttonManager;
    public Animator animator;

    public TMP_Text dialogueText; // Uncomment if using TextMeshPro
    public TMP_Text speakerText;

    private int currentNodeId = 1;
    // Start is called before the first frame update
    void Start()
    {
        LoadDialogue(csvFile);
        StartDialogue(1);
    }

    void LoadDialogue(TextAsset file)
    {
        Debug.Log(file.text);
        dialogueNodes = new Dictionary<int, Dialogue>();
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
            //Assign values of each dialogue line
            int nodeId = int.Parse(values[0]);
            string speaker = values[1];
            string text = values[2].Replace('|', ',');
            
            int? nextNodeId;
            if (string.IsNullOrEmpty(values[3]))
            {
                nextNodeId = null;
            }
            else
            {
                nextNodeId = int.Parse(values[3]);
            }

            // Init dialogue object
            Dialogue dialogue = new Dialogue
            {
                nodeId = nodeId,
                name = speaker,
                text = text,
                nextNodeId = nextNodeId
            };

            dialogueNodes[nodeId] = dialogue;

        }
    }

    void StartDialogue(int startNodeId)
    {
        animator.SetBool("isOpen", true);
        DisplayNode(dialogueNodes[startNodeId]);
    }

    void DisplayNode(Dialogue dialogue)
    {
        speakerText.text = dialogue.name;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(dialogue.text));
    }

    IEnumerator TypeSentence (String s) {
        dialogueText.text = "";
        foreach (char letter in s.ToCharArray()){
            dialogueText.text += letter;
            yield return null;
        }
    }
    public void DisplayNextNode()
    {
        if (dialogueNodes[currentNodeId].nextNodeId.HasValue)
        {
            currentNodeId = dialogueNodes[currentNodeId].nextNodeId.Value;
            DisplayNode(dialogueNodes[currentNodeId]);
        }
        else
        {
            animator.SetBool("isOpen", false);
            buttonManager.DialogueOver();
            dialogueText.text = "";
            speakerText.text = "";
        }
    }
}
