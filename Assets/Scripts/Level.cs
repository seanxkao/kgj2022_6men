using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Level
{
    public GridData[,,] gridDatas; // GridData[x,y,z]
    public int xLength { get { return gridDatas.GetLength(0); } }
    public int yLength { get { return gridDatas.GetLength(1); } }
    public int zLength { get { return gridDatas.GetLength(2); } }

    public Level(int x, int y, int z, GridData defaultGrid = null) 
    {
        gridDatas = new GridData[x,y,z];
        if(defaultGrid != null)
        {
            for(int i = 0; i < x; i++)
                for(int j = 0; j < y; j++)
                    for(int k = 0; k < z; k++)
                        gridDatas[i,j,k] = defaultGrid;
        }
    }

    public bool InvalidPosition(Vector3Int position)
    {
        return !((position.x > 0 && position.x < xLength) && 
                 (position.y > 0 && position.y < yLength) && 
                 (position.z > 0 && position.z < zLength));
    }
}

public class RuntimeLevel
{
    public GridBehaviour[,,] gridBehaviours; // GridData[x,y,z]
    public int xLength { get { return gridBehaviours.GetLength(0); } }
    public int yLength { get { return gridBehaviours.GetLength(1); } }
    public int zLength { get { return gridBehaviours.GetLength(2); } }

    public RuntimeLevel(int x, int y, int z) 
    {
        gridBehaviours = new GridBehaviour[x,y,z];
    }

    public void HideAll()
    {
        for(int i = 0; i < xLength; i++)
        {
            for(int j = 0; j < yLength; j++)
            {
                for(int k = 0; k < zLength; k++)
                {
                    if(gridBehaviours[i,j,k] != null)
                        gridBehaviours[i,j,k].gameObject.SetActive(false);
                }
            }
        }
    }

    public bool InvalidPosition(Vector3Int position)
    {
        return !((position.x > 0 && position.x < xLength) && 
                 (position.y > 0 && position.y < yLength) && 
                 (position.z > 0 && position.z < zLength));
    }
}