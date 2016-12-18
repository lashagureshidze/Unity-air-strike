using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {

	public int buttonKey = 0;

	private bool soundEnabled = true;
	private bool musicEnabled = true;

	// Use this for initialization
	void Start () {
		if (buttonKey == ButtonKey.music) {
			//load data
			if (! PlayerPrefs.HasKey("music")) {
				PlayerPrefs.SetInt("music", 1);
			}
			musicEnabled = (PlayerPrefs.GetInt("music")) == 1;

			updateMusicButtonAppearence ();

			updateMusicState();
		}
		if (buttonKey == ButtonKey.sound) {
			//load data
			if (! PlayerPrefs.HasKey("sound")) {
				PlayerPrefs.SetInt("sound", 1);
			}
			soundEnabled = (PlayerPrefs.GetInt("sound")) == 1;

			updateSoundButtonAppearence ();

			updateSoundState();
		}
	}

	public void onButtonClick() {
		//sound
		SoundEffectsHelper.Instance.MakeButtonSound ();


	//TODO button click
		if (buttonKey == ButtonKey.music) {
			musicEnabled = !musicEnabled;

			updateMusicButtonAppearence();

			PlayerPrefs.SetInt("music", musicEnabled ? 1 : 0);

			updateMusicState();

			return;
		}

		if (buttonKey == ButtonKey.sound) {
			soundEnabled = !soundEnabled;
			
			updateSoundButtonAppearence();

			PlayerPrefs.SetInt("sound", soundEnabled ? 1 : 0);

			updateSoundState();
			
			return;
		}

		if (buttonKey == ButtonKey.exit) {
			Debug.Log("quit");
			Application.Quit();
			return;
		}

		if (buttonKey == ButtonKey.play) {
			Application.LoadLevel("Game");
			return;
		}

		if (buttonKey == ButtonKey.pause) {
			//already stopped
			if (Time.timeScale == 0) return;
			//stop time
			Time.timeScale = 0;

			Transform popUp = Instantiate(Resources.Load("Prefabs/PausePopUp")) as Transform;
			return;
		}

		if (buttonKey == ButtonKey.resume) {
			Transform popUp = transform.parent;
			if (popUp != null) {
				Destroy(popUp.gameObject);
			}

			Time.timeScale = 1;

			return;
		}

		if (buttonKey == ButtonKey.back) {
			//save score
			ScoreScript scoreScripts = FindObjectOfType(typeof(ScoreScript)) as ScoreScript;
			if (scoreScripts != null) scoreScripts.commitScore();

			//load menu scene
			Time.timeScale = 1;
			Application.LoadLevel("Menu");

			return;
		}

		if (buttonKey == ButtonKey.restart) {
			//save score
			ScoreScript scoreScripts = FindObjectOfType(typeof(ScoreScript)) as ScoreScript;
			if (scoreScripts != null) scoreScripts.commitScore();

			//restart game
			Time.timeScale = 1;
			Application.LoadLevel("Game");
			
			return;
		}
	}

	private void updateMusicState() {
		//play/pause music
		MusicScript musicScript = FindObjectOfType(typeof(MusicScript)) as MusicScript;
		if (musicScript != null) {
			if (musicEnabled) {
				musicScript.enable();	
			}else {
				musicScript.disable();
			}
		}
	}

	private void updateSoundState() {
		//play/pause music
		SoundEffectsHelper soundScript = FindObjectOfType(typeof(SoundEffectsHelper)) as SoundEffectsHelper;
		if (soundScript != null) {
			if (soundEnabled) {
				soundScript.soundEnabled = true;	
			}else {
				soundScript.soundEnabled = false;
			}
		}
	}

	private void updateMusicButtonAppearence() {
		if (musicEnabled) {
			transform.FindChild("musicon").gameObject.SetActive(false);
			transform.FindChild("musicoff").gameObject.SetActive(true);
		}else {
			transform.FindChild("musicon").gameObject.SetActive(true);
			transform.FindChild("musicoff").gameObject.SetActive(false);
		}
	}

	private void updateSoundButtonAppearence() {
		if (soundEnabled) {
			transform.FindChild("soundon").gameObject.SetActive(false);
			transform.FindChild("soundoff").gameObject.SetActive(true);
		}else {
			transform.FindChild("soundon").gameObject.SetActive(true);
			transform.FindChild("soundoff").gameObject.SetActive(false);
		}
	}


}
