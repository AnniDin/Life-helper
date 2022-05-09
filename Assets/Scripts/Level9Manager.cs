using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level9Manager : MonoBehaviour
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
    public GameObject imageHotel;

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
        sentences.Add("Menos mal que lo conseguiste.");
        sentences.Add("Eso podría haber salido muy mal.");
        sentences.Add("Lamento haberte puesto en peligro.");
        sentences.Add("Al menos parece que ahora estamos en un sitio seguro.");
        sentences.Add("Esta habitación de hotel está vacía así que no deberíamos tener problemas.");
        sentences.Add("Tampoco es como si nos fueramos a quedar una eternidad.");
        sentences.Add("En aquella pared hay una televisión.");
        sentences.Add("Podríamos encenderla y descansar un rato.");
        sentences.Add("...");
        sentences.Add("No funciona.");
        sentences.Add("Solo salen unos colores y sonidos extraños.");
        sentences.Add("Aunque resulta agradable en cierta forma, ¿no crees?");

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
                if (hit.collider.name == "Cama")
                {
                    cam.transform.position = new Vector3(5.76f, 0.49f, 4.23f);
                    cam.transform.eulerAngles = new Vector3(40f, -90f, 0f);
                    cam.GetComponent<cameraMovement>().actualX = 40f;
                    cam.GetComponent<cameraMovement>().actualY = -90f;
                    cam.GetComponent<cameraMovement>().actualZ = 0f;
                    cam.GetComponent<cameraMovement>().initialX = 40f;
                    cam.GetComponent<cameraMovement>().initialY = -90f;
                    cam.GetComponent<cameraMovement>().initialZ = 0f;
                    actualRoom = "Cama";
                }
                else if (hit.collider.name == "Mesita")
                {
                    cam.transform.position = new Vector3(5.56f, 0.37f, 3.65f);
                    cam.transform.eulerAngles = new Vector3(10, -90, 0f);
                    cam.GetComponent<cameraMovement>().actualX = 10f;
                    cam.GetComponent<cameraMovement>().actualY = -90;
                    cam.GetComponent<cameraMovement>().actualZ = 0f;
                    cam.GetComponent<cameraMovement>().initialX = 10f;
                    cam.GetComponent<cameraMovement>().initialY = -90;
                    cam.GetComponent<cameraMovement>().initialZ = 0f;
                    actualRoom = "Mesita";
                }
            }
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

        if (pick.picked & actualRoom == "Mesita")
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

        if (dialog.lastSentence.Contains("Aunque resulta agradable en cierta forma, ¿no crees?"))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "¡Y tanto!";
                    option1Punctuation = new Vector2(0, 5);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Es muy molesto";
                    option2Punctuation = new Vector2(5, 0);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Preferiría ver la televisión";
                    option3Punctuation = new Vector2(0, 0);
                }
            }
        }

        else if (dialog.lastSentence.Contains("¿Has pasado por algo así antes?"))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Ninguna que me afectase";
                    option1Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Sí";
                    option2Punctuation = new Vector2(7, 5);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Para nada";
                    option3Punctuation = new Vector2(0, 0);
                }
            }
        }

        else if (dialog.lastSentence.Contains("¿A ti te ocurre?"))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Constantemente";
                    option1Punctuation = new Vector2(0, 7);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "De vez en cuando";
                    option2Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "De normal no";
                    option3Punctuation = new Vector2(0, 0);
                }
            }
        }

        else if (dialog.lastSentence.Contains("¿Crees que podrás dormir?"))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Me suele costar";
                    option1Punctuation = new Vector2(4, 7);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Sin problema";
                    option2Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "No suelo dormir";
                    option3Punctuation = new Vector2(4, 7);
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

        else if (dialog.lastSentence.Contains("¡Qué descanses!"))
        {
            imageHotel.gameObject.SetActive(true);

            sentences.Add("");
            sentences.Add("Espero que hayas dormido bien.");
            sentences.Add("Empecemos a buscar.");
            dialog.NextSentence(sentences, typingSpeed, textBox, nameBox, continueButton, Kairi);
        }

        else if ((dialog.lastSentence.Contains("Empecemos a buscar.") || dialog.lastSentence.Contains("Tómate el tiempo que necesites.")) && !returned)
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

        else if (dialog.lastSentence.Contains("Vayamos a por él."))
        {
            this.gameObject.GetComponent<PauseMenuManager>().punctuationToSave += punctuation;
            SceneManager.LoadScene("Level10");
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
                sentences.Add("Solo queda uno.");
                sentences.Add("Vayamos a por él.");

            }
            else
            {
                sentences.Add("Mmmm, no veo que lo tengas. Tómate el tiempo que necesites.");
            }
            selecctions.Remove("Sí, aquí está");
        }

        else if (selecctions.Contains("Me suele costar") || selecctions.Contains("No suelo dormir"))
        {

            sentences.Add("Oh...");
            sentences.Add("Aún así deberías intentar dormir.");
            sentences.Add("Hoy ha sido un día duro.");
            sentences.Add("¡Qué descanses!");

        }

        else if (selecctions.Contains("Sin problema"))
        {
            sentences.Add("Guay, al menos podrás pasar una buena noche.");
            sentences.Add("¡Qué descanses!");
        }

        else if (selecctions.Contains("Constantemente") || selecctions.Contains("De vez en cuando") || selecctions.Contains("De normal no"))
        {
            sentences.Add("Entiendo.");
            sentences.Add("Pero ahora ya no hay nada que hacer al respecto.");
            sentences.Add("Así que quizá sería mejor seguir con lo nuestro.");
            sentences.Add("¿Crees que podrás dormir?");

        }

        else if (selecctions.Contains("Sí"))
        {
            sentences.Add("Lamento mucho escuchar eso.");
            sentences.Add("Y encima has tenido que pasar por otra situación así por mi culpa.");
            sentences.Add("Empiezo a pensar que no debí pedirte que me ayudases.");
            sentences.Add("Creo que me siento triste.");
            sentences.Add("En mi dimensión no solemos sentirnos así.");
            sentences.Add("¿A ti te ocurre?");
        }

        else if (selecctions.Contains("Ninguna que me afectase") || selecctions.Contains("Para nada"))
        {

            sentences.Add("Eso está bien.");
            sentences.Add("Aunque por otro parte que tu primera experienccia cercana a la muerta haya sido por mí culpa...");
            sentences.Add("Me hace sentir peor.");
            sentences.Add("Empiezo a pensar que no debí pedirte que me ayudases.");
            sentences.Add("Creo que me siento triste.");
            sentences.Add("En mi dimensión no solemos sentirnos así.");
            sentences.Add("¿A ti te ocurre?");
        }

        else if (selecctions.Contains("¡Y tanto!") || selecctions.Contains("Es muy molesto") || selecctions.Contains("Preferiría ver la televisión"))
        {
            sentences.Add("Ver la televisión sería más entretenido, eso es verdad.");
            sentences.Add("Respecto a lo de antes...");
            sentences.Add("Realmente espero que te encuentres bien.");
            sentences.Add("Has tenido que sufrir mucha presión.");
            sentences.Add("¿Has pasado por algo así antes?");

        }

    }

}
