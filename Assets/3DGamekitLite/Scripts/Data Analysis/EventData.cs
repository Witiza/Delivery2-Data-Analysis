using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DataType
{
    NONE = 0,
    POSITION,
    BUTTON_PRESSED,
    PLAYER_DEATH,
    RECEIVE_DAMAGE,
    ENEMY_KILLED
}

[Serializable]
public class EventData 
{
    public uint event_id = 0;
    public DataType dataType = DataType.NONE;
    public DateTime timestamp;

    protected EventData(uint event_id)
    {
        this.event_id = event_id;
        timestamp = System.DateTime.Now;
    }

    public string GetJson()
    {
        string json = JsonUtility.ToJson(this);
        return json;
    }
}

public class PlayerPosition : EventData
{
    public PlayerPosition(Vector3 position, uint event_id) : base(event_id)
    {
        dataType = DataType.POSITION;
        this.position = position;
    }

    public Vector3 position;
}

public class ButtonPressed : EventData
{
    public ButtonPressed(Vector3 position, uint event_id) : base(event_id)
    {
        dataType = DataType.BUTTON_PRESSED;
        this.position = position;
    }

    public Vector3 position;
}

public class PlayerDeath : EventData
{
    public PlayerDeath(Vector3 position, uint event_id, string enemyType) : base(event_id)
    {
        dataType = DataType.PLAYER_DEATH;
        this.position = position;
        this.enemyType = enemyType;
    }

    public Vector3 position;
    public string enemyType;
}

public class ReceiveDamage : EventData
{
    public ReceiveDamage(Vector3 position, uint event_id, string enemyType) : base(event_id)
    {
        dataType = DataType.RECEIVE_DAMAGE;
        this.position = position;
        this.enemyType = enemyType;
    }

    public Vector3 position;
    public string enemyType;
}

public class EnemyKilled : EventData
{
    public EnemyKilled(Vector3 position, uint event_id, string enemyType) : base(event_id)
    {
        dataType = DataType.ENEMY_KILLED;
        this.position = position;
        this.enemyType = enemyType;
    }

    public Vector3 position;
    public string enemyType;
}