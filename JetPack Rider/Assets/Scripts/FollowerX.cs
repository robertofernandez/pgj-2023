using UnityEngine;
using System.Collections;

public class FollowerX : MonoBehaviour {
	public Transform characterTransform;

	void Start () {
	
	}
	
	void Update () {
		transform.position = new Vector3(characterTransform.position.x, transform.position.y, transform.position.z);
	}
}
