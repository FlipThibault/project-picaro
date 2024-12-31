using System.Collections;
using Assets.Scripts;
using Map;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    private TextMeshProUGUI partyInfoText;
    private Transform partyInfoTextTransform;

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

    IEnumerator DelayedAction(float delay)
    {
        Debug.Log("Execution started...");
        
        // Wait for the specified amount of time
        yield return new WaitForSeconds(delay);
        initializePartyInfoTextField();
    }
        
    void Start()
    {        
        StartCoroutine(DelayedAction(0f));
    }
}
