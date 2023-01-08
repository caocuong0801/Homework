using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class StageData
{
    public virtual int ID { get; set; }
    public virtual int Type { get; set; }
    public virtual int MapId { get; set; }
    public virtual int MapWidth { get; set; }
    public virtual int MapHeight { get; set; }
    public virtual int StartingSunCount { get; set; }
    public virtual int MaxSunCount { get; set; }
    public virtual string StageName { get; set; }
    public virtual string StageDescription { get; set; }
    public virtual int BattleTime { get; set; }
    public virtual int TargetType { get; set; }
    public virtual int TargetValue { get; set; }
    public virtual int SunSpawnedDelay { get; set; }
    public virtual int SunSpawnedDelayDelta { get; set; }

    public virtual string ZoombieWave { get; set; }
    public virtual string Rewards { get; set; }
    public virtual string Plants { get; set; }


    public IList<ZoombieWaveConfig> GetZoombieWaves()
    {
        var result = new List<ZoombieWaveConfig>();
        Debug.Log("begin` deserialize object: " + ZoombieWave);

        var waveData = JsonConvert.DeserializeObject<string[,]>(ZoombieWave);
        Debug.Log("after` deserialize object: " + waveData.Length);

        for (int count = 0; count < waveData.GetLength(0); count++)
        {
            var wave = new ZoombieWaveConfig();
            int.TryParse(waveData[count, 0], out int id);
            wave.ID = id;

            int.TryParse(waveData[count, 1], out int spawnedTime);
            wave.SpawnedTime = spawnedTime;

            var ZoombieStr = waveData[count, 2];
            var ZoombieData = JsonConvert.DeserializeObject<int[,]>(ZoombieStr);

            var enemies = new List<ZoombieConfig>();
            for (int idx = 0; idx < ZoombieData.GetLength(0); idx++)
            {
                var zoombie = new ZoombieConfig();
                zoombie.Type = ZoombieData[idx, 0];
                zoombie.Damage = ZoombieData[idx, 1];
                zoombie.HP = ZoombieData[idx, 2];
                zoombie.SkillId = ZoombieData[idx, 3];
                zoombie.RewardType = ZoombieData[idx, 4];
                zoombie.RewardValue = ZoombieData[idx, 5];

                enemies.Add(zoombie);
            }
            wave.Enemies = enemies;
            result.Add(wave);
        }
        return result;
    }


    public IList<PlantConfig> GetPlants()
    {
        // TODO: Like GetZoombieWaves()
        return null;
    }

    public IList<StageRewardsConfig> GetRewards()
    {
        // TODO: Like GetZoombieWaves()
        return null;
    }
}


public abstract class BaseObjectConfig
{
    public int Damage { get; set; }
    public int HP { get; set; }
    public int SkillId { get; set; } // Jump, Smash, Big boom...
    public int Type { get; set; }
}

//Stage Models
public class ZoombieConfig : BaseObjectConfig {
    public int RewardType { get; set; }
    public int RewardValue { get; set; }
    public float Speed { get; set; }
}

public class PlantConfig : BaseObjectConfig {
    public int Cost { get; set; }
    public int Countdown { get; set; }
}


public class ZoombieWaveConfig
{
    public int ID { get; set; }
    public List<ZoombieConfig> Enemies { get; set; }
    public int SpawnedTime { get; set; } // seconds
}

public class StageRewardsConfig
{
    public List<ChestConfig> Chests { get; set; } = new List<ChestConfig>();
}

public class ChestConfig
{
    public int Type { get; set; }
    public int Count { get; set; }
}