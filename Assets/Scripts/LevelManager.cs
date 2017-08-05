using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public void LoadLevel(string name){
		//Brick.breakableCount = 0;reset bricks when starting new lrvrl
		Application.LoadLevel(name);
	}
	public void Quit(){
		Application.Quit();
	}
	public void LoadNextLevel(){
		//Brick.breakableCount = 0;
		Application.LoadLevel(Application.loadedLevel +1);
	}
	/*
	public void BrickDestroyed(){
		if(Brick.breakableCount <=0){
			LoadNextLevel();
		}
	}
	*/
}
