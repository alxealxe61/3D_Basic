using UnityEngine;

public class RuntimeScriptableSingleton<T> : 
    ScriptableObject where T : ScriptableObject
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = CreateInstance<T>();
            }

            return instance;
        }
        
    }
}