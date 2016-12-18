using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

	private int maxScore;
	private int score;
	private Text text;

	public bool showMaxScore = false;
	public bool showCurScore = false;
	// Use this for initialization
	void Start () {
		if (!PlayerPrefs.HasKey ("score")) {
			PlayerPrefs.SetInt("score", 0);
		}
		maxScore = PlayerPrefs.GetInt ("score");
		score = 0;

		text = gameObject.GetComponent<Text> ();
		updateText ();
	}
	
	private void updateText() {
		if (showCurScore && !showMaxScore) {
			text.text = "Score : " + score + " .";
		} else if (showCurScore && showMaxScore){
			text.text = "Score : " + score + ". Max Score: " + maxScore;
		} else if (!showCurScore && showMaxScore){
			text.text = "Max Score: " + maxScore;
		} 
	}

	public void increase(int amount) {
		score += amount;
		updateText ();
	}

	public void commitScore() {
		int maxScore = PlayerPrefs.GetInt ("score");
		if (score > maxScore) {
			PlayerPrefs.SetInt ("score", score);
			maxScore = score;
		}

	}
}
