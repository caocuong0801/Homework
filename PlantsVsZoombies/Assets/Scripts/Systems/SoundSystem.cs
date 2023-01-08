using Hellmade.Sound;
using NBCore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem
{
    private const string SOUND_RESOURCES_PATH = "Sounds/";

    public float maxVolumeMusic = 1f;
    public float maxVolumeSound = 1f;

    private Dictionary<string, AudioClip> dicAudios = new Dictionary<string, AudioClip>();

    private Audio curmIDPlayer;
    private MusicName curMusicPlay;
    private float curVolumePlay;

    [HideInInspector] public MusicName CurMusicPlay { set { curMusicPlay = value; } }

    #region PLAY MUSIC

    public void PlayMusic(MusicName musicName, float volume = default)
    {
        if (volume == default) volume = maxVolumeMusic;

        if (curMusicPlay == musicName && curVolumePlay == volume) return;

        var clip = LoadAudio(musicName.ToString());
        if (clip == null) return;

        int id = EazySoundManager.PlayMusic(clip, volume, true, false, 1f, 1f);
        curmIDPlayer = EazySoundManager.GetSoundAudio(id);
        curMusicPlay = musicName;
        curVolumePlay = volume;

    }

    public void StopAllMusic()
    {
        CurMusicPlay = MusicName.None;
        EazySoundManager.StopAllMusic();
    }

    public void PauseAllMusic()
    {
        EazySoundManager.PauseAllMusic();
    }

    public void ResumeAllMusic()
    {
        EazySoundManager.ResumeAllMusic();
    }

    public void StopMusicByID(int idAudio)
    {
        var audio = EazySoundManager.GetMusicAudio(idAudio);
        if (audio != null) audio.Stop();
    }

    public void PauseMusicByID(int idAudio)
    {
        var audio = EazySoundManager.GetMusicAudio(idAudio);
        if (audio != null) audio.Pause();
    }

    public void ResumeMusicByID(int idAudio)
    {
        var audio = EazySoundManager.GetMusicAudio(idAudio);
        if (audio != null) audio.Resume();
    }

    #endregion

    #region PLAY SOUND

    public int PlaySound(SoundName soundName, bool loop = false, Transform transform = null, float volume = default)
    {
        if (volume == default) volume = maxVolumeSound;

        var clip = LoadAudio(soundName.ToString());
        if (clip == null) return -1;

        int id = EazySoundManager.PlaySound(clip, volume, loop, transform);

        return id;
    }

    public AudioClip GetAudioClipByName(SoundName soundName)
    {
        var clip = LoadAudio(soundName.ToString());
        if (clip == null) return null;

        return clip;
    }

    public void StopAllSound()
    {
        EazySoundManager.StopAllSounds();
    }

    public void PauseAllSound()
    {
        EazySoundManager.PauseAllSounds();
    }

    public void ResumeAllSound()
    {
        EazySoundManager.ResumeAllSounds();
    }

    public void StopSound(int idAudio)
    {
        var audio = EazySoundManager.GetSoundAudio(idAudio);
        if (audio != null) audio.Stop();
    }

    public void PauseSound(int idAudio)
    {
        var audio = EazySoundManager.GetSoundAudio(idAudio);
        if (audio != null) audio.Pause();
    }

    public void ResumeSound(int idAudio)
    {
        var audio = EazySoundManager.GetSoundAudio(idAudio);
        if (audio != null) audio.Resume();
    }

    #endregion

    private AudioClip LoadAudio(string soundName)
    {
        if (dicAudios.ContainsKey(soundName)) return dicAudios[soundName];

        string path = SOUND_RESOURCES_PATH + soundName;
        var audio = Resources.Load<AudioClip>(path);

        if (audio == null)
        {
            Debug.LogError("Could not load Audio at: " + path);
            return null;
        }

        dicAudios.Add(soundName, audio);

        return audio;
    }
}

public enum MusicName
{
    None,
    Background_Music,
}


public enum SoundName
{
    //10/10/2021
    coin,
    bleep,
    butter,
    balloon_pop,
    basketball,
}


