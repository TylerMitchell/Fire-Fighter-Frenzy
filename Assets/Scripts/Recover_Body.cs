using UnityEngine;
using System.Collections;

public class Recover_Body : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnControllerColiderHit(ControllerColliderHit hit){
		
	Debug.Log("Hit something");
			
		GameObject[] GUIRef = GameObject.FindGameObjectsWithTag("MainCamera");
			
		Gameplay_GUI ScoreRef = GUIRef[0].gameObject.GetComponent<Gameplay_GUI>();
		
		if(hit.gameObject.tag == "Person"){		
			
			ScoreRef.g_score += 500;
		
			Destroy(hit.gameObject);
		}
		
		if(hit.gameObject.tag == "Dead"){
		
			if(ScoreRef.Oxygen + 0.1f > 1.0f){
				ScoreRef.Oxygen = 1.0f;
			}
			else{
				ScoreRef.Oxygen += 0.1f;
			}
			
			Destroy(hit.gameObject);
		}
	}
	
	void OnCollisionEnter(Collision thing){
	//Debug.Log("On Enter");
	}
	
	void OnTriggerEnter(Collider hit){
		//Debug.Log("Trig Enter");
		
		GameObject[] GUIRef = GameObject.FindGameObjectsWithTag("MainCamera");
			
		Gameplay_GUI ScoreRef = GUIRef[0].gameObject.GetComponent<Gameplay_GUI>();
		
		if(hit.gameObject.tag == "Person"){		
			
			ScoreRef.g_score += 1000;
		
			Destroy(hit.gameObject);
		}
		
		if(hit.gameObject.tag == "Dead"){
		
			if(ScoreRef.Oxygen + 0.1f > 1.0f){
				ScoreRef.Oxygen = 1.0f;
			}
			else{
				ScoreRef.Oxygen += 0.1f;
			}
			
			Destroy(hit.transform.parent.gameObject);
		}
	}
	
	/*
	void OnCollisionEnter(Collision thing){
		GameObject[] GUIRef = GameObject.FindGameObjectsWithTag("MainCamera");
			
		Gameplay_GUI ScoreRef = GUIRef[0].gameObject.GetComponent<Gameplay_GUI>();
		
		if(thing.gameObject.tag == "Person"){		
			
			ScoreRef.g_score += 500;
		
			Destroy(thing.gameObject);
		}
		
		if(thing.gameObject.tag == "Dead"){
		
			if(ScoreRef.Oxygen + 0.1f > 1.0f){
				ScoreRef.Oxygen = 1.0f;
			}
			else{
				ScoreRef.Oxygen += 0.1f;
			}
			
			Destroy(thing.gameObject);
		}
	}
	*/
	// Update is called once per frame
	void Update () {
		//Debug.Log("Recovery Script");
	}
}
