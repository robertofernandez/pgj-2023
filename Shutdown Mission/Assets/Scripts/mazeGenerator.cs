using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.sdmission.logic.model;

public class mazeGenerator : MonoBehaviour
{
    //public GameObject tilePrefab;
    private mazeTile[,] mazeTileI;
    //public mazeTile mazeTilePrefab;
    public GameObject wallPrefab;
    public GameObject batteryPrefab;
    
    public int seed;

    //private GameMapTile[,,] tileMatrix;
    private int[,] intTileMatrix;

    public int mazeIterations = 5;
    // Start is called before the first frame update
    void Start()
    {
        int mazeSize = (int)Mathf.Pow(2, mazeIterations+1) + 1;
        
        Random.InitState(seed);
        intTileMatrix = new int[mazeSize,mazeSize];
        Debug.Log("mazeSize: " + mazeSize);
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                intTileMatrix[j,i] = 1;
            }
        }
        intTileMatrix[1,1] = 0;

        for (int iter = 0; iter < mazeIterations; iter++)
        {
            int fronteraTemporal = (int)Mathf.Pow(2,iter+1);
            for (int i = 0; i < (Mathf.Pow(2,iter+1)+1); i++)
            {
                for (int j = 0; j < (Mathf.Pow(2,iter+1)+1); j++)
                {
                    
                    
                        intTileMatrix[fronteraTemporal + j, fronteraTemporal +i] = intTileMatrix[j, i];
                        intTileMatrix[fronteraTemporal + j,                   i] = intTileMatrix[j, i];
                        intTileMatrix[                   j, fronteraTemporal +i] = intTileMatrix[j, i];
                    
                }
            }
            if (iter == 0)
            {
                intTileMatrix[1, fronteraTemporal] = 0;
                intTileMatrix[3, fronteraTemporal] = 0;
                intTileMatrix[fronteraTemporal, 3] = 0;
            }else{//remove de paredes de forma pseudo-controlada
                intTileMatrix[(Random.Range(1,fronteraTemporal/2))*2+1 , fronteraTemporal] = 0;
                intTileMatrix[fronteraTemporal + (Random.Range(1,fronteraTemporal/2))*2+1 , fronteraTemporal] = 0;
                intTileMatrix[fronteraTemporal, fronteraTemporal + Random.Range(1 , 2*(fronteraTemporal/2)+1)] = 0;
                intTileMatrix[fronteraTemporal, Random.Range(1 , 2*(fronteraTemporal/2)+1)] = 0;

                intTileMatrix[(Random.Range(1,fronteraTemporal/2))*2+1 , (Random.Range(1,fronteraTemporal))*2+1] = 0;
                intTileMatrix[(Random.Range(1,fronteraTemporal/2))*2+1 , (Random.Range(1,fronteraTemporal))*2+1] = 0;
                intTileMatrix[fronteraTemporal + (Random.Range(1,fronteraTemporal/2))*2+1 , fronteraTemporal] = 0;
                intTileMatrix[fronteraTemporal, fronteraTemporal + Random.Range(1 , 2*(fronteraTemporal/2)+1)] = 0;
            }
            
            
        }
        //La variable intTileMatrix contiene 0 y 1 representando pared(1) o hueco=bateria(0) no se como escribirla a un archivo .txt si te hace falta leerla para otro analisis.
        for (int i = 0; i < mazeSize; i++)
        {
            for (int j = 0; j < mazeSize; j++)
            {
                GameObject prefabToSpawn;                         // (1)pared : (0)bateria
                prefabToSpawn = Instantiate((intTileMatrix[j,i]==1)?wallPrefab:batteryPrefab,new Vector3(j,1,i), Quaternion.identity);
                if (prefabToSpawn != null)
                {
                    prefabToSpawn.transform.SetParent(this.transform);
                }
            }
        }
    }
}
