using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FlowManager : MonoBehaviour {

	public void GotoScene(string name) {
		SceneManager.LoadScene(name);
	}
}
