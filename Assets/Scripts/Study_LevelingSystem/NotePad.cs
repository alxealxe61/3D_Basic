using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Study_Camera.Study_LevelingSystem
{
    public class NotePad : MonoBehaviour
    {
        private void Start()
        {
            SO_LevelingSystem.Instance.AddExp(50);
            SceneManager.LoadScene(0);
        }

        private void OnEnable()
        {
            SO_LevelingSystem.Instance.OnLevelChange += OnChangedLevel;
            SO_LevelingSystem.Instance.OnExpChange += OnChangedExp;
        }


        private void OnDisable()
        {
            SO_LevelingSystem.Instance.OnLevelChange -= OnChangedLevel;
            SO_LevelingSystem.Instance.OnExpChange -= OnChangedExp;
        }

        private void OnChangedLevel(int level)
        {
            Debug.Log($"Level Up!! : {level}");
        }

        private void OnChangedExp(int exp)
        {
            Debug.Log($"Get Exp!! : {exp}");
        }
    }
}