using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class MapCube : MonoBehaviour
{
    public Color color;
    Vector2 position;
    EventData[] events;
    HeatMap heatmap;
    public Gradient gradient;

    public void Start()
    {
        gradient.
    }
    public void GenerateColor()
    {

        //Generate color depending on the events;
        color.r = Random.Range(0.0f, 1.0f);
        color.g = Random.Range(0.0f, 1.0f);
        color.b = Random.Range(0.0f, 1.0f);
        GetComponent<Renderer>().material.SetColor("_Color",color);
    }

    public void AdjoustPosition(Vector2 pos)
    {
        Vector3 position = transform.position;
        position.y = 1000;
        position.x = pos.x;
        position.z = pos.y;
        transform.position = position;
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, Vector3.up * -1, out hit))
        {
            position = transform.position;
            position.y = hit.point.y + 0.1f;

            transform.position = position;
        }
    }
    public void Draw()
    {

    }


}
