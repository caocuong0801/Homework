using System;
using UnityEngine;
using Zenject;
using NBCore;

public partial class ZGameManager : SingletonMono<ZGameManager>
{
    private const string STAGE_ID_KEY = "STAGE_ID";

    [Inject] private GameSystem gameSystem;
    [Inject] private MapSystem mapSystem;
    [Inject] private PlantSystem plantSystem;
    [Inject] private SunSystem sunSystem;
    [Inject] private ZoombieSystem zoombieSystem;
    [Inject] private SoundSystem soundSystem;

    private void Start()
    {
        var stageId = Storage.Instance.GetInt(STAGE_ID_KEY);
        if (stageId == 0) stageId = 1;

        //await ConfigManager.Instance.LoadStageData(stageId);

        gameSystem?.InitData();
        mapSystem?.InitData();
        plantSystem?.InitData();
        sunSystem?.InitData();
        zoombieSystem?.InitData();

        GUIManager.Instance.OnAddedPlant += handleAddPlant;

        soundSystem.PlayMusic(MusicName.Background_Music);
    }


    private void FixedUpdate()
    {
        gameSystem?.Tick();
        mapSystem?.Tick();
        plantSystem?.Tick();
        sunSystem?.Tick();
        zoombieSystem?.Tick();
    }
}
