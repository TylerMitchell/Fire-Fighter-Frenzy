using UnityEngine;
using System.Collections;

public class Opening_Scene : MonoBehaviour {
	
	public Texture StartSelected;
	public Texture StartUnselected;
	public Texture ExitSelected;
	public Texture ExitUnselected;
	
	public bool Select = true;
	
	
	//public Vector2 ControlsText = new Vector2(Screen.width/4, Screen.height*(5/6));
	
	//public Vector2 ControlsTextSize = new Vector2(Screen.width/8, Screen.height/12);
	
	void OnGUI(){
		GUI.Label(new Rect(Screen.width*(29.0f/64.0f), Screen.height/4, Screen.width/2, Screen.height/4), "Fire Fighter Frenzy!");
		
		GUI.TextField(new Rect(Screen.width*(3.0f/8.0f), Screen.height*(5.0f/6.0f), Screen.width*(7.0f/16.0f), 25), "Use the arrow keys to move! Use the left shift key for action!");
	
		if(!Select){
			GUI.Label(new Rect(Screen.width*(4.0f/8.0f), Screen.height*(4.0f/8.0f), 50, 25), ExitSelected);
			GUI.Label(new Rect(Screen.width*(4.0f/8.0f), Screen.height*(3.0f/8.0f), 50, 25), StartUnselected);
		}
		else{
			GUI.Label(new Rect(Screen.width*(4.0f/8.0f), Screen.height*(4.0f/8.0f), 50, 25), ExitUnselected);
			GUI.Label(new Rect(Screen.width*(4.0f/8.0f), Screen.height*(3.0f/8.0f), 50, 25), StartSelected);
		}
		
		
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetAxis("Vertical") < 0)
			Select = false;
		else if(Input.GetAxis("Vertical") > 0)
			Select = true;
		
		if(Input.GetButton("Fire1")){
			if(Select){
				Application.LoadLevel(1);
			}
			else{
				Application.Quit();
			}
		}
		
	}
}
