using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ScoreManager : MonoBehaviour {

	public float time = 0f;
	private int totalTime = 0;

	Text text;

	void Awake() {
		text = GetComponent<Text>();
	}

	void FixedUpdate () {
		if(PlayerControl.playerIsRunning){
			time += Time.fixedDeltaTime;
			//int t = (int)time*1000;
			float sec = Mathf.Round(time*100)/100;
			if(sec >= 60.0f){//
				totalTime = (int)sec/60;
				sec -= totalTime*60.0f;
				text.text = totalTime.ToString() + "." + Math.Round((double)sec, 2).ToString();
			}
			else {//
				//totalTime = sec;//
				text.text = Math.Round((double)sec, 2).ToString();
			}
		}
	}

	public float getTime(){
		return time;
	}
}
