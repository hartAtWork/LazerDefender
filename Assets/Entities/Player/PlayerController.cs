using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed = 10.0f;
	public float padding = 1f;
	public Sprite shipCenter, shipLeft, shipRight;
	
	public float health = 250;
	public GameObject projectile;
	public float projectileSpeed;
	public float firingRate = 0.2f;
	
	public AudioClip fireSound;
	
	float minX;
	float maxX;
	private SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () {
		//call screen width and set bounds
		float distanceZ = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distanceZ));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distanceZ));
		minX = leftmost.x + padding;
		maxX = rightmost.x - padding;
		//get access to sprite renderer attach to game object
		spriteRenderer = GetComponent<SpriteRenderer>();
		if(!spriteRenderer.sprite){
			spriteRenderer.sprite = shipCenter;
		}
	}
	void Fire(){
		Vector3 offset = new Vector3(0,1,0);
		GameObject beam = Instantiate(projectile, transform.position + offset, Quaternion.identity) as GameObject;
		beam.rigidbody2D.velocity = new Vector3(0, projectileSpeed,0);
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			InvokeRepeating ("Fire", 0.000001f, firingRate);
		}
		if(Input.GetKeyUp(KeyCode.Space)){
			CancelInvoke("Fire");
		}
		//move the ship left and right
		if(Input.GetKey(KeyCode.LeftArrow)){
			transform.position += Vector3.left * speed * Time.deltaTime;
			spriteRenderer.sprite = shipLeft;
		}else if(Input.GetKey(KeyCode.RightArrow)){
			transform.position += Vector3.right * speed * Time.deltaTime;
			spriteRenderer.sprite = shipRight;
		}else{
			spriteRenderer.sprite = shipCenter;
		}
		//restrict player to game place
		float newX = Mathf.Clamp(transform.position.x, minX ,maxX);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}
	void OnTriggerEnter2D(Collider2D collider){
		Projectile disruptor = collider.gameObject.GetComponent<Projectile>();
		if(disruptor){
			health -= disruptor.getDamage();
			disruptor.Hit();
			
			if(health <= 0){
				Die ();
			}
		}
	}
	void Die(){
		LevelManager man = GameObject.Find ("LevelManager").GetComponent<LevelManager>();
		man.LoadLevel("Win");
		Destroy(gameObject);
	}
}
