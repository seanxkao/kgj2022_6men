using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private GridData mockup;
    [SerializeField]
    private Vector3Int playerCoordinate = Vector3Int.zero;
    [Space(20)]
    [SerializeField]
    private LevelData levelData;
    [SerializeField]
    private float gridSize = 1;
    [SerializeField]
    private GameObject player;

    private RuntimeLevel runtimeLevel = null;
    private SectionAxis viewAxis = SectionAxis.Z;

    private void Start() 
    {
        //ReadLevel();
        ReadMockUpLevel();
        ShowSection();    
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            viewAxis = SectionAxis.X;
            ShowSection();
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            viewAxis = SectionAxis.Y;
            ShowSection();
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            viewAxis = SectionAxis.Z;
            ShowSection();
        }
    }

    public void ReadLevel()
    {
        runtimeLevel = levelData.ConvertToRuntime(transform);
        playerCoordinate = levelData.startingPoint;
    }

    public void ReadMockUpLevel()
    {
        runtimeLevel = new RuntimeLevel(9,9,9);
        for(int i = 0; i < 9; i++)
        {
            for(int j = 0; j < 9; j++)
            {
                for(int k = 0; k < 9; k++)
                {
                    if(i == 0 || i == 8 || j == 0 || j == 8 || k == 0 || k == 8 || (i == 4 && j == 4 && k == 4))
                    {
                        runtimeLevel.gridBehaviours[i,j,k] = MonoBehaviour.Instantiate(mockup.gridBehaviour, transform);
                        runtimeLevel.gridBehaviours[i,j,k].Construct(mockup);
                        runtimeLevel.gridBehaviours[i,j,k].gameObject.SetActive(false);
                    }
                    else
                        runtimeLevel.gridBehaviours[i,j,k] = null;
                }
            }
        }
        playerCoordinate = new Vector3Int(1,1,1);
    }

    public void ShowSection()
    {
        if(runtimeLevel.InvalidPosition(playerCoordinate))
            return;

        runtimeLevel.HideAll();
        Debug.Log("fuck");
        
        float halfX;
        float halfY;
        if(viewAxis == SectionAxis.X)
        {
            halfX = (runtimeLevel.zLength - 1) * gridSize / 2f;
            halfY = (runtimeLevel.yLength - 1) * gridSize / 2f;
            for(int i = 0; i < runtimeLevel.zLength; i ++)
            {
                for(int j = 0; j < runtimeLevel.yLength; j ++)
                {
                    if(runtimeLevel.gridBehaviours[playerCoordinate.x,i,j] != null)
                    {
                        runtimeLevel.gridBehaviours[playerCoordinate.x,i,j].transform.localPosition = new Vector3(halfX - gridSize * i, -halfY + gridSize * j);
                        runtimeLevel.gridBehaviours[playerCoordinate.x,i,j].gameObject.SetActive(true);
                    }
                }
            }
            player.transform.position = new Vector3(halfX - playerCoordinate.z * gridSize, -halfY + playerCoordinate.y * gridSize);
        }
        if(viewAxis == SectionAxis.Z)
        {
            halfX = (runtimeLevel.xLength - 1) * gridSize / 2f;
            halfY = (runtimeLevel.yLength - 1) * gridSize / 2f;
            for(int i = 0; i < runtimeLevel.xLength; i ++)
            {
                for(int j = 0; j < runtimeLevel.yLength; j ++)
                {
                    if(runtimeLevel.gridBehaviours[i,j,playerCoordinate.z] != null)
                    {
                        runtimeLevel.gridBehaviours[i,j,playerCoordinate.z].transform.localPosition = new Vector3(-halfX + gridSize * i, -halfY + gridSize * j);
                        runtimeLevel.gridBehaviours[i,j,playerCoordinate.z].gameObject.SetActive(true);
                    }
                }
            }
            player.transform.position = new Vector3(-halfX + playerCoordinate.x * gridSize, -halfY + playerCoordinate.y * gridSize);
        }
        if(viewAxis == SectionAxis.Y)
        {
            halfX = (runtimeLevel.xLength - 1) * gridSize / 2f;
            halfY = (runtimeLevel.zLength - 1) * gridSize / 2f;
            for(int i = 0; i < runtimeLevel.xLength; i ++)
            {
                for(int j = 0; j < runtimeLevel.zLength; j ++)
                {
                    if(runtimeLevel.gridBehaviours[i,playerCoordinate.y,j] != null)
                        continue;
                    if(runtimeLevel.gridBehaviours[i,playerCoordinate.y - 1,j] != null)
                    {
                        runtimeLevel.gridBehaviours[i,playerCoordinate.y - 1,j].transform.localPosition = new Vector3(-halfX + gridSize * i, -halfY + gridSize * j);
                        runtimeLevel.gridBehaviours[i,playerCoordinate.y - 1,j].gameObject.SetActive(true);
                    }
                }
            }
            player.transform.position = new Vector3(-halfX + playerCoordinate.x * gridSize, -halfY + playerCoordinate.z * gridSize);
        }
    }
}
