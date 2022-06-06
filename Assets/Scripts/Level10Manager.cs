using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level10Manager : MonoBehaviour
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

    private bool pregnant;

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

        if (PlayerPrefs.GetInt("pregnancyValue", 0) != 0)
        {
            pregnant = true;
        }
        else
        {
            pregnant = false;
        }

        typingSpeed = PlayerPrefs.GetFloat("textSpeed", 0.01f); ;
        dialog = gameObject.GetComponent<Dialog>();

        nameBox.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Kairi";

        pick = GameObject.Find("PowerAmulet").gameObject.GetComponent<pickFragment>();

        if (pregnant)
        {
            sentences.Add("");
            sentences.Add("Y ahora estamos en un hospital.");
            sentences.Add("Desde luego no es un sitio muy agradable.");
            sentences.Add("Pero al menos ya es el �ltimo.");

            sentences.Add("�De cu�ntas semanas est�?");

            sentences.Add("34.");

            sentences.Add("Podr�a ser que le beb� se est� adelantando, as� que llamaremos a la doctora para que la revise.");

            sentences.Add("Parece que esa mujer est� embarazada.");
            sentences.Add("A veces se me olvida que otras especies no salen de huevos.");
            sentences.Add("Es verdad, t� tambi�n tienes un beb�.");
            sentences.Add("�C�mo fue tu experiencia?");
        }
        else
        {
            sentences.Add("");
            sentences.Add("Y ahora estamos en un hospital.");
            sentences.Add("Desde luego no es un sitio muy agradable.");
            sentences.Add("Pero al menos ya es el �ltimo.");
            sentences.Add("La enfermera va a atender a aquella joven.");
            sentences.Add("No tiene buen aspecto, me pregunto que le habr� ocurrido.");

            sentences.Add("Necesitar�a un parte de lesiones...");
            sentences.Add("Tuve un altercado con mi pareja.");

            sentences.Add("Si t� lo dices...");
            sentences.Add("En seguida te pasamos a consulta.");

            sentences.Add("��Pero...?!");
            sentences.Add("�Y esa actitud?");
            sentences.Add("Esa no es forma de tratar a algu�en que ha pasado por algo tan dif�cil.");
            sentences.Add("La pobre se tiene que haber sentido fatal.");
            sentences.Add("Esto no puede ser normal.");
        }

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
                if (hit.collider.name == "Recepcion")
                {
                    cam.transform.position = new Vector3(1.814f, 3.56f, 80.36f);
                    cam.transform.eulerAngles = new Vector3(0, -180, 0f);
                    cam.GetComponent<cameraMovement>().actualX = 0;
                    cam.GetComponent<cameraMovement>().actualY = -180;
                    cam.GetComponent<cameraMovement>().actualZ = 0f;
                    cam.GetComponent<cameraMovement>().initialX = 0;
                    cam.GetComponent<cameraMovement>().initialY = -180;
                    cam.GetComponent<cameraMovement>().initialZ = 0f;
                    actualRoom = "Recepcion";
                }
                else if (hit.collider.name == "Sillas")
                {
                    cam.transform.position = new Vector3(5.44f, 2.3f, 84.55f);
                    cam.transform.eulerAngles = new Vector3(10, -90, 0f);
                    cam.GetComponent<cameraMovement>().actualX = 10f;
                    cam.GetComponent<cameraMovement>().actualY = -90;
                    cam.GetComponent<cameraMovement>().actualZ = 0f;
                    cam.GetComponent<cameraMovement>().initialX = 10f;
                    cam.GetComponent<cameraMovement>().initialY = -90;
                    cam.GetComponent<cameraMovement>().initialZ = 0f;
                    actualRoom = "Sillas";
                }
            }
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

        if (pick.picked & actualRoom == "Sillas")
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
        if (dialog.lastSentence.Contains("Pero al menos ya es el �ltimo.") || dialog.lastSentence.Contains("34.") 
            || dialog.lastSentence.Contains("En verdad, no s� nada del tema as� que creo que ser� mejor que no opine.") || dialog.lastSentence.Contains("Tuve un altercado con mi pareja."))
        {
            nameBox.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Enfermera";
            dialog.NextSentence(sentences, typingSpeed, textBox, nameBox, continueButton, Kairi);
        }

        else if (dialog.lastSentence.Contains("Podr�a ser que le beb� se est� adelantando, as� que llamaremos a la doctora para que la revise.") 
            || dialog.lastSentence.Contains("Recuerde firmar los papeles para dar el beb� en adopci�n despu�s del parto.")
            || dialog.lastSentence.Contains("En seguida te pasamos a consulta."))
        {
            nameBox.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Kairi";
            dialog.NextSentence(sentences, typingSpeed, textBox, nameBox, continueButton, Kairi);
        }

        else if (dialog.lastSentence.Contains("�De cu�ntas semanas est�?"))
        {
            nameBox.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Mujer";
            dialog.NextSentence(sentences, typingSpeed, textBox, nameBox, continueButton, Kairi);
        }


        else if (dialog.lastSentence.Contains("No tiene buen aspecto, me pregunto que le habr� ocurrido."))
        {
            nameBox.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Joven";
            dialog.NextSentence(sentences, typingSpeed, textBox, nameBox, continueButton, Kairi);
        }

        else if (dialog.lastSentence.Contains("�C�mo fue tu experiencia?"))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Estaba sola";
                    option1Punctuation = new Vector2(3, 5);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Tuve mucho apoyo";
                    option2Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Est�ndar";
                    option3Punctuation = new Vector2(0, 0);
                }
            }
        }

        else if (dialog.lastSentence.Contains("Puede que no pueda mantenerlo."))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Yo tampoco";
                    option1Punctuation = new Vector2(10, 6);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Lo dudo";
                    option2Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "A saber";
                    option3Punctuation = new Vector2(0, 0);
                }
            }
        }

        else if (dialog.lastSentence.Contains("En un momento as� es importante que te traten bien."))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Yo sufr� violencia obst�trica";
                    option1Punctuation = new Vector2(8, 8);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Mi atenci�n tambi�n ha sido buena";
                    option2Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Lo m�o fue un punto medio";
                    option3Punctuation = new Vector2(0, 0);
                }
            }
        }

        else if (dialog.lastSentence.Contains("Esto no puede ser normal."))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Pas� por lo mismo, y tambi�n me ridiculizaron";
                    option1Punctuation = new Vector2(19, 14);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Pas� por lo mismo, pero no me ridiculizaron";
                    option2Punctuation = new Vector2(10, 10);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "No sabr�a decirte";
                    option3Punctuation = new Vector2(0, 0);
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

        else if ((dialog.lastSentence.Contains("Econtremos el fragmento y marchemonos.") || dialog.lastSentence.Contains("T�mate el tiempo que necesites.")) && !returned)
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

        else if (dialog.lastSentence.Contains("Es hora de volver a casa."))
        {
            this.gameObject.GetComponent<PauseMenuManager>().punctuationToSave += punctuation;
            SceneManager.LoadScene("Level11");
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
                sentences.Add("Es hora de volver a casa.");

            }
            else
            {
                sentences.Add("Mmmm, no veo que lo tengas. T�mate el tiempo que necesites.");
            }
            selecctions.Remove("S�, aqu� est�");
        }

        else if (selecctions.Contains("No sabr�a decirte"))
        {
            sentences.Add("Ya veo.");
            sentences.Add("Este sitio no me est� gustando nada.");
            sentences.Add("Econtremos el fragmento y marchemonos.");
        }

        else if (selecctions.Contains("Pas� por lo mismo, y tambi�n me ridiculizaron") || selecctions.Contains("Pas� por lo mismo, pero no me ridiculizaron"))
        {
            sentences.Add("�Pero eso es horrible!");
            sentences.Add("Tuviste que pasarlo fatal.");
            sentences.Add("Este sitio no me est� gustando nada.");
            sentences.Add("Econtremos el fragmento y marchemonos.");

        }

        else if (selecctions.Contains("Yo sufr� violencia obst�trica"))
        {
            sentences.Add("�Eso es horrible!");
            sentences.Add("Tuvo que ser muy inc�modo para ti.");
            sentences.Add("Espero que no te afectase demasiado.");
            sentences.Add("Aunque s� que es complicado.");
            sentences.Add("Parece que ya ha venido la doctora a buscarla.");
            sentences.Add("Ahora la enfermera va a atender a aquella joven.");
            sentences.Add("No tiene buen aspecto, me pregunto que le habr� ocurrido.");

            sentences.Add("Necesitar�a un parte de lesiones...");
            sentences.Add("Tuve un altercado con mi pareja.");

            sentences.Add("Si t� lo dices...");
            sentences.Add("En seguida te pasamos a consulta.");

            sentences.Add("��Pero...?!");
            sentences.Add("�Y esa actitud?");
            sentences.Add("Esa no es forma de tratar a algu�en que ha pasado por algo tan dif�cil.");
            sentences.Add("La pobre se tiene que haber sentido fatal.");
            sentences.Add("Esto no puede ser normal.");
        }

        else if (selecctions.Contains("Mi atenci�n tambi�n ha sido buena") || selecctions.Contains("Lo m�o fue un punto medio"))
        {
            sentences.Add("Entiendo.");
            sentences.Add("Eso no est� tan mal.");
            sentences.Add("Parece que ya ha venido la doctora a buscarla.");
            sentences.Add("Ahora la enfermera va a atender a aquella joven.");
            sentences.Add("No tiene buen aspecto, me pregunto que le habr� ocurrido.");

            sentences.Add("Necesitar�a un parte de lesiones...");
            sentences.Add("Tuve un altercado con mi pareja.");

            sentences.Add("Si t� lo dices...");
            sentences.Add("En seguida te pasamos a consulta.");

            sentences.Add("��Pero...?!");
            sentences.Add("�Y esa actitud?");
            sentences.Add("Esa no es forma de tratar a algu�en que ha pasado por algo tan dif�cil.");
            sentences.Add("La pobre se tiene que haber sentido fatal.");
            sentences.Add("Esto no puede ser normal.");

        }

        else if (selecctions.Contains("Yo tampoco"))
        {
            sentences.Add("Seguro que eso fue un problema.");
            sentences.Add("Espero que la situaci�n mejore.");
            sentences.Add("Al menos la enfermera parece bastante simp�tica.");
            sentences.Add("En un momento as� es importante que te traten bien.");
        }

        else if (selecctions.Contains("Lo dudo") || selecctions.Contains("A saber"))
        {

            sentences.Add("Tienes raz�n.");
            sentences.Add("Es muy dif�cil de saber.");
            sentences.Add("Al menos la enfermera parece bastante simp�tica.");
            sentences.Add("En un momento as� es importante que te traten bien.");
        }

        else if (selecctions.Contains("Estaba sola") || selecctions.Contains("Tuve mucho apoyo") || selecctions.Contains("Est�ndar"))
        {
            sentences.Add("�Y eso es lo com�n...?");
            sentences.Add("En verdad, no s� nada del tema as� que creo que ser� mejor que no opine.");

            sentences.Add("Recuerde firmar los papeles para dar el beb� en adopci�n despu�s del parto.");

            sentences.Add("Vaya... Eso no me lo esperaba.");
            sentences.Add("Puede que no pueda mantenerlo.");

        }

    }

}

