using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level6Manager : MonoBehaviour
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
        sentences.Add("Mmm... aqu� hay muchas tiendas, esto debe ser lo que llamais un centro comercial.");
        sentences.Add("Habr� mucha gente de un lado a otro por aqu�.");
        sentences.Add("Eso nos ayudar� a pasar desapercibidos.");
        sentences.Add("Tengo entendido que aqu� soleis venir a pasar el tiempo con amigos.");
        sentences.Add("Es algo as� como un sitio de reuni�n, �no?");
        sentences.Add("En mi dimensi�n solemos estar siempre al aire libre.");
        sentences.Add("Por eso se me hace curioso.");
        sentences.Add("�T� tambi�n sueles venir con gente?");

        continueChat();

    }

    // Update is called once per frame
    void Update()
    {

        /*Vector2 mousePos = Input.mousePosition;
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
                    cam.transform.position = new Vector3(4.3f, 1.7f, 26.18f);
                    cam.transform.eulerAngles = new Vector3(19.84f, 270f, 0f);
                    actualRoom = "Mesa";
                }
                else if (hit.collider.name == "Silla")
                {
                    cam.transform.position = new Vector3(3.16f, 2.28f, 26.18f);
                    cam.transform.eulerAngles = new Vector3(32.06f, 270f, 0f);
                    actualRoom = "Silla";
                }
            }
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }*/

        if (pick.picked & actualRoom == "Campo")
        {
            GameObject.Destroy(pick.gameObject);
            fragmentFound = true;
            sentences.Add("");
            sentences.Add("�Genial!");
            sentences.Add("�Nos vamos ya!");
            continueChat();
        }
    }


    public void returnToVisualNovel()
    {
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
        if (dialog.lastSentence.Contains("Ve."))
        {
            nameBox.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Se�ora";
            dialog.NextSentence(sentences, typingSpeed, textBox, nameBox, continueButton, Kairi);
        }

        else if (dialog.lastSentence.Contains("Este centro comercial es muy lioso la primera vez que vienes."))
        {
            nameBox.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Kairi";
            dialog.NextSentence(sentences, typingSpeed, textBox, nameBox, continueButton, Kairi);
        }

        else if (dialog.lastSentence.Contains("�T� tambi�n sueles venir con gente?"))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "No tengo con quien";
                    option1Punctuation = new Vector2(3, 5);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "De vez en cuando";
                    option2Punctuation = new Vector2(2, 0);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "No me gusta";
                    option3Punctuation = new Vector2(6, 1);
                }
            }
        }

        else if (dialog.lastSentence.Contains("�Entiendes a qu� me refiero?"))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "S�, tampoco me gustan las multitudes";
                    option1Punctuation = new Vector2(10, 4);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Creo que est�s exagerando";
                    option2Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Te entiendo, pero no lo comparto";
                    option3Punctuation = new Vector2(0, 0);
                }
            }
        }

        else if (dialog.lastSentence.Contains("Yo estar�a m�s tranquilo con mi mejor amiga aqu�."))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Tambi�n tengo a alguien as�";
                    option1Punctuation = new Vector2(7, 0);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Yo no tengo nadie as�";
                    option2Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "S�, pero no es una necesidad";
                    option3Punctuation = new Vector2(0, 0);
                }
            }
        }

        else if (dialog.lastSentence.Contains("Lo har�a yo, pero.. no puedo."))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "No hay problema";
                    option1Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "No me gusta hablar con extra�os";
                    option2Punctuation = new Vector2(10, 4);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Dame un minuto que me mentalice";
                    option3Punctuation = new Vector2(10, 4);
                }
            }
        }

        else if (dialog.lastSentence.Contains("�Aqu� los cambios de temperatura son comunes?"))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Continuamente";
                    option1Punctuation = new Vector2(5, 0);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "No demasiado";
                    option2Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "�A mi qu� me cuentas?";
                    option3Punctuation = new Vector2(0, 0);
                }
            }
        }


        else if (dialog.lastSentence.Contains("Lo siento, no puedo controlarlo."))
        {
            canvasVisualNovel.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            cam.GetComponent<cameraMovement>().enabled = true;
            cam.transform.position = new Vector3(156.54f, 112.03f, 202.01f);
            cam.transform.eulerAngles = new Vector3(7.3f, 188f, 0f);
            actualRoom = "Campo";
            sentences.Add("No s� que hacemos aqu�.");
            sentences.Add("Pero el fragmento est� cerca.");
            dialog.NextSentence(sentences, typingSpeed, textBox, nameBox, continueButton, Kairi);
        }

        else if (dialog.lastSentence.Contains("�Nos vamos ya!"))
        {
            this.gameObject.GetComponent<PauseMenuManager>().punctuationToSave += punctuation;
            SceneManager.LoadScene("Level7");
        }

        else
        {
            dialog.NextSentence(sentences, typingSpeed, textBox, nameBox, continueButton, Kairi);
        }

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
                sentences.Add("A por el siguiente entonces.");
                sentences.Add("�Nos vamos!");

            }
            else
            {
                sentences.Add("Mmmm, no veo que lo tengas. T�mate el tiempo que necesites.");
            }
            selecctions.Remove("S�, aqu� est�");
        }

        else if (selecctions.Contains("Continuamente") || selecctions.Contains("No demasiado") || selecctions.Contains("�A mi qu� me cuentas?"))
        {
            sentences.Add("Entiendo.");
            sentences.Add("�Est� pasando otra vez!");
            sentences.Add("Pero si a�n no hemos recogido el fragmento.");
            sentences.Add("Lo siento, no puedo controlarlo.");
        }

        else if (selecctions.Contains("No me gusta hablar con extra�os") || selecctions.Contains("Dame un minuto que me mentalice"))
        {
            sentences.Add("�Te supone mucho problema?");
            sentences.Add("No quiero hacerte pasar por una situaci�n que te gusta.");
            sentences.Add("Pero ahora mismo no tenemos m�s alternativas.");
            sentences.Add("Por favor. Ve.");

            sentences.Add("�Hola? �Te puedo ayudar con algo?");
            sentences.Add("Oh, para salir solo tienes que seguir este pasillo hasta el fondo.");
            sentences.Add("Este centro comercial es muy lioso la primera vez que vienes.");

            sentences.Add("�Lo has hecho genial!");
            sentences.Add("�Crees que har� mucho frio fuera?");
            sentences.Add("La gente parece ir bastante abrigada.");
            sentences.Add("Aunque a mi me da igual, los de mi especie no notamos las temperaturas.");
            sentences.Add("�Aqu� los cambios de temperatura son comunes?");
        }

        else if (selecctions.Contains("No hay problema"))
        {
            sentences.Add("�Genial!");
            sentences.Add("Yo te espero. Ve.");

            sentences.Add("�Hola? �Te puedo ayudar con algo?");
            sentences.Add("Oh, para salir solo tienes que seguir este pasillo hasta el fondo.");
            sentences.Add("Este centro comercial es muy lioso la primera vez que vienes.");

            sentences.Add("�Lo has hecho genial!");
            sentences.Add("�Crees que har� mucho frio fuera?");
            sentences.Add("La gente parece ir bastante abrigada.");
            sentences.Add("Aunque a mi me da igual, los de mi especie no notamos las temperaturas.");
            sentences.Add("�Aqu� los cambios de temperatura son comunes?");
        }

        else if (selecctions.Contains("Tambi�n tengo a alguien as�"))
        {
            sentences.Add("Eso est� bien.");
            sentences.Add("Tener a alguien de confianza siempre es un apoyo.");
            sentences.Add("Yo... no s� que har�a sin ella, jajaja.");

            sentences.Add("Cambiando de tema, tenemos un problema.");
            sentences.Add("El fragmento est� cerca, pero lo noto en el exterior.");
            sentences.Add("�Y no tenemos ni idea de como salir de aqu�!");
            sentences.Add("Ah� hay una se�ora que parece simp�tica.");
            sentences.Add("�Podr�as preguntarle?");
            sentences.Add("Lo har�a yo, pero.. no puedo.");
        }


        else if (selecctions.Contains("S�, pero no es una necesidad"))
        {
            sentences.Add("Es lo mejor creo yo.");
            sentences.Add("Tener a alguien en quien confiar pero sin depender de �l.");
            sentences.Add("Ojal� pudiese decir lo mismo, jajaja.");

            sentences.Add("Cambiando de tema, tenemos un problema.");
            sentences.Add("El fragmento est� cerca, pero lo noto en el exterior.");
            sentences.Add("�Y no tenemos ni idea de como salir de aqu�!");
            sentences.Add("Ah� hay una se�ora que parece simp�tica.");
            sentences.Add("�Podr�as preguntarle?");
            sentences.Add("Lo har�a yo, pero.. no puedo.");
        }

        else if (selecctions.Contains("Yo no tengo nadie as�"))
        {
            sentences.Add("Oh, no quer�a sacar un tema inc�modo.");
            sentences.Add("Lo lamento.");
            sentences.Add("Al menos estoy yo aqu�.");
            sentences.Add("Espero ser de ayuda.");

            sentences.Add("Cambiando de tema, tenemos un problema.");
            sentences.Add("El fragmento est� cerca, pero lo noto en el exterior.");
            sentences.Add("�Y no tenemos ni idea de como salir de aqu�!");
            sentences.Add("Ah� hay una se�ora que parece simp�tica.");
            sentences.Add("�Podr�as preguntarle?");
            sentences.Add("Lo har�a yo, pero.. no puedo.");
        }

        else if (selecctions.Contains("S�, tampoco me gustan las multitudes"))
        {
            sentences.Add("Es que es muy dif�cil.");
            sentences.Add("Cualquiera puede estarte mirando y ni siquiera de dar�as cuenta.");
            sentences.Add("Es muy estresante.");
            sentences.Add("Preferiria ir a sitios menos concurridos.");
            sentences.Add("Pero supongo que no nos queda otra.");

            sentences.Add("Dime una cosa.");
            sentences.Add("�Hubieses preferido venir aqu� con alguien m�s?");
            sentences.Add("Yo estar�a m�s tranquilo con mi mejor amiga aqu�.");
        }

        else if (selecctions.Contains("Creo que est�s exagerando") || selecctions.Contains("Te entiendo, pero no lo comparto"))
        {
            sentences.Add("Ya.. lo imaginaba.");
            sentences.Add("Pero no puedo evitar sentirme nervioso.");
            sentences.Add("Preferiria ir a sitios menos concurridos.");
            sentences.Add("Pero supongo que no nos queda otra.");

            sentences.Add("Dime una cosa.");
            sentences.Add("�Hubieses preferido venir aqu� con alguien m�s?");
            sentences.Add("Yo estar�a m�s tranquilo con mi mejor amiga aqu�.");
        }

        else if (selecctions.Contains("No me gusta"))
        {
            sentences.Add("Entonces es cuesti�n de gustos.");
            sentences.Add("Ya veo, los humanos son muy interesantes.");
            sentences.Add("Pueden ser muy distintos entre s�.");
            sentences.Add("En mi dimensi�n somos todos iguales.");
            sentences.Add("Sobre todo en el aspecto.");

            sentences.Add("Ahora que lo pienso...");
            sentences.Add("��C�mo me tengo que comportar entre tanta gente?!");
            sentences.Add("Suelo ser muy solitario as� que esto es un poco inquietante.");
            sentences.Add("�Entiendes a qu� me refiero?");
        }

        else if (selecctions.Contains("De vez en cuando"))
        {
            sentences.Add("Entonces si es com�n venir aqu�.");
            sentences.Add("Ya veo, los humanos tiene unos pasatiempos interesantes.");
            sentences.Add("�Me gustar�a poder venir contigo en otra ocasi�n!");
            sentences.Add("Pero me temo que debo volver a mi dimensi�n despu�s de esto.");
            sentences.Add("Es una l�stima.");

            sentences.Add("Ahora que lo pienso...");
            sentences.Add("��C�mo me tengo que comportar entre tanta gente?!");
            sentences.Add("Suelo ser muy solitario as� que esto es un poco inquietante.");
            sentences.Add("�Entiendes a qu� me refiero?");
        }

        else if (selecctions.Contains("No tengo con quien"))
        {
            sentences.Add("Oh... eso es una l�stima.");
            sentences.Add("Yo ir�a contigo si puediese.");
            sentences.Add("Pero me temo que debo volver a mi dimensi�n despu�s de esto.");
            sentences.Add("Puede que encuentres a alguien en cualquier momento.");
            sentences.Add("No dejes que eso te deprima");

            sentences.Add("Ahora que lo pienso...");
            sentences.Add("��C�mo me tengo que comportar entre tanta gente?!");
            sentences.Add("Suelo ser muy solitario as� que esto es un poco inquietante.");
            sentences.Add("�Entiendes a qu� me refiero?");
        }

    }

}

