using UnityEngine;
using System.Collections;

public class CharacterFollower : MonoBehaviour {
	//public Transform characterTransform;
	public GameObject charactersManagerObject;
	public float minYinGroundZone;

	private CharactersManager charactersManager;

	void Start () {
		charactersManager = charactersManagerObject.GetComponent<CharactersManager>();
		if(charactersManager == null) {
			Debug.Log("charactersManager null");
		}
	}

	void Update () {
		Transform characterTransform = charactersManager.getCurrentCharacterTransform();
		if(characterTransform == null) {
			Debug.Log("characterTransform null");
		}
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
