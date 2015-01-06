using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	private int lootCount;
	//private double scoreCount;
	Text text;

	void Awake() {
		text = GetComponent<Text>();
		//scoreCount = PlayerControl.score;
	}

	void Update () {
		lootCount = PlayerControl.currLoot;
		text.text = "Meat: "+lootCount+"/"+PlayerControl.levelLoot;
		//text.text = "Score: " + scoreCount;
	}
}
