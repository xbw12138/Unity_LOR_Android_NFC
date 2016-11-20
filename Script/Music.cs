using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour
{

    private MusicPlayer music;
    public string[] songs;
    void Start()
    {
        music = (GetComponent("MusicPlayer") as MusicPlayer);//获取播放器对象
        if (quanju.songscho == 1)
        {
            int i = Random.Range(0, 10);
            quanju.song = i;
            music.Play(songs[i]);//调用播放器Play方法
        }
        else
        {
            music.Play(songs[quanju.song]);//调用播放器Play方法
        }
       
       
    }
   
}