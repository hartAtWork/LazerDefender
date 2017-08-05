using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	public GameObject projectile;
	public float projectileSpeed = 10;
	public float health = 150;
	public float shotsPerSeconds = 0.5f;
	public int scoreValue = 150;
	
	public AudioClip fireSound;
	public AudioClip deathSound;
	
	private ScoreKeeper scoreKeeper;
	
	void Start(){
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
	}
	
	void Update(){
		float probability = Time.deltaTime * shotsPerSeconds;
		if(Random.value < probability){
			Fire ();
		}
	}
	void Fire(){
		GameObject disruptor = Instantiate (projectile, transform.position, Quaternion.identity) as GameObject;
		disruptor.rigidbody2D.velocity = new Vector2(0, -projectileSpeed);
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
		//audio.Play();
	}
	void OnTriggerEnter2D(Collider2D collider){
		
		Projectile phazer = collider.gameObject.GetComponent<Projectile>();
		if(phazer){
			health -= phazer.getDamage();
			phazer.Hit();
		
			if(health <= 0){
				Die();
			}
		}
	}
	void Die(){
		AudioSource.PlayClipAtPoint(deathSound, transform.position);
		scoreKeeper.Score(scoreValue);
		Destroy(gameObject);
	}

}
