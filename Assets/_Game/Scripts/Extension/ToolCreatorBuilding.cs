using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using AnnulusGames.LucidTools.Inspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

public class ToolCreatorBuilding : MonoBehaviour
{
    [SerializeField] private Object folderSavePrefab;
    [SerializeField] private Object folderSaveCs;


    [SerializeField] private GameObject prefabBaseBuilding;
    [SerializeField] private List<GameObject> objBuildingModel = new List<GameObject>();
    [SerializeField] private List<GameObject> objConstructionModel = new List<GameObject>();

    public string nameT;

    void Start()
    {
    }

    [Button]
    public void TestPath()
    {
        string folderPath = UnityEditor.AssetDatabase.GetAssetPath(folderSaveCs);
        Debug.Log("Absolute Path: " + folderPath);
    }

    [Button]
    public void GenPrefabUnits()
    {
        foreach (var unitModel in objBuildingModel)
        {
            var unit = Instantiate(prefabBaseBuilding);
            unit.transform.SetParent(transform);
            unit.transform.localPosition = Vector3.zero;

            var unitAgent = Instantiate(unitModel);
            unitAgent.transform.SetParent(unit.transform.Find("Model"));
            unitAgent.transform.localPosition = Vector3.zero;
            unitAgent.transform.localScale = Vector3.one * 2;
            unitAgent.AddComponent(typeof(AnimatorHandle));
            string prefabName = unitModel.name;
            prefabName = prefabName.Replace("_A", "");
            prefabName = prefabName.Replace("_A_1x1", "");

            unit.name = prefabName;
            if (!string.IsNullOrEmpty(prefabName) && GetComponent(prefabName) == null)
            {
                // Nếu chưa tồn tại, thêm script vào đối tượng
                System.Type scriptType = System.Type.GetType(prefabName);
                if (scriptType != null)
                {
                    unit.AddComponent(scriptType);
                }
                else
                {
                    Debug.LogError("Không thể tìm thấy loại script: " + prefabName);
                }
            }

            string folderPath = UnityEditor.AssetDatabase.GetAssetPath(folderSavePrefab);

            GameObject prefab = PrefabUtility.SaveAsPrefabAsset(unit, folderPath + "/" + unit.name + ".prefab");

            if (prefab != null)
            {
                Debug.Log("Prefab saved at: " + AssetDatabase.GetAssetPath(prefab));
            }
            else
            {
                Debug.LogError("Failed to save prefab.");
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

    [Button]
    void GenTextCS()
    {
        nameT = "";
        foreach (GameObject dataPrefab in objBuildingModel)
        {
            if (dataPrefab != null)
            {
                string prefabName = dataPrefab.name;
                prefabName = prefabName.Replace("_A", "");
                prefabName = prefabName.Replace("_1x1", "");
                nameT += prefabName + ",";
                string generatedCSharp = GenJsonToCshap(prefabName);
                CreatorCs(prefabName+"Building", generatedCSharp);
            }
        }

        AssetDatabase.Refresh();
        //  CreatorCs("DataRemote", classStr);
    }

    string GenJsonToCshap(string className)
    {
        var classStr = "using System;\n";
        classStr += "using System.Collections.Generic;\n";
        classStr += "using UnityEngine;\n";
        classStr += "public class " + className+"Building" + " : BuildingBase\n";
        classStr += "{\n";

        classStr += "}\n";
        return classStr;
    }

    void CreatorCs(string className, string conten)
    {
        string fileNameCs = UnityEditor.AssetDatabase.GetAssetPath(folderSaveCs)+"/"+className+".cs";

        try
        {
            if (!File.Exists(fileNameCs))
            {
                FileStream fs = new FileStream(fileNameCs , FileMode.OpenOrCreate, FileAccess.Write);
                fs.Close();
                File.AppendAllText(fileNameCs, Environment.NewLine + conten);
                AssetDatabase.Refresh();
            }
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex);
        }
    }
      [Button]
    public void CreatorInventoryItem()
    {
        foreach (var i in objBuildingModel)
        {
            string prefabName = i.name;
            prefabName = prefabName.Replace("TT_", "");
            prefabName = prefabName.Replace("_A", "");
            prefabName = prefabName.Replace("_1x1", "");
            string itemName = prefabName;
            string relativePath = "_Game/SO_Data/Building/" + itemName + ".asset";

            string path = "Assets/" + relativePath; // Make sure the path is relative to the "Assets" folder

            if (File.Exists(path))
            {
                // Asset already exists at the specified path
                BuildingSO existingItem = UnityEditor.AssetDatabase.LoadAssetAtPath<BuildingSO>(path);
                if (existingItem != null)
                {
                    existingItem.name = itemName;
                    existingItem.timeBuildingHouse = 5;
                    existingItem.timeSpawnUnit = 1.5f;
                    existingItem.type = EnumConverter.StringToEnum<BuildingType>(itemName.ToUpper());

                    EditorUtility.SetDirty(existingItem);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
            }
            else
            {
                // Tạo một ScriptableObject theo tên i.name
                BuildingSO newItem = ScriptableObject.CreateInstance<BuildingSO>();

                newItem.name = itemName;
                newItem.name = itemName;
                newItem.timeBuildingHouse = 5;
                newItem.timeSpawnUnit = 1.5f;
                newItem.type = EnumConverter.StringToEnum<BuildingType>(itemName.ToUpper());

                // newItem.data.typeItem = i.type;

                // Lưu ScriptableObject vào đường dẫn đã chỉ định
                AssetDatabase.CreateAsset(newItem, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                Debug.Log("Đã tạo tệp " + itemName + ".asset.");
            }
        }
    }
}