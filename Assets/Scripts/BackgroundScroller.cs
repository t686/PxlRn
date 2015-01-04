using UnityEngine;
using System.Collections;

public class BackgroundScroller : MonoBehaviour {

	public float speed = 0;
	public static BackgroundScroller current;

	float pos = 0;

	void Start () {
		current = this;
	}
	

	void Update () {
		pos += speed;
		if(pos > 1.0f)
			pos -= 1.0f;

		renderer.material.mainTextureOffset = new Vector2(pos, 0);

	}
}
