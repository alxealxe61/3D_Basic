using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Study_LevelingSystem : MonoBehaviour
{
    // version1
    
    // private void Awake()
    // {
    //     var levelingSystem = SO_LevelingSystem.Instance;
    // }
    //
    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Alpha1))
    //     {
    //         SO_LevelingSystem.Instance.AddExp(1500);
    //     }
    //     
    //     if (Input.GetKeyDown(KeyCode.Alpha2))
    //     {
    //         (int, int) levelData = SO_LevelingSystem.Instance.GetCurrentLevelData();
    //         int level = levelData.Item1;
    //         int exp = levelData.Item2;
    //         
    //         Debug.Log($"level = {level}, exp = {exp}");
    //
    //         // 튜플을 사용할때는 반환받은 값개체를 수정하면 안됩니다. 불문율 같은거?
    //         // 실수가 많이 나옵니다.
    //         levelData.Item1 += 5;
    //         levelData.Item2 += 5;
    //         
    //         Debug.Log($"levelData.Item1 = {levelData.Item1}, levelData.Item2 = {levelData.Item2}");
    //     }
    // }
    //
    // private void OnEnable()
    // {
    //     SO_LevelingSystem.Instance.OnLevelChange += OnChangedLevel;
    //     SO_LevelingSystem.Instance.OnExpChange += OnChangedExp;
    // }
    //
    // private void OnDisable()
    // {
    //     SO_LevelingSystem.Instance.OnLevelChange -= OnChangedLevel;
    //     SO_LevelingSystem.Instance.OnExpChange -= OnChangedExp;
    // }
    //
    // private void OnChangedLevel(int level)
    // {
    //     Debug.Log($"Level Up!!! : {level}");
    //
    //     if (level >= 7)
    //     {
    //         Debug.Log($"파이어볼이 잠금해제 되었습니다");
    //     }
    // }
    //     
    // private void OnChangedExp(int exp, int remainExp)
    // {
    //     Debug.Log($"Get Exp : {exp}, Remain Exp : {remainExp} ");
    // }
    
    
    // version2
    
    private void Awake()
    {
        var levelingSystem = Mono_LevelingSystem.Instance;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Mono_LevelingSystem.Instance.AddExp(1500);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            (int, int) levelData = Mono_LevelingSystem.Instance.GetCurrentLevelData();
            int level = levelData.Item1;
            int exp = levelData.Item2;
            
            Debug.Log($"level = {level}, exp = {exp}");

            // 튜플을 사용할때는 반환받은 값개체를 수정하면 안됩니다. 불문율 같은거?
            // 실수가 많이 나옵니다.
            levelData.Item1 += 5;
            levelData.Item2 += 5;
            
            Debug.Log($"levelData.Item1 = {levelData.Item1}, levelData.Item2 = {levelData.Item2}");
        }
    }

    private void OnEnable()
    {
        Mono_LevelingSystem.Instance.OnLevelChange += OnChangedLevel;
        Mono_LevelingSystem.Instance.OnExpChange += OnChangedExp;
    }

    private void OnDisable()
    {
        Mono_LevelingSystem.Instance.OnLevelChange -= OnChangedLevel;
        Mono_LevelingSystem.Instance.OnExpChange -= OnChangedExp;
    }

    private void OnChangedLevel(int level)
    {
        Debug.Log($"Level Up!!! : {level}");

        if (level >= 7)
        {
            Debug.Log($"파이어볼이 잠금해제 되었습니다");
        }
    }
        
    private void OnChangedExp(int exp, int remainExp)
    {
        Debug.Log($"Get Exp : {exp}, Remain Exp : {remainExp} ");
    }
}