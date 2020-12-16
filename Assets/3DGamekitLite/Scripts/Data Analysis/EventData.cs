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
    DEAL_DAMAGE,
    ENEMY_KILLED
}

public enum EnemyType
{
    CHOMPER = 0,
    SPITTER
}

public class EventData : MonoBehaviour
{
    protected uint event_id = 0;
    protected DataType dataType = DataType.NONE;
    protected DateTime timestamp;

    protected EventData(uint event_id)
    {
        this.event_id = event_id;
        timestamp = System.DateTime.Now;
    }

    string GetJson()
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

    Vector3 position;
}

public class ButtonPressed : EventData
{
    public ButtonPressed(Vector3 position, uint event_id) : base(event_id)
    {
        dataType = DataType.BUTTON_PRESSED;
        this.position = position;
    }

    Vector3 position;
}

public class PlayerDeath : EventData
{
    public PlayerDeath(Vector3 position, uint event_id, EnemyType enemyType) : base(event_id)
    {
        dataType = DataType.PLAYER_DEATH;
        this.position = position;
        this.enemyType = enemyType;
    }

    Vector3 position;
    EnemyType enemyType;
}

public class ReceiveDamage : EventData
{
    public ReceiveDamage(Vector3 position, uint event_id, EnemyType enemyType) : base(event_id)
    {
        dataType = DataType.RECEIVE_DAMAGE;
        this.position = position;
        this.enemyType = enemyType;
    }

    Vector3 position;
    EnemyType enemyType;
}

public class DealDamage : EventData
{
    public DealDamage(Vector3 position, uint event_id, EnemyType enemyType) : base(event_id)
    {
        dataType = DataType.DEAL_DAMAGE;
        this.position = position;
        this.enemyType = enemyType;
    }

    Vector3 position;
    EnemyType enemyType;
}

public class EnemyKilled : EventData
{
    public EnemyKilled(Vector3 position, uint event_id, EnemyType enemyType) : base(event_id)
    {
        dataType = DataType.ENEMY_KILLED;
        this.position = position;
        this.enemyType = enemyType;
    }

    Vector3 position;
    EnemyType enemyType;
}