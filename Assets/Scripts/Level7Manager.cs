using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level7Manager : MonoBehaviour
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
        sentences.Add("Esto es un autobús...");
        sentences.Add("Me pregunto a donde estaremos yendo.");
        sentences.Add("El fragmento debería estar por aquí dentro.");
        sentences.Add("Deberíamos intentar encontrarlo antes de llegar.");

        sentences.Add("¡Los frenos no funcionan!");
        sentences.Add("¡Agarrense todos!");

        sentences.Add("¡¿Eh?! Se supone que debemos ponernos a salvo debajo de los  asientos.");
        sentences.Add("¡Pero aún así estaríamos en peligro!");
        sentences.Add("Si pudieramos encontrar el fragmento rápido nos podríamos ir.");
        sentences.Add("Será difícil moverse por aquí con el autobús en movimiento.");
        sentences.Add("Pero si todo sale bien estaremos seguros pronto.");
        sentences.Add("¡Adelante!");

        sentences.Add("¿¡Pero que haces?!");
        sentences.Add("¡Sientate que te vas a hacer daño!");

        sentences.Add("Parece que ese señor no te va a dejar levantarte.");
        sentences.Add("Tendrás que hacer algo al respecto.");

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
                if (hit.collider.name == "Ventana")
                {
                    cam.transform.position = new Vector3(22.2f, 2.4f, 25.63f);
                    cam.transform.eulerAngles = new Vector3(0f, 243.7f, 0f);
                    cam.GetComponent<cameraMovement>().actualX = 0f;
                    cam.GetComponent<cameraMovement>().actualY = 243.7f;
                    cam.GetComponent<cameraMovement>().actualZ = 0f;
                    cam.GetComponent<cameraMovement>().initialX = 0f;
                    cam.GetComponent<cameraMovement>().initialY = 243.7f;
                    cam.GetComponent<cameraMovement>().initialZ = 0f;
                    actualRoom = "Ventana";
                }
                else if (hit.collider.name == "Asiento")
                {
                    cam.transform.position = new Vector3(23.35f, 2.51f, 24.122f);
                    cam.transform.eulerAngles = new Vector3(15f, 180f, 0f);
                    cam.GetComponent<cameraMovement>().actualX = 15f;
                    cam.GetComponent<cameraMovement>().actualY = 180f;
                    cam.GetComponent<cameraMovement>().actualZ = 0f;
                    cam.GetComponent<cameraMovement>().initialX = 15f;
                    cam.GetComponent<cameraMovement>().initialY = 180f;
                    cam.GetComponent<cameraMovement>().initialZ = 0f;
                    actualRoom = "Asiento";
                }
            }
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

        if (pick.picked & actualRoom == "Asiento")
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
        if (dialog.lastSentence.Contains("¡Adelante!"))
        {
            nameBox.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Pasajero";
            dialog.NextSentence(sentences, typingSpeed, textBox, nameBox, continueButton, Kairi);
        }

        else if (dialog.lastSentence.Contains("¡Sientate que te vas a hacer daño!") || dialog.lastSentence.Contains("¿Quieres morir o qué?") || dialog.lastSentence.Contains("Ts... si te mueres será culpa tuya") || dialog.lastSentence.Contains("¡Agarrense todos!"))
        {
            nameBox.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Kairi";
            dialog.NextSentence(sentences, typingSpeed, textBox, nameBox, continueButton, Kairi);
        }

        else if (dialog.lastSentence.Contains("Deberíamos intentar encontrarlo antes de llegar."))
        {
            nameBox.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Conductor";
            dialog.NextSentence(sentences, typingSpeed, textBox, nameBox, continueButton, Kairi);
        }

        else if (dialog.lastSentence.Contains("Tendrás que hacer algo al respecto."))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Tranquilizarlo";
                    option1Punctuation = new Vector2(10, 7);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Ignorarlo";
                    option2Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Insultarlo";
                    option3Punctuation = new Vector2(0, 0);
                }
            }
        }

        else if (dialog.lastSentence.Contains("Tendrás que probar otra cosa."))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Insistir";
                    option1Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Zafarse";
                    option2Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Empujarle";
                    option3Punctuation = new Vector2(5, 3);
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

        else if ((dialog.lastSentence.Contains("Intenta darte prisa para encontrarlo.") || dialog.lastSentence.Contains("Tómate el tiempo que necesites.")) && !returned)
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

        else if (dialog.lastSentence.Contains("¡Vamonos ya!"))
        {
            this.gameObject.GetComponent<PauseMenuManager>().punctuationToSave += punctuation;
            SceneManager.LoadScene("Level9");
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
                sentences.Add("¡Muy bien!");
                sentences.Add("¡Vamonos ya!");

            }
            else
            {
                sentences.Add("Mmmm, no veo que lo tengas. Tómate el tiempo que necesites.");
            }
            selecctions.Remove("Sí, aquí está");
        }


        else if (selecctions.Contains("Insistir") || selecctions.Contains("Zafarse") || selecctions.Contains("Empujarle"))
        {
            nameBox.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Pasajero";

            sentences.Add("Ts... si te mueres será culpa tuya");

            sentences.Add("¡Genial!");
            sentences.Add("Probablemente nos odie pero al menos ha funcionado");
            sentences.Add("Intenta darte prisa para encontrarlo.");

        }

        else if (selecctions.Contains("Tranquilizarlo") || selecctions.Contains("Ignorarlo") || selecctions.Contains("Insultarlo"))
        {
            nameBox.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Pasajero";

            sentences.Add("Que me hagas caso, por dios.");
            sentences.Add("¿Quieres morir o qué?");

            sentences.Add("No ha funcionado.");
            sentences.Add("Tendrás que probar otra cosa.");

        }

    }

}