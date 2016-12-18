using UnityEngine;
using System.Collections;

public class MusicScript : MonoBehaviour {

	public static MusicScript Instance = null;



	void Awake() {
		// Register the singleton
		if (Instance != null && Instance != this) {
			Debug.Log("reinstanciete MusicScript");
			
			Destroy (this.gameObject);
			
			return;
		} else {
			Instance = this;
		}

		DontDestroyOnLoad(gameObject);
	}

	public void enable() {
		GetComponent<AudioSource> ().Play ();
	}

	public void disable() {
		GetComponent<AudioSource> ().Pause ();
	}
}
