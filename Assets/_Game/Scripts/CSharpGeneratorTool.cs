using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using AnnulusGames.LucidTools.Inspector;
using UnityEditor;
using UnityEngine;


public class CSharpGeneratorTool : MonoBehaviour
{
    public List<GameObject> Datas;
    public string nameT;

    [Button]
    void GenText()
    {
        nameT = "";
        foreach (GameObject dataPrefab in Datas)
        {
            if (dataPrefab != null)
            {
                string prefabName = dataPrefab.name;
                prefabName = prefabName.Replace("TT_", "");
                nameT += prefabName + ",";
                string generatedCSharp = GenJsonToCshap(prefabName);
                CreatorCs(prefabName, generatedCSharp);
            }
        }

        AssetDatabase.Refresh();
        //  CreatorCs("DataRemote", classStr);
    }

    void CreatorCs(string className, string conten)
    {
        string fileNameCs = Application.dataPath + "/_Game/Scripts/Units/Unit/" + className + ".cs";

        try
        {
            if (!File.Exists(fileNameCs))
            {
                FileStream fs = new FileStream(fileNameCs, FileMode.OpenOrCreate, FileAccess.Write);
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

    string GenJsonToCshap(string className)
    {
        var classStr = "using System;\n";
        classStr += "using System.Collections.Generic;\n";
        classStr += "using UnityEngine;\n";
        classStr += "public class " + className + " : UnitsBase\n";
        classStr += "{\n";

        classStr += "}\n";
        return classStr;
    }

    [Button]
    public void CreatorInventoryItem()
    {
        foreach (var i in Datas)
        {
            string prefabName = i.name;
            prefabName = prefabName.Replace("TT_", "");
            string itemName = prefabName;
            string relativePath = "_Game/SO_Data/Units/" + itemName + ".asset";

            string path = "Assets/" + relativePath; // Make sure the path is relative to the "Assets" folder

            if (File.Exists(path))
            {
                // Asset already exists at the specified path
                UnitSO existingItem = UnityEditor.AssetDatabase.LoadAssetAtPath<UnitSO>(path);
                if (existingItem != null)
                {
                    existingItem.entitySerializable.unitName = itemName;
                    existingItem.entityStatSerializable.unitName = itemName;
                    existingItem.entityStatSerializable.atkRange = 1;
                    existingItem.entityStatSerializable.atkSpeed = 1;
                    existingItem.entityStatSerializable.damage = 10;
                    existingItem.entityStatSerializable.healthMax = 100;
                    existingItem.entityStatSerializable.type = EnumConverter.StringToEnum<UnitType>(itemName.ToUpper());
                    if (existingItem.name == itemName)
                    {
                        existingItem.humanPrefab = i;
                    }
                    EditorUtility.SetDirty(existingItem);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
            }
            else
            {
                // Tạo một ScriptableObject theo tên i.name
                UnitSO newItem = ScriptableObject.CreateInstance<UnitSO>();

                newItem.name = itemName;

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