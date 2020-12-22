using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;



public class HeatMap : EditorWindow
{
    //---------HeatMap variables---------------------//
    Vector2 size;
    Vector2 middlepoint = Vector2.zero;
    GameObject[,] heat_matrix;
    GameObject root;
    public HeatMapRenderer renderer;
    public int events_per_pos;
    GameObject cube_prefab;

    List<PlayerPosition> positionList = null;
    List<ButtonPressed> buttonList = null;
    List<PlayerDeath> deathList = null;
    List<EnemyKilled> killedList = null;
    List<ReceiveDamage> damagedList = null;

   public bool show_positions = true;
   public bool show_buttons = true;
   public bool show_deaths = true;
   public bool show_killed = true;
   public bool show_damaged = true;



    [MenuItem("Window/Data Analysis/HeatMap")]
    public static void ShowWindow()
    {
        GetWindow<HeatMap>("HeatMap");
    }

    private void OnGUI()
    {
        GUILayout.Label(SceneManager.GetActiveScene().name, EditorStyles.boldLabel);

        size = EditorGUILayout.Vector2Field("Map Size", size);
        middlepoint = EditorGUILayout.Vector2Field("Middle Point", middlepoint);
        events_per_pos = EditorGUILayout.IntField("Max events per position", events_per_pos);
        cube_prefab = (GameObject)EditorGUILayout.ObjectField(cube_prefab, typeof(GameObject), true);

        show_positions = EditorGUILayout.ToggleLeft("Show Positions", show_positions);
        show_buttons = EditorGUILayout.ToggleLeft("Show Button Presses", show_buttons);
        show_deaths = EditorGUILayout.ToggleLeft("Show Player Deaths", show_deaths);
        show_killed = EditorGUILayout.ToggleLeft("Show Enemies Killed", show_killed);
        show_damaged = EditorGUILayout.ToggleLeft("Show when Player was Damaged", show_damaged);

        if (GUILayout.Button("Generate"))
        {
            DeleteMap();
            GenerateHeatmap();
        }
        if(GUILayout.Button("Regenerate Colors"))
        {
            RegenerateColors();
        }
        if (GUILayout.Button("Delete"))
        {
            DeleteMap();
        }

        //Debug.Log(renderer.name);
    }

    private void LoadAndAssignData()
    {
        Debug.Log("-------Loading Data-----------");
        positionList = EventHandler.LoadPositions();
        if(positionList != null)
        {
            for(int i = 0;i<positionList.Count;i++)
            {
                FindClosestPlane(positionList[i].position).positionList.Add(positionList[i]);
            }
        }
        buttonList = EventHandler.LoadButtons();
        if (buttonList != null)
        {
            for (int i = 0; i < buttonList.Count; i++)
            {
                FindClosestPlane(buttonList[i].position).buttonList.Add(buttonList[i]);
            }
        }
        deathList = EventHandler.LoadDeaths();
        if (deathList != null)
        {
            for (int i = 0; i < deathList.Count; i++)
            {
                FindClosestPlane(deathList[i].position).deathList.Add(deathList[i]);
            }
        }
        killedList = EventHandler.LoadKilled();
        if (killedList != null)
        {
            for (int i = 0; i < killedList.Count; i++)
            {
                FindClosestPlane(killedList[i].position).killedList.Add(killedList[i]);
            }
        }
        damagedList = EventHandler.LoadDamaged();
        if (damagedList != null)
        {
            for (int i = 0; i < damagedList.Count; i++)
            {
                FindClosestPlane(damagedList[i].position).damagedList.Add(damagedList[i]);
            }
        }

    }
    private MapCube FindClosestPlane(Vector3 pos)
    {
        MapCube ret = heat_matrix[0, 0].GetComponent<MapCube>();
        float current_distance = (heat_matrix[0, 0].transform.position - pos).magnitude;
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                if((heat_matrix[i,j].transform.position-pos).magnitude < current_distance)
                {
                    ret = heat_matrix[i, j].GetComponent<MapCube>();
                    current_distance = (heat_matrix[i, j].transform.position - pos).magnitude;
                }
            }
        }
        if (current_distance < 10)
        {
            return ret;
        }
        else
        {
            Debug.Log("WHOOPS");
            return null;
        }
    }
    private void GenerateHeatmap()
    {
        size.x = (int)size.x;
        size.y = (int)size.y;

        //Generate empty map
        if (size.x > 0 && size.y > 0)
        {
            root = new GameObject();
            root.name = "Heat Map";
            
            root.transform.position = middlepoint;
            Debug.Log("-------Generating Empty Map...-----------");
            heat_matrix = null;
            heat_matrix = new GameObject[(int)size.x, (int)size.y];
            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    Vector2 pos = middlepoint;
                    pos.x += i - size.x / 2;
                    pos.y += j - size.y / 2;
                    heat_matrix[i, j] = Instantiate(cube_prefab,root.transform);
                    heat_matrix[i, j].GetComponent<MapCube>().AdjoustPosition(pos);
                    heat_matrix[i, j].GetComponent<MapCube>().heatmap = this;
                }
            }
            RegenerateColors();
        }
        else
        {
            Debug.LogError("The heatmap needs a minimum size");
        }
        
    }

    void RegenerateColors()
    {
        //Load Deeta
        LoadAndAssignData();
        //Generate Colors
        Debug.Log("-------Generating Colors-----------");
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                heat_matrix[i, j].GetComponent<MapCube>().GenerateColor();
            }
        }
    }
    private void DeleteMap()
    {
        DestroyImmediate(root);
        heat_matrix = null;

    }



}
