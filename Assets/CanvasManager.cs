using System.Collections;
using System.Linq;
using Assets.Scripts;
using Map;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CanvasManager : MonoBehaviour
{
    private TextMeshProUGUI partyInfoText;
    private TextMeshProUGUI levelProgressText;
    private Button nextLevelButton;
    private Button previousLevelButton;
    private Button generateLevelButton;
    private MapManager mapManager;

    private Transform partyInfoTextTransform;
    private Transform levelProgressTextTransform;

    private Transform nextLevelButtonTransform;
    private Transform previousLevelButtonTransform;
    private Transform generateLevelButtonTransform;

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
            updatePartyInfoText();
        }
        else
        {
            Debug.LogError("Could not find PartyInfoText field");
        }
    }

    private void initializeLevelProgressTextField() 
    {
        if(levelProgressTextTransform == null) {
            levelProgressTextTransform = transform.Find("CurrentLevelText");
        }
        if (levelProgressTextTransform != null)
        {
            levelProgressText = levelProgressTextTransform.GetComponent<TextMeshProUGUI>();
        }

        if (levelProgressText != null)
        {
            updateLevelProgressText();
        }
        else
        {
            Debug.LogError("Could not find CurrentLevelText field");
        }
    }

    private void updatePartyInfoText() {
        if(partyInfoText != null) {
            partyInfoText.text = "Party Name: " + GameManager.currentGame.gameName + " - Party Level: " + GameManager.currentGame.partyLevel + " - Party Size: " + GameManager.currentGame.partySize + " - Environment: " + GameManager.currentGame.environment;
        }
    }

    private void updateLevelProgressText() {
        if(levelProgressText != null) {
            levelProgressText.text = MapManager.currentLevel + "/20";
        }
    }

    void initializeNextLevelButton() {
        if(nextLevelButtonTransform == null) {
            nextLevelButtonTransform = transform.Find("NextLevelButton");
        }
        if (nextLevelButtonTransform != null)
        {
            nextLevelButton = nextLevelButtonTransform.GetComponent<Button>();
            nextLevelButton.onClick.AddListener(mapManager.IncrementLevel);
        }
    }

    void initializePreviousLevelButton() {
        if(previousLevelButtonTransform == null) {
            previousLevelButtonTransform = transform.Find("PreviousLevelButton");
        }
        if (previousLevelButtonTransform != null)
        {
            previousLevelButton = previousLevelButtonTransform.GetComponent<Button>();
            previousLevelButton.onClick.AddListener(mapManager.GoBackLevel);
        }
    }

    void initializeGenerateLevelButton() {
        if(generateLevelButtonTransform == null) {
            generateLevelButtonTransform = transform.Find("GenerateButton");
        }
        if (generateLevelButtonTransform != null)
        {
            generateLevelButton = generateLevelButtonTransform.GetComponent<Button>();
        }
    }

    IEnumerator DelayedAction(float delay)
    {
        Debug.Log("Execution started...");
        mapManager = FindObjectOfType<MapManager>();
        // Wait for the specified amount of time
        yield return new WaitForSeconds(delay);
        initializePartyInfoTextField();
        initializePreviousLevelButton();
        initializeGenerateLevelButton();
        initializeNextLevelButton();
        initializeLevelProgressTextField();
    }

    void updateNextLevelButtonEnabled() {
        if(nextLevelButton != null && MapManager.getCurrentMap() != null) {
            nextLevelButton.interactable = MapManager.getCurrentMap().isComplete;
        }
    }
    void updatePreviousLevelButtonEnabled() {
        if(previousLevelButton != null && MapManager.getCurrentMap() != null) {
            previousLevelButton.interactable = MapManager.mapsPerLevel.Keys.First() < MapManager.currentLevel ;
        }
    }
    void updateGenerateButtonEnabled() {
        if(generateLevelButton != null && MapManager.getCurrentMap() != null) {
            Debug.Log("HERE: " + MapManager.mapsPerLevel.ToString());
            generateLevelButton.interactable = !MapManager.mapsPerLevel[MapManager.currentLevel].isInProgress ;
        }
    }

    void Start()
    {        
        StartCoroutine(DelayedAction(0f));
    }

    void Update() {
        updateNextLevelButtonEnabled();
        updatePreviousLevelButtonEnabled();
        updateGenerateButtonEnabled();
        updatePartyInfoText();
        updateLevelProgressText();
    }
}
