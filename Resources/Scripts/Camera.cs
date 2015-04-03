using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

	// the player's transform
	private Transform playerTransform;
	private const float y = 98.06f;

	// Use this for initialization
	void Start () 
	{
		// the player's gameobject must be named Bear
		playerTransform = GameObject.Find ("Bear").transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		float x = playerTransform.position.x - 5f;
		float z = playerTransform.position.z - 3f;
		transform.position = new Vector3(x,y,z);
	}
}
