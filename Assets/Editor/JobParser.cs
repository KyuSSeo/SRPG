using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections;

//  CSV파일을 읽어와서 프리팹 생성
public static class JobParser
{
    // 에디터메뉴에 항목 추가
    [MenuItem("Pre Production/Parse Jobs")]
    public static void Parse()
    {
        CreateDirectories();
        ParseStartingStats();
        ParseGrowthStats();
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    // Jobs 폴더 생성
    private static void CreateDirectories()
    {
        if (!AssetDatabase.IsValidFolder("Assets/Resources/Jobs"))
            AssetDatabase.CreateFolder("Assets/Resources", "Jobs");
    }

    // 초기 직업정보 JobStartingStats.csv 파싱
    private static void ParseStartingStats()
    {
        string readPath = string.Format("{0}/Settings/JobStartingStats.csv", Application.dataPath);
        string[] readText = File.ReadAllLines(readPath);
        // 첫 줄(1) 넘기고 반복
        for (int i = 1; i < readText.Length; ++i)
            PartsStartingStats(readText[i]);
    }
    private static void PartsStartingStats(string line)
    {
        // 쉼표 기준 분리
        string[] elements = line.Split(',');
        // 직업 이름으로 프리팹 생성
        GameObject obj = GetOrCreate(elements[0]);
        Job job = obj.GetComponent<Job>();
        // 각 기본 스탯 설정
        for (int i = 1; i < Job.statOrder.Length + 1; ++i)
            job.baseStats[i - 1] = Convert.ToInt32(elements[i]);
        // 이동, 점프 수치 설정
        StatModifierFeature move = GetFeature(obj, StatTypes.MOV);
        move.amount = Convert.ToInt32(elements[8]);
        StatModifierFeature jump = GetFeature(obj, StatTypes.JMP);
        jump.amount = Convert.ToInt32(elements[9]);
    }

    // 직업 성장정보 JobGrowthStats.csv 파싱
    private static void ParseGrowthStats()
    {
        string readPath = string.Format("{0}/Settings/JobGrowthStats.csv", Application.dataPath);
        string[] readText = File.ReadAllLines(readPath);
        // 첫 줄(1) 넘기고 반복
        for (int i = 1; i < readText.Length; ++i)
            ParseGrowthStats(readText[i]);
    }
    private static void ParseGrowthStats(string line)
    {
        string[] elements = line.Split(',');
        GameObject obj = GetOrCreate(elements[0]);
        Job job = obj.GetComponent<Job>();
        for (int i = 1; i < elements.Length; ++i)
            job.growStats[i - 1] = Convert.ToSingle(elements[i]);
    }

    // 지정된 StatType의 StatModifierFeature 가져오기
    private static StatModifierFeature GetFeature(GameObject obj, StatTypes type)
    {
        // 모든 Feature 확인
        StatModifierFeature[] smf = obj.GetComponents<StatModifierFeature>();
        for (int i = 0; i < smf.Length; ++i)
        {   
            // 이미 존재하면 반환
            if (smf[i].type == type)
                return smf[i];
        }  

        // 없으면 새로 추가
        StatModifierFeature feature = obj.AddComponent<StatModifierFeature>();
        feature.type = type;
        return feature;
    }


    //  프리팹 생성
    private static GameObject GetOrCreate(string jobName)
    {
        //  경로 확인
        string fullPath = string.Format("Assets/Resources/Jobs/{0}.prefab", jobName);
        GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(fullPath);
        // 없으면 생성
        if (obj == null)
            obj = Create(fullPath);
        return obj;
    }

    // 새 프리팹 생성
    private static GameObject Create(string fullPath)
    {
        GameObject instance = new GameObject("temp");        
        instance.AddComponent<Job>();                         
        GameObject prefab = PrefabUtility.SaveAsPrefabAsset(instance, fullPath);
        GameObject.DestroyImmediate(instance);                
        return prefab;
    }
}