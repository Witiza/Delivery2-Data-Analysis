using Gamekit3D;
using Gamekit3D.GameCommands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Gamekit3D.Damageable;

public class EventHandler : MonoBehaviour
{
    public List<PlayerPosition> positionList;
    public List<ButtonPressed> buttonList;
    public List<PlayerDeath> deathList;
    public List<EnemyKilled> killedList;
    public List<ReceiveDamage> damagedList;

    public string posData;
    public string buttonData;
    public string deathData;
    public string dmgData;
    public string enemyData;

    private float lastTimeSent = 0;
    private uint eventCount = 0;
    
    private void SaveData()
    {
        string to_save;
        if (positionList.Count > 0)
        {
            List<PlayerPosition> positions = LoadPositions();
            if(positions != null)
            {
                positionList.AddRange(positions);
            }
            to_save = JsonHelper.ToJson(positionList.ToArray());
            System.IO.File.WriteAllText(Application.persistentDataPath + "/PositionData.json", to_save);
        }
        if(buttonList.Count > 0)
        {
            List<ButtonPressed> buttons = LoadButtons();
            if(buttons != null)
            {
                buttonList.AddRange(buttons);
            }
            to_save = JsonHelper.ToJson(buttonList.ToArray());
            System.IO.File.WriteAllText(Application.persistentDataPath + "/ButtonData.json", to_save);
        }
        if (deathList.Count > 0)
        {
            List<PlayerDeath> deaths = LoadDeaths();
            if (deaths != null)
            {
                deathList.AddRange(deaths);
            }
            to_save = JsonHelper.ToJson(deathList.ToArray());
            System.IO.File.WriteAllText(Application.persistentDataPath + "/DeathData.json", to_save);
        }
        if (killedList.Count > 0)
        {
            List<EnemyKilled> kills = LoadKilled();
            if(kills != null)
            {
                killedList.AddRange(kills);
            }
            to_save = JsonHelper.ToJson(killedList.ToArray());
            System.IO.File.WriteAllText(Application.persistentDataPath + "/KilledData.json", to_save);
        }
        if (damagedList.Count > 0)
        {
            List<ReceiveDamage> damage = LoadDamaged();
            if(damage != null)
            {

            }
            to_save = JsonHelper.ToJson(damagedList.ToArray());
            System.IO.File.WriteAllText(Application.persistentDataPath + "/DamagedData.json", to_save);
        }
    }

    public static List<PlayerPosition> LoadPositions()
    {
        if (System.IO.File.Exists(Application.persistentDataPath + "/PositionData.json"))
        {
            string loaded = System.IO.File.ReadAllText(Application.persistentDataPath + "/PositionData.json");
            List<PlayerPosition> positions = new List<PlayerPosition>(JsonHelper.FromJson<PlayerPosition>(loaded));
            return positions;
        }
        Debug.Log("whops 2.0");
        return null;
    }
    public static List<ButtonPressed> LoadButtons()
    {
        if (System.IO.File.Exists(Application.persistentDataPath + "/ButtonData.json"))
        {
            string loaded = System.IO.File.ReadAllText(Application.persistentDataPath + "/ButtonData.json");
            List<ButtonPressed> buttons = new List<ButtonPressed>(JsonHelper.FromJson<ButtonPressed>(loaded));
            return buttons;
        }
        return null;
    }

    public static List<PlayerDeath> LoadDeaths()
    {
        if (System.IO.File.Exists(Application.persistentDataPath + "/DeathData.json"))
        {
            string loaded = System.IO.File.ReadAllText(Application.persistentDataPath + "/DeathData.json");
            List<PlayerDeath> deaths = new List<PlayerDeath>(JsonHelper.FromJson<PlayerDeath>(loaded));
            return deaths;
        }
        return null;
    }
    public static List<EnemyKilled> LoadKilled()
    {
        if (System.IO.File.Exists(Application.persistentDataPath + "/KilledData.json"))
        {
            string loaded = System.IO.File.ReadAllText(Application.persistentDataPath + "/KilledData.json");
            List<EnemyKilled> kills = new List<EnemyKilled>(JsonHelper.FromJson<EnemyKilled>(loaded));
            return kills;
        }
        return null;
    }
    public static List<ReceiveDamage> LoadDamaged()
    {
        if (System.IO.File.Exists(Application.persistentDataPath + "/DamagedData.json"))
        {
            string loaded = System.IO.File.ReadAllText(Application.persistentDataPath + "/DamagedData.json");
            List<ReceiveDamage> damaged = new List<ReceiveDamage>(JsonHelper.FromJson<ReceiveDamage>(loaded));
            return damaged;
        }
        return null;
    }


    private void OnEnable()
    {
        Damageable.damageDelegateEvent += AddDamageEvent;
        Damageable.deathDelegateEvent += AddDeathEvent;
        PlayerController.positionDelegateEvent += AddPlayerPositionEvent;
        SendOnTriggerEnter.buttonDelegateEvent += AddButtonPressedEvent;
    }

    private void OnDisable()
    {
        SaveData();
        Damageable.damageDelegateEvent -= AddDamageEvent;
        Damageable.deathDelegateEvent -= AddDeathEvent;
        PlayerController.positionDelegateEvent -= AddPlayerPositionEvent;
        SendOnTriggerEnter.buttonDelegateEvent -= AddButtonPressedEvent;
    }

    void Start()
    {
        positionList = new List<PlayerPosition>();

        buttonList = new List<ButtonPressed>();
        deathList = new List<PlayerDeath>();
        killedList = new List<EnemyKilled>();
        damagedList = new List<ReceiveDamage>();

    }
    
    void Update()
    {
        // Send list of stored events every 3 seconds
        if (Time.time - lastTimeSent > 3)
        {
            //foreach (EventData e in eventList)
            //{
            //    Load(e);
            //}
            //SaveData to_save = new SaveData();
            //to_save.events = eventList;
            //string potion = JsonUtility.ToJson(to_save);
            //System.IO.File.WriteAllText(Application.persistentDataPath + "/PotionData.json", potion);
            //potion = System.IO.File.ReadAllText(Application.persistentDataPath + "/PotionData.json");
            //test = JsonUtility.FromJson<SaveData>(potion);
            //lastTimeSent = Time.time;
            //eventList.Clear();
        }
    }

    // Events
    public void AddPlayerPositionEvent(PlayerController character)
    {
        eventCount++;

        PlayerPosition newEvent = new PlayerPosition(character.transform.position, eventCount);
        positionList.Add(newEvent);

        //Debug.Log(newEvent.GetJson());
    }

    public void AddButtonPressedEvent(Vector3 position)
    {
        eventCount++;

        ButtonPressed newEvent = new ButtonPressed(position, eventCount);
        buttonList.Add(newEvent);
    }

    public void AddDeathEvent(GameObject character, GameObject damager)
    {
        eventCount++;

        if (LayerMask.LayerToName(damager.layer) == "Player")
        {
            EnemyKilled newEvent = new EnemyKilled(damager.transform.position, eventCount, character.name);
            killedList.Add(newEvent);
            Debug.Log(newEvent.GetJson());
        }
        else if (LayerMask.LayerToName(damager.layer) == "Enemy" || LayerMask.LayerToName(damager.layer) == "Collider")
        {
            string enemy = damager.transform.parent?.gameObject.name;
            if (damager.GetComponent<Spit>() != null)
                enemy = "Spitter";

            PlayerDeath newEvent = new PlayerDeath(character.transform.position, eventCount, enemy);
            deathList.Add(newEvent);
            Debug.Log(newEvent.GetJson());
        }
        else if (LayerMask.LayerToName(damager.layer) == "Environment")
        {
            PlayerDeath newEvent = new PlayerDeath(character.transform.position, eventCount, damager.name);
            deathList.Add(newEvent);
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
            damagedList.Add(newEvent);
            Debug.Log(newEvent.GetJson());
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

//https://stackoverflow.com/questions/36239705/serialize-and-deserialize-json-and-json-array-in-unity/36244111#36244111
public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}

