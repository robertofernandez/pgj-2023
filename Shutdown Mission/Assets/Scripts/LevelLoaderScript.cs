using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderScript : MonoBehaviour
{
    
   public Animator transision;
   public float transisionTime = 1f;

    public void LoadNextScene()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    public void LoadFirstScene()
    {
        StartCoroutine(LoadLevel(0));
    }
    public void LoadIndexScene(int index)
    {
        StartCoroutine(LoadLevel(index));
    }

    void Update()
    {
        if(Input.GetButtonDown("Cancel")){
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                LoadFirstScene();
            }
        }
    }
    IEnumerator LoadLevel(int levelIndex)
    {
         transision.SetTrigger("Start");
         yield return new WaitForSeconds(transisionTime);
         SceneManager.LoadScene(levelIndex);
    }
}
