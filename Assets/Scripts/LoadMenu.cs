using System.IO;
using Map;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{

    private TMP_Dropdown saveSlotsDropdown;

    private void OnSaveSlotsDropdownValueChanged(int index)
    {
        saveSlotsDropdown.value = index;
        saveSlotsDropdown.RefreshShownValue();
    }

    void initializeSaveSlotsDropdown() 
    {
        Transform saveSlotsDropdownTransform = transform.Find("Dropdown");
        if (saveSlotsDropdownTransform != null)
        {
            saveSlotsDropdown = saveSlotsDropdownTransform.GetComponent<TMP_Dropdown>();
        }

        if (saveSlotsDropdown != null)
        {
            string directoryPath = Application.persistentDataPath;

            if (Directory.Exists(directoryPath))
            {
                string[] filePaths = Directory.GetFiles(directoryPath);

                foreach (string filePath in filePaths)
                {                    
                    saveSlotsDropdown.options.Add(new TMP_Dropdown.OptionData(Path.GetFileNameWithoutExtension(filePath)));
                }
            }
            else
            {
                Debug.LogError("Directory does not exist: " + directoryPath);
            }

            saveSlotsDropdown.RefreshShownValue();
            saveSlotsDropdown.value = 1;
            saveSlotsDropdown.onValueChanged.AddListener(OnSaveSlotsDropdownValueChanged);
        }
        else
        {
            Debug.LogError("Could not find Enviroment dropdown field");
        }
    }

    public void loadGame() {
        PlayerPrefs.DeleteAll();
        string selectedPath = Application.persistentDataPath + "/" + saveSlotsDropdown.options[saveSlotsDropdown.value].text + ".json";

        string saveDataJson = File.ReadAllText(selectedPath);

        SaveData saveData = JsonConvert.DeserializeObject<SaveData>(saveDataJson);

        string mysteryLikelihoodJson = JsonConvert.SerializeObject(saveData.mysteryRoomTypeLikelihoods, Formatting.Indented,
                new JsonSerializerSettings {ReferenceLoopHandling = ReferenceLoopHandling.Ignore});

        string mapsPerLevelJson = JsonConvert.SerializeObject(saveData.mapsPerLevel, Formatting.Indented,
                new JsonSerializerSettings {ReferenceLoopHandling = ReferenceLoopHandling.Ignore});

        PlayerPrefs.SetString("MapsPerLevel", mapsPerLevelJson);
        PlayerPrefs.SetString("Game", saveData.game.ToJson());
        PlayerPrefs.SetInt("PotionLikelihood", saveData.potionLikelihood);
        PlayerPrefs.SetString("MysteryLikelihood", mysteryLikelihoodJson);

        SceneManager.LoadScene("Map");
    }

    void Start()
    {
        initializeSaveSlotsDropdown();
    }

    void Destroy() {
        saveSlotsDropdown.onValueChanged.RemoveAllListeners();
    }
}
