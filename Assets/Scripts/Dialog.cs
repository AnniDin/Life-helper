using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog : MonoBehaviour
{

    private int index = 0;

    [HideInInspector]
    public string lastSentence;

    private IEnumerator Type(List<string> sentences, float typingSpeed, GameObject textBox, GameObject continueButton)
    {

        continueButton.SetActive(false);

        if (sentences[index] == "")
        {
            yield return null;
        }

        else
        {
            foreach (char letter in sentences[index].ToCharArray())
            {

                textBox.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        continueButton.SetActive(true);
    }

    public void NextSentence(List<string> sentences, float typingSpeed, GameObject textBox, GameObject nameBox, GameObject continueButton, GameObject kairi)
    {
        if (index < sentences.Count() - 1)
        {
            textBox.SetActive(true);
            nameBox.SetActive(true);
            continueButton.SetActive(true);
            kairi.SetActive(true);
            index++;
            textBox.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "";
            StartCoroutine(Type(sentences, typingSpeed, textBox, continueButton));
            lastSentence = sentences[index];
        }

        else
        {
            index = 0;
            nameBox.gameObject.SetActive(false);
            textBox.SetActive(false);
            continueButton.SetActive(false);
            kairi.SetActive(false);

            sentences.Clear();
        }

    }

}
