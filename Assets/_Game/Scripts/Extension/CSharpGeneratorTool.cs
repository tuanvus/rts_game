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
    public List<GameObject> building;
    public string nameBuilding;

 

    [Button]
    public void Building()
    {
        Debug.Log("game");
        foreach (GameObject dataPrefab in building)
        {
                string prefabName = dataPrefab.name;
                nameBuilding += prefabName + ",";
        }
    }
    
    [Button]
    void Test()
    {
        string objectName = gameObject.name;

        // Tên của script bạn muốn thêm
        string scriptToBeAdded = objectName;


        // Kiểm tra xem script đã tồn tại chưa
        if (!string.IsNullOrEmpty(scriptToBeAdded) && GetComponent(scriptToBeAdded) == null)
        {
            Debug.Log("tồn tại");
            // Nếu chưa tồn tại, thêm script vào đối tượng
            System.Type scriptType = System.Type.GetType(scriptToBeAdded);
            if (scriptType != null)
            {
                gameObject.AddComponent(scriptType);
            }
            else
            {
                Debug.LogError("Không thể tìm thấy loại script: " + scriptToBeAdded);
            }
        }
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