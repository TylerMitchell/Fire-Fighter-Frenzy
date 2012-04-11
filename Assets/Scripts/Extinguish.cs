using UnityEngine;
using System.Collections;

public class Extinguish : MonoBehaviour {
	
	public float Damage = 1.0f;
	
	public GameObject Water;
	
	public AudioClip WaterSound;
	
	// Use this for initialization
	void Start () {

	}
	/*
	void OnParticleCollision(GameObject other){
		Debug.Log("Hit Ex");
		
		Rigidbody body = other.rigidbody;
		if(body){
			Fire_Properties fire = body.GetComponent<Fire_Properties>();
			fire.Health = fire.Health - Damage;
			Debug.Log("Health " + fire.Health);
		}
	}*/
	
	// Update is called once per frame
	void Update () {
		audio.clip = WaterSound;
		
		if(Input.GetButton("Fire1")){
			Water.particleEmitter.emit = true;
			if(!audio.isPlaying){				
				audio.Play();
			}
		}		
		else{
			Water.particleEmitter.emit = false;
			if(audio.isPlaying){
				audio.Stop();
			}
		}
	}
}
