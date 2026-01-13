using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelingHub",menuName = "SO", order = 0)]
public class SO_LevelingSystem : ScriptableObject
{
    public static SO_LevelingSystem Instance;

    public int Level;
    public int Exp;
    
    public Action<int> OnLevelChange;
    public Action<int> OnExpChange;
    
    
    private void Awake()
    {
        Instance = this;
    }
    
    public void Print()
    {
        Debug.Log($"{Level},{Exp}");
    }

    public void AddExp(int exp)
    {
        Exp += exp;
        OnExpChange?.Invoke(Exp);
        if (Exp >= 100)
        {
            Level++;
            Exp = 0;
            OnLevelChange?.Invoke(Level);
        }
    }
}
