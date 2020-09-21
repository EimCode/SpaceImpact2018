using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour {

    public Movement player;
    public GameObject deathMenu;
    public GameObject GameControl;
    public Text lvlComplete;
    public Text lvlFail;


    // Use this for initialization

    public void OnRestart()
    {
        Time.timeScale = 1f;
        LoadScene("SpaceImpactReplica");

    }

    void Start() {

    }

    // Update is called once per frame
    void Update()
    {

        if (player.isDead)
        {
            deathMenu.SetActive(true);
            Time.timeScale = 0;
            if (PlayerPrefs.GetInt("Level1") >= player.maxPoints)
                lvlFail.text = "Your score: " + player.maxPoints + "\nAll time highscore: " + PlayerPrefs.GetInt("Level1");
            else
            {
                PlayerPrefs.SetInt("Level1", player.maxPoints);
                lvlFail.text = "New Highscore: " + PlayerPrefs.GetInt("Level1");
            }

            GameObject canvas = GameObject.FindWithTag("Canvas");
            GameObject price = getChildGameObject(canvas, "Price");
            price.SetActive(false);
            //GameObject.FindGameObjectWithTag("ProceedButton").SetActive(false);
            //GameObject.FindGameObjectWithTag("PlayButton").SetActive(true);
        }
    }
    public void LoadScene(string scene)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene);
    }

    static public GameObject getChildGameObject(GameObject fromGameObject, string withName)
    {
        Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
        return null;
    }

}
