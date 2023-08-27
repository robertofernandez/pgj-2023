using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.sdmission.logic.model;

public class mazeGenerator : MonoBehaviour
{
    //public GameObject tilePrefab;
    private mazeTile[,] mazeTileI;
    public mazeTile mazeTilePrefab;
    public int seed;

    private GameMapTile[,,] tileMatrix;

    private int mazeIterations = 5;
    // Start is called before the first frame update
    void Start()
    {
        int mazeSize = 2^mazeIterations + 1;
        //generacion del laberinto
        tileMatrix = new GameMapTile[mazeSize,1,mazeSize];
        mazeTileI = new mazeTile[mazeSize,mazeSize];

        
        for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    tileMatrix[j,0,i] = new GameMapTile("pared", "noPasable");
                    // mazeTileI[j,i] = Instantiate(mazeTilePrefab);
                    // mazeTileI[j,i].mazeTileStarter(true,j,i);
                    // Debug.Log(mazeTilePrefab.name);

                }
            }
            tileMatrix[1,0,1] = new GameMapTile("piso", "Pasable");
            //mazeTileI[1,1].mazeTileStarter(false,1,1);
    }
}
