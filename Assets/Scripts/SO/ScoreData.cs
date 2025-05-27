using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class ScoreData : ScriptableObject
{
    public Action<int,int> OnShot;
    
    private int _shotsTaken;
    private int _shotsMade;
    public int ShotsTaken
    {
        get => _shotsTaken;
        set
        {
        _shotsTaken = value;
        OnShot?.Invoke(_shotsTaken, _shotsMade);
        }
    }
    public int ShotsMade
    {
        get => _shotsMade;
        set
        {
            _shotsMade = value;
            OnShot?.Invoke(_shotsTaken, _shotsMade);
        }
    }
}
