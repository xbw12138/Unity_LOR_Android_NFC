using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour
{

    public AudioSource Sound;

    public void Play(string songs)
    {
        Sound.clip = (AudioClip)Resources.Load(songs,typeof(AudioClip));//调用Resources方法加载AudioClip资源
        Sound.Play();
    }

}