using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventData : MonoBehaviour
{
    uint eventID;
    DateTime timestamp;

    string GetJson()
    {
        string json = JsonUtility.ToJson(this);
        return json;
    }

    void Update()
    {
        
    }
}

public class DeathEvent : EventData
{
    Vector3 position;
    Vector3 eulerAngles;
}