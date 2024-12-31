using System.Collections;
using Assets.Scripts;
using Map;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Map {
    
}
public class CanvasManager : MonoBehaviour
{
    // private MapManager mapManager;
    private TextMeshProUGUI partyInfoText;
    private Button nextLevelButton;
    private Button previousLevelButton;

    private Transform partyInfoTextTransform;
    private Transform nextLevelButtonTransform;
    private Transform previousLevelButtonTransform;

    private void initializePartyInfoTextField() 
    {
        if(partyInfoTextTransform == null) {
            partyInfoTextTransform = transform.Find("PartyInfoText");
        }
        if (partyInfoTextTransform != null)
        {
            partyInfoText = partyInfoTextTransform.GetComponent<TextMeshProUGUI>();
        }

        if (partyInfoText != null)
        {
            partyInfoText.text = "Party Name: " + GameManager.currentGame.gameName + " - Party Level: " + GameManager.currentGame.partyLevel + " - Party Size: " + GameManager.currentGame.partySize;
        }
        else
        {
            Debug.LogError("Could not find PartyInfoText field");
        }
    }

    void initializeNextLevelButton() {
        if(nextLevelButtonTransform == null) {
            nextLevelButtonTransform = transform.Find("NextLevelButton");
        }
        if (nextLevelButtonTransform != null)
        {
            nextLevelButton = nextLevelButtonTransform.GetComponent<Button>();
        }
    }

    void initializePreviousLevelButton() {
        if(previousLevelButtonTransform == null) {
            previousLevelButtonTransform = transform.Find("PreviousLevelButton");
        }
        if (previousLevelButtonTransform != null)
        {
            previousLevelButton = previousLevelButtonTransform.GetComponent<Button>();
        }
    }

    IEnumerator DelayedAction(float delay)
    {
        Debug.Log("Execution started...");
        
        // Wait for the specified amount of time
        yield return new WaitForSeconds(delay);
        initializePartyInfoTextField();
        initializePreviousLevelButton();
        initializeNextLevelButton();
    }

    void updateNextLevelButtonEnabled() {
        nextLevelButton.interactable = MapManager.CurrentMap.isComplete;
    }
    void updatePreviousLevelButtonEnabled() {
        //CONDITION: should be based on previous
        previousLevelButton.interactable = false;
    }
        
    void Start()
    {        
        StartCoroutine(DelayedAction(0f));
    }

    void Update() {
        updateNextLevelButtonEnabled();
        updatePreviousLevelButtonEnabled();
    }
}
