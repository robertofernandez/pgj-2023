using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.sdmission.logic.model;
using System.IO;


public class mazeGenerator : MonoBehaviour
{
    public int droneCount = 7;
    public int seed;
    private Queue<char> propsToSpawn;

    //private GameMapTile[,,] tileMatrix;
    private char[,] intTileMatrix;
    [Range(1, 6)]
    public int mazeIterations = 5;
    // Start is called before the first frame update
    void Awake()
    {
        int mazeSize = (int)Mathf.Pow(2, mazeIterations+1) + 1;
        int auxRand = (int)(Random.value * 100);
        Random.InitState((seed>0)?seed:auxRand);
        Debug.Log(auxRand);

        intTileMatrix = new char[mazeSize,mazeSize];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                intTileMatrix[j,i] = '1';
            }
        }
        intTileMatrix[1,1] = '2';

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
            if (iter == 0)//Eliminacion manual de paredes en primer iteracion
            {
                intTileMatrix[1, fronteraTemporal] = '2';
                intTileMatrix[3, fronteraTemporal] = '2';
                intTileMatrix[fronteraTemporal, 3] = '2';
            }else{//remove de paredes de forma pseudo-controlada
                intTileMatrix[(Random.Range(1,fronteraTemporal/2))*2+1 , fronteraTemporal] = '2';
                intTileMatrix[fronteraTemporal + (Random.Range(1,fronteraTemporal/2))*2+1 , fronteraTemporal] = '2';
                intTileMatrix[fronteraTemporal, fronteraTemporal + Random.Range(1 , 2*(fronteraTemporal/2)+1)] = '2';
                intTileMatrix[fronteraTemporal, Random.Range(1 , 2*(fronteraTemporal/2)+1)] = '2';

                intTileMatrix[(Random.Range(1,fronteraTemporal/2))*2+1 , (Random.Range(1,fronteraTemporal))*2+1] = '2';
                intTileMatrix[(Random.Range(1,fronteraTemporal/2))*2+1 , (Random.Range(1,fronteraTemporal))*2+1] = '2';
                intTileMatrix[fronteraTemporal + (Random.Range(1,fronteraTemporal/2))*2+1 , fronteraTemporal] = '2';
                intTileMatrix[fronteraTemporal, fronteraTemporal + Random.Range(1 , 2*(fronteraTemporal/2)+1)] = '2';
            }
            
            
        }
        propsToSpawn = new Queue<char>();
        propsToSpawn.Enqueue('3');//Preparar una sola EVE
        for (int i = 0; i < droneCount; i++)//Preparar X drones, ver randoms de tipos de drones
        {
            propsToSpawn.Enqueue('4');
        }
        propsToSpawn.Enqueue('5');
        propsToSpawn.Enqueue('6');
        while (propsToSpawn.Count > 0)
        {   
            int coorX = Random.Range(0,mazeSize);
            int coorY = Random.Range(0,mazeSize);
            if(intTileMatrix[coorX,coorY] == '2')
            {
                intTileMatrix[coorX,coorY] = propsToSpawn.Dequeue();
            }
        }
        //Crear archivo txt con el laberinto creado y spawnpoints de drones y eve
        string filePath = Application.dataPath + "/Levels/secret_mission.txt";
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        StreamWriter writer = new StreamWriter(filePath, true);
        for (int i = 0; i < mazeSize; i++)
        {
            for (int j = 0; j < mazeSize; j++)
            {
                writer.Write(intTileMatrix[j,i].ToString());
            }
            writer.Write('\n');
        }
        writer.Close();

        //La variable intTileMatrix contiene 0 y 1 representando pared(1) o hueco=bateria(0) no se como escribirla a un archivo .txt si te hace falta leerla para otro analisis.
        // for (int i = 0; i < mazeSize; i++)
        // {
        //     for (int j = 0; j < mazeSize; j++)
        //     {
        //         GameObject prefabToSpawn;                         // (1)pared : (0)bateria
        //         prefabToSpawn = Instantiate((intTileMatrix[j,i]=='1')?wallPrefab:batteryPrefab,new Vector3(j,1,i), Quaternion.identity);
        //         if (prefabToSpawn != null)
        //         {
        //             prefabToSpawn.transform.SetParent(this.transform);
        //         }
        //     }
        // }
    }
}
