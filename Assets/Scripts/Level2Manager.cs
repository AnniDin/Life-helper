using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Manager : MonoBehaviour
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
        sentences.Add("¿Dónde estamos...?");
        sentences.Add("Me atrevería a decir que es un centro de videojuegos.");
        sentences.Add("Aunque probablemente tú lo sepas mejor que yo.");
        sentences.Add("¿Eh? ¿Que qué ha pasado?");
        sentences.Add("Parece que al recoger el fragmento su energía nos a transportado automaticamente.");
        sentences.Add("Seguro que no lo he podido controlar porqué está roto...");
        sentences.Add("Será mejor que nos demos prisa, ¡no me quiero imaginar que pasaría si otra persona encontrase un fragmento por casualidad!");
        sentences.Add("Lo bueno es que puedo notar que aquí hay otro fragmento.");
        sentences.Add("Si al recoger uno nos lleva a otro quizá no sea tan malo.");

        sentences.Add("¡Eh!¡Mirad! Alguien nuevo.");
        sentences.Add("Si quieres quedarte aquí tendrás que jugar con nosotros.");
        sentences.Add("Aunque es imposible que ganes jajaja.");

        sentences.Add("Esto es malo, necesitamos quedarnos para buscar el fragmento.");
        sentences.Add("Lo bueno es que al parecer nadie más puede verme.");
        sentences.Add("¿Qué vas a hacer?");

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
                if (hit.collider.name == "Mesa")
                {
                    cam.transform.position = new Vector3(7.3f, 1.32f, 3.39f);
                    cam.transform.eulerAngles = new Vector3(10f, 270f, 0f);
                    cam.GetComponent<cameraMovement>().actualX = 10f;
                    cam.GetComponent<cameraMovement>().actualY = 270f;
                    cam.GetComponent<cameraMovement>().actualZ = 0f;
                    cam.GetComponent<cameraMovement>().initialX = 10f;
                    cam.GetComponent<cameraMovement>().initialY = 270f;
                    cam.GetComponent<cameraMovement>().initialZ = 0f;
                    actualRoom = "Mesa";
                }
                else if (hit.collider.name == "Escritorio")
                {
                    cam.transform.position = new Vector3(12.39f, 1.379f, 4.971f);
                    cam.transform.eulerAngles = new Vector3(0f, 270f, 0f);
                    cam.GetComponent<cameraMovement>().actualX = 0f;
                    cam.GetComponent<cameraMovement>().actualY = 270f;
                    cam.GetComponent<cameraMovement>().actualZ = 0f;
                    cam.GetComponent<cameraMovement>().initialX = 0f;
                    cam.GetComponent<cameraMovement>().initialY = 270f;
                    cam.GetComponent<cameraMovement>().initialZ = 0f;
                    actualRoom = "Escritorio";
                }
            }
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

        if (pick.picked & actualRoom == "Mesa")
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
        if (dialog.lastSentence.Contains("Si al recoger uno nos lleva a otro quizá no sea tan malo."))
        {
            nameBox.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Chico";
            dialog.NextSentence(sentences, typingSpeed, textBox, nameBox, continueButton, Kairi);
        }

        else if (dialog.lastSentence.Contains("Aunque es imposible que ganes jajaja."))
        {
            nameBox.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Kairi";
            dialog.NextSentence(sentences, typingSpeed, textBox, nameBox, continueButton, Kairi);
        }

        else if (dialog.lastSentence.Contains("Que te den."))
        {
            nameBox.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Kairi";
            dialog.NextSentence(sentences, typingSpeed, textBox, nameBox, continueButton, Kairi);
        }

        else if (dialog.lastSentence.Contains("¿Qué vas a hacer?"))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Les voy a callar la boca";
                    option1Punctuation = new Vector2(2, 0);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "No pasa nada, jugaré";
                    option2Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Prefiero irme";
                    option3Punctuation = new Vector2(0, 0);
                }
            }
        }

        else if (dialog.lastSentence.Contains("Eso ha sido patético."))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Lanzar el mando";
                    option1Punctuation = new Vector2(3, 4);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "No decir nada";
                    option2Punctuation = new Vector2(2, 0);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Pedir respeto";
                    option3Punctuation = new Vector2(0, 0);
                }
            }
        }

        else if (dialog.lastSentence.Contains("¿Te enseñan a manejar situaciones incómodas?"))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "A mí me enseñaron";
                    option1Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Para nada";
                    option2Punctuation = new Vector2(5, 5);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "¿Y a ti qué te importa?";
                    option3Punctuation = new Vector2(5, 5);
                }
            }
        }

        else if (dialog.lastSentence.Contains("Si continuas hablandome así no tendré ningún problema en mandarte a otro dimensión y dajarte allí."))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Lo lamento";
                    option1Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "No eres capaz";
                    option2Punctuation = new Vector2(4, 2);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "¡Atrévete!";
                    option3Punctuation = new Vector2(4, 2);
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

        else if ((dialog.lastSentence.Contains("Vamos a ello.") || dialog.lastSentence.Contains("Tómate el tiempo que necesites.")) && !returned)
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

        else if (dialog.lastSentence.Contains("¡Nos vamos!"))
        {
            this.gameObject.GetComponent<PauseMenuManager>().punctuationToSave += punctuation;
            SceneManager.LoadScene("Level3");
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
                sentences.Add("¡Genial!");
                sentences.Add("Oh, parece que ya nos estamos marchando.");
                sentences.Add("Aún me sorprende que funcione por sí solo.");
                sentences.Add("¡Nos vamos!");

            }
            else
            {
                sentences.Add("Mmmm, no veo que lo tengas. Tómate el tiempo que necesites.");
            }
            selecctions.Remove("Sí, aquí está");
        }


        else if (selecctions.Contains("Lo lamento") || selecctions.Contains("No eres capaz") || selecctions.Contains("¡Atrévete!"))
        {
            sentences.Add("Será mejor que sigamos buscando.");
            sentences.Add("Vamos a ello.");
        }

        else if (selecctions.Contains("A mí me enseñaron") || selecctions.Contains("Para nada"))
        {
            sentences.Add("Entiendo, supongo que cada lugar es diferente.");
            sentences.Add("Creo que ya podemos buscar el siguiente fragmento.");
            sentences.Add("Vamos a ello.");
        }

        else if (selecctions.Contains("¿Y a ti qué te importa?"))
        {
            sentences.Add("Que sea adorable no significa que no tenga mal genio.");
            sentences.Add("Si continuas hablandome así no tendré ningún problema en mandarte a otro dimensión y dajarte allí.");
        }

        else if (selecctions.Contains("Pedir respeto"))
        {
            sentences.Add("Cuando me ganes lo tendrás, jajaja.");
            sentences.Add("Que te den.");

            sentences.Add("Intenta ignorarlo, solo quería molestar.");
            sentences.Add("Esa ha parecido ser una situación complicada.");
            sentences.Add("Yo no habría sabido como reaccionar.");
            sentences.Add("También es verdad que en mi dimensión no ocurren estas cosas.");
            sentences.Add("¿Qué haceis aquí?");
            sentences.Add("¿Te enseñan a manejar situaciones incómodas?");

        }

        else if (selecctions.Contains("No decir nada"))
        {
            sentences.Add("Ya veo, quien calla otorga, jajaja.");
            sentences.Add("Que te den.");

            sentences.Add("Intenta ignorarlo, solo quería molestar.");
            sentences.Add("Esa ha parecido ser una situación complicada.");
            sentences.Add("Yo no habría sabido como reaccionar.");
            sentences.Add("También es verdad que en mi dimensión no ocurren estas cosas.");
            sentences.Add("¿Qué haceis aquí?");
            sentences.Add("¿Te enseñan a manejar situaciones incómodas?");
        }

        else if (selecctions.Contains("Lanzar el mando"))
        {
            sentences.Add("¿Qué demonios pasa contigo?");
            sentences.Add("Que te den.");

            sentences.Add("Intenta ignorarlo, solo quería molestar.");
            sentences.Add("Esa ha parecido ser una situación complicada.");
            sentences.Add("Yo no habría sabido como reaccionar.");
            sentences.Add("También es verdad que en mi dimensión no ocurren estas cosas.");
            sentences.Add("¿Qué haceis aquí?");
            sentences.Add("¿Te enseñan a manejar situaciones incómodas?");
        }

        else if (selecctions.Contains("No pasa nada, jugaré") || selecctions.Contains("Les voy a callar la boca") || selecctions.Contains("Prefiero irme"))
        {
            sentences.Add("En realidad, no es como si nos importase tu opinión.");
            sentences.Add("¡A jugar!");
            sentences.Add("...");
            sentences.Add("Mmmmm...");
            sentences.Add("¡Ajá, ya te tengo!");
            sentences.Add("...");
            sentences.Add("¿No sabes hacerlo mejor?");
            sentences.Add("Esto es ridículo.");
            sentences.Add("...");
            sentences.Add("Y... ¡Se acabó! ¡Gané!");
            sentences.Add("Eso ha sido patético.");
            nameBox.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Chico";
        }


    }

}
