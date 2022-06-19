using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game instance = null;

    public GameObject levelParent { get; private set; }
    private Camera cam = null;
    private List<GridBehaviour> grids = new List<GridBehaviour>();

    public float minX { get; private set; } = float.MaxValue;
    public float maxX { get; private set; } = float.MinValue;
    public float minY { get; private set; } = float.MaxValue;
    public float maxY { get; private set; } = float.MinValue;
    public float minZ { get; private set; } = float.MaxValue;
    public float maxZ { get; private set; } = float.MinValue;
    
    public ProjectionAxis projectionAxis { get; private set; } = ProjectionAxis.Z;
    public GameEvent<ProjectionAxis> AxisChange = new GameEvent<ProjectionAxis>();

    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
            return;
        }
        FindAllGrids();
        ShowProjection();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable() 
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindAllGrids();
        ShowProjection();
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            projectionAxis = ProjectionAxis.X;
            ShowProjection();
            AxisChange.Invoke(projectionAxis);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            projectionAxis = ProjectionAxis.Y;
            ShowProjection();
            AxisChange.Invoke(projectionAxis);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)) 
        {
            projectionAxis = ProjectionAxis.Z;
            ShowProjection();
            AxisChange.Invoke(projectionAxis);
        }  
    }

    private void FindAllGrids()
    {
        grids = FindObjectsOfType<GridBehaviour>().ToList();

        minX = float.MaxValue;
        maxX = float.MinValue;
        minY = float.MaxValue;
        maxY = float.MinValue;
        minZ = float.MaxValue;
        maxZ = float.MinValue;
        foreach(var grid in grids)
        {
            minX = Mathf.Min(minX, grid.transform.position.x);
            maxX = Mathf.Max(maxX, grid.transform.position.x);
            minY = Mathf.Min(minY, grid.transform.position.y);
            maxY = Mathf.Max(maxY, grid.transform.position.y);
            minZ = Mathf.Min(minZ, grid.transform.position.z);
            maxZ = Mathf.Max(maxZ, grid.transform.position.z);
        }

        levelParent = new GameObject();
        levelParent.transform.position = new Vector3(minX + maxX, minX + maxY, minZ + maxZ) / 2f;
        foreach(var grid in grids)
            grid.transform.SetParent(levelParent.transform);
    }

    public void UpdateLevel(List<GridBehaviour> newGrids)
    {
        foreach(var grid in newGrids)
        {
            minX = Mathf.Min(minX, grid.transform.position.x);
            maxX = Mathf.Max(maxX, grid.transform.position.x);
            minY = Mathf.Min(minY, grid.transform.position.y);
            maxY = Mathf.Max(maxY, grid.transform.position.y);
            minZ = Mathf.Min(minZ, grid.transform.position.z);
            maxZ = Mathf.Max(maxZ, grid.transform.position.z);
            grids.Add(grid);
        }
        levelParent.transform.position = new Vector3(minX + maxX, minX + maxY, minZ + maxZ) / 2f;
        foreach(var grid in newGrids)
            grid.transform.SetParent(levelParent.transform);
    }

    void ShowProjection()
    {
        if(cam == null)
            cam = Camera.main;
        
        cam.transform.position = levelParent.transform.position;

        switch(projectionAxis)
        {
            case ProjectionAxis.X:
                cam.transform.position += Vector3.right * 100;
                cam.transform.rotation = Quaternion.Euler(0,270,0);
                break;
            case ProjectionAxis.Y:
                cam.transform.position += Vector3.up * 100;
                cam.transform.rotation = Quaternion.Euler(90,0,180);
                break;
            case ProjectionAxis.Z:
                cam.transform.position += Vector3.forward * 100;
                cam.transform.rotation = Quaternion.Euler(0,180,0);
                break;
        }
    }

    public bool InvalidPosition(Vector3 position, out Vector3 nearest)
    {
        var result = position.x > maxX || 
                     position.x < minX ||  
                     position.y < minY || 
                     position.z > maxZ || 
                     position.z < minZ;
        nearest = new Vector3
        (
            Mathf.Clamp(position.x, minX, maxX),
            Mathf.Max(position.y, minY),
            Mathf.Clamp(position.z, minZ, maxZ)
        );
        return result;
    }
}
