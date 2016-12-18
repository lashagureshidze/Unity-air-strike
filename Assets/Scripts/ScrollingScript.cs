using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class ScrollingScript : MonoBehaviour {

	public Vector2 speed = new Vector2(2,2);

	public Vector2 direction = new Vector2(0, -1);

	public bool isLinkedToCamera = false;

	public bool isLooping = false;

	private List<Transform> backgroundPart;

	// Use this for initialization
	void Start () {
		if (isLooping) {
			backgroundPart = new List<Transform>();

			for (int i = 0; i < transform.childCount; i++) {
				Transform child = transform.GetChild(i);

				//add only the visible children
				if (child.GetComponent<Renderer>() != null) {
					backgroundPart.Add(child);
				}
			}

			// Sort by position.
			// Note: Get the children from left to right.
			// We would need to add a few conditions to handle
			// all the possible scrolling directions.
			backgroundPart = backgroundPart.OrderBy(
				t => t.position.y).ToList();
		}
	}
	
	// Update is called once per frame
	void Update () {
		//movement
		Vector3 movement = new Vector3 (
			speed.x * direction.x,
			speed.y * direction.y,
			0);

		movement *= Time.deltaTime;
		transform.Translate (movement);

		//move camera
		if (isLinkedToCamera) {
			Camera.main.transform.Translate(movement);
		}

		//loop
		if (isLooping) {
			looping();
		}

	}

	void looping ()
	{
		// Get the first object.
		// The list is ordered from bottom (y position) to top.
		Transform firstChild = backgroundPart.FirstOrDefault();
		
		if (firstChild != null)	{
			// Check if the child is already (partly) before the camera.
			// We test the position first because the IsVisibleFrom
			// method is a bit heavier to execute.
			if (firstChild.position.y < Camera.main.transform.position.y)
			{
				// If the child is already on the left of the camera,
				// we test if it's completely outside and needs to be
				// recycled.
				if (firstChild.GetComponent<Renderer>().IsVisibleFrom(Camera.main) == false)
				{
					// Get the last child position.
					Transform lastChild = backgroundPart.LastOrDefault();
					Vector3 lastPosition = lastChild.transform.position;
					Vector3 lastSize = (lastChild.GetComponent<Renderer>().bounds.max - lastChild.GetComponent<Renderer>().bounds.min);
					
					// Set the position of the recyled one to be AFTER
					// the last child.
					// Note: Only work for horizontal scrolling currently.
					firstChild.position = new Vector3(firstChild.position.x, lastPosition.y + lastSize.y, firstChild.position.z);
					
					// Set the recycled child to the last position
					// of the backgroundPart list.
					backgroundPart.Remove(firstChild);
					backgroundPart.Add(firstChild);
				}
			}
		}
	}
}
