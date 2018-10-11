using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBasket : MonoBehaviour {
	public Text scoreText;

	private int score;


	// Use this for initialization
	void Start () {
		score = 0;
		scoreText.text = score.ToString();
	}

	void OnTriggerEnter(Collider other)
    {
		score++;
		scoreText.text = score.ToString();
	}
}
