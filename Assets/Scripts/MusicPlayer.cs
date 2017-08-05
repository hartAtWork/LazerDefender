using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;
	public AudioClip startClip;
	public AudioClip gameClip;
	public AudioClip endClip;
	
	private AudioSource music;
	// Use this for initialization
	void Start (){
		if(instance != null && instance != this){
			Destroy(gameObject); 
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject); //keeps playing after lvl change
			music = GetComponent<AudioSource>();
			music.clip = startClip;
			music.loop = true;
			music.Play ();
		}
	}
	void OnLevelWasLoaded(int level){
		if (!music) return; //check for music first
		Debug.Log("MusicPlayer Loaded" +level);
		music.Stop ();
		if(level == 0){
			music.clip = startClip;
		}
		if(level == 1){
			music.clip = gameClip;	
		}
		if(level == 2){
			music.clip = endClip;
		}
		music.loop = true;
		music.Play ();
	}
}
