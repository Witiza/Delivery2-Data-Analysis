using Gamekit3D;
using Gamekit3D.GameCommands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Gamekit3D.Damageable;

public class EventHandler : MonoBehaviour
{
    public List<EventData> eventList;

    public string posData;
    public string buttonData;
    public string deathData;
    public string dmgData;
    public string enemyData;

    private float lastTimeSent = 0;
    private uint eventCount = 0;

    private void OnEnable()
    {
        Damageable.damageDelegateEvent += AddDamageEvent;
        Damageable.deathDelegateEvent += AddDeathEvent;
        PlayerController.positionDelegateEvent += AddPlayerPositionEvent;
        SendOnTriggerEnter.buttonDelegateEvent += AddButtonPressedEvent;
    }

    private void OnDisable()
    {
        Damageable.damageDelegateEvent -= AddDamageEvent;
        Damageable.deathDelegateEvent -= AddDeathEvent;
        PlayerController.positionDelegateEvent -= AddPlayerPositionEvent;
        SendOnTriggerEnter.buttonDelegateEvent -= AddButtonPressedEvent;
    }

    void Start()
    {
        eventList = new List<EventData>();
    }
    
    void Update()
    {
        // Send list of stored events every 3 seconds
        if (Time.time - lastTimeSent > 3)
        {
            foreach (EventData e in eventList)
            {
                Load(e);    
            }

            lastTimeSent = Time.time;
            eventList.Clear();
        }
    }

    // Events
    public void AddPlayerPositionEvent(PlayerController character)
    {
        eventCount++;

        PlayerPosition newEvent = new PlayerPosition(character.transform.position, eventCount);
        eventList.Add(newEvent);

        //Debug.Log(newEvent.GetJson());
    }

    public void AddButtonPressedEvent(Vector3 position)
    {
        eventCount++;

        ButtonPressed newEvent = new ButtonPressed(position, eventCount);
        eventList.Add(newEvent);
    }

    public void AddDeathEvent(GameObject character, GameObject damager)
    {
        eventCount++;

        if (LayerMask.LayerToName(damager.layer) == "Player")
        {
            EnemyKilled newEvent = new EnemyKilled(damager.transform.position, eventCount, character.name);
            eventList.Add(newEvent);
            Debug.Log(newEvent.GetJson());
        }
        else if (LayerMask.LayerToName(damager.layer) == "Enemy" || LayerMask.LayerToName(damager.layer) == "Collider")
        {
            string enemy = damager.transform.parent?.gameObject.name;
            if (damager.GetComponent<Spit>() != null)
                enemy = "Spitter";

            PlayerDeath newEvent = new PlayerDeath(character.transform.position, eventCount, enemy);
            eventList.Add(newEvent);
            Debug.Log(newEvent.GetJson());
        }
        else if (LayerMask.LayerToName(damager.layer) == "Environment")
        {
            PlayerDeath newEvent = new PlayerDeath(character.transform.position, eventCount, damager.name);
            eventList.Add(newEvent);
            Debug.Log(newEvent.GetJson());
        }
    }

    public void AddDamageEvent(GameObject character, GameObject damager)
    {
        eventCount++;

        if (LayerMask.LayerToName(damager.layer) == "Enemy" || LayerMask.LayerToName(damager.layer) == "Collider")
        {
            string enemy = damager.transform.parent?.gameObject.name;
            if (damager.GetComponent<Spit>() != null)
                enemy = "Spitter";

            ReceiveDamage newEvent = new ReceiveDamage(character.transform.position, eventCount, enemy);
            eventList.Add(newEvent);
            Debug.Log(newEvent.GetJson());
        }
    }

    public void Load(EventData eventData)
    {
        string data = eventData.GetJson();
        string[] variables = data.Split(',');
        foreach (string variable in variables)
        { 
            if (variable == "\"dataType\":1")
            {
                posData = data;
            }
            else if (variable == "\"dataType\":2")
            {
                buttonData = data;
            }
            else if(variable == "\"dataType\":3")
            {
                deathData = data;
            }
            else if(variable == "\"dataType\":4")
            {
                dmgData = data;
            }
            else if(variable == "\"dataType\":5")
            {
                enemyData = data;
            }
        }
    }

    public string GetPos()
    {
        return posData;
    }

    public string GetButton()
    {
        return buttonData;
    }

    public string GetDeath()
    {
        return deathData;
    }

    public string GetDamage()
    {
        return dmgData;
    }

    public string GetEnemy()
    {
        return enemyData;
    }
}

