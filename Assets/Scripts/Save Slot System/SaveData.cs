using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public string playerName;
    public int playerLevel;
    public int playerScore;
    public int playerHearts;
    public SerializableVector3 playerPosition;
    public SerializableVector3 playerRotation;
    public List<MeteorData> meteorDataList = new List<MeteorData>();
    public List<SmallMeteorData> smallMeteorDataList = new List<SmallMeteorData>();

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
