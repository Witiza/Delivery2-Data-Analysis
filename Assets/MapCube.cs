using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCube : MonoBehaviour
{
    public Color color;
    Vector2 position;
    public List<PlayerPosition> positionList;
    public List<ButtonPressed> buttonList;
    public List<PlayerDeath> deathList;
    public List<EnemyKilled> killedList;
    public List<ReceiveDamage> damagedList;
    public HeatMap heatmap;
    public Gradient gradient;
    public LayerMask mask;

    public void Start()
    {
        gameObject.SetActive(false);
    }
    public void GenerateColor()
    {
        float factor = 0;
        if (positionList.Count > 0 && heatmap.show_positions)
            factor += positionList.Count;
        if (buttonList.Count > 0 && heatmap.show_buttons)
            factor += buttonList.Count;
        if (deathList.Count > 0 && heatmap.show_deaths)
            factor += deathList.Count;
        if (killedList.Count > 0 && heatmap.show_killed)
            factor += killedList.Count;
        if (damagedList.Count > 0 && heatmap.show_damaged)
            factor += damagedList.Count;
        //float factor = positionList.Count + buttonList.Count + deathList.Count + killedList.Count;
        factor /= heatmap.events_per_pos;
        factor = 1 - factor;
        //Generate color depending on the events;
        color.r = factor;
        color.g = factor;
        color.b = factor;
        var tmp_material = new Material(GetComponent<Renderer>().sharedMaterial);
        tmp_material.color = color;
        GetComponent<Renderer>().material = tmp_material;
    }

    public void AdjoustPosition(Vector2 pos)
    {
        Vector3 position = transform.position;
        position.y = 1000;
        position.x = pos.x;
        position.z = pos.y;
        transform.position = position;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.up * -1, out hit,mask))
        {
            position = transform.position;
            position.y = hit.point.y + 0.1f;

            transform.position = position;
        }
    }

    private void OnEnable()
    {
        Debug.Log("Enabled");
    }
}
