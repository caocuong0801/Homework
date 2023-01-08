using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NBCore;
using System.IO;
using System.Threading.Tasks;
using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

//using Cysharp.Threading.Tasks;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameResource
{
    /// <summary>
    /// Wrapper for Resources loading
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public static async Task<T> LoadAssetFromResources<T>(string path) where T : UnityEngine.Object
    {
        Debug.Log($"Load Asset from resource {path}");

        try
        {
            ResourceRequest request = Resources.LoadAsync<T>(path);
            while (!request.isDone)
            {
                await Task.Delay(100);
            }
            return (T)request.asset;

        }
        catch (Exception ex)
        {
            Debug.LogError("Can not find asset : " + path + ex.ToString());
            return default(T);
        } // catch
    }

    /// <summary>
    /// Load Asset by Addressable 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public static async Task<T> LoadAssetAsync<T>(string path) where T : UnityEngine.Object
    {
        Debug.Log($"Load Asset {path}");

        try
        {
            AsyncOperationHandle<T> download = Addressables.LoadAssetAsync<T>(path);

            T result = await download.Task;
            return result;

            //return await Resources.LoadAsync<T>(path) as T;

        }
        catch (Exception ex)
        {
            Debug.LogError("Can not find asset : " + path + ex.ToString());
            //return await Resources.LoadAsync<T>(path);
            return null;
        } // catch
    } // LoadAssetAsync ()

}
