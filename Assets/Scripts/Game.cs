using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public static Game instance = null;

    public GameObject levelParent { get; private set; }
    private Camera cam = null;
    private List<GridBehaviour> grids = new List<GridBehaviour>();
    private Dictionary<Vector3Int, GridBehaviour> dictionary = new Dictionary<Vector3Int, GridBehaviour>();

    [SerializeField]
    private Text universeText;
    [SerializeField]
    private Gradient textGradient;
    [SerializeField]
    private float textColorLoopTime;
    [SerializeField]
    private Canvas _canvas;

    private float textTimer;


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
        if(_canvas.worldCamera == null)
        {
            _canvas.worldCamera = Camera.main;
        }

        universeText.color = textGradient.Evaluate((textTimer / textColorLoopTime));
        textTimer = (textTimer + Time.deltaTime) % textColorLoopTime;

        if(Input.GetKeyDown(KeyCode.Alpha1) && projectionAxis != ProjectionAxis.X)
        {
            projectionAxis = ProjectionAxis.X;
            ShowProjection();
            AxisChange.Invoke(projectionAxis);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2) && projectionAxis != ProjectionAxis.Y)
        {
            projectionAxis = ProjectionAxis.Y;
            ShowProjection();
            AxisChange.Invoke(projectionAxis);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3) && projectionAxis != ProjectionAxis.Z) 
        {
            projectionAxis = ProjectionAxis.Z;
            ShowProjection();
            AxisChange.Invoke(projectionAxis);
        }  
    }

    private void FindAllGrids()
    {
        grids.Clear();
        dictionary.Clear();

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

            Vector3Int intVect = new Vector3Int
                                    (
                                        (int)grid.transform.position.x,
                                        (int)grid.transform.position.y,
                                        (int)grid.transform.position.z
                                    );
            dictionary.Add(intVect, grid);
        }

        levelParent = new GameObject();
        levelParent.transform.position = new Vector3(minX + maxX, minX + maxY, minZ + maxZ) / 2f;
        foreach(var grid in grids)
            grid.transform.SetParent(levelParent.transform);
    }

    void ShowProjection()
    {
        if(cam == null)
            cam = Camera.main;
        
        string text = "";
        cam.transform.position = levelParent.transform.position;

        switch(projectionAxis)
        {
            case ProjectionAxis.X:
                cam.transform.position += Vector3.right * 100;
                cam.transform.rotation = Quaternion.Euler(0,270,0);
                text = "A";
                break;
            case ProjectionAxis.Y:
                cam.transform.position += Vector3.up * 100;
                cam.transform.rotation = Quaternion.Euler(90,0,180);
                text = "B";
                break;
            case ProjectionAxis.Z:
                cam.transform.position += Vector3.forward * 100;
                cam.transform.rotation = Quaternion.Euler(0,180,0);
                text = "C";
                break;
        }

        universeText.text = "宇宙 " + text;
    }

    public bool ShowNoEntrySign(Vector3 playerPosition, Vector3 playerVelocity)
    {
        Vector3 rawScanPosition = playerPosition + playerVelocity + Vector3.down;
        Vector3Int scanPosition = new Vector3Int
                                (
                                    Mathf.RoundToInt(rawScanPosition.x),
                                    Mathf.RoundToInt(rawScanPosition.y),
                                    Mathf.RoundToInt(rawScanPosition.z)
                                );
        
        if(
            !dictionary.ContainsKey(scanPosition) && 
            !dictionary.ContainsKey(scanPosition + Vector3Int.up) && 
            !dictionary.ContainsKey(scanPosition - Vector3Int.up) && 
            !dictionary.ContainsKey(scanPosition - Vector3Int.up * 2) && 
            !dictionary.ContainsKey(scanPosition - Vector3Int.up * 3) &&
            !dictionary.ContainsKey(scanPosition - Vector3Int.up * 4) 
          )
            return true;
        else
            return false;
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
