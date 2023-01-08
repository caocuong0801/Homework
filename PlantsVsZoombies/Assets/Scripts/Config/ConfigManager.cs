using UnityEngine;
using NBCore;
using System.Threading.Tasks;

public class ConfigManager : Singleton<ConfigManager>
{
    //=== App Game
    public StageData stageData { get; set; }


    public async Task LoadStageData(int stageId)
    {
        stageData = await loadConfig<StageData>("DataConfig/Stage_" + stageId);
    } // Init ()


    #region CONFIG UTILS

    private async Task<T> loadConfig<T>(string path)
    {
        var bytes = (await GameResource.LoadAssetFromResources<TextAsset>(path)).bytes;

        if (bytes != null)
        {
            T result = Utility.Deserialize<T>(bytes);
            Debug.Log("load config finish : " + path);
            return result;
        } // if
        return default(T);
    }

    #endregion
}
