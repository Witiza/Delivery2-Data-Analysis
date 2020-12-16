using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public List<EventData> eventList;

    private GameObject player;

    private float lastTimeSent = 0;
    private uint eventCount = 0;

    void Start()
    {
        eventList = new List<EventData>();
    }
    
    void Update()
    {
        // Send list of stored events every 5 seconds
        if (Time.time - lastTimeSent > 5)
        {
            foreach (EventData e in eventList)
            {
                // cositas amb el codi de l'ivan
            }

            lastTimeSent = Time.time;
            eventList.Clear();
        }
    }

    public void AddPositionEvent()
    {
        eventCount++;

        PlayerPosition newEvent = new PlayerPosition(player.transform.position, eventCount);
        eventList.Add(newEvent);
    }

    public void AddDamageEvent()
    {
        /*eventCount++;

        DealDamage newEvent = new DealDamage();
        eventList.Add(newEvent);*/
    }
}