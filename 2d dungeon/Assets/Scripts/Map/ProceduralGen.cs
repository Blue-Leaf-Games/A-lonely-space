using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGen : MonoBehaviour
{
    public int[,] DevMap = new int[1024, 1024];
    public int SizeX = 20;
    public int SizeY = 20;
    public int SpawnX = -1;
    public int SpawnY = -1;
    // Start is called before the first frame update
    void Start()
    {

        for (int x = 0; x < SizeX; x++)
        {
            for (int y = 0; y < SizeY; y++)
            {
                if(x == 0 || x == SizeX || y == 0 || y == SizeY)
                {
                    DevMap[x, y] = 2;
                }
                else
                {
                    DevMap[x, y] = 1;
                }
                
            }
        }
        if(SpawnX == -1)
        {
            SpawnX =UnityEngine.Random.Range(1,SizeX-1);
        }
        if (SpawnY == -1)
        {
            SpawnY = UnityEngine.Random.Range(1, SizeY - 1);
        }
        DevMap[SpawnX,SpawnY] = 3;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
