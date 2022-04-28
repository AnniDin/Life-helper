using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class ChangeColorText : MonoBehaviour
{
    GameObject textObject;

    private void Start()
    {
        textObject = this.gameObject;
    }

    public void Mouseon()
    {
        textObject.GetComponent<Text>().color = Color.black;
    }

    public void Mouseoff()
    {
        textObject.GetComponent<Text>().color = Color.white;
    }
}
