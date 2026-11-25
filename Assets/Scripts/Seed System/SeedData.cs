
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SeedData", menuName = "Scriptable Objects/SeedData")]
public class SeedData : ScriptableObject
{
    public List<Seed> seedList;

    public void ResetAllSeedCount()
    {
        foreach (var seed in seedList)
        {
            seed.ResetSeedCount();
        }
    }

    public enum seedType
    {
      ceiba,
      flormorado,
      totumo
    }
}

[Serializable]
public class Seed
{
    public string name;
    public int seedcount;

    public Sprite seedSprite;

    public SeedData.seedType seedType;

    public void ResetSeedCount()
    {
        seedcount = 0;
    }

}
