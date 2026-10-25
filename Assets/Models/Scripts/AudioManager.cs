using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    private float _musicVolume;
    private float _sfxVolume;

    private float _tempSfxValue;
    private float _tempMusicValue;

    public void SetSfxVolume( float volume )
    {
        audioMixer.SetFloat( "SFXVolume", volume );
        _sfxVolume = volume;
    }
    
    public void SetMusicVolume( float volume )
    {
        audioMixer.SetFloat( "MusicVolume", volume );
        _musicVolume = volume;
    }

    public void MuteSfxVolume()
    {
        Debug.Log(_tempSfxValue);
        _ = audioMixer.GetFloat("SFXVolume", out _tempSfxValue);
        Debug.Log(_tempSfxValue);
        
        if (_tempSfxValue == -80) audioMixer.SetFloat("SFXVolume", _sfxVolume);
        else audioMixer.SetFloat("SFXVolume", -80);
    }
    
    public void MuteMusicVolume()
    {
        _ = audioMixer.GetFloat("MusicVolume", out _tempMusicValue);
        
        if (_tempMusicValue == -80) audioMixer.SetFloat("MusicVolume", _musicVolume);
        else audioMixer.SetFloat("MusicVolume", -80);
    }
}
