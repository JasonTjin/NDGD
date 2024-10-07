using UnityEngine;

public class OptionSelection : MonoBehaviour
{
    private ChoiceManager choiceManager;
    public int choiceIndex;

    void Update(){
        if (!choiceManager){
            try{
                choiceManager = GameObject.FindGameObjectWithTag("ChoiceManager").GetComponent<ChoiceManager>();
            }
            catch{}
        }
    }
    public void SelectOption (){
        choiceManager.HandleChoiceSelection(choiceIndex);
    }
}
