using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickFragment : MonoBehaviour
{
    [HideInInspector]
    public bool picked = false;

    void OnMouseDown()
    {
        picked = true;
    }
    private void OnMouseUp()
    {
        picked = false;
    }

    private void OnDestroy()
    {
        picked = false;
    }
}