using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level11Manager : MonoBehaviour
{

    public List<string> sentences;
    public GameObject textBox;
    public GameObject nameBox;
    public GameObject continueButton;
    public GameObject Kairi;
    public GameObject[] options;
    public GameObject canvasVisualNovel;
    public float typingSpeed;

    public GameObject results;

    private Dialog dialog;

    private float m_Timer;

    private List<string> selecctions;

    [SerializeField]
    private Camera cam;


    private Vector2 punctuation;

    private Vector2 option1Punctuation;
    private Vector2 option2Punctuation;
    private Vector2 option3Punctuation;


    // Start is called before the first frame update
    void Start()
    {
        selecctions = new List<string>();
        punctuation = new Vector2(0, 0);
        option1Punctuation = new Vector2(0, 0);
        option2Punctuation = new Vector2(0, 0);
        option3Punctuation = new Vector2(0, 0);


        typingSpeed = PlayerPrefs.GetFloat("textSpeed", 0.01f); ;
        dialog = gameObject.GetComponent<Dialog>();

        nameBox.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Kairi";

        sentences.Add("");
        sentences.Add("�Bienvenido a casa!");
        sentences.Add("Parece mentira que ya hayamos terminado.");
        sentences.Add("Ha sido un viaje largo.");
        sentences.Add("�Est�s cansado?");

        continueChat();

    }


    public void continueChat()
    {

        if (dialog.lastSentence.Contains("�Est�s cansado?"))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Siempre estoy cansado";
                    option1Punctuation = new Vector2(2, 6);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Lo normal";
                    option2Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "No mucho";
                    option3Punctuation = new Vector2(0, 0);
                }
            }
        }

        else if (dialog.lastSentence.Contains("T� compa��a ha sido muy �til."))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "No he hecho nada";
                    option1Punctuation = new Vector2(4, 7);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Gracias";
                    option2Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Solo he sido una carga";
                    option3Punctuation = new Vector2(4, 7);
                }
            }
        }

        else if (dialog.lastSentence.Contains("Me has ayudado bastante."))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "No ha sido para tanto";
                    option1Punctuation = new Vector2(4, 7);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "De nada";
                    option2Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Ha sido un placer";
                    option3Punctuation = new Vector2(0, 0);
                }
            }
        }

        else if (dialog.lastSentence.Contains("�Me entiendes?"))
        {
            nameBox.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(true);
                if (option.name == "option1")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "�Qu� dices?";
                    option1Punctuation = new Vector2(0, 0);
                }
                else if (option.name == "option2")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "A veces tambi�n me pasa";
                    option2Punctuation = new Vector2(6, 6);
                }
                else if (option.name == "option3")
                {
                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Si, pero no me ocurre";
                    option3Punctuation = new Vector2(0, 0);
                }
            }
        }

        else if (dialog.lastSentence.Contains("�Adi�s!"))
        {
            this.gameObject.GetComponent<PauseMenuManager>().punctuationToSave += punctuation;
            this.gameObject.GetComponent<PauseMenuManager>().saveGame();
            results.SetActive(true);

            if (PlayerPrefs.GetFloat("punctuationX", 0f) + PlayerPrefs.GetFloat("questionnairePunctuationX", 0f) >= 0f & 
                PlayerPrefs.GetFloat("punctuationX", 0f) + PlayerPrefs.GetFloat("questionnairePunctuationX", 0f) <= 10f)
            {
                results.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = "No se han detectado niveles de ansiedad.";
            }
            else if (PlayerPrefs.GetFloat("punctuationX", 0f) + PlayerPrefs.GetFloat("questionnairePunctuationX", 0f) > 10f & 
                PlayerPrefs.GetFloat("punctuationX", 0f) + PlayerPrefs.GetFloat("questionnairePunctuationX", 0f) < 30f)
            {
                results.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = "Se han detectado ciertos niveles de ansiedad pero entran dentro de los rangos normales." +
                    " A�n as�, si estos resultan un problema en tu vida diaria se recomienda acudir a un psic�logo que ayude a gestionarlos";
            }
            else if (PlayerPrefs.GetFloat("punctuationX", 0f) + PlayerPrefs.GetFloat("questionnairePunctuationX", 0f) >= 30f & 
                PlayerPrefs.GetFloat("punctuationX", 0f) + PlayerPrefs.GetFloat("questionnairePunctuationX", 0f) < 50f)
            {
                results.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = "Se han detectado ciertos niveles de ansiedad que podr�an estar afectando a tu vida diaria." +
                    " Se recomienda acudir a un psic�logo que ayude a gestionarlos.";
            }
            else if (PlayerPrefs.GetFloat("punctuationX", 0f) + PlayerPrefs.GetFloat("questionnairePunctuationX", 0f) >= 50f)
            {
                results.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = "Se han detectado altos niveles de ansiedad preocupantes." +
                    " Se recomienda acudir de forma inmediata a un especialista.";
            }
            else
            {
                results.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = "No se han detectado niveles de ansiedad.";
            }

            if (PlayerPrefs.GetFloat("punctuationY", 0f) + PlayerPrefs.GetFloat("questionnairePunctuationY", 0f) >= 0f & 
                PlayerPrefs.GetFloat("punctuationY", 0f) + PlayerPrefs.GetFloat("questionnairePunctuationY", 0f) <= 10f)
            {
                results.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().text = "No se ha detectado sintomatolog�a depresiva.";
            }
            else if (PlayerPrefs.GetFloat("punctuationY", 0f) + PlayerPrefs.GetFloat("questionnairePunctuationY", 0f) > 10f & 
                PlayerPrefs.GetFloat("punctuationY", 0f) + PlayerPrefs.GetFloat("questionnairePunctuationY", 0f) < 30f)
            {
                results.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().text = "Se han detectado ciertos niveles de sintomatolog�a depresiva pero entran dentro de los rangos bajos." +
                    " A�n as�, si estos resultan un problema en tu vida diaria se recomienda acudir a un psic�logo que ayude a gestionarlos";
            }
            else if (PlayerPrefs.GetFloat("punctuationY", 0f) + PlayerPrefs.GetFloat("questionnairePunctuationY", 0f) >= 30f & 
                PlayerPrefs.GetFloat("punctuationY", 0f) + PlayerPrefs.GetFloat("questionnairePunctuationY", 0f) < 50f)
            {
                results.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().text = "Se han detectado ciertos niveles de sintomatolog�a depresiva que podr�an estar afectando a tu vida diaria." +
                    " Se recomienda acudir a un psic�logo que ayude a gestionarlos.";
            }
            else if (PlayerPrefs.GetFloat("punctuationY", 0f) >= 50f)
            {
                results.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().text = "Se han detectado altos niveles de sintomatolog�a depresiva preocupantes." +
                    " Se recomienda acudir de forma inmediata a un especialista.";
            }
            else
            {
                results.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().text = "No se ha detectado sintomatolog�a depresiva.";
            }
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
        if (selecctions.Contains("�Qu� dices?"))
        {
            sentences.Add("�Tan raro es?");
            sentences.Add("Ser�n cosas mias entonces-");
            sentences.Add("Ah...");
            sentences.Add("Creo que ha llegado el momento de que me vaya.");
            sentences.Add("Ha sido un placer conocerte.");
            sentences.Add("Qui�n sabe, puede que nos volvamos a encontrar.");
            sentences.Add("�Adi�s!");
        }

        else if (selecctions.Contains("A veces tambi�n me pasa") || selecctions.Contains("Si, pero no me ocurre"))
        {
            sentences.Add("Saber que me entiendes me relaja");
            sentences.Add("Ah...");
            sentences.Add("Creo que ha llegado el momento de que me vaya.");
            sentences.Add("Ha sido un placer conocerte.");
            sentences.Add("Qui�n sabe, puede que nos volvamos a encontrar.");
            sentences.Add("�Adi�s!");
        }

        else if (selecctions.Contains("No ha sido para tanto") || selecctions.Contains("De nada") || selecctions.Contains("Ha sido un placer"))
        {
            sentences.Add("Ha sido un d�a extra�o para mi tambi�n.");
            sentences.Add("No suelo salir de mi dimensi�n.");
            sentences.Add("Estando aqu� me siento como si no fuese yo.");
            sentences.Add("�Me entiendes?");

        }


        else if (selecctions.Contains("No he hecho nada") || selecctions.Contains("Solo he sido una carga"))
        {
            sentences.Add("No digas eso.");
            sentences.Add("Sin ti habr�a estado completamente perdido.");
            sentences.Add("Te tengo que dar las gracias.");
            sentences.Add("Me has ayudado bastante.");
        }

        else if (selecctions.Contains("Gracias"))
        {
            sentences.Add("�Gracias a ti!");
            sentences.Add("Me has ayudado bastante.");
        }

        else if (selecctions.Contains("Siempre estoy cansado"))
        {

            sentences.Add("�Y est�s seguro de qu� eso es normal?");
            sentences.Add("Bueno, al menos ahora ya podr�s descansar.");
            sentences.Add("Seguro que te ir� bien.");
            sentences.Add("T� compa��a ha sido muy �til.");
        }

        else if (selecctions.Contains("Lo normal") || selecctions.Contains("No mucho"))
        {
            sentences.Add("Algo es algo.");
            sentences.Add("Al menos ahora ya podr�s descansar.");
            sentences.Add("Seguro que te ir� bien.");
            sentences.Add("T� compa��a ha sido muy �til.");

        }

    }

}
