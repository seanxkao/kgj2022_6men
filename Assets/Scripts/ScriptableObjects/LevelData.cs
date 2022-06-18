using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "3D Puzzling Platformer/Level")]
public class LevelData : ScriptableObject
{
    public Level level = new Level(1,1,1);
    public Vector3Int startingPoint;

    public RuntimeLevel ConvertToRuntime(Transform parent)
    {
        var runtime = new RuntimeLevel(level.xLength, level.yLength, level.zLength);
        for(int i = 0; i < runtime.xLength; i++)
        {
            for(int j = 0; j < runtime.yLength; j++)
            {
                for(int k = 0; k < runtime.zLength; k++)
                {
                    if(level.gridDatas[i,j,k].gridBehaviour != null)
                    {
                        runtime.gridBehaviours[i,j,k] = MonoBehaviour.Instantiate(level.gridDatas[i,j,k].gridBehaviour, parent);
                        runtime.gridBehaviours[i,j,k].Construct(level.gridDatas[i,j,k]);
                        runtime.gridBehaviours[i,j,k].gameObject.SetActive(false);
                    }
                    else
                        runtime.gridBehaviours[i,j,k] = null;
                }
            }
        }
        return runtime;
    }
}
