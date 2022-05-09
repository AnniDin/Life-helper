using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{

    public GameObject pauseMenu;

    private bool pauseMenuEnable;

    [HideInInspector]
    public Vector2 punctuationToSave;

    private void Start()
    {
        pauseMenuEnable = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseMenuEnable)
            {
                pauseMenu.SetActive(true);
                pauseMenuEnable = true;
            }
            else
            {
                closePauseMenu();
            }
        }
    }

    public void returnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void saveGame()
    {
        PlayerPrefs.SetFloat("punctuationX", punctuationToSave.x);
        PlayerPrefs.SetFloat("punctuationY", punctuationToSave.y);
        PlayerPrefs.SetString("currentLevel", SceneManager.GetActiveScene().name);
    }

    public void closePauseMenu()
    {
        pauseMenu.SetActive(false);
        pauseMenuEnable = false;
    }

    private void OnEnable()
    {
        punctuationToSave.x = PlayerPrefs.GetFloat("punctuationX", 0f);
        punctuationToSave.y = PlayerPrefs.GetFloat("punctuationY", 0f);
    }
}
