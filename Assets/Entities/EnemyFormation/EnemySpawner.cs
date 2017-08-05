using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemyPrefab;
	public GameObject enemyPrefab2;
	public float width = 10f;
	public float height = 5f;
	public float speed = 5.0f;
	public float padding = 0.4f;
	public float spawnDelay = 0.5f;

	private float minX;
	private float maxX;
	private bool movingRight = true;
	// Use this for initialization
	void Start () {
		SpawnUntilFull();
		//call screen width and set bounds
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distanceToCamera));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distanceToCamera));
		minX = leftmost.x;
		maxX = rightmost.x;
	}

	// Update is called once per frame
	void Update () {
		//move back and forwards
		if(movingRight){
			transform.position += Vector3.right * speed * Time.deltaTime;
		}else{
			transform.position += Vector3.left * speed * Time.deltaTime;
		} 
		//restrict to game place
		float rightEdgeOfFormation = transform.position.x + (padding*width);
		float leftEdgeofFormation = transform.position.x - (padding* width);
		if(leftEdgeofFormation < minX){
			movingRight = true;
		}else if(rightEdgeOfFormation > maxX){
			movingRight = false;
		}
		
		if(AllMembersDead()){
			Debug.Log ("Their dead Jim");
			SpawnUntilFull();
		}
	}
	Transform NextFreePosition(){
		foreach(Transform childPositionGameObject in transform){
			if(childPositionGameObject.childCount == 0){
				return childPositionGameObject;
			}
		
		}
		return null;
	}
	bool AllMembersDead(){
		foreach(Transform childPositionGameObject in transform){
			if(childPositionGameObject.childCount > 0 ){
				return false;
			}
		}
		return true;
	}
	void spawnEnemies(GameObject ship){
		foreach(Transform child in transform){
			GameObject enemy = Instantiate(ship, child.transform.position, Quaternion.identity) as GameObject;
			//add to enemy formation hierarchy
			enemy.transform.parent = child;
		}
	} 
	void SpawnUntilFull(){
		Transform freePosition = NextFreePosition();
		if(freePosition){
			GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		if(NextFreePosition()){
			Invoke ("SpawnUntilFull", spawnDelay);
		}
	}
	//show formation size
	public void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
	}
	

}
