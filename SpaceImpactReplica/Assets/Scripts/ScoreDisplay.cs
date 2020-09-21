using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {
    public Text scoreBox;
    public GameObject player;
    public int maxPoints = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(player)
            scoreBox.text = "SCORE: " + player.GetComponent<Movement>().maxPoints.ToString() + "\nMoney: " + player.GetComponent<Movement>().points.ToString();
	}
}
