using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    public Action OnShot;
    private int _shotsTaken;
    private int _shotsMade;
    public int ShotsTaken
    {
        get => _shotsTaken;
        set
        {
        _shotsTaken = value;
        OnShot?.Invoke();
        }
    }

    public int ShotsMade
    {
        get => _shotsMade;
        set
        {
            _shotsMade = value;
            OnShot?.Invoke();
        }
    }
}
