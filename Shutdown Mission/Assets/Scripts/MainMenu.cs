using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour

{
    public LevelLoaderScript levelLoader;
    public void PlayGame(){
        levelLoader.LoadNextScene();
        //SceneManager.LoadScene()
    }

    public void QuitGame(){
        Application.Quit();
    }
}
