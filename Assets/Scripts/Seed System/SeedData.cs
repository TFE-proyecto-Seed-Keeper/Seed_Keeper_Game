
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SeedData", menuName = "Scriptable Objects/SeedData")]
public class SeedData : ScriptableObject
{
    public List<Seed> seedList;

    public enum seedType
    {
      ceiba,
      guayacan,
      totumo
    }
}

[Serializable]
public class Seed
{
    public string name;
    public int seedcount;

    public Sprite seedDprite;

    public SeedData.seedType seedType;

}
