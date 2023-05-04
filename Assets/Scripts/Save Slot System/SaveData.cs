using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public string playerName;
    public int playerScore;
    public int playerHearts;
    public SerializableVector3 playerPosition;
    public SerializableVector3 playerRotation;
    public List<MeteorData> meteorDataList = new List<MeteorData>();
    public List<SmallMeteorData> smallMeteorDataList = new List<SmallMeteorData>();
    public List<PowerUpData> powerUpDataList = new List<PowerUpData>();

}

[System.Serializable]
public class MeteorData {
    public SerializableVector3 position;
    public SerializableVector3 velocity;
    public SerializableVector3 scale;
}

[System.Serializable]
public class SmallMeteorData {
    public int meteorType;
    public SerializableVector3 position;
    public SerializableVector3 velocity;
    public SerializableVector3 scale;
    public SerializableVector3 rotation;
    public SerializableFloat angularVelocity;
}

[System.Serializable]
public class PowerUpData {
    public int powerUpType;
    public SerializableVector3 position;
    public SerializableVector3 velocity;
    public SerializableVector3 scale;
}