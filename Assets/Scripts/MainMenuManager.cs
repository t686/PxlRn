using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour {

	public GameObject cutScene;

	public void StartButton(){
		Color cutSceneOpacity = cutScene.GetComponent<SpriteRenderer>().color;
		cutSceneOpacity.a = 1;
		cutScene.GetComponent<SpriteRenderer>().color = cutSceneOpacity;
		GameObject.FindGameObjectWithTag("GUI").SetActive(false);
		Invoke("ChangeLevel", 3.0f);
	}

	void ChangeLevel(){
		Application.LoadLevel("GameLevel01");
	}

	public void OnAbout() {
		Application.LoadLevel ("About");
	}
}
