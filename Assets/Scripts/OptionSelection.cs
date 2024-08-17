using UnityEngine;

public class OptionSelection : MonoBehaviour
{
    public ChoiceManager choiceManager;
    public int choiceIndex;
    public void SelectOption (){
        choiceManager.HandleChoiceSelection(choiceIndex);
    }
}
