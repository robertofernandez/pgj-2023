using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {
	public Transform camera;
	public float[] layersDistance;
	private Transform[] layers;
	private int layersAmount;
	private float impact = 0.1f;

	void Start () {
		layersAmount = transform.childCount;
		layers = new Transform[layersAmount];
		for (int i = 0; i < layersAmount; i++) {
			layers [i] = transform.GetChild (i);
			//Debug.Log ("creating layer " + i + " with z = " + layers [i].transform.position.z);
		}

	}

	void Update () {
		//offset = offset + 0.01f;
		for (int i = 0; i < layersAmount; i++) {
			float x = (camera.position.x - camera.position.x * (layersDistance[i]/layersDistance[0])) * impact;
			layers [i].transform.position = new Vector3 (x, layers [i].transform.position.y, layers [i].transform.position.z);
			//Debug.Log ("layer " + i + " with z = " + layers [i].transform.position.z + "; x = " + layers [i].transform.position.x);
			//Debug.Log ("position " + camera.position.x + ": layer " + i + " is at " + x);
		}
	}
}
