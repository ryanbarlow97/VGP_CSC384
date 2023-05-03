[System.Serializable]
public struct SerializableFloat {
    public float value;

    public SerializableFloat(float value) {
        this.value = value;
    }

    public float ToFloat()
    {
        return value;
    }
}
