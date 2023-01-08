
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
#if UNITY_IOS
using UnityEngine.iOS;
#endif
#if UNITY_EDITOR
using UnityEditor;
#endif
using NBCore;

public enum DeviceScreenState
{
    Portrait,
    LandScape
}

public static class Utility
{


    public static string GetPlatformName()
    {
#if UNITY_EDITOR
        return GetPlatformForAssetBundles(EditorUserBuildSettings.activeBuildTarget);
#else
        return GetPlatformForAssetBundles(Application.platform);
#endif
    }

#if UNITY_EDITOR
    private static string GetPlatformForAssetBundles(BuildTarget target)
    {
        switch (target)
        {
            case BuildTarget.Android:
                return "Android";
            case BuildTarget.iOS:
                return "iOS";
            case BuildTarget.WebGL:
                return "WebGL";
            case BuildTarget.StandaloneWindows:
            case BuildTarget.StandaloneWindows64:
                return "Windows";
            case BuildTarget.StandaloneOSXIntel:
            case BuildTarget.StandaloneOSXIntel64:
                //        case BuildTarget.StandaloneOSX:
                return "OSX";
            // Add more build targets for your own.
            // If you add more targets, don't forget to add the same platforms to GetPlatformForAssetBundles(RuntimePlatform) function.
            default:
                return null;
        }
    }
#endif

    private static string GetPlatformForAssetBundles(RuntimePlatform platform)
    {
        switch (platform)
        {
            case RuntimePlatform.Android:
                return "Android";
            case RuntimePlatform.IPhonePlayer:
                return "iOS";
            case RuntimePlatform.WebGLPlayer:
                return "WebGL";
            case RuntimePlatform.WindowsPlayer:
                return "Windows";
            case RuntimePlatform.OSXPlayer:
                return "OSX";
            // Add more build targets for your own.
            // If you add more targets, don't forget to add the same platforms to GetPlatformForAssetBundles(RuntimePlatform) function.
            default:
                return null;
        }
    }


    public static DeviceScreenState GetDeviceScreenState()
    {
        if (Screen.height > Screen.width)
        {
            return DeviceScreenState.Portrait;
        } // if
        return DeviceScreenState.LandScape;
    } // 


    #region Serialize byte data
    public static byte[] Serialize<T>(T data)
    {
        byte[] bytes = null;
        var formatter = new BinaryFormatter();
        using (var stream = new MemoryStream())
        {
            formatter.Serialize(stream, data);
            bytes = stream.ToArray();
        } // using
        return bytes;
    }


    public static T Deserialize<T>(byte[] bytes)
    {
        var formatter = new BinaryFormatter();
        var data = default(T);
        using (var stream = new MemoryStream(bytes))
        {
            data = (T)formatter.Deserialize(stream);
        }
        return data;
    }

    #endregion

    //cmdev
    public static Vector3 ScreenToWorld(Camera camera, Vector3 position)
    {
        position.z = camera.nearClipPlane;
        return camera.ScreenToWorldPoint(position);
    }
    //cmdev

    public static T ParseStringToEnum<T>(string sEnum)
    {
        return (T)System.Enum.Parse(typeof(T), sEnum, true);
    }

    #region VIBRATE

#if UNITY_ANDROID
    static AndroidJavaClass unity;
    static AndroidJavaObject ca;
    static AndroidJavaObject vibratorService;
#endif
    public static void Vibrate(long miliSec)
    {
        Debug.Log("device vibrate");
#if UNITY_IPHONE
        Handheld.Vibrate();
        return;
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
        try {
            if (unity == null)
            {
                unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                ca = unity.GetStatic<AndroidJavaObject>("currentActivity");
                vibratorService = ca.Call<AndroidJavaObject>("getSystemService", "vibrator");
            }

            if (vibratorService != null) vibratorService.Call("vibrate", miliSec);
        } catch(Exception ex)
        {
            Debug.LogError(" Vibrate Exception : " + ex.ToString() +"==" + ex.StackTrace);
        }
#endif
    }

    #endregion

    public static bool OnMultiplyMoreLineMoreItem(float widthScrollview, int countData, float mItemWidthOrHeight, float target)
    {
        int mRowCount = (int)Math.Round(Math.Abs(widthScrollview / mItemWidthOrHeight));
        if (countData > 0)
        {
            int mColumnCount = (int)Math.Round((float)Math.Abs(countData / mRowCount));
            if (countData - (mColumnCount * mRowCount) >= 1)
                mColumnCount += 1;

            float lenghtItem = mColumnCount * mItemWidthOrHeight;

            if (lenghtItem > target)
                return true;
            else
                return false;
        }
        else
            return false;
    }

    public static bool OnMultiplyOneLineMoreItem(int countData, float mItemWidthOrHeight, float target)
    {
        if (countData > 0)
        {
            float lenghtItem = countData * mItemWidthOrHeight;

            if (lenghtItem > target)
                return true;
            else
                return false;
        }
        else
            return false;
    }

    public static Transform FindDeepChild(GameObject _target, string _childName)
    {
        if (_target.name == _childName)
        {
            return _target.transform;
        }
        Transform resultTrs = null;
        resultTrs = _target.transform.Find(_childName);
        if (resultTrs == null)
        {
            foreach (Transform trs in _target.transform)
            {
                resultTrs = Utility.FindDeepChild(trs.gameObject, _childName);
                if (resultTrs != null)
                    return resultTrs;
            }
        }
        return resultTrs;
    }

    public static T FindDeepChild<T>(GameObject _target, string _childName) where T : Component
    {
        Transform resultTrs = Utility.FindDeepChild(_target, _childName);
        if (resultTrs != null)
            return resultTrs.gameObject.GetComponent<T>();
        return (T)((object)null);
    }

    public static string GetUDID()
    {
        var result = string.Empty;
#if UNITY_IOS
        result = Device.vendorIdentifier;
#else
        result = SystemInfo.deviceUniqueIdentifier;
#endif
        return result;
    }


    public static bool CheckIsUrlFormat(string strValue)
    {
        return Utility.CheckIsFormat("(http://)?([\\w-]+\\.)+[\\w-]+(/[\\w- ./?%&=]*)?", strValue);
    }

    public static bool CheckIsFormat(string strRegex, string strValue)
    {
        if (strValue != null && strValue.Trim() != string.Empty)
        {
            Regex regex = new Regex(strRegex);
            return regex.IsMatch(strValue);
        }
        return false;
    }

    //Converter

    public static Sprite Texture2DToSprite(Texture2D texture)
    {
        var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        return sprite;
    }


    public static string GetSubItemID(string id, int index = 0, int subLenght = 6)
    {
        string value = "";

        if (id.Length >= subLenght)
            value = id.Substring(index, subLenght);

        return value;
    }

    public static int ConvertTypeToConfig(int type)
    {
        int value = 31;
        switch (type)
        {
            case 1:
                value = 32;
                break;
            case 2:
                value = 33;
                break;
            case 3:
                value = 34;
                break;
            case 4:
                value = 35;
                break;
        }

        return value;
    }

    public static Dictionary<T, U> AddRangeDictionary<T, U>(this Dictionary<T, U> destination, Dictionary<T, U> source)
    {
        if (destination == null) destination = new Dictionary<T, U>();
        foreach (var e in source)
            destination.Add(e.Key, e.Value);
        return destination;
    }
}
