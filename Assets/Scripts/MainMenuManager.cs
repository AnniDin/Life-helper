using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{

    bool completedQuestionnaire;
    bool activedQuestionnaire;
    GameObject questionnaire;
    int[] dropdownValues;
    bool[] togglesValues;

    string[] diagnosedDisorders;
    string[] antecedentDisorders;
    string[] medication;
    string pregnancy;
    string diabetes;
    string[] risks;

    int[] diagnosedDisordersValue;
    int[] antecedentDisordersValue;
    int[] medicationValue;
    int pregnancyValue;
    int diabetesValue;
    bool[] risksValue;
    bool contidionValue;

    void Awake()
    {
        completedQuestionnaire = false;
        activedQuestionnaire = false;

        diagnosedDisorders = new string[2];
        antecedentDisorders = new string[2];
        medication = new string[2];
        risks = new string[5];

        diagnosedDisordersValue = new int[2];
        antecedentDisordersValue = new int[2];
        medicationValue = new int[2];
        risksValue = new bool[5];
    }

    // Update is called once per frame
    void Update()
    {
        if (activedQuestionnaire)
        {

            GameObject[] toggles = GameObject.FindGameObjectsWithTag("Toggle");

            if (toggles[0].GetComponent<Toggle>().isOn)
            {
                GameObject.Find("Canvas/Questionnaire/Scroll View/Viewport/Content/Text (TMP)_Last/ButtonFinalice").GetComponent<Button>().interactable = true;
            }
            else
            {
                GameObject.Find("Canvas/Questionnaire/Scroll View/Viewport/Content/Text (TMP)_Last/ButtonFinalice").GetComponent<Button>().interactable = false;
            }
        }
    }

    public void startGameplay()
    {
        if (completedQuestionnaire)
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("currentLevel", "Level1")); 
        }
    }

    public void openQuestionnaire()
    {
        questionnaire = GameObject.Find("Canvas").transform.GetChild(6).gameObject;
        questionnaire.SetActive(true);
        GameObject.Find("Canvas/Questionnaire/Scroll View/Viewport/Content/Text (TMP)_Last/ButtonFinalice").GetComponent<Button>().interactable = false;
        activedQuestionnaire = true;
    }

    public void closeQuestionnaire()
    {
        if (dropdownValues == null && togglesValues == null)
        {
            GameObject[] dropdowns = GameObject.FindGameObjectsWithTag("DropDown");

            foreach (GameObject drop in dropdowns)
            {
                drop.GetComponent<TMP_Dropdown>().value = 0;
            }

            GameObject[] toggles = GameObject.FindGameObjectsWithTag("Toggle");

            foreach (GameObject toggle in toggles)
            {
                toggle.GetComponent<Toggle>().isOn = false;
            }
        }

        else
        {
            GameObject[] dropdowns = GameObject.FindGameObjectsWithTag("DropDown");
            int i = 0;

            foreach (GameObject drop in dropdowns)
            {
                drop.GetComponent<TMP_Dropdown>().value = dropdownValues[i];
                i++;
            }

            GameObject[] toggles = GameObject.FindGameObjectsWithTag("Toggle");
            i = 0;

            foreach (GameObject toggle in toggles)
            {
                toggle.GetComponent<Toggle>().isOn = togglesValues[i];
                i++;
            }
        }

        questionnaire = GameObject.Find("Canvas").transform.GetChild(6).gameObject;
        questionnaire.SetActive(false);
        activedQuestionnaire = false;
    }

    public void finalizeQuestionnaire()
    {   

        GameObject[] dropdowns = GameObject.FindGameObjectsWithTag("DropDown");
        dropdownValues = new int[dropdowns.Length];
        int i = 0;

        foreach (GameObject drop in dropdowns)
        {
            dropdownValues[i] = drop.GetComponent<TMP_Dropdown>().value;

            if (i == 0 )
            {
                diagnosedDisorders[0] = drop.GetComponent<TMP_Dropdown>().options[drop.GetComponent<TMP_Dropdown>().value].text;
                diagnosedDisordersValue[0] = drop.GetComponent<TMP_Dropdown>().value;
            }

            else if (i == 1)
            {
                diagnosedDisorders[1] = drop.GetComponent<TMP_Dropdown>().options[drop.GetComponent<TMP_Dropdown>().value].text;
                diagnosedDisordersValue[1] = drop.GetComponent<TMP_Dropdown>().value;
            }

            else if (i == 2)
            {
                antecedentDisorders[0] = drop.GetComponent<TMP_Dropdown>().options[drop.GetComponent<TMP_Dropdown>().value].text;
                antecedentDisordersValue[0] = drop.GetComponent<TMP_Dropdown>().value;
            }

            else if (i == 3)
            {
                antecedentDisorders[1] = drop.GetComponent<TMP_Dropdown>().options[drop.GetComponent<TMP_Dropdown>().value].text;
                antecedentDisordersValue[1] = drop.GetComponent<TMP_Dropdown>().value;
            }

            else if (i == 4)
            {
                medication[0] = drop.GetComponent<TMP_Dropdown>().options[drop.GetComponent<TMP_Dropdown>().value].text;
                medicationValue[0] = drop.GetComponent<TMP_Dropdown>().value;
            }

            else if (i == 5)
            {
                medication[1] = drop.GetComponent<TMP_Dropdown>().options[drop.GetComponent<TMP_Dropdown>().value].text;
                medicationValue[1] = drop.GetComponent<TMP_Dropdown>().value;
            }

            else if (i == 6)
            {
                pregnancy = drop.GetComponent<TMP_Dropdown>().options[drop.GetComponent<TMP_Dropdown>().value].text;
                pregnancyValue = drop.GetComponent<TMP_Dropdown>().value;
            }

            else if (i == 7)
            {
                diabetes = drop.GetComponent<TMP_Dropdown>().options[drop.GetComponent<TMP_Dropdown>().value].text;
                diabetesValue = drop.GetComponent<TMP_Dropdown>().value;
            }

            i++;
        }

        GameObject[] toggles = GameObject.FindGameObjectsWithTag("Toggle");
        togglesValues = new bool[toggles.Length];
        i = 0;

        foreach (GameObject toggle in toggles)
        {
            togglesValues[i] = toggle.GetComponent<Toggle>().isOn;

            if (i != 0)
            {
                if (togglesValues[i])
                {
                    risks[i - 1] = toggle.transform.GetChild(1).GetComponent<Text>().text;
                }

                risksValue[i - 1] = toggle.GetComponent<Toggle>().isOn;
            }
            else
            {
                contidionValue = toggle.GetComponent<Toggle>().isOn;
            }

            i++;
        }

        questionnaire = GameObject.Find("Canvas").transform.GetChild(6).gameObject;
        questionnaire.SetActive(false);
        completedQuestionnaire = true;
        activedQuestionnaire = false;
    }

    public void exit()
    {
        Application.Quit();
    }

    public void openSettings()
    {
        GameObject.Find("Canvas").transform.GetChild(7).gameObject.SetActive(true);
    }

    public void closeSettings()
    {
        GameObject.Find("Canvas").transform.GetChild(7).gameObject.SetActive(false);
    }

    public void openReferences()
    {
        GameObject.Find("Canvas/Settings/References").SetActive(true);
    }

    public void closeReferences()
    {
        GameObject.Find("Canvas/Settings/References").SetActive(false);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt("diabetesValue", diabetesValue);
        PlayerPrefs.SetInt("pregnancyValue", pregnancyValue);
        PlayerPrefs.SetInt("medicationValue0", medicationValue[0]);
        PlayerPrefs.SetInt("medicationValue1", medicationValue[1]);
        PlayerPrefs.SetInt("antecedentDisordersValue0", antecedentDisordersValue[0]);
        PlayerPrefs.SetInt("antecedentDisordersValue1", antecedentDisordersValue[1]);
        PlayerPrefs.SetInt("diagnosedDisordersValue0", diagnosedDisordersValue[0]);
        PlayerPrefs.SetInt("diagnosedDisordersValue1", diagnosedDisordersValue[1]);
        PlayerPrefs.SetString("risksValue0", risksValue[0].ToString());
        PlayerPrefs.SetString("risksValue1", risksValue[1].ToString());
        PlayerPrefs.SetString("risksValue2", risksValue[2].ToString());
        PlayerPrefs.SetString("risksValue3", risksValue[3].ToString());
        PlayerPrefs.SetString("risksValue4", risksValue[4].ToString());
        PlayerPrefs.SetString("contidionValue", contidionValue.ToString());
    }

    private void OnEnable()
    {
        diabetesValue = PlayerPrefs.GetInt("diabetesValue", 0);
        pregnancyValue = PlayerPrefs.GetInt("pregnancyValue", 0);
        medicationValue[0] = PlayerPrefs.GetInt("medicationValue0", 0);
        medicationValue[1] = PlayerPrefs.GetInt("medicationValue1", 0);
        antecedentDisordersValue[0] = PlayerPrefs.GetInt("antecedentDisordersValue0", 0);
        antecedentDisordersValue[1] = PlayerPrefs.GetInt("antecedentDisordersValue1", 0);
        diagnosedDisordersValue[0] = PlayerPrefs.GetInt("diagnosedDisordersValue0", 0);
        diagnosedDisordersValue[1] = PlayerPrefs.GetInt("diagnosedDisordersValue1", 0);

        if (PlayerPrefs.GetString("risksValue0", "false") == "true")
            risksValue[0] = true;
        else
            risksValue[0] = false;

        if (PlayerPrefs.GetString("risksValue1", "false") == "true")
            risksValue[1] = true;
        else
            risksValue[1] = false;

        if (PlayerPrefs.GetString("risksValue2", "false") == "true")
            risksValue[2] = true;
        else
            risksValue[2] = false;

        if (PlayerPrefs.GetString("risksValue3", "false") == "true")
            risksValue[3] = true;
        else
            risksValue[3] = false;

        if (PlayerPrefs.GetString("risksValue4", "false") == "true")
            risksValue[4] = true;
        else
            risksValue[4] = false;

        if (PlayerPrefs.GetString("contidionValue", "false") == "true")
            contidionValue = true;
        else
            contidionValue = false;


        GameObject[] dropdowns = GameObject.FindGameObjectsWithTag("DropDown");
        int i = 0;

        foreach (GameObject drop in dropdowns)
        {

            if (i == 0)
            {
                drop.GetComponent<TMP_Dropdown>().value = diagnosedDisordersValue[0];
                diagnosedDisorders[0] = drop.GetComponent<TMP_Dropdown>().options[drop.GetComponent<TMP_Dropdown>().value].text;
            }

            else if (i == 1)
            {
                drop.GetComponent<TMP_Dropdown>().value = diagnosedDisordersValue[1];
                diagnosedDisorders[1] = drop.GetComponent<TMP_Dropdown>().options[drop.GetComponent<TMP_Dropdown>().value].text;
            }

            else if (i == 2)
            {
                drop.GetComponent<TMP_Dropdown>().value = antecedentDisordersValue[0];
                antecedentDisorders[0] = drop.GetComponent<TMP_Dropdown>().options[drop.GetComponent<TMP_Dropdown>().value].text;
            }

            else if (i == 3)
            {
                drop.GetComponent<TMP_Dropdown>().value = antecedentDisordersValue[1];
                antecedentDisorders[1] = drop.GetComponent<TMP_Dropdown>().options[drop.GetComponent<TMP_Dropdown>().value].text;
            }

            else if (i == 4)
            {
                drop.GetComponent<TMP_Dropdown>().value = medicationValue[0];
                medication[0] = drop.GetComponent<TMP_Dropdown>().options[drop.GetComponent<TMP_Dropdown>().value].text;
            }

            else if (i == 5)
            {
                drop.GetComponent<TMP_Dropdown>().value = medicationValue[1];
                medication[1] = drop.GetComponent<TMP_Dropdown>().options[drop.GetComponent<TMP_Dropdown>().value].text;
            }

            else if (i == 6)
            {
                drop.GetComponent<TMP_Dropdown>().value = pregnancyValue;
                pregnancy = drop.GetComponent<TMP_Dropdown>().options[drop.GetComponent<TMP_Dropdown>().value].text;
            }

            else if (i == 7)
            {
                drop.GetComponent<TMP_Dropdown>().value = diabetesValue;
                diabetes = drop.GetComponent<TMP_Dropdown>().options[drop.GetComponent<TMP_Dropdown>().value].text;
            }

            i++;
        }

        GameObject[] toggles = GameObject.FindGameObjectsWithTag("Toggle");
        i = 0;

        foreach (GameObject toggle in toggles)
        {
            if (i != 0)
            {
                toggle.GetComponent<Toggle>().isOn = risksValue[i - 1];

                if (toggle.GetComponent<Toggle>().isOn)
                {
                    risks[i - 1] = toggle.transform.GetChild(1).GetComponent<Text>().text;
                }
            }
            else
            {
                toggle.GetComponent<Toggle>().isOn = contidionValue;
            }

            i++;
        }

    }
}
