using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    public GameObject ui;

	public void LoadScene(string scene) {
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene);
    }
    public void SetACtive() {
        if (ui.activeSelf==true) {
            ui.SetActive(false);
        } else { ui.SetActive(true); }
    }

    public void ExitGame() {
        Application.Quit();
    }
}
