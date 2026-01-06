using System;
using System.Collections.Generic;
using System.IO;
using Jay;
using UnityEngine;
using Random = UnityEngine.Random;


public class TSVController : MonoBehaviour
{
     public class CubeData
     {
          public float PosX {get; set;}
          public float PosY {get; set;}
          public float PosZ {get; set;}
          
          public float AngleX {get; set;}
          public float AngleY {get; set;}
          public float AngleZ {get; set;}

          public CubeData(Vector3 position, Quaternion rotation)
          {
               PosX = position.x;
               PosY = position.y;
               PosZ = position.z;

               AngleX = rotation.eulerAngles.x;
               AngleY = rotation.eulerAngles.y;
               AngleZ = rotation.eulerAngles.z;
          }
     }
     
     [SerializeField] private GameObject SampleCube;
    
     private Camera mainCamera;
    
     private List<GameObject> cubeList = new List<GameObject>();
     private List<CubeData> cubeDataList = new List<CubeData>();
     // Start is called once before the first execution of Update after the MonoBehaviour is created
     void Start()
     {
          mainCamera = Camera.main;
          
          cubeDataList = TSVReader.ReadTable<CubeData>
               (Path.Combine(Application.streamingAssetsPath, "cubes.tsv"));

          for (int i = 0; i < cubeDataList.Count; i++)
          {
               CubeData cubeData = cubeDataList[i];
               
               var cube = Instantiate(SampleCube);
               cube.transform.position = new Vector3(cubeData.PosX, cubeData.PosY, cubeData.PosZ);
               cube.transform.rotation = Quaternion.Euler(cubeData.AngleX, cubeData.AngleY, cubeData.AngleZ);
               cube.SetActive(true);
               
               cubeList.Add(cube);
          }
     }

     // 런타임 삭제랑 파일 삭제는 
     // Update is called once per frame
     void Update()
     {
          if (Input.GetMouseButtonDown(0))
          {
               Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
               if (Physics.Raycast(ray, out RaycastHit hit, 100, LayerMask.GetMask("Terrain")))
               {
                    var cube = Instantiate(SampleCube);
                    cube.transform.position = hit.point;
                    cube.transform.rotation = Quaternion.Euler
                         (Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
                    cube.SetActive(true);
                    
                    cubeList.Add(cube);
               }
          }

          if (Input.GetMouseButtonDown(1))
          {
               Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
               if (Physics.Raycast(ray, out RaycastHit hit, 100, LayerMask.GetMask("Terrain")))
               {
                    LayerMask mask = LayerMask.GetMask("Terrain");
                    if(mask.Contains(hit.transform.gameObject.layer)) return;
                    
                    var hitObject = hit.transform.gameObject;
                    cubeList.Remove(hitObject);
                    Destroy(hitObject);
               }
          }
     }

     
     private void OnApplicationQuit()
     {
          // OnApplicationQuit은 조심히 사용해야합니다.
          // 다른 객체를 호출하거나 참조하면 안됨.
          for (int i = 0; i < cubeList.Count; i++)
          {
               var cube = cubeList[i];
               cubeDataList.Add(new CubeData(cube.transform.position, cube.transform.rotation));
          }
          
          TSVWriter.SaveTable(cubeDataList, Path.Combine(Application.streamingAssetsPath, "cubes.tsv"));
          
     }

     public void Save()
     {
          
     }

     public void Load()
     {
          
     }
}