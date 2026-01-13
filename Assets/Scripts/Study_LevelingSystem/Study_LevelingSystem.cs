using UnityEngine;
using UnityEngine.SceneManagement;

namespace Study_Camera.Study_LevelingSystem
{
    public class Study_LevelingSystem : MonoBehaviour
    {
        public SO_LevelingSystem SO_levelingSystem;

        public void Start()
        {
            SO_levelingSystem = Instantiate(SO_levelingSystem);

            SO_LevelingSystem.Instance.Print();

        }
    }
}