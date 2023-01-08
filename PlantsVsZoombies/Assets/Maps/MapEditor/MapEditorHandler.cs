using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using Newtonsoft.Json;

#if UNITY_EDITOR

public class MapEditorHandler : MonoBehaviour
{
    const string DEFAULT_MAP_NAME = "DefaultMap";
    const string PACKAGE_SUFFIX = ".prefab";
    const string SAVING_PATH = "Assets/Maps/MapData/";
    const string JSON_SAVING_PATH = "Assets/Maps/MapData/";

    private string oldMapName = DEFAULT_MAP_NAME;
    // private String openMapName;
    private String savedMapNames;

    public Editor CurrentEditor;
    [TextArea]
    public String savingName;

    public string GetSavedMapNames()
    {

        var info = new DirectoryInfo(SAVING_PATH);

        var filesInfo = info.GetFiles();
        var current = "";
        foreach (var file in filesInfo)
        {
            if (file.Name.EndsWith(".prefab"))
            {
                current += file.Name + ";";
            }
        }
        return current;

        //var current = "";
        //var logPath = SAVING_PATH + "savedMap.txt";
        //if (File.Exists(logPath)) {
        //    current = File.ReadAllText(logPath);
        //}
        //return current;
    }

    public void Initialized()
    {
        savedMapNames = GetSavedMapNames();
    }

    public void OpenMap(string openMapName)
    {
        if (string.IsNullOrEmpty(openMapName))
        {
            openMapName = DEFAULT_MAP_NAME;
        }
        Debug.Log("Opening name = " + openMapName);
        GameObject mf = GameObject.Find(openMapName);
        Debug.Log("Check map is opening");
        oldMapName = "";
        if (mf)
        {
            Debug.Log("Map is opening");
            oldMapName = openMapName;
            EditorUtility.DisplayDialog("", "Tilemap [" + openMapName + "] đang được mở?", "OK");
            return;
        }
        var activeScene = SceneManager.GetActiveScene();
        if (activeScene != null)
        {
            var savedPath = SAVING_PATH + openMapName + PACKAGE_SUFFIX;
            try
            {

                Debug.Log("prefab path : " + savedPath);
                GameObject mapPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(savedPath, typeof(GameObject));
                var newObject = (GameObject)PrefabUtility.InstantiatePrefab(mapPrefab);
                newObject.transform.SetParent(transform);
                newObject.name = openMapName;
                savingName = openMapName;
                GameObject oldMap = null;
                try
                {
                    oldMap = GameObject.Find(oldMapName);
                    if (oldMap != null)
                    {
                        DestroyImmediate(oldMap);
                    }
                }
                catch (Exception e)
                {
                    Debug.Log("Old map is not existed");
                }
                oldMapName = openMapName;
                Debug.Log("Delete old map === " + oldMapName);
            }
            catch (Exception e)
            {
                Debug.Log("Can not open map = " + openMapName);
            }
        }
    }


    public void OpenDefaultMap()
    {
        OpenMap(DEFAULT_MAP_NAME);
    }

    public void SaveMap()
    {
        if (string.IsNullOrEmpty(savingName))
        {
            EditorUtility.DisplayDialog("Lỗi", "Vui long nhập tên cho file được lưu vào ô [Saving Name].", "OK");
        }
        else
        {
            Debug.Log("Saved map = " + oldMapName);
            var mf = GameObject.Find(oldMapName);
            if (mf == null)
            {
                EditorUtility.DisplayDialog("Lỗi", "Không tìm thấy map để lưu :D", "OK");
                return;
            }
            var mapSavingPath = SAVING_PATH + savingName + PACKAGE_SUFFIX;
            var exists = false;
            try
            {
                GameObject mapPrefab = PrefabUtility.LoadPrefabContents(mapSavingPath);
                if (mapPrefab != null)
                {
                    exists = true;
                }
            }
            catch (System.Exception e)
            {
                exists = false;
            }
            if (exists)
            {

                //EditorUtility.DisplayDialog("Cảnh báo", "Map có tên giống vầy đã tồn tại. Bạn hãy chọn một tên khác để lưu nhé", "OK");
                var select = EditorUtility.DisplayDialogComplex("Cảnh báo", "Map có tên giống vầy đã tồn tại. Bạn xác nhận ghi đè?", "Vẫn ghi", "Huỷ", "");
                if (select == 0)
                {
                    bool success = false;
                    PrefabUtility.SaveAsPrefabAsset(mf, mapSavingPath, out success);
                    Debug.LogWarning("is success : " + success);
                }
                //EditorUtility.DisplayDialog("Cảnh báo", "Map có tên giống vầy đã tồn tại. Bạn hãy chọn một tên khác để lưu nhé", "OK");
                return;
            }
            else
            {
                if (PrefabUtility.CreatePrefab(mapSavingPath, mf))
                {
                    EditorUtility.DisplayDialog("Xong", "Tilemap đã được lưu thành công vào " + mapSavingPath, "OK");
                    updateSavingInformation(savingName);
                }
                else
                {
                    EditorUtility.DisplayDialog("Lỗi", "Không thể lưu được map. Vui lòng thử lại", "OK");
                }
            }
        }
    }

    private void updateSavingInformation(string savingName)
    {
        var current = "";
        var existFile = false;
        var logPath = SAVING_PATH + "savedMap.txt";
        if (File.Exists(logPath))
        {
            current = File.ReadAllText(logPath);
            existFile = true;
        }
        current += (existFile ? ";" : "") + savingName;

        if (CurrentEditor != null)
        {
            CurrentEditor.Repaint();
        }
        File.WriteAllText(logPath, current);
        OpenMap(savingName);
    }


    //public async void GenerateMap(int mapID)
    //{
    //    await BConfigManager.Instance.Init();
    //    var ctrl = getMapHandler();
    //    if (ctrl != null)
    //    {
    //        await ctrl.ClearMap();
    //        var stageData = BConfigManager.Instance.StageData.GetItemByID(mapID);
    //        ctrl.GenerateMap(mapID, stageData.Theme);
    //    }
    //}

    //public async void GenerateMaps(int startMapId, int endMapId)
    //{
    //    await BConfigManager.Instance.Init();
    //    var ctrl = getMapHandler();
    //    if (ctrl.gameObject.name == "pve")
    //    {
    //        if (startMapId < endMapId)
    //        {
    //            for (int i = startMapId; i <= endMapId; i++)
    //            {
    //                var mapData = TileGenerator.BuildMapData(i, out int rowCount, out int ColCount);
    //                if (rowCount == 0 || ColCount == 0) continue;
    //                var dataPath = JSON_SAVING_PATH + "" + i + ".txt";
    //                var savingData = JsonConvert.SerializeObject(mapData);
    //                File.WriteAllText(dataPath, savingData);
    //            }
    //        }
    //    }
    //    AssetDatabase.Refresh();
    //}


    //public async void LoadMapDataFromJson(string stageID)
    //{
    //    await BConfigManager.Instance.Init();
    //    var ctrl = getMapHandler();
    //    if (ctrl != null)
    //    {
    //        await ctrl.ClearMap();
    //        var mapdata = await BUtil.GetMapDataFromConfig(stageID);
    //        //build tile Data
    //        ctrl.BuildTileData(true);
    //        var stageData = BConfigManager.Instance.StageData.GetItemByID(int.Parse(stageID));
    //        ctrl.BuildColliderMapFromData(mapdata, stageData.Theme, true);
    //    }
    //}

    public void ExportToJson(string mapID)
    {
        var ctrl = getMapHandler();
        if (ctrl != null)
        {
            var result = ctrl.ExportToJson();
            Debug.Log($"Exported =============== {result}");
            var dataPath = JSON_SAVING_PATH + "mapData.txt";
            if (ctrl.gameObject.name == "pve")
            {
                if (string.IsNullOrEmpty(mapID))
                {
                    throw new Exception("mapID can not empty when export pve map");
                }
                dataPath = JSON_SAVING_PATH + "" + mapID + ".txt";
            }
            File.WriteAllText(dataPath, result);
            AssetDatabase.Refresh();
        }
    }

    private MapHandler getMapHandler()
    {
        var mf = GameObject.Find(oldMapName);
        if (mf == null && transform.childCount > 0)
        {
            mf = transform.GetChild(0).gameObject;
        }
        if (mf)
        {
            Debug.Log($"Old map name  ----- {oldMapName}");
            var ctrl = mf.GetComponent<MapHandler>();
            if (ctrl) return ctrl;

            ctrl = mf.GetComponentInParent<MapHandler>();
            if (ctrl) return ctrl;

            ctrl = mf.GetComponentInChildren<MapHandler>();
            if (ctrl) return ctrl;
        }
        return null;
    }


    //public void CopyObjectsToTheRight()
    //{
    //    var ctrl = getMapHandler();
    //    if (ctrl != null)
    //    {
    //        Debug.Log("Copy object to the right ----- ");
    //        ctrl.CopyObjectsToTheRight();
    //    }
    //}

    //public void DeleteObjectsOnTheRight()
    //{
    //    var ctrl = getMapHandler();
    //    if (ctrl != null)
    //    {
    //        Debug.Log("Delete right objects ----- ");
    //        ctrl.DeleteRightObjects();
    //    }
    //}

    //public void CopyObjectsToTheTop()
    //{
    //    var ctrl = getMapHandler();
    //    if (ctrl != null)
    //    {
    //        Debug.Log("Copy object to the top ----- ");
    //        ctrl.CopyObjectsToTheTop();
    //    }
    //}

    //public void DeleteObjectsOnTheTop()
    //{
    //    var ctrl = getMapHandler();
    //    if (ctrl != null)
    //    {
    //        Debug.Log("Copy object to the top ----- ");
    //        ctrl.DeleteTopObjects();
    //    }
    //}

}

#endif