using UnityEngine;

[System.Serializable]
public class SerializableVector3
{
    public float[] data;

    public SerializableVector3(float x, float y, float z)
    {
        data = new float[] { x, y, z };
    }

    public Vector3 ToVector3()
    {
        return new Vector3(data[0], data[1], data[2]);
    }

    public static SerializableVector3 FromVector3(Vector3 vector)
    {
        return new SerializableVector3(vector.x, vector.y, vector.z);
    }
}
