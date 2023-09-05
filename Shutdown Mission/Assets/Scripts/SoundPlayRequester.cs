using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayRequester : MonoBehaviour
{
    public GameObject soundManager = null;
    // Start is called before the first frame update
    void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("Sounder");
    }

    void OnDestroy(){
        soundManager?.GetComponent<SoundManager>().PlaySound();
    }
}
