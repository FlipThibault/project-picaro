using Map;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewRunMenu : MonoBehaviour
{
    private TMP_InputField partyNameInput;
    private TMP_InputField partySizeInput;
    private TMP_InputField partyLevelInput;
    private TMP_Dropdown environmentDropdown;

    private Game game;

    private void initializePartyNameInput() 
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

    private void initializePartyLevelInput() 
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

    private void initializePartySizeInput() 
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
    private void OnEnvironmentDropdownValueChanged(int index)
    {
        environmentDropdown.value = index;
        environmentDropdown.RefreshShownValue();
    }

    void initializeEnvironmentDropdown() 
    {
        Transform environmentDropdownTransform = transform.Find("Panel/Environment/EnvironmentDropdown/TextArea/Dropdown");
        if (environmentDropdownTransform != null)
        {
            environmentDropdown = environmentDropdownTransform.GetComponent<TMP_Dropdown>();
        }

        if (environmentDropdown != null)
        {
            foreach (Environment env in System.Enum.GetValues(typeof(Environment)))
            {
                // Convert enum value to string representation
                string envString = env.ToString();

                // Output the enum value
                environmentDropdown.options.Add(new TMP_Dropdown.OptionData(envString));
            }
            environmentDropdown.RefreshShownValue();
            environmentDropdown.value = 0;
            environmentDropdown.onValueChanged.AddListener(OnEnvironmentDropdownValueChanged);
        }
        else
        {
            Debug.LogError("Could not find Enviroment dropdown field");
        }
    }

    private void setGameValues() {
        game.gameName = partyNameInput.text;
        game.partyLevel = int.Parse(partyLevelInput.text);
        game.partySize = int.Parse(partySizeInput.text);
        game.environment = (Environment)System.Enum.GetValues(typeof(Environment)).GetValue(environmentDropdown.value);
    }

    public void createGame() {
        PlayerPrefs.DeleteAll();
        setGameValues();
        string gameJson = JsonConvert.SerializeObject(game, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        PlayerPrefs.SetString("Game", gameJson);
        SceneManager.LoadScene("Map");
    }

    void Start()
    {
        game = new Game();
        initializePartyNameInput();
        initializePartyLevelInput();
        initializePartySizeInput();
        initializeEnvironmentDropdown();
    }

    void Destroy() {
        environmentDropdown.onValueChanged.RemoveAllListeners();
    }

}
