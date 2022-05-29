using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Leve1Manager : MonoBehaviour
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
        selecctions = new List<string>();

        activeBlink = true;
        addSentences = false;
        fragmentFound = false;
        returned = false;
        actualRoom = null;
        punctuation = new Vector2(0, 0);
        option1Punctuation = new Vector2(0, 0);
        option2Punctuation = new Vector2(0, 0);
        option3Punctuation = new Vector2(0, 0);

        blinkMaterial.SetVector("_Param", new Vector4(0.61f, 0f, 1f, 1f));

        typingSpeed = PlayerPrefs.GetFloat("textSpeed", 0.01f); ;
        dialog = gameObject.GetComponent<Dialog>();
        blink = GameObject.Find("VisualNovelCanvas/Image_Blink").GetComponent<blinkEffect>();

        nameBox.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Voz desconocida";

        pick = GameObject.Find("PowerAmulet").gameObject.GetComponent<pickFragment>();

        sentences.Add("");
        sentences.Add("�Hola?");
        sentences.Add("�Me escuchas? Despierta por favor.");

        continueChat();

    }

    // Update is called once per frame
    void Update()
    {
        if (addSentences)
        {

            m_Timer += Time.deltaTime;

            if (m_Timer >= 6)
            {
                sentences.Add("");
                sentences.Add("Menos mal que he encontrado a algu�en. Escucha necesito tu ayuda.");
                sentences.Add("Oh, quiz� deber�a presentarme primero.");
                sentences.Add("Me llamo Kairi. Estaba viajendo entre dimensiones, pero hubo turbulencias y termin� aqu�.");
                sentences.Add("�Y ahora mi talism�n transportador se ha roto y no puedo volver a mi dimensi�n!");
                sentences.Add("�Qu� c�mo me puedes ayudar? Necesito encontrar los fragmentos del talism�n, pero esta dimensi�n me resulta extra�a y me vendr�a bien tu compa��a.");
                sentences.Add("Pero ser�a raro estar en compa��a de un extra�o. �Ya s�!, �y si me cuentas algo sobre ti? Por ejemplo, �qu� edad tienes?");
                sentences.Add("Tambi�n s� que los humanos distinguis por g�nero, pero no s� como se usan. �C�mo deber�a referirme a ti?");
                sentences.Add("Entendido. �Qu� m�s te podr�a preguntar...?");
                sentences.Add("�C�mo era esa pregunta que hacen los humanos al conocerse? Ah s�, �estudias o trabajas?");
                continueChat();
                addSentences = false;
            }
        }

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
                    cam.transform.position = new Vector3(6.58f, 0.46f, 2.8f);
                    cam.transform.eulerAngles = new Vector3(20f, 270f, 0f);
                    cam.GetComponent<cameraMovement>().actualX = 20f;
                    cam.GetComponent<cameraMovement>().actualY = 270f;
                    cam.GetComponent<cameraMovement>().actualZ = 0f;
                    cam.GetComponent<cameraMovement>().initialX = 20f;
                    cam.GetComponent<cameraMovement>().initialY = 270f;
                    cam.GetComponent<cameraMovement>().initialZ = 0f;
                    actualRoom = "Cama";
                }
                else if (hit.collider.name == "Estanteria")
                {
                    cam.transform.position = new Vector3(12f, 1.3f, 5.27f);
                    cam.transform.eulerAngles = new Vector3(0f, 270f, 0f);
                    cam.GetComponent<cameraMovement>().actualX = 0f;
                    cam.GetComponent<cameraMovement>().actualY = 270f;
                    cam.GetComponent<cameraMovement>().actualZ = 0f;
                    cam.GetComponent<cameraMovement>().initialX = 0f;
                    cam.GetComponent<cameraMovement>().initialY = 270f;
                    cam.GetComponent<cameraMovement>().initialZ = 0f;
                    actualRoom = "Estanteria";
                }
                else if (hit.collider.name == "Escritorio")
                {
                    cam.transform.position = new Vector3(11.7f, 1.57f, 3.66f);
                    cam.transform.eulerAngles = new Vector3(20f, 270f, 0f);
                    cam.GetComponent<cameraMovement>().actualX = 20f;
                    cam.GetComponent<cameraMovement>().actualY = 270f;
                    cam.GetComponent<cameraMovement>().actualZ = 0f;
                    cam.GetComponent<cameraMovement>().initialX = 20f;
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

        if (pick.picked & actualRoom == "Estanteria")
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
        if (dialog.lastSentence.Contains("�Me escuchas? Despierta por favor.") & activeBlink)
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            blink.enabled = true;
            sentences.Clear();
            activeBlink = false;
            addSentences = true;
        }

        else if (dialog.lastSentence.Contains("Me llamo Kairi. Estaba viajendo entre dimensiones, pero hubo turbulencias y termin� aqu�."))
        {
            nameBox.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Kairi";
            dialog.NextSentence(sentences, typingSpeed, textBox, nameBox, continueButton, Kairi);
        }

        else if (dialog.lastSentence.Contains("Pero ser�a raro estar en compa��a de un extra�o. �Ya s�!, �y si me cuentas algo sobre ti? Por ejemplo, �qu� edad tienes?"))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Soy un/una ni�o/a";
                    option1Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Soy un/una adolescente";
                    option2Punctuation = new Vector2(7, 5);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Soy un/una adulto/a";
                    option3Punctuation = new Vector2(5, 5);
                }
            }
        }

        else if (dialog.lastSentence.Contains("Tambi�n s� que los humanos distinguis por g�nero, pero no s� como se usan. �C�mo deber�a referirme a ti?"))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "�l";
                    option1Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Ella";
                    option2Punctuation = new Vector2(4, 5);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Elle";
                    option3Punctuation = new Vector2(5, 6);
                }
            }
        }

        else if (dialog.lastSentence.Contains("�C�mo era esa pregunta que hacen los humanos al conocerse? Ah s�, �estudias o trabajas?"))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Estudio";
                    option1Punctuation = new Vector2(7, 3);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Trabajo";
                    option2Punctuation = new Vector2(6, 1);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Ninguna";
                    option3Punctuation = new Vector2(0, 0);
                }
            }
        }

        else if (dialog.lastSentence.Contains("Vaya, �y qu� tal llevas las clases?"))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Bastante bien, no me dan problemas";
                    option1Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Ni bien ni mal, voy tirando";
                    option2Punctuation = new Vector2(3, 0);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Algo mal, estudiar no es lo m�o";
                    option3Punctuation = new Vector2(6, 2);
                }
            }
        }

        else if (dialog.lastSentence.Contains("He o�do que el mundo laboral de esta dimensi�n puedo ser complicado, �qu� tal lo llevas?"))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "No es mi caso, me va bien";
                    option1Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Me da de comer, con eso me sirve";
                    option2Punctuation = new Vector2(4, 2);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Ojal� pudiese cambiar de trabajo";
                    option3Punctuation = new Vector2(5, 7);
                }
            }
        }

        else if (dialog.lastSentence.Contains("Por lo que s� eso puede ser problem�tico, �a qu� se debe?"))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Por algunas cirsumstancias, pero me gustar�a volver a hacer algo";
                    option1Punctuation = new Vector2(4, 1);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Solo no me apetece";
                    option2Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "No es de tu incumbencia";
                    option3Punctuation = new Vector2(3, 3);
                }
            }
        }

        else if (dialog.lastSentence.Contains("No ser� alg�n tipo de droga, �no?"))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Creo que te has confundido, nunca me drogar�a";
                    option1Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Lo dudo, ya dej� eso atr�s";
                    option2Punctuation = new Vector2(3, 1);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Puede, �y?";
                    option3Punctuation = new Vector2(7, 7);
                }
            }
        }

        else if (dialog.lastSentence.Contains("Ay, si alguien m�s me descubriese sin duda ser�a un esc�ndalo."))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Vivo con gente de confianza, pero no creo que hayan escuchado nada";
                    option1Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option2")
                {
                    if (selecctions.Contains("Ella"))
                    {
                        option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Vivo sola, pero me gusta m�s";
                    }
                    else if (selecctions.Contains("�l"))
                    {
                        option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Vivo solo, pero me gusta m�s";
                    }
                    else if (selecctions.Contains("Elle"))
                    {
                        option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Vivo sole, pero me gusta m�s";
                    }
                    option2Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option3")
                {
                    if (selecctions.Contains("Ella"))
                    {
                        option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Vivo sola, a veces extra�o la compa�ia";
                    }
                    else if (selecctions.Contains("�l"))
                    {
                        option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Vivo solo, a veces extra�o la compa�ia";
                    }
                    else if (selecctions.Contains("Elle"))
                    {
                        option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Vivo sole, a veces extra�o la compa�ia";
                    }
                    option3Punctuation = new Vector2(2, 4);
                }
            }
            
        }

        else if (dialog.lastSentence.Contains("Por cierto, �me podr�as decir d�nde nos encontramos? Estoy completamente desorientado."))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "En un pa�s primer mundista";
                    option1Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "En un pa�s segundo mundista";
                    option2Punctuation = new Vector2(4, 4);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "En un pa�s tercer mundista";
                    option3Punctuation = new Vector2(7, 7);
                }
            }
        }

        else if (dialog.lastSentence.Contains("�Y siempre has vivido aqu�?"))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "S�";
                    option1Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "No, pero desde hace mucho tiempo";
                    option2Punctuation = new Vector2(0, 1);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "No, hace poco que llegu�";
                    option3Punctuation = new Vector2(3, 5);
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

        else if ((dialog.lastSentence.Contains("�Me ayudar�as a buscar?") || dialog.lastSentence.Contains("T�mate el tiempo que necesites.")) && !returned)
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
            SceneManager.LoadScene("Level2");
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
                sentences.Add("Espera... �Notas eso?");
                sentences.Add("Creo que es algo que solo yo puedo sentir, pero parece que se est� iniciando un transporte.");
                sentences.Add("�Yo no lo he iniciado, pero no puedo detenerlo!");
                sentences.Add("En fin, que remedio.");
                sentences.Add("�Nos vamos!");

            }
            else
            {
                sentences.Add("Mmmm, no veo que lo tengas. T�mate el tiempo que necesites.");
            }
            selecctions.Remove("S�, aqu� est�");
        }

        else if (selecctions.Contains("S�") || selecctions.Contains("No, pero desde hace mucho tiempo") || selecctions.Contains("No, hace poco que llegu�"))
        {
            sentences.Add("Ya veo. Quiz� deber�a de dejar las preguntas, te tengo que estar aburriendo.");
            sentences.Add("La situaci�n es que puedo sentir donde se encuentr�n  los fragmentos del talism�n, y llegu� aqu� porqu� not� uno en esta habitaci�n");
            sentences.Add("�Me ayudar�as a buscar?");
        }

        else if (selecctions.Contains("En un pa�s primer mundista") || selecctions.Contains("En un pa�s segundo mundista") || selecctions.Contains("En un pa�s tercer mundista"))
        {
            sentences.Add("�Y siempre has vivido aqu�?");
        }

        else if (selecctions.Contains("Vivo sola, a veces extra�o la compa�ia") || selecctions.Contains("Vivo solo, a veces extra�o la compa�ia") || selecctions.Contains("Vivo sole, a veces extra�o la compa�ia"))
        {
            sentences.Add("La soledad puede ser muy abrumadora, entiendo como te sientes.");
            sentences.Add("Por cierto, �me podr�as decir d�nde nos encontramos? Estoy completamente desorientado.");
        }
        else if (selecctions.Contains("Vivo sola, pero me gusta m�s") || selecctions.Contains("Vivo solo, pero me gusta m�s") || selecctions.Contains("Vivo sole, pero me gusta m�s"))
        {
            sentences.Add("Est� genial que est�s agusto siendo independiente.");
            sentences.Add("Por cierto, �me podr�as decir d�nde nos encontramos? Estoy completamente desorientado.");
        }

        else if (selecctions.Contains("Vivo con gente de confianza, pero no creo que hayan escuchado nada"))
        {
            sentences.Add("Eso me tranquiliza. Intentar� mantenerme m�s calmado de todos modos.");
            sentences.Add("Por cierto, �me podr�as decir d�nde nos encontramos? Estoy completamente desorientado.");
        }

        else if (selecctions.Contains("Puede, �y?"))
        {
            sentences.Add("Yo... no soy quien para juzgar a nadie, solo espero que no te perjudique.");
            sentences.Add("Creo que me he alterado demasiado, espero no haberte molestado.");
            sentences.Add("Espera, no vivir�s con algu�en, �verdad?");
            sentences.Add("Ay, si alguien m�s me descubriese sin duda ser�a un esc�ndalo.");
        }

        else if (selecctions.Contains("Lo dudo, ya dej� eso atr�s"))
        {
            sentences.Add("Al menos saliste de aquello.");
            sentences.Add("Creo que me he alterado demasiado, espero no haberte molestado.");
            sentences.Add("Espera, no vivir�s con algu�en, �verdad?");
            sentences.Add("Ay, si alguien m�s me descubriese sin duda ser�a un esc�ndalo.");
        }

        else if (selecctions.Contains("Creo que te has confundido, nunca me drogar�a"))
        {
            sentences.Add("Es un alivio escuchar eso.");
            sentences.Add("Creo que me he alterado demasiado, espero no haberte molestado.");
            sentences.Add("Espera, no vivir�s con algu�en, �verdad?");
            sentences.Add("Ay, si alguien m�s me descubriese sin duda ser�a un esc�ndalo.");
        }

        else if (selecctions.Contains("Solo no me apetece") || selecctions.Contains("No es de tu incumbencia"))
        {
            sentences.Add("Lamento si te he incomodado, no era la intenci�n.");
            sentences.Add("�Eh!�Qu� es eso encima del escritorio?");
            sentences.Add("No ser� alg�n tipo de droga, �no?");
        }

        else if (selecctions.Contains("Por algunas cirsumstancias, pero me gustar�a volver a hacer algo"))
        {
            sentences.Add("Al menos tienes la actitud, es un buen comienzo.");
            sentences.Add("�Eh!�Qu� es eso encima del escritorio?");
            sentences.Add("No ser� alg�n tipo de droga, �no?");
        }

        else if (selecctions.Contains("Ojal� pudiese cambiar de trabajo"))
        {
            sentences.Add("Tiene que ser una situaci�n dif�cil para ti, lo siento.");
            sentences.Add("�Eh!�Qu� es eso encima del escritorio?");
            sentences.Add("No ser� alg�n tipo de droga, �no?");
        }

        else if (selecctions.Contains("Me da de comer, con eso me sirve"))
        {
            sentences.Add("Uno no siempre puede dedicarse a lo que le gusta, �eh?");
            sentences.Add("�Eh!�Qu� es eso encima del escritorio?");
            sentences.Add("No ser� alg�n tipo de droga, �no?");
        }

        else if (selecctions.Contains("Ni bien ni mal, voy tirando") || selecctions.Contains("Algo mal, estudiar no es lo m�o"))
        {
            sentences.Add("Entiendo, al fin y al cabo una nota tampoco te define.");
            sentences.Add("�Eh!�Qu� es eso encima del escritorio?");
            sentences.Add("No ser� alg�n tipo de droga, �no?");
        }

        else if (selecctions.Contains("Bastante bien, no me dan problemas") || selecctions.Contains("No es mi caso, me va bien"))
        {
            sentences.Add("Me alegra escuchar eso.");
            sentences.Add("�Eh!�Qu� es eso encima del escritorio?");
            sentences.Add("No ser� alg�n tipo de droga, �no?");
        }

        else if (selecctions.Contains("Ninguna") && !(selecctions.Contains("Por algunas cirsumstancias, pero me gustar�a volver a hacer algo") || selecctions.Contains("Solo no me apetece") || selecctions.Contains("No es de tu incumbencia")))
        {
            sentences.Add("Por lo que s� eso puede ser problem�tico, �a qu� se debe?");
        }

        else if (selecctions.Contains("Trabajo") && !(selecctions.Contains("No es mi caso, me va bien") || selecctions.Contains("Me da de comer, con eso me sirve") || selecctions.Contains("Ojal� pudiese cambiar de trabajo")))
        {
            sentences.Add("He o�do que el mundo laboral de esta dimensi�n puedo ser complicado, �qu� tal lo llevas?");
        }

        else if (selecctions.Contains("Estudio") && !(selecctions.Contains("Bastante bien, no me dan problemas") || selecctions.Contains("Ni bien ni mal, voy tirando") || selecctions.Contains("Algo mal, estudiar no es lo m�o")))
        {
            sentences.Add("Vaya, �y qu� tal llevas las clases?");
        }

    }

}
