using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FBController : MonoBehaviour {

	public static GameObject fbButton;
	public GameObject fbAvatar;

	void Awake() {
		fbButton = GameObject.Find("FBButton");
	}

	public void InitFB() {

		FB.Init (SetInit, OnHideUnity);
	}

	private void SetInit() {

		Debug.Log ("FB init done");

		if (FB.IsLoggedIn) {
			Debug.Log("FB logged in");
			FB.API(Util.GetPictureURL("me", 128, 128), Facebook.HttpMethod.GET, DealWithProfilePicture);
		}else {
			FB.Login("publish_actions", AuthCallback); 
			fbButton.SetActive(false);
			FB.API(Util.GetPictureURL("me", 128, 128), Facebook.HttpMethod.GET, DealWithProfilePicture);

		} 

	}

	private void OnHideUnity(bool isGameShown) {

		if (!isGameShown) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}

	void AuthCallback(FBResult result) {

		if (FB.IsLoggedIn) {
			Debug.Log ("FB login worked!");
		} else {
			Debug.Log ("FB login fail :(");
		}
	}

	void DealWithProfilePicture(FBResult result){

		if (result.Error != null) {
			Debug.Log ("Problem with getting profile picture!");
			FB.API (Util.GetPictureURL ("me", 128, 128), Facebook.HttpMethod.GET, DealWithProfilePicture);
			return;
		} else {
			Debug.Log ("Picture has been downloaded!");
		}
		
		fbAvatar.SetActive(true);
		Image userAvatar = fbAvatar.GetComponent<Image> (); 
		userAvatar.sprite = Sprite.Create (result.Texture, new Rect(0,0,128,128), new Vector2(0,0)); 
	}
}
