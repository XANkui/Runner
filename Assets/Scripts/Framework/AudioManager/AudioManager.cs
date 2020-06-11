using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音频管理类
/// </summary>
public class AudioManager : MonoSingleton<AudioManager>
{
    private AudioSource m_bgAudioSource;
    private AudioSource m_effectAudioSource;

    // 音频路径
    private const string ResourceDir = "Audio";

    protected override void Awake()
    {
        base.Awake();

        
        m_bgAudioSource = gameObject.AddComponent<AudioSource>();
        m_bgAudioSource.playOnAwake = false;
        m_bgAudioSource.loop = true;

        m_effectAudioSource = gameObject.AddComponent<AudioSource>();
    }

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="audioName"></param>
    public void PlayBG(string audioName) {
        string oldName;
        if (m_bgAudioSource.clip == null) {
            oldName = "";
        }
        else
        {

            oldName = m_bgAudioSource.clip.name;
        }

        if (oldName.Equals(audioName)==false) {

            string path = ResourceDir + "/" + audioName;

            AudioClip clip = Resources.Load<AudioClip>(path);

            if (clip != null) {
                m_bgAudioSource.clip = clip;
                m_bgAudioSource.Play();
            }
            
        }
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="audioName"></param>
    public void PlayEffect(string audioName) {
        string path = ResourceDir + "/" + audioName;

        AudioClip clip = Resources.Load<AudioClip>(path);

        if (clip != null)
        {
            m_effectAudioSource.PlayOneShot(clip);
        }
    }

}
