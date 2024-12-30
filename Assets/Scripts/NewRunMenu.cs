using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewRunMenu : MonoBehaviour
{
    public TMP_InputField partyNameInput;
    public TMP_InputField partySizeInput;
    public TMP_InputField partyLevelInput;

    private Game game;

    void initializePartyNameInput() 
    {
        Transform partyNameInputTransform = transform.Find("Panel/PartyName/PartyNameInput");
        if (partyNameInputTransform != null)
        {
            partyNameInput = partyNameInputTransform.GetComponent<TMP_InputField>();
        }

        if (partyNameInput != null)
        {
            partyNameInput.text = game.gameName;
        }
        else
        {
            Debug.LogError("Could not find PartyName field");
        }
    }

    void initializePartyLevelInput() 
    {
        Transform partyLevelInputTransform = transform.Find("Panel/PartyLevel/PartyLevelNumber");
        if (partyLevelInputTransform != null)
        {
            partyLevelInput = partyLevelInputTransform.GetComponent<TMP_InputField>();
        }

        if (partyLevelInput != null)
        {
            partyLevelInput.text = game.partyLevel.ToString();
        }
        else
        {
            Debug.LogError("Could not find PartyLevel field");
        }
    }

    void initializePartySizeInput() 
    {
        Transform partySizeInputTransform = transform.Find("Panel/PartySize/PartySizeNumber");
        if (partySizeInputTransform != null)
        {
            partySizeInput = partySizeInputTransform.GetComponent<TMP_InputField>();
        }

        if (partySizeInput != null)
        {
            partySizeInput.text = game.partySize.ToString();
        }
        else
        {
            Debug.LogError("Could not find PartySize field");
        }
    }

    void initializeEnvironmentDropdown() 
    {
        Transform partySizeInputTransform = transform.Find("Panel/PartySize/PartySizeNumber");
        if (partySizeInputTransform != null)
        {
            partySizeInput = partySizeInputTransform.GetComponent<TMP_InputField>();
        }

        if (partySizeInput != null)
        {
            partySizeInput.text = game.partySize.ToString();
        }
        else
        {
            Debug.LogError("Could not find PartySize field");
        }
    }

    void Start()
    {
        game = new Game();
        initializePartyNameInput();
        initializePartyLevelInput();
        initializePartySizeInput();
        initializeEnvironmentDropdown();
    }

    void OnDestroy()
    {
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
