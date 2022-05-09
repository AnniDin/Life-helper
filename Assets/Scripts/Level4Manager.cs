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
        sentences.Add("�Hala! Qu� casa m�s bonita.");
        sentences.Add("Es la primera vez que veo una as� desde que llegu� a la tierra.");
        sentences.Add("Aunque tampoco es que haga mucho tiempo de eso, jeje.");
        sentences.Add("Me pregunto quien vivir� aqu�.");
        sentences.Add("Seguro que es una persona exitosa, incluso podr�a ser un famoso.");
        sentences.Add("�T� qu� dices?");
        sentences.Add("�No te gustar�a vivir en una casa as�?");

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
        sentences.Add("�Lo has encontrado?");
        continueChat();
    }

    public void continueChat()
    {
        if (dialog.lastSentence.Contains("Muy bien, nos podemos ir...") || dialog.lastSentence.Contains("No me vengas con esas, aceptaste por voluntad propia."))
        {
            nameBox.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Mujer"; 
            dialog.NextSentence(sentences, typingSpeed, textBox, nameBox, continueButton, Kairi);
        }

        else if (dialog.lastSentence.Contains("�Te odio! Nunca deb� casarme contigo.") || dialog.lastSentence.Contains("Y me arrepiento cada d�a de mi vida."))
        {
            nameBox.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Kairi";
            dialog.NextSentence(sentences, typingSpeed, textBox, nameBox, continueButton, Kairi);
        }

        else if (dialog.lastSentence.Contains("�Eh? Parece que hay alguien en la casa."))
        {
            nameBox.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Hombre";
            dialog.NextSentence(sentences, typingSpeed, textBox, nameBox, continueButton, Kairi);
        }

        else if (dialog.lastSentence.Contains("�No te gustar�a vivir en una casa as�?"))
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
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Quiz� alg�n d�a";
                    option2Punctuation = new Vector2(2, 0);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Nunca podr�a pagarlo";
                    option3Punctuation = new Vector2(8, 8);
                }
            }
        }

        else if (dialog.lastSentence.Contains("�Aqu� lo es?"))
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
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Conozco alguna familia as�";
                    option2Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "La m�a es as�";
                    option3Punctuation = new Vector2(7, 9);
                }
            }
        }

        else if (dialog.lastSentence.Contains("�Lo has encontrado?"))
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
                        option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "S�, aqu� est�";
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

        else if ((dialog.lastSentence.Contains("�Comenzamos a buscar?") || dialog.lastSentence.Contains("T�mate el tiempo que necesites.")) && !returned)
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
            sentences.Add("T�mate el tiempo que necesites.");
            selecctions.Remove("No, necesito seguir buscando");
        }

        else if (selecctions.Contains("S�, aqu� est�"))
        {
            if (fragmentFound)
            {
                sentences.Add("Muy bien, nos podemos ir...");
     
                sentences.Add("�Te odio! Nunca deb� casarme contigo.");

                sentences.Add("�Eh? Parece que hay alguien en la casa.");

                sentences.Add("No me vengas con esas, aceptaste por voluntad propia.");

                sentences.Add("Y me arrepiento cada d�a de mi vida.");

                sentences.Add("Parece una discusi�n familiar...");
                sentences.Add("Aunque no creo que llegar a ese nivel sea normal.");
                sentences.Add("No en mi dimensi�n al menos.");
                sentences.Add("�Aqu� lo es?");

            }
            else
            {
                sentences.Add("Mmmm, no veo que lo tengas. T�mate el tiempo que necesites.");
            }
            selecctions.Remove("S�, aqu� est�");
        }

        else if (selecctions.Contains("No"))
        {
            sentences.Add("Eso es bueno.");
            sentences.Add("Ese tipo de situaciones no deber�an ser normales.");
            sentences.Add("Ser� mejor que nos marchemos.");
            sentences.Add("Probablemente no se han dado cuenta de que estabamos aqu� porque estaban ocupados discutiendo.");
            sentences.Add("Es mejor que no nos arriesguemos quedandonos m�s de lo necesario.");
            sentences.Add("Esperemos que puedan arreglar sus asuntos.");
            sentences.Add("Nos vamos.");
        }

        else if (selecctions.Contains("Conozco alguna familia as�"))
        {
            sentences.Add("Eso me preocupa un poco.");
            sentences.Add("Pero supongo que mientras no te afecte a ti est� bien.");
            sentences.Add("Ese tipo de situaciones no deber�an ser normales.");
            sentences.Add("Ser� mejor que nos marchemos.");
            sentences.Add("Probablemente no se han dado cuenta de que estabamos aqu� porque estaban ocupados discutiendo.");
            sentences.Add("Es mejor que no nos arriesguemos quedandonos m�s de lo necesario.");
            sentences.Add("Esperemos que puedan arreglar sus asuntos.");
            sentences.Add("Nos vamos.");
        }

        else if (selecctions.Contains("La m�a es as�"))
        {
            sentences.Add("Lo lamento mucho.");
            sentences.Add("Tiene que ser dif�cil para ti vivir en un entorno as�.");
            sentences.Add("Ese tipo de situaciones no deber�an ser normales.");
            sentences.Add("Ser� mejor que nos marchemos.");
            sentences.Add("Probablemente no se han dado cuenta de que estabamos aqu� porque estaban ocupados discutiendo.");
            sentences.Add("Es mejor que no nos arriesguemos quedandonos m�s de lo necesario.");
            sentences.Add("Esperemos que puedan arreglar sus asuntos.");
            sentences.Add("Nos vamos.");
        }

        else if (selecctions.Contains("Nunca podr�a pagarlo"))
        {
            sentences.Add("Vaya... me gustar�a pensar que solo est�s siendo pesimista.");
            sentences.Add("Pero es cierto que las situaciones econ�micas son muy dif�ciles de cambiar.");
            sentences.Add("A�n as�, intentemos esforzarnos.");
            sentences.Add("Nunca se sabe.");
            sentences.Add("Seguramente estemos en propiedad privada.");
            sentences.Add("As� que deber�amos darnos prisa para encontrar el fragmento.");
            sentences.Add("�Comenzamos a buscar?");

        }

        else if (selecctions.Contains("Quiz� alg�n d�a"))
        {
            sentences.Add("Eso ser�a genial.");
            sentences.Add("A m� tambi�n me gustar�a poder vivir en un lugar lujoso alg�n d�a.");
            sentences.Add("Pero no perdamos la esperanza, nunca se sabe, jajaja.");
            sentences.Add("Seguramente estemos en propiedad privada.");
            sentences.Add("As� que deber�amos darnos prisa para encontrar el fragmento.");
            sentences.Add("�Comenzamos a buscar?");

        }

        else if (selecctions.Contains("Ya lo hago"))
        {
            sentences.Add("�En serio?");
            sentences.Add("Solo vi tu habitaci�n as� que no me di cuenta.");
            sentences.Add("�Pero eso es genial!");
            sentences.Add("Ahhh... espero poder vivir en un lugar lujoso tambi�n alg�n d�a.");
            sentences.Add("Seguramente estemos en propiedad privada.");
            sentences.Add("As� que deber�amos darnos prisa para encontrar el fragmento.");
            sentences.Add("�Comenzamos a buscar?");
        }

    }

}
