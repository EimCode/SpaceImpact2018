using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour {

    public GameObject pauseMenuUI;
    public GameObject Settings;
    bool gameIsPaused = false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        EscapePressed();
	}

    void EscapePressed()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !Settings.activeSelf)
        {
            if (gameIsPaused)
                Resume();
            else
                Pause();
        }
    }
    public void Resume()
    {
        if (!Settings.activeSelf) {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1;
            gameIsPaused = false;
        }
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Levels");
        //Application.Quit();
    }
}
