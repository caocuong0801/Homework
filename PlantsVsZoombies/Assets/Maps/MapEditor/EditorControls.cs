using System;
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

[CustomEditor(typeof(MapEditorHandler))]
public class EditorControls : Editor
{

    int selGridInt = 0;
    string stageID = "";
    string startMapID = "";
    string endMapID = "";
    string[] selStrings = { "radio1", "radio2", "radio3" };

    Vector2 scrollPosition;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();


        MapEditorHandler myScript = (MapEditorHandler)target;
        myScript.Initialized();
        myScript.CurrentEditor = this;

        //var savedMapNames = myScript.GetSavedMapNames().Split(char.Parse(";"));

        // Starts an area to draw elements
        EditorGUILayout.HelpBox("[Saving Name] Nhập tên tilemap mới sau khi edit xong. Rồi click [Lưu Tilemap với tên]", MessageType.Info);
        if (GUILayout.Button("Lưu Tilemap với tên [Saving Name]", GUILayout.Height(30)))
        {
            myScript.SaveMap();
        }
        GUILayout.Space(50);

        GUILayout.Space(50);
        GUILayout.Label("Map Json file name");
        stageID = GUILayout.TextField(stageID, 20, GUILayout.Height(30));

        EditorGUILayout.HelpBox("Export object on tilemap to JSON.", MessageType.Info);


        //EditorGUILayout.HelpBox("Chọn 1 hoặc nhiều tile ở Tile Pallete sau đó chấm lên các ô trong Tilemap để vẽ.", MessageType.Info);
        //if (GUILayout.Button("Mở Tile Palette", GUILayout.Height(30)))
        //{
        //    EditorApplication.ExecuteMenuItem("Window/2D/Tile Palette");
        //}
        //GUILayout.Space(50);

        //EditorGUILayout.HelpBox("Các Tilemap đã lưu nằm bên dưới", MessageType.Info);
        //if (GUILayout.Button("Mở Tilemap mặc định", GUILayout.Height(30)))
        //{
        //    myScript.OpenDefaultMap();
        //}
        //if (savedMapNames.Length > 0)
        //{
        //    // Debug.Log("Saved Map names = " + savedMapNames);
        //    scrollPosition = GUILayout.BeginScrollView(
        //        scrollPosition, GUILayout.Width(200), GUILayout.Height(200));
        //    foreach (var item in savedMapNames)
        //    {
        //        if (item.Length > 0)
        //        {
        //            var mapName = item.Contains(".prefab") ? item.Remove(item.IndexOf(".prefab")) : item;
        //            if (GUILayout.Button("Mở Tilemap [" + mapName + "]"))
        //            {
        //                myScript.OpenMap(mapName);
        //            }
        //        }
        //    }
        //    GUILayout.EndScrollView();
        //}


        ////if (GUILayout.Button("Generator Map", GUILayout.Height(30)))
        ////{
        ////    myScript.GenerateMap(int.Parse(stageID));
        ////}

        ////if (GUILayout.Button("Load Map From Json", GUILayout.Height(30)))
        ////{
        ////    myScript.LoadMapDataFromJson(stageID);
        ////}

        //if (GUILayout.Button("Export to JSON", GUILayout.Height(30)))
        //{
        //    myScript.ExportToJson(stageID);
        //}

        //GUILayout.Space(50);
        //EditorGUILayout.HelpBox("Generate ra 1 nùi map từ start => end.", MessageType.Info);
        //GUILayout.Label("Start stage Id");
        //startMapID = GUILayout.TextField(startMapID, 20, GUILayout.Height(30));
        //GUILayout.Label("End stage Id");
        //endMapID = GUILayout.TextField(endMapID, 20, GUILayout.Height(30));

        //if (GUILayout.Button("Generator Maps", GUILayout.Height(30)))
        //{
        //    myScript.GenerateMaps(int.Parse(startMapID), int.Parse(endMapID));
        //}

        //GUILayout.Space(50);
        //if (GUILayout.Button("ReloadConfig", GUILayout.Height(30)))
        //{
        //    reloadConfig();
        //}

    }


//    private async void reloadConfig()
//    {
//        TilemapDrawingScript myScript = (TilemapDrawingScript)target;
//        //load GoogleSheetData
//        string configPath = "https://sheets.googleapis.com/v4/spreadsheets/16jpp9ph87pzqcsFLOcFLMQfbAXsEe-DoWPZbtctyRpc/values/Stage_Config!A2:O1000?key=AIzaSyArJaIzgcoaVJBAMhRPq3vqGJ5tjShICX0";
//        var stageValues = await GoogleSheetService.Instance.GetGoogleSheetValue(configPath);

//        Dictionary<int, StageRecords> stages = new Dictionary<int, StageRecords>();

//        Debug.Log("reload stage : " + stageValues.Count);
//        foreach (var value in stageValues)
//        {
//            var stageID = int.Parse(value[0]);
//            if (!stages.ContainsKey(stageID))
//            {
//                var stage = new StageRecords()
//                {
//                    ID = stageID,
//                    Type = int.Parse(value[1]),
//                    StageName = value[2],
//                    StageDescription = value[3],
//                    MapSize = int.Parse(value[4]),
//                    BattleTime = int.Parse(value[5]),
//                    Wall = int.Parse(value[6]),
//                    Object = int.Parse(value[7]),
//                    Enemy = value[8],
//                    TotalEnergy = int.Parse(value[9]),
//                    BossID = int.Parse(value[10]),
//                    BossHP = int.Parse(value[11]),
//                    MissionTarget = int.Parse(value[12]),
//                    Theme = (StageThemes)int.Parse(value[13]),
//                };
//                Debug.LogWarning("parse stage: " + stageID + "===" + stage.Theme);
//                stages.Add(stage.ID, stage);
//            }
//        }

//        StageData stageData = new StageData();
//        stageData.SetupDictionaryData(stages);
//        Debug.LogWarning("after load stage config: " + stageData.GetAllItems().Count);
//        //TODO: Need Refactor later
//        //write Byte
//        byte[] bytes = null;
//        bytes = Utility.Serialize<StageData>(stageData);
//        string destinationPath = "Assets/Boombergame/Resources/DataConfig/StageConfig.bytes";
//#if UNITY_EDITOR
//        //create File and Directory
//        if (destinationPath.Contains("/"))
//        {
//            string destinationFolder = destinationPath.Remove(destinationPath.LastIndexOf("/"));
//            if (!FileUtility.CreateDirectory(destinationFolder))
//            {
//                Debug.LogError(" can not create destination config folder : " + destinationFolder);
//            } // if

//        } // if
//        File.WriteAllBytes(destinationPath, bytes);
//        AssetDatabase.Refresh();
//#endif
//    }


    [MenuItem("MapHelper/ClearCached")]
    static void ClearCached()
    {
        Caching.ClearCache();
        PlayerPrefs.DeleteAll();
    }
    //// Export Map
    //[MenuItem("MapHelper/ExportMap")]
    //static void Export()
    //{
    //    var exportedPackageAssetList = new List<string>();

    //    //Add Prefabs folder into the asset list
    //    exportedPackageAssetList.Add("Assets/MapEditor");
    //    exportedPackageAssetList.Add("Assets/Boombergame/Art/Sprites/Tilesets");
    //    exportedPackageAssetList.Add("Assets/Boombergame/Resources/Map");
    //    exportedPackageAssetList.Add("Assets/Boombergame/Script/Objects/Tilemap/BrushTile.cs");
    //    exportedPackageAssetList.Add("Assets/Boombergame/Script/Data/Models/TileData.cs");
    //    exportedPackageAssetList.Add("Assets/Boombergame/Script/Data/Models/TileType.cs");
    //    exportedPackageAssetList.Add("ProjectSettings/TagManager.asset");
    //    exportedPackageAssetList.Add("ProjectSettings/ProjectSettings.asset");

    //    var currentTime = System.DateTime.Now.ToString("dd_MM_yyyy");
    //    //Export Shaders and Prefabs with their dependencies into a .unitypackage
    //    AssetDatabase.ExportPackage(exportedPackageAssetList.ToArray(), $"../MapExport/{currentTime}.unitypackage",
    //        ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies);
    //}

}

