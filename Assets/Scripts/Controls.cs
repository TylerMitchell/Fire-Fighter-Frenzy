using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {
	
	public float CharacterSpeed = 2.5f;
	
	public float RotationSpeed = 2.5f;
	
	public bool wait = false;

	
	//private Vector3 moveDirection = Vector3.zero;
	
	private bool Firing = false;
	
	//private bool Keyboard = true;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		/*
		Debug.Log("A " + Input.GetAxis("A"));
		Debug.Log("B " + Input.GetAxis("B"));
		Debug.Log("X " + Input.GetAxis("X"));
		Debug.Log("Y " + Input.GetAxis("Y"));
		Debug.Log("R Bumper " + Input.GetAxis("right bumper"));
		Debug.Log("L Bumper " + Input.GetAxis("left bumper"));
		Debug.Log("start " + Input.GetAxis("start"));
		Debug.Log("back " + Input.GetAxis("back"));
		Debug.Log("Left click " + Input.GetAxis("left stick(click)"));
		Debug.Log("Right click " + Input.GetAxis("right stick(click)"));
		
		Debug.Log("Left Analog X" + Input.GetAxisRaw("Left Analog X"));
		Debug.Log("Left Analog Y" + Input.GetAxisRaw("Left Analog Y"));
		Debug.Log("Right Analog X" + Input.GetAxisRaw("Right Analog X"));
		Debug.Log("Right Analog Y" + Input.GetAxisRaw("Right Analog Y"));
		*/
		//Debug.Log("D-Pad L" + Input.GetAxis("D-pad left"));
		//Debug.Log("D-Pad R" + Input.GetAxis("D-pad right"));
		//Debug.Log("D-Pad U" + Input.GetAxis("D-pad up"));
		//Debug.Log("D-Pad D" + Input.GetAxis("D-pad down"));
		
		//if(Keyboard){
		
		GameObject[] GUIRef = GameObject.FindGameObjectsWithTag("MainCamera");
			
		Kill_Screen GameOverRef = GUIRef[0].gameObject.GetComponent<Kill_Screen>();
		
		if(!GameOverRef.GameOver){
			Keyboard_Control();
		}
		/* }
		else{
			Xbox360_Control();
		} */
	}
	
	void Keyboard_Control(){
		CharacterController controller = GetComponent<CharacterController>();
		
		if(Input.GetButton("Fire1"))
		{
			Firing = true;
			//moveDirection = Vector3.zero;
			controller.SimpleMove(Vector3.zero);
		}
		else
		{
			Firing = false;
		}
		
		GameObject[] GUIRef = GameObject.FindGameObjectsWithTag("MainCamera");
			
		Kill_Screen GameOverRef = GUIRef[0].gameObject.GetComponent<Kill_Screen>();
			
		if(GameOverRef.GameOver == false){
			wait = false;
		}
		
		if(GameOverRef.GameOver & !wait){
			this.animation.Stop("idle");
			//this.animation.Play("death");
			wait = true;
			StartCoroutine("PlayDeath");
			
		}	
		else if(!GameOverRef.GameOver){
			
			if(!Firing){
				/*
				moveDirection = new Vector3(Input.GetAxis("Vertical"), 0, -1.0f * Input.GetAxis("Horizontal"));
				moveDirection = transform.TransformDirection(moveDirection);
				moveDirection *= CharacterSpeed;
				*/
				transform.Rotate(0,Input.GetAxis("Horizontal") * RotationSpeed, 0);
				Vector3 forward = transform.TransformDirection(Vector3.forward);
				float curSpeed = CharacterSpeed * Input.GetAxis("Vertical");
				if(Mathf.Abs(Input.GetAxis("Vertical")) > 0.1)
				{
					this.animation.Play("run");
				}
				else{
					this.animation.Play("idle");
				}
				
				controller.SimpleMove(forward * curSpeed);
			}
			else
			{
				transform.Rotate(0, Input.GetAxis("Horizontal") * RotationSpeed, 0);
				this.animation.Play("idle");
			}
		}
		//controller.Move(moveDirection * Time.deltaTime);
	}
	
	IEnumerator PlayDeath(){
		this.animation.Play("death");
		yield return new WaitForSeconds(2.0f);
	}
	/*
	void Xbox360_Control(){
		CharacterController controller = GetComponent<CharacterController>();
		Debug.Log("A " + Input.GetAxis("A"));
		Debug.Log("B " + Input.GetAxis("B"));
		Debug.Log("X " + Input.GetAxis("X"));
		Debug.Log("Y " + Input.GetAxis("Y"));
		Debug.Log("R Bumper " + Input.GetAxis("right bumper"));
		Debug.Log("L Bumper " + Input.GetAxis("left bumper"));
		Debug.Log("start " + Input.GetAxis("start"));
		Debug.Log("back " + Input.GetAxis("back"));
		Debug.Log("Left click " + Input.GetAxis("left stick(click)"));
		Debug.Log("Right click " + Input.GetAxis("right stick(click)"));
		Debug.Log("Left Analog X" + Input.GetAxis("Left Analog X"));
		//Debug.Log("Left Analog Y" + Input.GetAxisRaw("Left Analog Y"));
		//Debug.Log("Right Analog X" + Input.GetAxisRaw("Right Analog X"));
		//Debug.Log("Right Analog Y" + Input.GetAxisRaw("Right Analog Y"));
		
	} */
}
