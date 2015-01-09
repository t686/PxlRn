using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	private float time = 0f;

	Text text;

	void Awake() {
		text = GetComponent<Text>();
	}

	void FixedUpdate () {
		if(PlayerControl.isAlive){
			time += Time.fixedDeltaTime;
			float sec = Mathf.Round(time*100)/100;
			text.text = sec.ToString();
		}
	}
}
