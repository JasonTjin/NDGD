using UnityEngine;

public class OptionSelection : MonoBehaviour
{
    private ChoiceManager choiceManager;
    public int choiceIndex;

    private void Start(){
        choiceManager = GameObject.FindGameObjectWithTag("ChoiceManager").GetComponent<ChoiceManager>();
    }
    public void SelectOption (){
        choiceManager.HandleChoiceSelection(choiceIndex);
    }
}
