using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeControllerScript : MonoBehaviour
{
    public string leftArrowText = "";
    public string rightArrowText = "";
    public string upArrowText = "";
    public TMP_Text leftArrowTextComponent;
    public TMP_Text rightArrowTextComponent;
    public TMP_Text upArrowTextComponent;

    public void LeftArrowSelected(){
        Debug.Log("Left Arrow Selected");
        updateArrows();
    }

    public void RightArrowSelected(){
        Debug.Log("Right Arrow Selected");
        updateArrows();
    }

    public void UpArrowSelected(){
        Debug.Log("Up Arrow Selected");
        updateArrows();
    }

    private void updateArrows(){
        leftArrowTextComponent.text = leftArrowText;
        rightArrowTextComponent.text = rightArrowText;
        upArrowTextComponent.text = upArrowText;
    }
}
