using System.Collections.Generic;
using System.IO;
using Jay;
using UnityEngine;


public class TSVHomeWorkController : MonoBehaviour
{
    // TSV란?
    // 탭(Tab, \t)을 구분자로 사용하여 데이터를 저장하는 텍스트 포맷.
    // 주로 기획데이터(각종 테이블), 다국어 텍스트(Localization, StringTagle)
    // 등을 관리할때 CSV보다 훨씬 안정적이고(테이블에서 ','를 사용가능함),
    // 파싱이 용이하여 사용함. (콤마면 CSV, 탭이면 TSV)
    
    // 특징 
    // - 행(Row)외 열(Column)로 구분되어 구조가 단순하다.
    // - 거의 모든 관계형DB들과 별도의 처리 없이 호환가능하다.
    // - 닷넬 기준으로 CSVHelper.dll 을 이용해서 많이 사용함
    // - 제공해드린 TSV Reader. Writer는 내부의 프로퍼티로만 자동 매핑이 지원됩니다.
    
    [SerializeField] private Camera cam;
    public GameObject cubePrefabs;
    public Transform CubeRoot;
    int cubeIndex = 0;
    
    [System.Serializable]
    public class CubeData
    {
        public string Key { get; set; }

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
    }

    private string savePath;
    
    void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "cubes.tsv");
        Load();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject cube = Instantiate(
                    cubePrefabs,
                    hit.point,
                    Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)),
                    CubeRoot
                );
                
                cube.name = $"Cube_{cubeIndex++}";
            }
            Save();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.parent == CubeRoot)
                {
                    Destroy(hit.transform.gameObject);
                    Save();
                }
            }
            
        }
    }

    // 씬 에서 CubeData 수집 
    List<CubeData> CollectCubeData()
    {
        var list = new List<CubeData>();
        
        foreach (Transform child in CubeRoot)
        {
            list.Add(new CubeData{Key = child.name, 
                X =  child.position.x,
                Y =  child.position.y,
                Z =  child.position.z,
                
            });
        }
        
        return list;
    }
    
    private void Save()
    {
        TSVWriter.SaveTable(CollectCubeData(), savePath);
    }
    
    private void Load()
    {
        // Not 연산자는 고유한 상태를 반전 시킬 때만 사용해야한다 
        // Not 연산자는 잘 안보여서 
        // 아래에 경우 사용 X
        if (File.Exists(savePath) ==  false)
        {
            Debug.Log("저장된 TSV 파일이 없습니다.");
            return;
        }
        
        List<CubeData> list = new List<CubeData>();
        
        list = TSVReader.ReadTable<CubeData>(savePath);
        
        foreach (var data in list)
        {
            Vector3 position = new Vector3(data.X, data.Y, data.Z);
            
            GameObject cube = Instantiate(
                cubePrefabs,
                position,
                Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)),
                CubeRoot
            );
            cube.name = data.Key;
        }
    }
}