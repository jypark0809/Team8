using Newtonsoft.Json;
using NUnit.Framework.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class DataTransformer : EditorWindow
{
#if UNITY_EDITOR
    [MenuItem("Tools/DeleteGameData")]
    public static void DeleteGameData()
    {
        string path = Application.persistentDataPath + "/SaveData.json";
        if (File.Exists(path))
            File.Delete(path);
    }

    [MenuItem("Tools/ParseExcel")]
    public static void ParseExcel()
    {
        ParseLevelExpData("LevelExp");
        ParseStatSpeedData("StatSpeed");
        ParseStatCooltimeData("StatCooltime");
        ParseStatMagnetData("StatMagnet");
        ParseDestroyableObjectData("DestroyableObject");
        ParseFurnitureData("Furniture");
    }

    static void ParseLevelExpData(string filename)
    {
        LevelExpDataLoader loader = new LevelExpDataLoader();

        #region ExcelData
        string[] lines = File.ReadAllText($"{Application.dataPath}/Resources/Data/Excel/{filename}Data.csv").Split("\n");

        for (int y = 1; y < lines.Length; y++)
        {
            string[] row = lines[y].Replace("\r", "").Split(',');
            if (row.Length == 0)
                continue;
            if (string.IsNullOrEmpty(row[0]))
                continue;
            int i = 0;

            loader.levelExps.Add(new LevelExpData()
            {
                Game_Lv = int.Parse(row[i++]),
                Game_Lv_Exp = int.Parse(row[i++])
            });
        }

        #endregion

        string jsonStr = JsonConvert.SerializeObject(loader, Formatting.Indented);
        File.WriteAllText($"{Application.dataPath}/Resources/Data/Json/{filename}Data.json", jsonStr);
        AssetDatabase.Refresh();
    }

    static void ParseStatSpeedData(string filename)
    {
        StatSpeedDataLoader loader = new StatSpeedDataLoader();

        #region ExcelData
        string[] lines = File.ReadAllText($"{Application.dataPath}/Resources/Data/Excel/{filename}Data.csv").Split("\n");

        for (int y = 1; y < lines.Length; y++)
        {
            string[] row = lines[y].Replace("\r", "").Split(',');
            if (row.Length == 0)
                continue;
            if (string.IsNullOrEmpty(row[0]))
                continue;
            int i = 0;

            loader.statSpeeds.Add(new StatSpeedData()
            {
                Stats_Lv = int.Parse(row[i++]),
                Stats_Speed = float.Parse(row[i++])
            });
        }

        #endregion

        string jsonStr = JsonConvert.SerializeObject(loader, Formatting.Indented);
        File.WriteAllText($"{Application.dataPath}/Resources/Data/Json/{filename}Data.json", jsonStr);
        AssetDatabase.Refresh();
    }

    static void ParseStatCooltimeData(string filename)
    {
        StatCooltimeDataLoader loader = new StatCooltimeDataLoader();

        #region ExcelData
        string[] lines = File.ReadAllText($"{Application.dataPath}/Resources/Data/Excel/{filename}Data.csv").Split("\n");

        for (int y = 1; y < lines.Length; y++)
        {
            string[] row = lines[y].Replace("\r", "").Split(',');
            if (row.Length == 0)
                continue;
            if (string.IsNullOrEmpty(row[0]))
                continue;
            int i = 0;

            loader.statCooltimes.Add(new StatCooltimeData()
            {
                Stats_Lv = int.Parse(row[i++]),
                Stats_Cooltime = float.Parse(row[i++])
            });
        }

        #endregion

        string jsonStr = JsonConvert.SerializeObject(loader, Formatting.Indented);
        File.WriteAllText($"{Application.dataPath}/Resources/Data/Json/{filename}Data.json", jsonStr);
        AssetDatabase.Refresh();
    }

    static void ParseStatMagnetData(string filename)
    {
        StatMagnetDataLoader loader = new StatMagnetDataLoader();

        #region ExcelData
        string[] lines = File.ReadAllText($"{Application.dataPath}/Resources/Data/Excel/{filename}Data.csv").Split("\n");

        for (int y = 1; y < lines.Length; y++)
        {
            string[] row = lines[y].Replace("\r", "").Split(',');
            if (row.Length == 0)
                continue;
            if (string.IsNullOrEmpty(row[0]))
                continue;
            int i = 0;

            loader.statMagnets.Add(new StatMagnetData()
            {
                Stats_Lv = int.Parse(row[i++]),
                Stats_Magnet = float.Parse(row[i++])
            });
        }

        #endregion

        string jsonStr = JsonConvert.SerializeObject(loader, Formatting.Indented);
        File.WriteAllText($"{Application.dataPath}/Resources/Data/Json/{filename}Data.json", jsonStr);
        AssetDatabase.Refresh();
    }

    static void ParseDestroyableObjectData(string filename)
    {
        DestroyableObjectDataLoader loader = new DestroyableObjectDataLoader();

        #region ExcelData
        string[] lines = File.ReadAllText($"{Application.dataPath}/Resources/Data/Excel/{filename}Data.csv").Split("\n");

        for (int y = 1; y < lines.Length; y++)
        {
            string[] row = lines[y].Replace("\r", "").Split(',');
            if (row.Length == 0)
                continue;
            if (string.IsNullOrEmpty(row[0]))
                continue;
            int i = 0;

            loader.destroyableObjects.Add(new DestroyableObjectData()
            {
                Object_Id = int.Parse(row[i++]),
                Object_Int_Name = row[i++],
                Player_Lv = int.Parse(row[i++]),
                Touch_Count = int.Parse(row[i++]),
                Object_Gold = int.Parse(row[i++]),
                Object_Diamond = int.Parse(row[i++]),
                Image_Path = row[i++]
            });
        }

        #endregion

        string jsonStr = JsonConvert.SerializeObject(loader, Formatting.Indented);
        File.WriteAllText($"{Application.dataPath}/Resources/Data/Json/{filename}Data.json", jsonStr);
        AssetDatabase.Refresh();
    }

    static void ParseFurnitureData(string filename)
    {
        FurnitureDataLoader loader = new FurnitureDataLoader();

        #region ExcelData
        string[] lines = File.ReadAllText($"{Application.dataPath}/Resources/Data/Excel/{filename}Data.csv").Split("\n");

        for (int y = 1; y < lines.Length; y++)
        {
            string[] row = lines[y].Replace("\r", "").Split(',');
            if (row.Length == 0)
                continue;
            if (string.IsNullOrEmpty(row[0]))
                continue;
            int i = 0;

            loader.furnitures.Add(new FurnitureData()
            {
                F_Id = int.Parse(row[i++]),
                F_Int_Name = row[i++],
                F_Name = row[i++],
                F_Desc = row[i++],
                F_Space_Num = int.Parse(row[i++]),
                F_Happiness = int.Parse(row[i++]),
                F_Gold = int.Parse(row[i++]),
                F_Path = row[i++]
            });
        }

        #endregion

        string jsonStr = JsonConvert.SerializeObject(loader, Formatting.Indented);
        File.WriteAllText($"{Application.dataPath}/Resources/Data/Json/{filename}Data.json", jsonStr);
        AssetDatabase.Refresh();
    }
#endif
}
