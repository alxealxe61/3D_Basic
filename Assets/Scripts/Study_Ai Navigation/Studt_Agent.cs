using UnityEngine;
using UnityEngine.AI;

public class Studt_Agent : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform pointerGuide;
    private NavMeshAgent agent;
    private Collider Collider { get; set; }
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Collider = GetComponent<Collider>();
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //.ScreenPointToRay(Vector3 position) position : 화면 기준 좌표
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000))
            {
                pointerGuide.position = hitInfo.point;
                agent.SetDestination(hitInfo.point);
            }
        }
    }
}
