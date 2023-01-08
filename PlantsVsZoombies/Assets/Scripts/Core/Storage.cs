using System.Collections;
using System.Collections.Generic;
using NBCore;
using UnityEngine;

public class Storage : Singleton<Storage>
{
    public void SaveInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }
    public int GetInt(string key)
    {
        return PlayerPrefs.GetInt(key);
    }

    public void SaveFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }
    public float GetFloat(string key)
    {
        return PlayerPrefs.GetFloat(key);
    }

    public void SaveString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }
    public string GetString(string key)
    {
        return PlayerPrefs.GetString(key);
    }
}
