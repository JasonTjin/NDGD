using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeControllerScript : MonoBehaviour
{
    public int storyStatus;
    public int choice;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LeftArrowSelected(){
        Debug.Log("Left Arrow Selected");
    }

    public void RightArrowSelected(){
        Debug.Log("Right Arrow Selected");
    }

    public void UpArrowSelected(){
        Debug.Log("Up Arrow Selected");
    }
}
