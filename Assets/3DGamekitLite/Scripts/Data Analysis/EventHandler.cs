using Gamekit3D;
using Gamekit3D.GameCommands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Gamekit3D.Damageable;

public class EventHandler : MonoBehaviour
{
    public List<EventData> eventList;

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
    public void AddPlayerPositionEvent(PlayerController character)
    {
        eventCount++;

        PlayerPosition newEvent = new PlayerPosition(character.transform.position, eventCount);
        eventList.Add(newEvent);
    }

    public void AddButtonPressedEvent(Vector3 position)
    {
        eventCount++;

        ButtonPressed newEvent = new ButtonPressed(position, eventCount);
        eventList.Add(newEvent);
    }

    public void AddDeathEvent(Damageable character, DamageMessage msg)
    {
        eventCount++;

        if (LayerMask.LayerToName(msg.damager.gameObject.layer) == "Player")
        {
            EnemyKilled newEvent = new EnemyKilled(msg.damager.gameObject.transform.position, eventCount, character.gameObject.name);
            eventList.Add(newEvent);
        }
        else if (LayerMask.LayerToName(msg.damager.gameObject.layer) == "Enemy")
        {
            PlayerDeath newEvent = new PlayerDeath(character.transform.position, eventCount, msg.damager.gameObject.name);
            eventList.Add(newEvent);
        }
        else
        {
            int a = 0;
        }
    }

    public void AddDamageEvent(Damageable character, DamageMessage msg)
    {
        eventCount++;

        if (LayerMask.LayerToName(msg.damager.gameObject.layer) == "Enemy")
        {
            ReceiveDamage newEvent = new ReceiveDamage(character.transform.position, eventCount, msg.damager.gameObject.name);
            eventList.Add(newEvent);
        }
    }
}