using Gamekit3D;
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

    // Events
    public void AddPlayerPositionEvent()
    {
        eventCount++;

        PlayerPosition newEvent = new PlayerPosition(player.transform.position, eventCount);
        eventList.Add(newEvent);
    }

    public void AddButtonPressedEvent()
    {
        eventCount++;

        ButtonPressed newEvent = new ButtonPressed(player.transform.position, eventCount);
        eventList.Add(newEvent);
    }

    public void AddPlayerDeathEvent(Damageable character)
    {
        eventCount++;

        /*PlayerDeath newEvent = new PlayerDeath(player.transform.position, eventCount, enemyType);
        eventList.Add(newEvent);*/
    }

    public void AddReceiveDamageEvent(Damageable character)
    {
        eventCount++;

        /*ReceiveDamage newEvent = new ReceiveDamage(player.transform.position, eventCount, enemyType);
        eventList.Add(newEvent);*/
    }

    public void AddDealDamageEvent(EnemyType enemyType)
    {
        eventCount++;

        DealDamage newEvent = new DealDamage(player.transform.position, eventCount, enemyType);
        eventList.Add(newEvent);
    }

    public void AddEnemyKilledEvent(EnemyType enemyType)
    {
        eventCount++;

        EnemyKilled newEvent = new EnemyKilled(player.transform.position, eventCount, enemyType);
        eventList.Add(newEvent);
    }
}