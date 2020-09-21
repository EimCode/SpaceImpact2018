using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scores : MonoBehaviour {

    public Text highScores;
    int level1score=0;
    int level2score=0;

	// Use this for initialization
	void Start () {
        if (PlayerPrefs.GetInt("Level1")>level1score) {
            level1score = PlayerPrefs.GetInt("Level1");
        }
        if (PlayerPrefs.GetInt("Level2") > level2score) {
            level2score = PlayerPrefs.GetInt("Level2");
        }

        highScores.text = "Level1: " + level1score+"\nLevel2: "+level2score;
	}

}
