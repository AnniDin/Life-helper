using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    private float actualX;
    private float actualY;
    private float actualZ;

    private float initialX;
    private float initialY;
    private float initialZ;

    private float m_Timer;

    public DemoScript cursorControl;

    private Resolution resolution;

    // Start is called before the first frame update
    void Start()
    {
        actualX = transform.eulerAngles.x;
        actualY = transform.eulerAngles.y;
        actualZ = transform.eulerAngles.z;

        initialX = transform.eulerAngles.x;
        initialY = transform.eulerAngles.y;
        initialZ = transform.eulerAngles.z;

        cursorControl.CenterCursor();

        m_Timer = 0;

        resolution = Screen.currentResolution;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 mousePos = Input.mousePosition;
        if (mousePos.y > resolution.height-200 && actualX > (initialX - 10))
        {
            m_Timer += Time.deltaTime;
            if(m_Timer >= 0.03)
            {
                actualX--;
                transform.eulerAngles = new Vector3(actualX, actualY, actualZ);
                m_Timer = 0;
            }
        }
        else if (mousePos.y < resolution.height/3 && actualX < (initialX + 10))
        {
            m_Timer += Time.deltaTime;
            if (m_Timer >= 0.03)
            {
                actualX++;
                transform.eulerAngles = new Vector3(actualX, actualY, actualZ);
                m_Timer = 0;
            }
        }

        if (mousePos.x > resolution.width - 200 && actualY < (initialY + 10))
        {
            m_Timer += Time.deltaTime;
            if (m_Timer >= 0.03)
            {
                actualY++;
                transform.eulerAngles = new Vector3(actualX, actualY, actualZ);
                m_Timer = 0;
            }
        }

        else if (mousePos.x < resolution.width/3 && actualY > (initialY - 10))
        {
            m_Timer += Time.deltaTime;
            if (m_Timer >= 0.03)
            {
                actualY--;
                transform.eulerAngles = new Vector3(actualX, actualY, actualZ);
                m_Timer = 0;
            }
        }
    }
}
