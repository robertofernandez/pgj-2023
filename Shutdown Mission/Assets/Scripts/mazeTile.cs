using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mazeTile : MonoBehaviour
{
    public bool isEmpty = true;
    //public Transform trnsf;
    public bool isWall = false;
    private int posX, posY;
    private GameObject showedPrefab;
    public GameObject wallPrefab;
    public GameObject batteryPrefab;
    
    public void mazeTileStarter(bool isWall, int coorX, int coorZ){
        transform.position.Set(coorX,1,coorZ);
        this.isWall = isWall;
        this.showedPrefab = (isWall)?wallPrefab:batteryPrefab;
        this.isEmpty = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        //showedPrefab = wallPrefab;
    }
}
