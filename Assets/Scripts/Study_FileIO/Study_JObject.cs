using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using UnityEngine;
using Study.FileIO.JsonDataModule;
    
public class Study_JObject : MonoBehaviour
{
    // JObject란?
    //  구체적인 데이터 클래스(DTO, Data Transfer Object)를 정의 하지 않고도 
    // JSON 데이터를 다루기 위한 도구.
    //  Unity에서 JSON을 다루기 위해 JsonUtility와 JObject(Newtonsoft.Json)
    // 두가지 중 Newtonsoft.Json에서만 사용 가능한 도구.
    //  JsonUtility가 정적인 데이터 구조를 처리하는데에 최적화 되어 있다면,
    // JObject는 비정형 데이터나 복잡한 중첩 구조를 유연하게 처리하는데 특화되어
    // 있습니다.
    
    // JObject의 특징 3가지
    // - 동적 접근이 가능하다
    // - 유연성 : 런타임에 추가, 삭제, 수정이 가능하다
    // - Linq 지원 : JToken, JArray와 함께 사용되며, 복잡한 JSON 쿼리가 가능하다.
    
    // JObject 사용시 주의사항
    // Garbage Collection(GC) 이슈
    // - JObject 파싱 과정에서 많은 참조 타입 객체를 생성합니다. Update Loop에서는 
    // 쓰지마세요. 초기화에 한번만 사용하는거는 괜찮습니다. 런타임시에 가끔 사용하는것도 OK
    // - 기본적으로 C#의 리플렉션 기능을 사용합니다. 느릴수 있다는것(성늘을 잡아먹는다)
    // 라는것을 기억해야합니다.
    
    // Garbage Collection 요거는 따로 특강 예정(1월27일)
    
    public enum UserType
    {
        User = 0,
        SuperUser,
        SuperSuperUser,
    }

    [Serializable]
    public class EquipmentData
    {
        public string UniqueID { get; set; }
        public int 강화Level { get; set; }
        public int Durability { get; set; } // 내구도

        public EquipmentData SetRandom()
        {
            UniqueID = Guid.NewGuid().ToString(); // Guid = 전역적으 고유한 식별자를 만들어내는 클래스 
            강화Level = UnityEngine.Random.Range(0, 100);
            Durability = UnityEngine.Random.Range(0, 100);
            return this;
        }

        public override string ToString()
        {
            return $"{UniqueID},{강화Level}, {Durability}";
        }
    }

    [Serializable]
    private class Quest
    {
        public int QuestNumber { get; set; }

        public Quest SetRandom()
        {
            QuestNumber = UnityEngine.Random.Range(0, 100);
            return this;
        }
    }
    [Serializable]
    private class Vector3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vector3(UnityEngine.Vector3 position)
        {
            X = position.x;
            Y = position.y;
            Z = position.z;
        }
    }
    
    private IJsonDataModule[] UserDataModule { get; set; }
    
    List<EquipmentData> inventory = new List<EquipmentData>();
    Dictionary<string, EquipmentData> equipInventory =  new Dictionary<string, EquipmentData>();
    HashSet<Quest> quest = new HashSet<Quest>();
    private string savePath;
    
    void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "UserData");
        CreateUserDataModule();
        
        Save();
        //Load();
        PrintUserDataModule();
    }

    private void CreateUserDataModule()
    {
        var head = new EquipmentData().SetRandom();
        var rightHead = new EquipmentData().SetRandom();
        var body = new EquipmentData().SetRandom();
        var leg = new EquipmentData().SetRandom();
        var foot = new EquipmentData().SetRandom();
        
        var Quest = new Quest();
        
        // 인벤토리에는 10개의 아이템이 있음
        inventory.Add(head);
        inventory.Add(rightHead);
        inventory.Add(body);
        inventory.Add(leg);
        inventory.Add(foot);
        
        inventory.Add(new EquipmentData().SetRandom());
        inventory.Add(new EquipmentData().SetRandom());
        inventory.Add(new EquipmentData().SetRandom());
        inventory.Add(new EquipmentData().SetRandom());
        inventory.Add(new EquipmentData().SetRandom());
        
        // 창작한것은 5개
        equipInventory.Add("head", head);
        equipInventory.Add("rightHead", rightHead);
        equipInventory.Add("body", body);
        equipInventory.Add("leg", leg);
        equipInventory.Add("foot", foot);
        
        quest.Add(Quest);
        quest.Add(Quest);
        quest.Add(Quest);
        quest.Add(Quest);
        quest.Add(Quest);
        
        quest.Add(new Quest().SetRandom());
        quest.Add(new Quest().SetRandom());
        quest.Add(new Quest().SetRandom());
        quest.Add(new Quest().SetRandom());
        quest.Add(new Quest().SetRandom());
        
        UserDataModule = new IJsonDataModule[]
        {
            new JsonDataModule<string>("name", "Tester"),
            new JsonDataModule<int>("gold", 0),
            new JsonDataModule<uint>("exp", 0),
            new JsonDataModule<float>("float", 0.139f),
            new JsonDataModule<bool>("isFirst", true),
            new JsonDataModule<UserType>("userType", UserType.User),
            
            new JsonDataModule<Vector3>("lastPos", new Vector3(new UnityEngine.Vector3( 9, 9, 9))),
            
            new ListDataModule<EquipmentData>("inventory", inventory),
            new DictionaryDataModule<string, EquipmentData>("equipInventory", equipInventory),
            new HashSetDataModule<Quest>("quest", quest),
        };
    }

    private void PrintUserDataModule()
    {
        foreach (var data in UserDataModule)
        {
            Debug.Log($"{data.Key}, {data.ToString()}");
        }
    }
    
    // Save 와 Load 는 방어적으로 프로그래밍 해야한다
    private void Save()
    {
        JObject data = new JObject();

        foreach (IJsonDataModule UserDataModule in UserDataModule)
        {
            if (UserDataModule == null || string.IsNullOrEmpty(UserDataModule.Key))
                continue;
            
            data[UserDataModule.Key] = UserDataModule.OnSave();
        }
        
        JsonWriter.Save(data, savePath);
        Debug.Log($"[UserDataManager] 사용자 데이터를 저장했습니다. 경로: {savePath}");
    }

    private void Load()
    {
        JObject data = JsonReader.Load<JObject>(savePath);

        foreach (IJsonDataModule userDataModule in UserDataModule)
        {
            if (userDataModule == null || string.IsNullOrEmpty(userDataModule.Key))
                continue;
            
            if (data.TryGetValue(userDataModule.Key, out JToken dataToken))
            {
                userDataModule.OnLoad(dataToken);
            }
        }

        Debug.Log($"[UserDataManager] 사용자 데이터를 로드했습니다. 경로: {savePath}");
    }
}
