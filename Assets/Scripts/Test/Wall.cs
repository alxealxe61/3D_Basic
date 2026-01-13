using UnityEngine;

public class Wall : MonoBehaviour
{
    private GameObject gameObject;
    // Update is called once per frame

    void Start()
    {
        
    }
    
    void Update()
    {
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}
