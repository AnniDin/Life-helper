using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blinkEffect : MonoBehaviour
{

    private Material material;
    private float m_Timer;
    private float secondsToCount = 0.05F;
    private float diference = 0f;
    private float diference2 = 0f;
    private float diference3 = 0f;

    [HideInInspector]
    public bool finishBlink;

    // Start is called before the first frame update
    void Start()
    {
        material = this.GetComponent<Image>().material;
        material.SetVector("_Param", new Vector4(0.61f, -0f, 1f, 1f));
        m_Timer = 0;
        finishBlink = false;
    }

    void Update()
    {
        m_Timer += Time.deltaTime;

        if (m_Timer >= secondsToCount)
        {
            if (diference < 0.25f)
            {
                material.SetVector("_Param", new Vector4(0.61f, diference, 1f, 1f));
                diference += 0.01f;
                diference2 = diference;
                m_Timer = 0;
            }
            else if (diference2 > 0)
            {
                material.SetVector("_Param", new Vector4(0.61f, diference2, 1f, 1f));
                diference2 -= 0.01f;
                diference3 = diference2;
                m_Timer = 0;
            }
            else if (diference3 < 1.1)
            {
                material.SetVector("_Param", new Vector4(0.61f, diference3, 1f, 1f));
                diference3 += 0.01f;
                m_Timer = 0;
            }
        }

        if (diference3 >= 1f)
        {
            finishBlink = true;
        }
    }
}
