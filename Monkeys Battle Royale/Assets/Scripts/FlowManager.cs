using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UIElements;

public class FlowManager : MonoBehaviour {

    public string level1;
	public void GotoScene(string name) {
		SceneManager.LoadScene(name);
	}

    private void Start()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            GotoScene(level1);
        }
    }
}
