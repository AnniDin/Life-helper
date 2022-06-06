using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3Manager : MonoBehaviour
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
        sentences.Add("Este sitio me parece m�s familiar.");
        sentences.Add("Es un colegio, �verdad?");
        sentences.Add("En mi dimensi�n tambi�n tenemos, aunque son un poco distintos.");
        sentences.Add("Solemos entrar por la ventana.");
        sentences.Add("Es una zona muy amplia.");
        sentences.Add("Puede que tardemos m�s de lo que me gustar�a para encontrar este fragmento.");
        sentences.Add("Encima hay estudiantes dando vueltas por aqu�, tendremos que ir con cuidado.");
        sentences.Add("Intentemos no llamar la atenci�n.");

        sentences.Add("�Vas a llorar? Jajaja.");

        sentences.Add("�Eh? Parece que algu�en tiene problemas.");

        sentences.Add("No s� como se te ocurre pensar que podr�as jugar con nosotros.");
        sentences.Add("Solo eres un in�til.");
        sentences.Add("Y no se te ocurra contarle algo a la profe.");
        sentences.Add("Te recuerdo que sabemos donde vives.");
        sentences.Add("Y no querr�s que le pase nada a tu perrito.");

        sentences.Add("...");
        sentences.Add("Parece que ya lo han dejado en paz, pero estoy seguro de que se ha sido llorando.");
        sentences.Add("Realmente no entiendo a los humanos.");
        sentences.Add("Debe de ser horrible que te humillen as�.");
        sentences.Add("Espero que haya sido un caso especial y que no sea algo com�n.");
        sentences.Add("No para la mayor�a al menos.");
        sentences.Add("�T� que opinas?");

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
                if (hit.collider.name == "Porteria")
                {
                    cam.transform.position = new Vector3(9.3f, 2.38f, 22.35f);
                    cam.transform.eulerAngles = new Vector3(10f, 270f, 0f);
                    cam.GetComponent<cameraMovement>().actualX = 10f;
                    cam.GetComponent<cameraMovement>().actualY = 270f;
                    cam.GetComponent<cameraMovement>().actualZ = 0f;
                    cam.GetComponent<cameraMovement>().initialX = 10f;
                    cam.GetComponent<cameraMovement>().initialY = 270f;
                    cam.GetComponent<cameraMovement>().initialZ = 0f;
                    actualRoom = "Porteria";
                }
                else if (hit.collider.name == "Terraza")
                {
                    cam.transform.position = new Vector3(21.24f, 2.66f, 24.64f);
                    cam.transform.eulerAngles = new Vector3(10f, -270f, 0f);
                    cam.GetComponent<cameraMovement>().actualX = 10f;
                    cam.GetComponent<cameraMovement>().actualY = -270f;
                    cam.GetComponent<cameraMovement>().actualZ = 0f;
                    cam.GetComponent<cameraMovement>().initialX = 10f;
                    cam.GetComponent<cameraMovement>().initialY = -270f;
                    cam.GetComponent<cameraMovement>().initialZ = 0f;
                    actualRoom = "Terraza";
                }
            }
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

        if (pick.picked & actualRoom == "Terraza")
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
        if (dialog.lastSentence.Contains("Intentemos no llamar la atenci�n.") || dialog.lastSentence.Contains("�Eh? Parece que algu�en tiene problemas."))
        {
            nameBox.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Ni�o";
            dialog.NextSentence(sentences, typingSpeed, textBox, nameBox, continueButton, Kairi);
        }

        else if (dialog.lastSentence.Contains("�Vas a llorar? Jajaja.") || dialog.lastSentence.Contains("Y no querr�s que le pase nada a tu perrito."))
        {
            nameBox.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Kairi";
            dialog.NextSentence(sentences, typingSpeed, textBox, nameBox, continueButton, Kairi);
        }

        else if (dialog.lastSentence.Contains("Que te den."))
        {
            nameBox.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Kairi";
            dialog.NextSentence(sentences, typingSpeed, textBox, nameBox, continueButton, Kairi);
        }

        else if (dialog.lastSentence.Contains("�T� que opinas?"))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Tambi�n me pas�";
                    option1Punctuation = new Vector2(10, 10);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "No es algo com�n";
                    option2Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Algo les habr�a hecho";
                    option3Punctuation = new Vector2(0, 0);
                }
            }
        }

        else if (dialog.lastSentence.Contains("�Tuviste ayuda?"))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "S�, mucha";
                    option1Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "No, nadie me apoy�";
                    option2Punctuation = new Vector2(3, 5);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "No la suficiente";
                    option3Punctuation = new Vector2(2, 4);
                }
            }
        }

        else if (dialog.lastSentence.Contains("A ti no te habr� pasado, �no?"))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Afortunadamente no";
                    option1Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "No, pero conozco a alguien a quien s�";
                    option2Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Por desgracia, s�";
                    option3Punctuation = new Vector2(7, 6);
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

        else if ((dialog.lastSentence.Contains("Cuanto antes nos marchemos mejor.") || dialog.lastSentence.Contains("T�mate el tiempo que necesites.")) && !returned)
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

        else if (dialog.lastSentence.Contains("�Nos vamos!"))
        {
            this.gameObject.GetComponent<PauseMenuManager>().punctuationToSave += punctuation;
            SceneManager.LoadScene("Level4");
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
                sentences.Add("�Genial! Eso significa que podemos seguir buscando los siguientes.");
                sentences.Add("As� que con tu permiso...");
                sentences.Add("�Nos vamos!");

            }
            else
            {
                sentences.Add("Mmmm, no veo que lo tengas. T�mate el tiempo que necesites.");
            }
            selecctions.Remove("S�, aqu� est�");
        }


        else if (selecctions.Contains("Por desgracia, s�"))
        {
            sentences.Add("Creo que cada vez los humanos me caen peor.");
            sentences.Add("Parec�an una especie interesante, pero estas cosas no tienen sentido.");
            sentences.Add("�No te incluyo a ti, claro!");
            sentences.Add("Siento mucho que hayas pasado por todo eso.");
            sentences.Add("Creo que ser� mejor que busquemos ya el fragmento.");
            sentences.Add("Cuanto antes nos marchemos mejor.");
        }

        else if (selecctions.Contains("Afortunadamente no") || selecctions.Contains("No, pero conozco a alguien a quien s�"))
        {
            sentences.Add("Menos mal, ya me estaba empezando a preocupar.");
            sentences.Add("Creo que ser� mejor que busquemos ya el fragmento.");
            sentences.Add("Cuanto antes nos marchemos mejor.");
        }

        else if (selecctions.Contains("No, nadie me apoy�") || selecctions.Contains("No la suficiente"))
        {
            sentences.Add("Eso debi� haber sido a�n peor.");
            sentences.Add("Me hubiese guatado haber podido estar en ese momento.");
            sentences.Add("�Mira! Parece que se le cay� el tel�fono m�vil a ese chico.");
            sentences.Add("La pantalla esta desbloqueda.");
            sentences.Add("Creo que puedo llegar a ver que pone desde aqu�.");
            sentences.Add("��Eh?! Esto es horrible.");
            sentences.Add("Algu�en estaba difundiendo sus datos personales por internet.");
            sentences.Add("�Por eso se estaban riendo de �l!.");
            sentences.Add("A ti no te habr� pasado, �no?");
        }


        else if (selecctions.Contains("S�, mucha"))
        {
            sentences.Add("Me alegro, al menos ten�as apoyo.");
            sentences.Add("�Mira! Parece que se le cay� el tel�fono m�vil a ese chico.");
            sentences.Add("La pantalla esta desbloqueda.");
            sentences.Add("Creo que puedo llegar a ver que pone desde aqu�.");
            sentences.Add("��Eh?! Esto es horrible.");
            sentences.Add("Algu�en estaba difundiendo sus datos personales por internet.");
            sentences.Add("�Por eso se estaban riendo de �l!.");
            sentences.Add("A ti no te habr� pasado, �no?");
        }

        else if (selecctions.Contains("Algo les habr�a hecho"))
        {
            sentences.Add("Yo.. no estoy de acuerdo.");
            sentences.Add("Nadie merece pasar por algo as�.");
            sentences.Add("�Mira! Parece que se le cay� el tel�fono m�vil a ese chico.");
            sentences.Add("La pantalla esta desbloqueda.");
            sentences.Add("Creo que puedo llegar a ver que pone desde aqu�.");
            sentences.Add("��Eh?! Esto es horrible.");
            sentences.Add("Algu�en estaba difundiendo sus datos personales por internet.");
            sentences.Add("�Por eso se estaban riendo de �l!.");
            sentences.Add("A ti no te habr� pasado, �no?");
        }

        else if (selecctions.Contains("No es algo com�n"))
        {
            sentences.Add("Ya veo, espero que as� sea.");
            sentences.Add("Nadie merece pasar por algo as�.");
            sentences.Add("�Mira! Parece que se le cay� el tel�fono m�vil a ese chico.");
            sentences.Add("La pantalla esta desbloqueda.");
            sentences.Add("Creo que puedo llegar a ver que pone desde aqu�.");
            sentences.Add("��Eh?! Esto es horrible.");
            sentences.Add("Algu�en estaba difundiendo sus datos personales por internet.");
            sentences.Add("�Por eso se estaban riendo de �l!.");
            sentences.Add("A ti no te habr� pasado, �no?");
        }

        else if (selecctions.Contains("Tambi�n me pas�"))
        {
            sentences.Add("�Qu�? �De verdad?");
            sentences.Add("Lo siento mucho, no ten�a ni idea, nadie merece pasar por algo as�..");
            sentences.Add("�Tuviste ayuda?");
        }

    }

}