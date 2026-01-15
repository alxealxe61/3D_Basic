using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class ItemDropSystem : MonoBehaviour
{
    // 실제 아이템과 연동될 게임 리소스 
    [SerializeField] private GameObject[] itemObjectsPrefab;
    [SerializeField] private ItemBox itemBox;

    private Dictionary<string, Package> packages = new();
    
    [Header("Tset Package Name")]
    public string TestPackageName;
    void Awake()
    {
        InitTables();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (packages.ContainsKey(TestPackageName))
            {
                itemBox.ClearBox();
                DropItems(packages[TestPackageName]);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            int rand =  Random.Range(0, packages.Count);
            var keys = packages.Keys.ToArray();
            var select = packages[keys[rand]];
            
            Debug.Log($"Open Package ! ::: {select}");
            DropItems(select);
        }
            
    }
    
    private void InitTables() 
    {
        // 3개로 나뉘어진 테이블들을 각각 초기화하여 연동한다
        // 1. 종속성이 없는 테이블 부터 초기화하면 됨 (ItemData)
        // 2. 1을 참조하고 있는 녀석 초기화 (ItemGroup)
        // 3. 가장 많은 종속성을 갖고 있는 테이블 초기화 (Package)

        string path = Path.Combine(Application.streamingAssetsPath, "DropTable");
        
        var allItem = TSVReader.ReadTable<ItemData>
            (Path.Combine(path, "ItemDataTable.tsv")).ToArray();
        
        var allItemGroup = TSVReader.ReadTable<ItemGroup>
            (Path.Combine(path, "ItemGroup.tsv")).ToArray();
        
        // 원본 데이터를 보존하면서, 런타임에서 사용할 객체로 바꿔준다

        for (int i = 0; i < allItemGroup.Length; i++)
        {
            allItemGroup[i].SetChance();
            allItemGroup[i].SetItems(allItem);
        }
        
        var allPackage = TSVReader.ReadTable<Package>
            (Path.Combine(path, "PackageTable.tsv")).ToArray();
        
        for (int i = 0; i < allPackage.Length; i++)
        {
            allPackage[i].SetItemGroup(allItemGroup);
            packages.Add(allPackage[i].PackageName, allPackage[i]);
        }

        // 테스트용 코드 
        // foreach (var packages in packages.Values)
        // {
        //     Debug.Log(packages.PackageName);
        // }
    }

    private void DropItems(Package package)
    {
        var itemGroupArray = package.ItemGroup;
        GameObject[] itemObjectArray = new GameObject[itemGroupArray.Length];
        
        // 확률로 표현된 ItemGroup을 구체화(오브젝트 하나 선택) 해서
        // itemObjectArray를 채우는 로직이 들어가면 됩니ㅏㄷ

        for (int i = 0; i < itemGroupArray.Length; i++)
        {
            itemObjectArray[i] = 
                GetObjectByItemData(itemGroupArray[i].GetItemByChance());
        }
        
        
        itemBox.SetBox(itemObjectArray);
        GameObject GetObjectByItemData(ItemData data)
        {
            GameObject sample = null;

            for (int i = 0; i < itemObjectsPrefab.Length; i++)
            {
                if(data.ResourceKey.Equals(itemObjectsPrefab[i].name))
                {
                    sample = itemObjectsPrefab[i];
                }
            }
            if (sample == null) return null;
            return Instantiate(sample);
        }
    }
}
