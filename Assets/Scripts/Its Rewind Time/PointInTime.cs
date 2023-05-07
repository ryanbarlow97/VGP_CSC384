using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PointInTime
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector2 velocity;
    public float angularVelocity;
    public Vector3 scale;
    public string tag;
    public float spawnedAt;
    public float destroyedAt;
    public bool isEnabled;

    public PointInTime(Vector3 _position, Quaternion _rotation, Vector2 _velocity, float _angularVelocity, Vector3 _scale, string _tag, float _spawnedAt, float _destroyedAt, bool _isEnabled)
    {
        position = _position;
        rotation = _rotation;
        velocity = _velocity;
        angularVelocity = _angularVelocity;
        scale = _scale;
        tag = _tag;
        spawnedAt = _spawnedAt;
        destroyedAt = _destroyedAt;
        isEnabled = _isEnabled;
    }
}