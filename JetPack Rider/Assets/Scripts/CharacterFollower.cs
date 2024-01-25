using UnityEngine;
using System.Collections;

public class CharacterFollower : MonoBehaviour {
	public Transform characterTransform;
	public float minYinGroundZone;

	void Start () {
	
	}

	void Update () {
		if (inGroundZone (characterTransform.position.x) && characterTransform.position.y < minYinGroundZone) {
			transform.position = new Vector3(characterTransform.position.x, minYinGroundZone, transform.position.z);
		} else {
			transform.position = new Vector3(characterTransform.position.x, characterTransform.position.y, transform.position.z);
		}
	}

	/**
	 * @return if the character is in ground zone, which means camera should stop to follow in y if near ground.
	 */
	private bool inGroundZone(float x) {
		return true;
	}
}
