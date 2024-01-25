using UnityEngine;
using System.Collections;

public class ContinuosBackgroundRender : MonoBehaviour {
	public Transform camera;
	private Transform[] backgroundSprites = new Transform[2];
	public float fixedWidth = -1;
    public float backgroundWidth = 1;
    public float camera_x;
    public float layer_x;
    public int nextSlot;
    public int currentSeenSlot;
    public float initialX0;
    public float finalX0;
    public float initialX1;
    public float finalX1;

	void Start () {
		backgroundSprites [0] = transform.GetChild (0);
		backgroundSprites [1] = transform.GetChild (1);
		if (fixedWidth > -1) {
			backgroundWidth = fixedWidth;
		} else {
			backgroundWidth = backgroundSprites [0].GetComponent<SpriteRenderer> ().bounds.size.x;
		}
		//Debug.Log ("Size is: " + backgroundWidth);
	}

	void Update () {
        camera_x = camera.position.x;
        layer_x = transform.position.x;
        currentSeenSlot = 0;
        initialX0 = initialX(backgroundSprites[0], backgroundWidth);
        finalX0 = finalX(backgroundSprites[0], backgroundWidth);
        initialX1 = initialX(backgroundSprites[1], backgroundWidth);
        finalX1 = finalX(backgroundSprites[1], backgroundWidth);
        if (initialX0 < camera_x && finalX0 > camera_x) {
            currentSeenSlot = 0;
        } else if (initialX1 < camera_x && finalX1 > camera_x) {
            currentSeenSlot = 1;
		} else {
			//TODO adjust!!
			//backgroundSprites [1].position = new Vector3 (slotX - backgroundWidth, backgroundSprites [0].position.y, backgroundSprites [0].position.z);
		}
        nextSlot = (currentSeenSlot + 1) % 2;

        if (camera_x > backgroundSprites[currentSeenSlot].position.x) {
            backgroundSprites [nextSlot].position = new Vector3 (backgroundSprites[currentSeenSlot].position.x + backgroundWidth, backgroundSprites [0].position.y, backgroundSprites [0].position.z);
        } else {
            backgroundSprites [nextSlot].position = new Vector3 (backgroundSprites[currentSeenSlot].position.x - backgroundWidth, backgroundSprites [0].position.y, backgroundSprites [0].position.z);
        }
    }

    private float initialX(Transform input, float width) {
        return input.position.x - width / 2;
    }

    private float finalX(Transform input, float width) {
        return input.position.x + width / 2;

	}
}
