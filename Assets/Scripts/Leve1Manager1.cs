using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Leve1Manager1 : MonoBehaviour
{

    public List<string> sentences;
    public GameObject textBox;
    public GameObject nameBox;
    public GameObject continueButton;
    public GameObject Kairi;
    public GameObject[] options;
    public GameObject[] fragmentsUbications;
    public GameObject canvas3D;
    public GameObject canvasVisualNovel;
    public float typingSpeed;
    public GameObject pauseMenu;

    private bool pauseMenuEnable;

    private blinkEffect blink;
    private Dialog dialog;

    public Material blinkMaterial;

    private bool activeBlink;
    private bool addSentences;
    private float m_Timer;

    private List<string> selecctions;

    [SerializeField]
    private Camera cam;

    public Texture2D cursorTexture;

    private bool fragmentFound;
    private bool returned;

    private pickFragment pick;

    private string actualRoom;

    private Vector2 punctuation;

    private Vector2 option1Punctuation;
    private Vector2 option2Punctuation;
    private Vector2 option3Punctuation;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize varibales, specify the name of the first character to speak and add the first sentences
        //Start conversation
    }

    // Update is called once per frame
    void Update()
    {
        //ONLY LEVEL 1: wait for the blinking effect to end and add the following phrases

        //Change the cursor to a magnifying glass if it is on a space to investigate
        //Detect if the mouse has been clicked on any of the spaces available to investigate, 
        //whose collider is only available during the search mini-game, 
        //and transport player to it enabling camera movement


        //Check if the talisman fragment has been clicked

    }

    public void returnToVisualNovel()
    {
        //Returns the player to the visual novel and disables camera movement.
    }

    public void continueChat()
    {
        //The last sentence is examined to know how the conversation should continue

        if (dialog.lastSentence.Contains("Example sentence"))
        {
            //Actions
        }

        else if (dialog.lastSentence.Contains("Example sentence"))
        {
            //According to the last sentence it can mean that it is time for the player to make a choice

            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    //Modify the text of the option box
                    //Specify the score corresponding to this option
                }
                else if (option.name == "option2")
                {
                    //Modify the text of the option box
                    //Specify the score corresponding to this option
                }
                else if (option.name == "option3")
                {
                    //Modify the text of the option box
                    //Specify the score corresponding to this option
                }
            }
        }

        else
        {
            //If there is no specific action to perform, the dialog simply continues.

            dialog.NextSentence(sentences, typingSpeed, textBox, nameBox, continueButton, Kairi);
        }

    }

    public void selectOption1()
    {

        //The selected option is checked and stored

        foreach (GameObject option in options)
        {
            if (option.name == "option1")
            {
                selecctions.Add(option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text);
            }

            option.SetActive(false);
        }

        
        punctuation += option1Punctuation;

        //The checkSelections() function is called and the dialog continues.
        checkSelections();
        dialog.NextSentence(sentences, typingSpeed, textBox, nameBox, continueButton, Kairi);
    }

    public void selectOption2()
    {
        foreach (GameObject option in options)
        {
            if (option.name == "option2")
            {
                selecctions.Add(option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text);
            }

            option.SetActive(false);
        }

        checkSelections();
        dialog.NextSentence(sentences, typingSpeed, textBox, nameBox, continueButton, Kairi);
    }


    public void selectOption3()
    {
        foreach (GameObject option in options)
        {
            if (option.name == "option3")
            {
                selecctions.Add(option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text);
            }

            option.SetActive(false);
        }

        checkSelections();
        dialog.NextSentence(sentences, typingSpeed, textBox, nameBox, continueButton, Kairi);
    }

    private void checkSelections()
    {
        //It checks which option has been selected

        if (selecctions.Contains("Example sentence"))
        {
            //The sentence that corresponds to the answer to said selection is added
        }
    }

}
