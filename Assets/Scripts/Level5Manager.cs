using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level5Manager : MonoBehaviour
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
        sentences.Add("Esto es una cafetería, ¿cierto?");
        sentences.Add("Se está bastante tranquilo, parece que por fin tendremos algo de paz.");
        sentences.Add("No pensaba que tendríamos que presenciar situaciones tan incómodas cuando te pedí que me acompañases.");
        sentences.Add("Lamento si lo estás pasando mal.");
        sentences.Add("Al menos ahora podemos descansar un poco.");
        sentences.Add("Oh, la camarera está viniendo hacía aquí.");
        sentences.Add("Deberíamos pedir algo para poder quedarnos un rato.");
        sentences.Add("¿Te parece bien?");

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
                    cam.transform.position = new Vector3(4.3f, 1.7f, 26.18f);
                    cam.transform.eulerAngles = new Vector3(19.84f, 270f, 0f);
                    cam.GetComponent<cameraMovement>().actualX = 19.84f;
                    cam.GetComponent<cameraMovement>().actualY = 270f;
                    cam.GetComponent<cameraMovement>().actualZ = 0f;
                    cam.GetComponent<cameraMovement>().initialX = 19.84f;
                    cam.GetComponent<cameraMovement>().initialY = 270f;
                    cam.GetComponent<cameraMovement>().initialZ = 0f;
                    actualRoom = "Mesa";
                }
                else if (hit.collider.name == "Silla")
                {
                    cam.transform.position = new Vector3(3.16f, 2.28f, 26.18f);
                    cam.transform.eulerAngles = new Vector3(32.06f, 270f, 0f);
                    cam.GetComponent<cameraMovement>().actualX = 32.06f;
                    cam.GetComponent<cameraMovement>().actualY = 270f;
                    cam.GetComponent<cameraMovement>().actualZ = 0f;
                    cam.GetComponent<cameraMovement>().initialX = 32.06f;
                    cam.GetComponent<cameraMovement>().initialY = 270f;
                    cam.GetComponent<cameraMovement>().initialZ = 0f;
                    actualRoom = "Silla";
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
        if (dialog.lastSentence.Contains("Vaya, aquí está.") || dialog.lastSentence.Contains("¿Puedes ser más rápida?"))
        {
            nameBox.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Camarera";
            dialog.NextSentence(sentences, typingSpeed, textBox, nameBox, continueButton, Kairi);
        }

        else if (dialog.lastSentence.Contains("En seguida te lo traigo.") || dialog.lastSentence.Contains("Aquí lo tienes."))
        {
            nameBox.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Kairi";
            dialog.NextSentence(sentences, typingSpeed, textBox, nameBox, continueButton, Kairi);
        }

        else if (dialog.lastSentence.Contains("Me preguntó si la mayoría de humanos podrán."))
        {
            nameBox.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Hombre";
            dialog.NextSentence(sentences, typingSpeed, textBox, nameBox, continueButton, Kairi);
        }

        else if (dialog.lastSentence.Contains("¿Te parece bien?"))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Prefiero no comer";
                    option1Punctuation = new Vector2(10, 7);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "¡Tengo mucha hambre!";
                    option2Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Pediré algo ligero";
                    option3Punctuation = new Vector2(0, 0);
                }
            }
        }

        else if (dialog.lastSentence.Contains("¿Sabes a qué me refiero?"))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Nunca me ha pasado";
                    option1Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Me ha pasado muy poco";
                    option2Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Me pasa coninuamente";
                    option3Punctuation = new Vector2(5, 3);
                }
            }
        }

        else if (dialog.lastSentence.Contains("Aunque es más fácil decirlo que hacerlo, ¿verdad?"))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Yo nunca lo consigo";
                    option1Punctuation = new Vector2(7, 4);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Pero se puede";
                    option2Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "No he tenido la necesidad";
                    option3Punctuation = new Vector2(0, 0);
                }
            }
        }

        else if (dialog.lastSentence.Contains("Quizá estoy hablando por hablar y eso es normal aquí."))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "No lo es";
                    option1Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Sí lo es";
                    option2Punctuation = new Vector2(6, 1);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Según a quien le preguntes";
                    option3Punctuation = new Vector2(0, 0);
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

        else if ((dialog.lastSentence.Contains("No deberíamos perder mucho tiempo tampoco.") || dialog.lastSentence.Contains("Tómate el tiempo que necesites.")) && !returned)
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
            SceneManager.LoadScene("Level6");
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
                sentences.Add("A por el siguiente entonces.");
                sentences.Add("¡Nos vamos!");

            }
            else
            {
                sentences.Add("Mmmm, no veo que lo tengas. Tómate el tiempo que necesites.");
            }
            selecctions.Remove("Sí, aquí está");
        }

        else if (selecctions.Contains("No lo es") || selecctions.Contains("Sí lo es") || selecctions.Contains("Según a quien le preguntes"))
        {
            sentences.Add("Entiendo.");
            sentences.Add("¿Seguimos buscando los fragmentos?");
            sentences.Add("No deberíamos perder mucho tiempo tampoco.");
        }


        else if (selecctions.Contains("Pero se puede"))
        {
            sentences.Add("Sí, estoy de acuerdo.");
            sentences.Add("Al final depende mucho de la persona y de cuanto le importen las opiniones externas.");
            sentences.Add("Me alegro de que a ti no te suponga un problema.");
            sentences.Add("Me preguntó si la mayoría de humanos podrán.");

            sentences.Add("¡Llevo diez minutos esperando un café!");
            sentences.Add("¿Puedes ser más rápida?");

            sentences.Add("Aquí lo tienes.");

            sentences.Add("Pensaba que ese señor iba a montar una escena.");
            sentences.Add("Pero se ha calmado en cuanto le han dado el café.");
            sentences.Add("¿No ha sido como muy repentino?");
            sentences.Add("Quizá estoy hablando por hablar y eso es normal aquí.");
        }

        else if (selecctions.Contains("No he tenido la necesidad"))
        {
            sentences.Add("Es mejor así.");
            sentences.Add("Solo tenlo en mente para cuanndo lo necesites.");
            sentences.Add("Nunca se sabe.");
            sentences.Add("Me pregunto si la mayoría de humanos podrán.");

            sentences.Add("¡Llevo diez minutos esperando un café!");
            sentences.Add("¿Puedes ser más rápida?");

            sentences.Add("Aquí lo tienes.");

            sentences.Add("Pensaba que ese señor iba a montar una escena.");
            sentences.Add("Pero se ha calmado en cuanto le han dado el café.");
            sentences.Add("¿No ha sido como muy repentino?");
            sentences.Add("Quizá estoy hablando por hablar y eso es normal aquí.");
        }

        else if (selecctions.Contains("Yo nunca lo consigo"))
        {
            sentences.Add("Es dífícil.");
            sentences.Add("Pero estoy seguro de que con el tiempo aprenderás.");
            sentences.Add("Es algo necesario al fin y al cabo.");
            sentences.Add("Me pregunto si la mayoría de humanos podrán.");

            sentences.Add("¡Llevo diez minutos esperando un café!");
            sentences.Add("¿Puedes ser más rápida?");

            sentences.Add("Aquí lo tienes.");

            sentences.Add("Pensaba que ese señor iba a montar una escena.");
            sentences.Add("Pero se ha calmado en cuanto le han dado el café.");
            sentences.Add("¿No ha sido como muy repentino?");
            sentences.Add("Quizá estoy hablando por hablar y eso es normal aquí.");
        }

        else if (selecctions.Contains("Nunca me ha pasado") || selecctions.Contains("Me ha pasado muy poco"))
        {
            sentences.Add("Oh, será cosa mía entonces.");
            sentences.Add("Es cierto que no conozco a nadie más que le pase.");
            sentences.Add("¿Y si estoy enfermo?");
            sentences.Add("¿Debería ir al médico en cuanto vuelva a mi dimensión?");
            sentences.Add("No, no creo que sea para tanto.");

            sentences.Add("Quizá dejé que los comentarios de que nunca me entero de nada me afectaran demasiado.");
            sentences.Add("Al final los comentarios de los demás afectan más de lo que crees.");
            sentences.Add("Supongo que al final lo importante es saber distingir tu propia opinión.");
            sentences.Add("Y no dejar que te impongan la suya.");
            sentences.Add("Aunque es más fácil decirlo que hacerlo, ¿verdad?");
        }

        else if (selecctions.Contains("Me pasa coninuamente"))
        {
            sentences.Add("¡Sabía que no podía ser el único!");
            sentences.Add("Me dejas más tranquilo.");
            sentences.Add("Por un momento pensé que era raro y me asusté.");
            sentences.Add("Pero que le pase a alguien más me relaja.");

            sentences.Add("Quizá dejé que los comentarios de que nunca me entero de nada me afectaran demasiado.");
            sentences.Add("Al final los comentarios de los demás afectan más de lo que crees.");
            sentences.Add("Supongo que al final lo importante es saber distingir tu propia opinión.");
            sentences.Add("Y no dejar que te impongan la suya.");
            sentences.Add("Aunque es más fácil decirlo que hacerlo, ¿verdad?");

        }

        else if (selecctions.Contains("¡Tengo mucha hambre!") || selecctions.Contains("Pediré algo ligero"))
        {
            sentences.Add("¡Genial!"); 
            sentences.Add("Me gustaría poder pedir algo a mi también.");
            sentences.Add("Pero creo que aquí vendan comida de mi dimensión.");
            sentences.Add("Me tendré que conformar con verte comer a ti.");
            sentences.Add("Vaya, aquí está.");

            sentences.Add("¿Qué te sirvo?");
            sentences.Add("Mmm... entendido.");
            sentences.Add("En seguida te lo traigo.");

            sentences.Add("Realmente este es un buen sitio para perderse en tu mundo.");
            sentences.Add("¿Qué a qué me refiero");
            sentences.Add("Ya sabes, cuando te quedas con la mente en blanco.");
            sentences.Add("A mí me solían decir que era un despistado porque me pasaba a menudo.");
            sentences.Add("¿Sabes a qué me refiero?");

        }

        else if (selecctions.Contains("Prefiero no comer"))
        {
            sentences.Add("¿En serio?");
            sentences.Add("Bueno... como tú veas.");
            sentences.Add("Quizá podamos pedir solo un agua entonces.");
            sentences.Add("Vaya, aquí está.");

            sentences.Add("¿Qué te sirvo?");
            sentences.Add("Mmm... entendido.");
            sentences.Add("En seguida te lo traigo.");

            sentences.Add("Realmente este es un buen sitio para perderse en tu mundo.");
            sentences.Add("¿Qué a qué me refiero?");
            sentences.Add("Ya sabes, cuando te quedas con la mente en blanco.");
            sentences.Add("A mí me solían decir que era un despistado porque me pasaba a menudo.");
            sentences.Add("¿Sabes a qué me refiero?");
        }

    }

}

