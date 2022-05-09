using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level4Manager : MonoBehaviour
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


    private Dialog dialog;

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
        selecctions = new List<string>();

        fragmentFound = false;
        returned = false;
        actualRoom = null;
        punctuation = new Vector2(0, 0);
        option1Punctuation = new Vector2(0, 0);
        option2Punctuation = new Vector2(0, 0);
        option3Punctuation = new Vector2(0, 0);


        typingSpeed = PlayerPrefs.GetFloat("textSpeed", 0.01f); ;
        dialog = gameObject.GetComponent<Dialog>();

        nameBox.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Kairi";

        pick = GameObject.Find("PowerAmulet").gameObject.GetComponent<pickFragment>();

        sentences.Add("");
        sentences.Add("¡Hala! Qué casa más bonita.");
        sentences.Add("Es la primera vez que veo una así desde que llegué a la tierra.");
        sentences.Add("Aunque tampoco es que haga mucho tiempo de eso, jeje.");
        sentences.Add("Me pregunto quien vivirá aquí.");
        sentences.Add("Seguro que es una persona exitosa, incluso podría ser un famoso.");
        sentences.Add("¿Tú qué dices?");
        sentences.Add("¿No te gustaría vivir en una casa así?");

        continueChat();

    }

    // Update is called once per frame
    void Update()
    {

        Vector2 mousePos = Input.mousePosition;
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit)
        {
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
            if (Input.GetMouseButtonDown(0))
            {
                canvasVisualNovel.SetActive(false);
                canvas3D.SetActive(true);
                cam.GetComponent<cameraMovement>().enabled = true;
                if (hit.collider.name == "Puerta")
                {
                    cam.transform.position = new Vector3(1.8f, 2.69f, 26.98f);
                    cam.transform.eulerAngles = new Vector3(10f, 270f, 0f);
                    cam.GetComponent<cameraMovement>().actualX = 10f;
                    cam.GetComponent<cameraMovement>().actualY = 270f;
                    cam.GetComponent<cameraMovement>().actualZ = 0f;
                    cam.GetComponent<cameraMovement>().initialX = 10f;
                    cam.GetComponent<cameraMovement>().initialY = 270f;
                    cam.GetComponent<cameraMovement>().initialZ = 0f;
                    actualRoom = "Puerta";
                }
                else if (hit.collider.name == "Ventana")
                {
                    cam.transform.position = new Vector3(1.66f, 3.48f, 21.54f);
                    cam.transform.eulerAngles = new Vector3(10f, 270f, 0f);
                    cam.GetComponent<cameraMovement>().actualX = 10f;
                    cam.GetComponent<cameraMovement>().actualY = 270f;
                    cam.GetComponent<cameraMovement>().actualZ = 0f;
                    cam.GetComponent<cameraMovement>().initialX = 10f;
                    cam.GetComponent<cameraMovement>().initialY = 270f;
                    cam.GetComponent<cameraMovement>().initialZ = 0f;
                    actualRoom = "Ventana";
                }
            }
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

        if (pick.picked & actualRoom == "Ventana")
        {
            GameObject.Destroy(pick.gameObject);
            fragmentFound = true;
        }


    }


    public void returnToVisualNovel()
    {
        returned = true;
        actualRoom = null;

        canvasVisualNovel.SetActive(true);
        canvas3D.SetActive(false);
        cam.GetComponent<cameraMovement>().enabled = false;

        nameBox.SetActive(true);
        textBox.SetActive(true);
        continueButton.SetActive(true);
        Kairi.SetActive(true);
        foreach (GameObject ubication in fragmentsUbications)
        {
            ubication.SetActive(false);
        }
        sentences.Add("¿Lo has encontrado?");
        continueChat();
    }

    public void continueChat()
    {
        if (dialog.lastSentence.Contains("Muy bien, nos podemos ir...") || dialog.lastSentence.Contains("No me vengas con esas, aceptaste por voluntad propia."))
        {
            nameBox.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Mujer"; 
            dialog.NextSentence(sentences, typingSpeed, textBox, nameBox, continueButton, Kairi);
        }

        else if (dialog.lastSentence.Contains("¡Te odio! Nunca debí casarme contigo.") || dialog.lastSentence.Contains("Y me arrepiento cada día de mi vida."))
        {
            nameBox.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Kairi";
            dialog.NextSentence(sentences, typingSpeed, textBox, nameBox, continueButton, Kairi);
        }

        else if (dialog.lastSentence.Contains("¿Eh? Parece que hay alguien en la casa."))
        {
            nameBox.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Hombre";
            dialog.NextSentence(sentences, typingSpeed, textBox, nameBox, continueButton, Kairi);
        }

        else if (dialog.lastSentence.Contains("¿No te gustaría vivir en una casa así?"))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Ya lo hago";
                    option1Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Quizá algún día";
                    option2Punctuation = new Vector2(2, 0);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Nunca podría pagarlo";
                    option3Punctuation = new Vector2(8, 8);
                }
            }
        }

        else if (dialog.lastSentence.Contains("¿Aquí lo es?"))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "No";
                    option1Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Conozco alguna familia así";
                    option2Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "La mía es así";
                    option3Punctuation = new Vector2(7, 9);
                }
            }
        }

        else if (dialog.lastSentence.Contains("¿Lo has encontrado?"))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                if (option.name == "option1" || option.name == "option1Button")
                {
                    option.SetActive(true);
                    if (option.name == "option1")
                    {
                        option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Sí, aquí está";
                        option1Punctuation = new Vector2(0, 0);
                    }
                }
                else if (option.name == "option2" || option.name == "option2Button")
                {
                    option.SetActive(true);
                    if (option.name == "option2")
                    {
                        option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "No, necesito seguir buscando";
                        option2Punctuation = new Vector2(0, 0);
                        option3Punctuation = new Vector2(0, 0);
                    }
                }
            }
        }

        else if ((dialog.lastSentence.Contains("¿Comenzamos a buscar?") || dialog.lastSentence.Contains("Tómate el tiempo que necesites.")) && !returned)
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            Kairi.SetActive(false);
            foreach (GameObject ubication in fragmentsUbications)
            {
                ubication.SetActive(true);
            }

        }

        else if (dialog.lastSentence.Contains("Nos vamos."))
        {
            this.gameObject.GetComponent<PauseMenuManager>().punctuationToSave += punctuation;
            SceneManager.LoadScene("Level5");
        }

        else
        {
            dialog.NextSentence(sentences, typingSpeed, textBox, nameBox, continueButton, Kairi);
        }

        returned = false;
    }

    public void selectOption1()
    {
        foreach (GameObject option in options)
        {
            if (option.name == "option1")
            {
                selecctions.Add(option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text);
            }

            option.SetActive(false);
        }

        punctuation += option1Punctuation;
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

        punctuation += option2Punctuation;
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

        punctuation += option3Punctuation;
        checkSelections();
        dialog.NextSentence(sentences, typingSpeed, textBox, nameBox, continueButton, Kairi);
    }

    private void checkSelections()
    {
        if (selecctions.Contains("No, necesito seguir buscando"))
        {
            sentences.Add("Tómate el tiempo que necesites.");
            selecctions.Remove("No, necesito seguir buscando");
        }

        else if (selecctions.Contains("Sí, aquí está"))
        {
            if (fragmentFound)
            {
                sentences.Add("Muy bien, nos podemos ir...");
     
                sentences.Add("¡Te odio! Nunca debí casarme contigo.");

                sentences.Add("¿Eh? Parece que hay alguien en la casa.");

                sentences.Add("No me vengas con esas, aceptaste por voluntad propia.");

                sentences.Add("Y me arrepiento cada día de mi vida.");

                sentences.Add("Parece una discusión familiar...");
                sentences.Add("Aunque no creo que llegar a ese nivel sea normal.");
                sentences.Add("No en mi dimensión al menos.");
                sentences.Add("¿Aquí lo es?");

            }
            else
            {
                sentences.Add("Mmmm, no veo que lo tengas. Tómate el tiempo que necesites.");
            }
            selecctions.Remove("Sí, aquí está");
        }

        else if (selecctions.Contains("No"))
        {
            sentences.Add("Eso es bueno.");
            sentences.Add("Ese tipo de situaciones no deberían ser normales.");
            sentences.Add("Será mejor que nos marchemos.");
            sentences.Add("Probablemente no se han dado cuenta de que estabamos aquí porque estaban ocupados discutiendo.");
            sentences.Add("Es mejor que no nos arriesguemos quedandonos más de lo necesario.");
            sentences.Add("Esperemos que puedan arreglar sus asuntos.");
            sentences.Add("Nos vamos.");
        }

        else if (selecctions.Contains("Conozco alguna familia así"))
        {
            sentences.Add("Eso me preocupa un poco.");
            sentences.Add("Pero supongo que mientras no te afecte a ti está bien.");
            sentences.Add("Ese tipo de situaciones no deberían ser normales.");
            sentences.Add("Será mejor que nos marchemos.");
            sentences.Add("Probablemente no se han dado cuenta de que estabamos aquí porque estaban ocupados discutiendo.");
            sentences.Add("Es mejor que no nos arriesguemos quedandonos más de lo necesario.");
            sentences.Add("Esperemos que puedan arreglar sus asuntos.");
            sentences.Add("Nos vamos.");
        }

        else if (selecctions.Contains("La mía es así"))
        {
            sentences.Add("Lo lamento mucho.");
            sentences.Add("Tiene que ser difícil para ti vivir en un entorno así.");
            sentences.Add("Ese tipo de situaciones no deberían ser normales.");
            sentences.Add("Será mejor que nos marchemos.");
            sentences.Add("Probablemente no se han dado cuenta de que estabamos aquí porque estaban ocupados discutiendo.");
            sentences.Add("Es mejor que no nos arriesguemos quedandonos más de lo necesario.");
            sentences.Add("Esperemos que puedan arreglar sus asuntos.");
            sentences.Add("Nos vamos.");
        }

        else if (selecctions.Contains("Nunca podría pagarlo"))
        {
            sentences.Add("Vaya... me gustaría pensar que solo estás siendo pesimista.");
            sentences.Add("Pero es cierto que las situaciones económicas son muy difíciles de cambiar.");
            sentences.Add("Aún así, intentemos esforzarnos.");
            sentences.Add("Nunca se sabe.");
            sentences.Add("Seguramente estemos en propiedad privada.");
            sentences.Add("Así que deberíamos darnos prisa para encontrar el fragmento.");
            sentences.Add("¿Comenzamos a buscar?");

        }

        else if (selecctions.Contains("Quizá algún día"))
        {
            sentences.Add("Eso sería genial.");
            sentences.Add("A mí también me gustaría poder vivir en un lugar lujoso algún día.");
            sentences.Add("Pero no perdamos la esperanza, nunca se sabe, jajaja.");
            sentences.Add("Seguramente estemos en propiedad privada.");
            sentences.Add("Así que deberíamos darnos prisa para encontrar el fragmento.");
            sentences.Add("¿Comenzamos a buscar?");

        }

        else if (selecctions.Contains("Ya lo hago"))
        {
            sentences.Add("¿En serio?");
            sentences.Add("Solo vi tu habitación así que no me di cuenta.");
            sentences.Add("¡Pero eso es genial!");
            sentences.Add("Ahhh... espero poder vivir en un lugar lujoso también algún día.");
            sentences.Add("Seguramente estemos en propiedad privada.");
            sentences.Add("Así que deberíamos darnos prisa para encontrar el fragmento.");
            sentences.Add("¿Comenzamos a buscar?");
        }

    }

}
