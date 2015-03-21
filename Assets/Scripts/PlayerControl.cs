using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	// For determining which way the player is currently facing.
	//[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.
	public static bool playerIsRunning = false; 	// State if Dino is alive
	public static bool playerIsDead = false; //Checking if the player is alive
	public static int tilesPassed = 1;		//Number of passed tiles
	public static float maxSpeed = 4f;				// The fastest the player can travel in the x axis.

	//public bool tutorialDone = false;		// Check if the user is not afk and ready to start the level
	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float jumpForce = 1000f;			// Amount of force added when the player jumps.

	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	public bool isGrounded = false;			// Whether or not the player is grounded.
	private Animator anim;					// Reference to the player's animator component.

	//private GameObject[] backgroundLayers;	//Variables for activating the layout scripts

	//Static variables are "connected" to the class (Player control) and not to an object

	//public static int currLoot = 0; 		//Storing the collected loot
	//public static int levelLoot = 5;		//Number of loot required for a specific level

	public static double score = 0; 		//Storing players score

	public static float restartDelay = 0.5f; //Time to wait after restarting the level



	private int myLayerMask = 1;

	void Awake(){
		playerIsRunning = false;
		playerIsDead = false;
		GameManager.tutorialDone = false;

		groundCheck = transform.Find("groundCheck");
		anim = GetComponent<Animator>();

		// Setting Sound mute
		audio.volume = PlayerPrefs.GetFloat ("SoundVolume")*0.7f;
		//backgroundLayers = GameObject.FindGameObjectsWithTag("Background");
	}

	void Start(){
		myLayerMask = 1 << LayerMask.NameToLayer("Ground");
	}

	void Update(){
		isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, myLayerMask);

		if(Input.GetButtonDown("Jump") && isGrounded) jump = true;
	}


	void FixedUpdate (){
		if(GameManager.tutorialDone){
			rigidbody2D.AddForce(Vector2.right * moveForce);

			float h = Input.GetAxis("Horizontal");
			//Touch myJump = Input.GetTouch(0);

			anim.SetFloat("hSpeed", Mathf.Abs(h));

			// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
			if(h * rigidbody2D.velocity.x < maxSpeed)
				// ... add a force to the player.
				rigidbody2D.AddForce(Vector2.right * h * moveForce);

			// If the player's horizontal velocity is greater than the maxSpeed...
			if(Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
				// ... set the player's velocity to the maxSpeed in the x axis.
				rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
			
			if(jump){
				anim.SetTrigger("Jump");
					if(!audio.isPlaying) audio.Play();
				rigidbody2D.AddForce(new Vector2(0, jumpForce));
				// Make sure the player can't jump again until the jump conditions from Update are satisfied.
				jump = false;
			}
		}
	}
}
