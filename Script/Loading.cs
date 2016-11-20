using UnityEngine;
using System.Collections;

public class Loading : MonoBehaviour { 
    public float timeElasped=0;
    public int curFrame=0;
    public float fps=10;
    public Texture2D[] ani;
    public Texture2D img;
    public float loading = 0f;
    public GUISkin GUIskin4;
    public GUISkin GUIskin2;
    public Texture2D load1;
    public Texture2D load2;
    //public GUITexture[] ani2; 
    void Update () {

        timeElasped += Time.deltaTime;
        if(timeElasped >= 1.0 / fps)
        {
                timeElasped = 0;
                curFrame++;
                if(curFrame >= ani.Length)
                 {
                        curFrame = 0;
                    }
               // guiTexture.texture = ani[curFrame];
         }


        if (loading >= 100)
            Application.LoadLevel("GAME1");
        else
            loading += Time.deltaTime * 10;
    }
    void OnGUI()
       {
           GUIStyle backGround = new GUIStyle();
           backGround.normal.background = img;
           GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "", backGround);
           
           if (loading <= 100)
           {
               float blood_width = Screen.width * 0.74f * loading / 100;
               GUI.DrawTexture(new Rect(blood_width, Screen.height * 0.7f, Screen.width * 0.12f, Screen.height * 0.13f), ani[curFrame]);
               //进度条底纹
               GUI.DrawTexture(new Rect(Screen.width * 0.1f, Screen.height * 0.8f, Screen.width * 0.8f, Screen.height * 0.2f),load1);
               //进度条
               GUI.DrawTexture(new Rect(Screen.width * 0.14f, Screen.height * 0.83f, blood_width, Screen.height * 0.15f), load2);
           }

           GUI.skin = GUIskin4;

           GUI.Label(new Rect(Screen.width * 0.3f, Screen.height * 0.89f, Screen.width * 0.6f, Screen.height * 0.12f), "游戏加载中....." + (int)loading);

           GUI.skin = GUIskin4;
        if(GUI.Button(new Rect(Screen.width*0.25f,Screen.height*0.3f,Screen.width*0.5f,Screen.height*0.5f),"游戏规则：\n当红色箭头出现，则感应与箭头相反方向；\n当蓝色箭头出现，则感应相同方向；\n当灰色箭头出现，则感应与出现位置相反方向。"))
        {

        }
          // GUI.Label(new Rect(Screen.width * 0.72f, Screen.height * 0.86f, Screen.width * 0.1f, Screen.height * 0.12f), (int)loading + "");
       }
}
