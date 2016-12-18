using UnityEngine;
using System.Collections;

public class BonusItemScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//destroy bonus item in 5 seconds
		Destroy(gameObject, 5);
	}
	
	void OnTriggerEnter2D(Collider2D otherCollider) {
		Debug.Log ("take bonus");
		//check if player take bonus
		PlayerScript player = otherCollider.GetComponent<PlayerScript> ();
		if (player != null) {
			//sounds effect
			SoundEffectsHelper.Instance.MakeBonusSound();

			//increase score
			ScoreScript[] scoreScripts = FindObjectsOfType(typeof(ScoreScript)) as ScoreScript[];
			foreach (ScoreScript script in scoreScripts) {
				script.increase(50);
			}
			
			//destroy bonus item
			Destroy(gameObject);
		}
	}
}
