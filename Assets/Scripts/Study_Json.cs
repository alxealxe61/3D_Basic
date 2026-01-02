using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Study_Json : MonoBehaviour
{
    [System.Serializable]
    public class SampleData
    {
        public string Name;
        public int Level;
        public int EXP;
        public float HP;
        public float MP;
        public int Attack;
        public int Defence;
    }
    
    private string savePath;

    private void Start()
    {
        // Unity에서 자주 사용하는 경로(Editor, Build(Window, Android))
        
        // 1. Application.dataPath
        // 프로젝트의 핵심 데이터가 위치한 경로
        // - Editor : 프로젝트의 "Assets" 폴더를 가리킵니다. ({Project Path}/Assets)
        // - Window Build : 실행 파일의 데이터 폴더 ({Product Name}_Date 폴더)
        // - Android Build : 앱 패키지 내부의 프라이빗 영역(.apk, ReadOnly)
        // - Android처럼 읽기 전용으로 되어있는 경우가 있음. mac에서도 그럴껄?(사실 잘 모름)
        //   데이터를 저장하기에는 부적합하다
        
        // 2. Application.streamingAssetsPath
        // 빌드 시 "Assets/StreamingAssets" 폴더의 내용이 그대로 복사되는 경로
        // - Editor : "Assets/StreamingAssets"
        // - Window Build : 실행 파일의 StreamingAssets 폴더 ({Product Name}/StreamingAssets)
        // - Android Build : jar:file:///data/app/com.Company.Game.apk/!/assets (URL 형태)
        // - System.IO.File 클래스로 접근이 불가능하며, 반드시 UnityWebRequest를 통해 데이터를 읽어야 합니다
        //   Better Streaming Assets 라이브러리(에셋 스토어) 사용하길 권장
        
        // 3. Application.persistentDataPath
        // 런타임중에 데이터를 저장하고 유지할 수 있는 샌드박스 경로입니다.
        // 프로그램의 정보에 따라 종덕으로 변경되는 경로 입니다.
        // User Base Path(사용자/AppDate/LocalLow 경로)/Company Name/Product Name
        // - Editor : {사용자}/AppDate/LocalLow/{Company Name}/{Product Name}
        // - Window Build : {사용자}/AppDate/LocalLow/{Company Name}/{Product Name}
        // - Android Build : /storage/emulated/0/Android/data/[PackageName}/files

        savePath = Path.Combine(Application.persistentDataPath, "sampleData");
        SaveSample(savePath);
        //LoadSample(savePath);
        // SampleData loadedData = JsonReader.Load<SampleData>(savePath);
        // Debug.Log($"name : {loadedData.Name}, Level : {loadedData.Level}," +
        //           $" EXP : {loadedData.EXP}, HP : {loadedData.HP}");
    }

    private void SaveSample(string path)
    {
        var date = new SampleData();
        date.Name = "Jay";
        date.Level = 100;
        date.EXP = 1000;
        date.HP = 10.05f;
        date.MP = 100.0f;
        date.Attack = 100;
        date.Defence = 100;
        
        var date2 = new SampleData();
        date2.Name = "Seyang";
        date2.Level = 100;
        date2.EXP = 1000;
        date2.HP = 10.05f;
        date2.MP = 100.0f;
        date2.Attack = 100;
        date2.Defence = 100;
        
        var date3 = new SampleData();
        date3.Name = "alex";
        date3.Level = 100;
        date3.EXP = 1000;
        date3.HP = 10.05f;
        date3.MP = 100.0f;
        date3.Attack = 100;
        date3.Defence = 100;
        
        
        var date4 = new SampleData();
        date4.Name = "mitten";
        date4.Level = 100;
        date4.EXP = 1000;
        date4.HP = 10.05f;
        date4.MP = 100.0f;
        date4.Attack = 100;
        date4.Defence = 100;
        
        
        var date5 = new SampleData();
        date5.Name = "박건준";
        date5.Level = 100;
        date5.EXP = 1000;
        date5.HP = 10.05f;
        date5.MP = 100.0f;
        date5.Attack = 100;
        date5.Defence = 100;
        
        var date6 = new SampleData();
        date6.Name = "밑트";
        date6.Level = 100;
        date6.EXP = 1000;
        date6.HP = 10.05f;
        date6.MP = 100.0f;
        date6.Attack = 100;
        date6.Defence = 100;
        

        List<SampleData> list = new();
        list.Add(date);
        list.Add(date2);
        list.Add(date3);
        list.Add(date4);
        list.Add(date5);
        list.Add(date6);
            
        JsonWriter.Save(list, path);
        
        Debug.Log($"저장 완료 : {path}");
    }

    private void LoadSample(string path)
    {
        List<SampleData> list = JsonReader.Load<List<SampleData>>(path);

        foreach (var sampleData in list)
        {
            Debug.Log($"name : {sampleData.Name}, Level : {sampleData.Level}," +
                $" EXP : {sampleData.EXP}, HP : {sampleData.HP}");
        }
    }
}
