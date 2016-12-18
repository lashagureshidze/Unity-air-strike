using UnityEngine;

public class MoveScript : MonoBehaviour {
	public Vector2 speed = new Vector2(10, 10);
	
	public Vector2 direction = new Vector2(0, -1);
	
	private Vector2 movement;

	private Rigidbody2D rigidBody;

	void Start() {
		rigidBody = GetComponent<Rigidbody2D> ();
	}

	void Update()
	{
		movement = new Vector2(
			speed.x * direction.x,
			speed.y * direction.y);
	}
	
	void FixedUpdate()
	{
		// Apply movement to the rigidbody
		rigidBody.velocity = movement;
	}
}