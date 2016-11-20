/////////////////////////////////////////////////////////////
//此菜单的思路
//
/////////////////////////////////////////////////////////////
//进入游戏，游戏介绍，退出游戏没什么特别的，只是用了按钮事件，
//来说一下顶部的菜单栏，这个呢可以点击右箭头使几个按钮显示出来，类似抽屉一样
//实现思路：点击右箭头，向右依次显示出几个按钮，同时右箭头变为左箭头
//右上角的声音控制，点一下为整个游戏静音，这是两张声音UI
//一张有音量的，一张没有音量的，在设置中，当音乐与音效都不为关闭时，右上角显示有音量图，
//反之，显示没有音量的图；
//点击设置以及提示按钮，弹出菜单凸显出来，背景变暗，背景上的按钮失效
//实现思路：当点击设置按钮时，我们在GUI函数最顶端显示一张全屏的按钮，同时使用皮肤，
//皮肤的按钮按下与悬停的图案删除，这样我们点击时就不会闪烁了，同时这个按钮没有命令
//发现GUI的覆盖关系有点乱，可能是自己不够理解，之前把全屏按钮写在了GUI函数的末尾，
//结果背景按钮仍然起作用，后来移到最顶端反而使背景按钮失效，不知道谁遮挡了谁；
//设置菜单里的声音的开关也是，什么时候显示off跟on很清楚了；
//再就是这些音乐与音效的分离，
//实现思路：音效呢就是点击的声音片段，我用了AudioClip，
//然后初始化了AudioSource控件，定义一个音效的控件
//public AudioSource music；这个music用来控制音效的，
//因为两个音效是用声音片段AudioClip定义的，我并没有查到他的播放与暂停，
//所以定义了一个声音资源，来总的控制，
//music.PlayOneShot(beep);这样点击发出音效；
//当music.volume = 0;时，音效声音为0，
//实现了音效的关闭，
//而音乐呢，直接拖到了AudioSource的AudioClip上，
//这样呢，当audio.Stop();时，背景音乐停止，放弃了之前的GetComponent<AudioSource>().mute = true;
//这样已静音音乐音效都不响了，没法区分开了；
//这次呢，还发现了用皮肤使按钮透明，之前一直用的办法是用贴图遮挡住按钮，每次调整按钮与贴图的位置真是费劲；
//现在好了，把按钮放到合适的位置，不至于完好对齐，用皮肤使按钮透明就好了；
//////////////////////////////////////////////////////////////
using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
    public AudioClip beep;
    public AudioClip beep1;
    public AudioSource music = null;

    public int open = 1;
    public int kai = 1;
    public int kai1 = 1;
    public int kai2 = 1;
    public int kai3 = 1;
    public int kai4 = 1;
    public float sx;
    public float sy;
    public int kai5 = 1;
    public GUISkin GUIskin;
    public GUISkin GUIskin1;
    public GUISkin GUIskin2;
    public GUISkin GUIskin4;

    public Texture2D img;
    public Texture2D bt1;
    public Texture2D bt2;
    public Texture2D bt3;

    public Texture2D p1;
    public Texture2D p2;

    public Texture2D p3;
    public Texture2D p4;
    public Texture2D p5;
    public Texture2D p6;
    public Texture2D p7;
    public Texture2D p8;//问号提示
    public Texture2D p9;//设置
    public Texture2D p10;//音效开关
    public Texture2D p11;//触摸脚印
    public Texture2D storypic;//商店
    public Texture2D yifupic;//yifu
    public Texture2D jinbi;//金币
    public Texture2D zuanshi;//钻石
    public Texture2D[] cloth;
    public Texture2D cloud;//白云
    public Texture2D playname;//标题
    public Texture2D bdf;
    public Texture2D toutoukan;
    //NFCtishi
    public Texture2D p12;
    public Texture2D p13;
    public Texture2D p14;
    //退出
    public Texture2D quitpic;
    public int NFC = 1;
    //音乐
    public Texture2D musicpic;
    public Texture2D musicchoose;
    public int Musicopp = 1;
    public string[] songsss;
    private MusicPlayer musics;


    public bool clo = true;
    //NFC写入成功返回值
    public string reNFC1 = "Y";
    public int NFC2 = 1;
    //NFC感应键位
    private string name = "P";
    private string name1 = "R";

    //白云飘动
    private float loading = 0.0f;
    private float loading1 = 0.0f;
    private int judge = 1;
    private int judge1 = 1;
    //退出游戏
    private int quit1 = 1;
    //模式选择
    private int chose = 1;
    public Texture2D putong;
    public Texture2D teshu;
    //商店开关
    public int shangdianopen = 1;
    //衣服选择
    private int choose1 = 0;
    private int choose2 = 0;
    private int index = 0;
    //增加金币
    private int jinbiopen = 1;
    //增加钻石
    private int zuanshiopen = 1;
    //输入
    public string FromText = "";
    private int number;
    //音乐循环
    private int xunhuan;
    private int xunhuancho1 = 1;
    private int xunhuancho2 = 1;
   
    // Use this for initialization
    void Start()
    {
        music = gameObject.AddComponent<AudioSource>();
        clo = true;
        Time.timeScale = 1;

        musics = (GetComponent("MusicPlayer") as MusicPlayer);//获取播放器对象
        xunhuan = Random.Range(0, 10);
        musics.Play(songsss[xunhuan]);//调用播放器Play方法 
    }
    // Update is called once per frame
    void Update()
    {
        //点击手机返回键关闭应用程序
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Home))
        {
            quit1 = 0;
        }
        //衣服选择
        if (choose1 == 1)
        {
            index += 1;
            if (index >= cloth.Length)
            {
                index = 0;
            }
           
            choose1 = 0;
        }
        if (choose2 == 1)
        {
            index -= 1;

            if (index < 0)
            {
                index = cloth.Length - 1;
            }
           
            choose2 = 0;
        }
        //音乐选择
        if (xunhuancho1 == 1)
        {
            xunhuan += 1;
            if (xunhuan >= songsss.Length)
            {
                xunhuan = 0;
            }
            musics.Play(songsss[xunhuan]);//调用播放器Play方法 

            xunhuancho1 = 0;
        }
        if (xunhuancho2 == 1)
        {
            xunhuan -= 1;
            if (xunhuan < 0)
            {
                xunhuan = songsss.Length-1;
            }
            musics.Play(songsss[xunhuan]);//调用播放器Play方法 
            xunhuancho2 = 0;

        }
        //获取触摸位置屏幕坐标
        if (Input.GetMouseButtonDown(0) == true)
        {
            sx = Input.mousePosition.x;
            sy = Input.mousePosition.y;
            kai5 = 0;
        }


        //白云飘动
        if(judge==1)
        {
            loading += Time.deltaTime*20;
            //loading1 += Time.deltaTime * 50;
            if(loading>=100)
            {
                judge = 0;
            }
        }
        else
        {
            loading -= Time.deltaTime*20;
            //loading1 -= Time.deltaTime * 50;
            if(loading<=0)
            {
                judge = 1;
            }
        }
        //人头
        if (judge1 == 1)
        {
            //loading += Time.deltaTime * 20;
            loading1 += Time.deltaTime * 80;
            if (loading1 >= 100)
            {
                judge1 = 0;
            }
        }
        else
        {
            //loading -= Time.deltaTime * 20;
            loading1 -= Time.deltaTime * 80;
            if (loading1 <= 0)
            {
                judge1 = 1;
            }
        }
        //for (var i = 0; i < Input.touchCount; ++i)
        //{
        //    if (Input.GetTouch(i).phase == TouchPhase.Began)
        //    {
        //        sx = Input.GetTouch(i).position.x;
        //        sy = Input.GetTouch(i).position.y;
        //        kai5 = 0;
        //    }
        //}

    }
    void OnGUI()
    {
        

        GUIStyle backGround = new GUIStyle();
        backGround.normal.background = img;
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "", backGround);

        GUI.DrawTexture(new Rect(Screen.width * 0.6f + loading , 0, Screen.width * 0.3f, Screen.height * 0.2f), cloud);
        GUI.DrawTexture(new Rect(Screen.width * 0.2f - loading, Screen.height * 0.1f, Screen.width * 0.4f, Screen.height * 0.25f), cloud);
        GUI.DrawTexture(new Rect(Screen.width * 0.4f + loading, Screen.height * 0.05f, Screen.width * 0.35f, Screen.height * 0.2f), cloud);

        GUI.DrawTexture(new Rect(Screen.width * 0.39f, Screen.height * 0.24f - loading1/2, Screen.width * 0.2f, Screen.height * 0.2f), toutoukan);
        GUI.DrawTexture(new Rect(Screen.width * 0.2f, Screen.height * 0.2f, Screen.width * 0.5f, Screen.height * 0.5f), playname);
   
        if(shangdianopen==0)
        {
            GUI.skin = GUIskin;
            if (GUI.Button(new Rect(Screen.width * 0.1f, Screen.height * 0.45f, Screen.width * 0.12f, Screen.height * 0.18f), ""))
            {
                //左
                choose2 = 1;
            }
            if (GUI.Button(new Rect(Screen.width * 0.78f, Screen.height * 0.45f, Screen.width * 0.12f, Screen.height * 0.18f), ""))
            {
                //右
                choose1 = 1;
            }
            if (GUI.Button(new Rect(Screen.width * 0.28f, Screen.height * 0.75f, Screen.width * 0.1f, Screen.height * 0.14f), ""))
            {
                shangdianopen = 1;
            }
            if (GUI.Button(new Rect(Screen.width * 0.6f, Screen.height * 0.75f, Screen.width * 0.1f, Screen.height * 0.14f), ""))
            {
                quanju.cloth = index;
                PlayerPrefs.SetInt("cloth", quanju.cloth);
                shangdianopen = 1;
            }
            if (GUI.Button(new Rect(Screen.width * 0.34f, Screen.height * 0.28f, Screen.width * 0.08f, Screen.height * 0.1f), ""))
            {
                //左
                jinbiopen = 0;
            }
            if (GUI.Button(new Rect(Screen.width * 0.78f, Screen.height * 0.28f, Screen.width * 0.08f, Screen.height * 0.1f), ""))
            {
                //左
                zuanshiopen = 0;
            }

            GUI.skin = GUIskin1;
            if (GUI.Button(new Rect(0, 0, Screen.width, Screen.height), ""))
            {
            }
        }

        if (quit1 == 0)
        {
            GUI.skin = GUIskin;
            if (GUI.Button(new Rect(Screen.width * 0.37f, Screen.height * 0.53f, Screen.width * 0.1f, Screen.height * 0.18f), ""))
            {
                //取消退出游戏
                quit1 = 1;
            }
            if (GUI.Button(new Rect(Screen.width * 0.52f, Screen.height * 0.53f, Screen.width * 0.1f, Screen.height * 0.18f), ""))
            {
                //确认退出游戏
                Application.Quit();
            }
            
            GUI.skin = GUIskin1;
            if (GUI.Button(new Rect(0, 0, Screen.width, Screen.height), ""))
            {
            }
        }
        //GUIStyle go = new GUIStyle();

        //go.fontSize = 35;

        //go.normal.textColor = new Color(255, 255, 255);

        if (reNFC1 == "N")
        {
            if (clo)
            {
                //GUI.skin = GUIskin;
                if (GUI.Button(new Rect(Screen.width * 0.36f, Screen.height * 0.6f, Screen.width * 0.24f, Screen.height * 0.13f), ""))//点击确定进入游戏NFC为读取模式，进入游戏
                {
                    music.PlayOneShot(beep1);
                    using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                    {
                        using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
                        {
                            //调用Android插件中UnityTestActivity中StartActivity0方法，stringToEdit表示它的参数
                            jo.Call("StartActivity0", name1);
                        }
                    }
                    //Application.LoadLevel("Loading");
                    NFC2 = 0;
                    clo = false;
                    reNFC1 = "Y";
                }
                if (GUI.Button(new Rect(Screen.width * 0.73f, Screen.height * 0.24f, Screen.width * 0.1f, Screen.height * 0.1f), ""))
                {
                    music.PlayOneShot(beep1);
                    reNFC1 = "N";
                }
                GUI.skin = GUIskin1;
                if (GUI.Button(new Rect(0, 0, Screen.width, Screen.height), ""))
                {

                }
            }
            else
            {
                //GUI.skin = GUIskin;
                if (GUI.Button(new Rect(Screen.width * 0.36f, Screen.height * 0.6f, Screen.width * 0.24f, Screen.height * 0.13f), ""))//点击确定进入游戏NFC为读取模式，进入游戏
                {
                    music.PlayOneShot(beep1);
                    using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                    {
                        using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
                        {
                            //调用Android插件中UnityTestActivity中StartActivity0方法，stringToEdit表示它的参数
                            jo.Call("StartActivity1", name);
                        }
                    }
                    Application.LoadLevel("Loading");
                }
                if (GUI.Button(new Rect(Screen.width * 0.73f, Screen.height * 0.24f, Screen.width * 0.1f, Screen.height * 0.1f), ""))
                {
                    music.PlayOneShot(beep1);
                    reNFC1 = "Y";
                }
                GUI.skin = GUIskin1;
                if (GUI.Button(new Rect(0, 0, Screen.width, Screen.height), ""))
                {

                }
            }
        }

        if (NFC2 == 0)
        {
            GUI.skin = GUIskin1;
            if (GUI.Button(new Rect(0, 0, Screen.width, Screen.height), ""))
            {

            }
            NFC = 1;
        }
        ////////////////////////////////////////////////////////////////////////////////////////
        //音乐选择
        GUI.skin = GUIskin;
        if (GUI.Button(new Rect(Screen.width - Screen.width * 0.1f, Screen.height - Screen.height * 0.12f, Screen.width * 0.1f, Screen.height * 0.12f), ""))
        {
            Musicopp = 0;
        }

        if(Musicopp==0)
        {
            GUI.skin = GUIskin;
            GUI.DrawTexture(new Rect(Screen.width * 0.25f, Screen.height * 0.15f, Screen.width * 0.5f, Screen.height * 0.5f), musicchoose);
           
           if (GUI.Button(new Rect(Screen.width*0.24f, Screen.height*0.35f, Screen.width*0.1f, Screen.height*0.15f), ""))
            {
                xunhuancho2 = 1;
                
            }
            if (GUI.Button(new Rect(Screen.width*0.65f, Screen.height*0.35f, Screen.width*0.1f, Screen.height*0.15f), ""))
            {
                xunhuancho1 = 1;
            }
            
            GUI.skin = GUIskin2;
            if (GUI.Button(new Rect(Screen.width * 0.4f, Screen.height * 0.45f, Screen.width * 0.2f, Screen.height * 0.25f), "确定"))
            {
                quanju.songscho = 0;
                quanju.song = xunhuan;
                Musicopp = 1;
            }
          
            GUI.skin = GUIskin1;
            if (GUI.Button(new Rect(Screen.width * 0.35f, Screen.height * 0.35f, Screen.width * 0.3f, Screen.height * 0.1f), songsss[xunhuan]))
            {
                
            }
       
            GUI.skin = GUIskin4;
            if (GUI.Button(new Rect(0, 0, Screen.width, Screen.height), ""))
            {

            }
            //GUI.DrawTexture(new Rect(Screen.width - Screen.width * 0.1f, Screen.height - Screen.height * 0.1f, Screen.width * 0.1f, Screen.height * 0.1f), musicpic);
        
        }
        GUI.DrawTexture(new Rect(Screen.width - Screen.width * 0.1f, Screen.height - Screen.height * 0.12f, Screen.width * 0.1f, Screen.height * 0.12f), musicpic);
        //////////////////////////////////////////////////////////////////////////////////////////// 

        if (open == 0 && kai1 == 0)
        {
            GUI.skin = GUIskin;
            if (GUI.Button(new Rect(Screen.width * 0.36f, Screen.height * 0.6f, Screen.width * 0.24f, Screen.height * 0.12f), ""))
            {
                music.PlayOneShot(beep1);
                kai1 = 1;
            }
            if (GUI.Button(new Rect(Screen.width * 0.73f, Screen.height * 0.22f, Screen.width * 0.1f, Screen.height * 0.08f), ""))
            {
                music.PlayOneShot(beep1);
                kai1 = 1;
            }
            GUI.skin = GUIskin1;
            if (GUI.Button(new Rect(0, 0, Screen.width, Screen.height), ""))
            {
            }
            GUI.DrawTexture(new Rect(Screen.width * 0.08f, -Screen.height * 0.16f, Screen.width * 0.8f, Screen.height * 1.2f), p8);
           
            GUI.Label(new Rect(Screen.width * 0.2f, Screen.height * 0.35f, Screen.width, Screen.height), "本游戏通过NFC感应\n进行游戏控制\n请先对您的NFC标签\n进行键位扫入\n");
        }


        if (open == 0 && kai2 == 0)
        {


            GUI.skin = GUIskin;
            if (GUI.Button(new Rect(Screen.width * 0.36f, Screen.height * 0.6f, Screen.width * 0.24f, Screen.height * 0.12f), ""))
            {
                music.PlayOneShot(beep1);
                kai2 = 1;
            }
            if (GUI.Button(new Rect(Screen.width * 0.73f, Screen.height * 0.22f, Screen.width * 0.1f, Screen.height * 0.08f), ""))
            {
                music.PlayOneShot(beep1);
                kai2 = 1;
            }

            if (GUI.Button(new Rect(Screen.width * 0.45f, Screen.height * 0.36f, Screen.width * 0.266f, Screen.height * 0.11f), ""))
            {
                music.PlayOneShot(beep1);
                if (kai3 == 0)
                {
                    GetComponent<AudioSource>().Stop();
                    quanju.yinyue = 0;
                    kai3 = 1;
                }

                else
                {
                    GetComponent<AudioSource>().Play();
                    quanju.yinyue = 1;
                    kai3 = 0;
                }

            }

            if (GUI.Button(new Rect(Screen.width * 0.45f, Screen.height * 0.48f, Screen.width * 0.266f, Screen.height * 0.11f), ""))
            {
                music.PlayOneShot(beep1);
                if (kai4 == 0)
                {
                    music.volume = 0;
                    quanju.yinxiao = 0;
                    kai4 = 1;
                }

                else
                {
                    music.volume = 1;
                    quanju.yinxiao = 1;
                    kai4 = 0;
                }

            }
            GUI.skin = GUIskin1;
            if (GUI.Button(new Rect(0, 0, Screen.width, Screen.height), ""))
            {
            }
            GUI.DrawTexture(new Rect(Screen.width * 0.08f, -Screen.height * 0.16f, Screen.width * 0.8f, Screen.height * 1.2f), p9);

            

        }
        //进入商店
        GUI.skin = GUIskin;
        if (GUI.Button(new Rect(Screen.width * 0.35f, Screen.height * 0.8f, Screen.width * 0.14f, Screen.height * 0.16f), ""))
        {
            music.PlayOneShot(beep);
            shangdianopen = 0;

        }
        GUI.DrawTexture(new Rect(Screen.width * 0.35f, Screen.height * 0.8f, Screen.width * 0.14f, Screen.height * 0.16f), storypic);


        //进入游戏
        GUI.skin = GUIskin;
        if (GUI.Button(new Rect(Screen.width * 0.1f, Screen.height * 0.75f, Screen.width * 0.2f, Screen.height * 0.22f), ""))
        {
            music.PlayOneShot(beep);
            chose=0;
           
        }
        GUI.DrawTexture(new Rect(Screen.width * 0.1f, Screen.height * 0.75f, Screen.width * 0.2f, Screen.height * 0.22f), bt1);
       if(chose==0)
       {
           if (GUI.Button(new Rect(Screen.width * 0.7f, Screen.height * 0.4f, Screen.width * 0.3f, Screen.height * 0.2f), ""))
           {
               music.PlayOneShot(beep);
               quanju.model = 0;
               NFC = 0;
           }
           GUI.DrawTexture(new Rect(Screen.width * 0.7f, Screen.height * 0.4f, Screen.width * 0.3f, Screen.height * 0.2f), teshu);

           if (GUI.Button(new Rect(Screen.width * 0.7f, Screen.height * 0.6f, Screen.width * 0.3f, Screen.height * 0.2f), ""))
           {
               music.PlayOneShot(beep);
               quanju.model = 1;
               Application.LoadLevel("Loading");
           }
           GUI.DrawTexture(new Rect(Screen.width * 0.7f, Screen.height * 0.6f, Screen.width * 0.3f, Screen.height * 0.2f), putong);


       }
        
        if (NFC == 0)
        {
            //GUI.skin = GUIskin;
            if (GUI.Button(new Rect(Screen.width * 0.21f, Screen.height * 0.64f, Screen.width * 0.24f, Screen.height * 0.1f), ""))
            {
                music.PlayOneShot(beep1);
                //显示NFC感应
                NFC2 = 0;

                using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                {
                    using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
                    {
                        //调用Android插件中UnityTestActivity中StartActivity0方法，stringToEdit表示它的参数
                        jo.Call("StartActivity0", name);
                    }
                }

            }

            if (GUI.Button(new Rect(Screen.width * 0.53f, Screen.height * 0.64f, Screen.width * 0.24f, Screen.height * 0.1f), ""))
            {
                music.PlayOneShot(beep1);
                //进入游戏
                using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                {
                    using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
                    {
                        //调用Android插件中UnityTestActivity中StartActivity0方法，stringToEdit表示它的参数
                        jo.Call("StartActivity1", name);
                    }
                }
                Application.LoadLevel("Loading");
            }

            if (GUI.Button(new Rect(Screen.width * 0.73f, Screen.height * 0.28f, Screen.width * 0.1f, Screen.height * 0.05f), ""))
            {
                music.PlayOneShot(beep1);
                NFC = 1;

            }
            GUI.skin = GUIskin1;
            if (GUI.Button(new Rect(0, 0, Screen.width, Screen.height), ""))
            {

            }
            GUI.DrawTexture(new Rect(Screen.width * 0.08f, -Screen.height*0.16f, Screen.width * 0.8f, Screen.height * 1.2f), p12);

            GUI.skin = GUIskin1;
            GUI.Label(new Rect(Screen.width * 0.2f, Screen.height * 0.35f, Screen.width, Screen.height), "您是否已经扫入一个NFC\n标签,如果是请点击已有\n进入游戏,反之,请点击\n还没进行键位扫入");

            
        }


        //游戏介绍
        //if (GUI.Button(new Rect(Screen.width * 0.29f, Screen.height * 0.705f, Screen.width * 0.365f, Screen.height * 0.08f), ""))
        //{
        //    music.PlayOneShot(beep);
        //    kai1 = 0;
        //    open = 0;

        //    print("111");
        //}
        //GUI.DrawTexture(new Rect(Screen.width * 0.27f, Screen.height * 0.7f, Screen.width * 0.4f, Screen.height * 0.1f), bt2);
        //退出游戏
        //if (GUI.Button(new Rect(Screen.width * 0.29f, Screen.height * 0.805f, Screen.width * 0.365f, Screen.height * 0.08f), ""))
        //{
        //    music.PlayOneShot(beep);
        //    Application.Quit();
        //}
        //GUI.DrawTexture(new Rect(Screen.width * 0.27f, Screen.height * 0.8f, Screen.width * 0.4f, Screen.height * 0.1f), bt3);



        GUI.skin = GUIskin;
        //GUI.Label(new Rect(Screen.width * 0.3f, Screen.height * 0.8f, 100, 100), "Loading.....", go);
        if (open == 1)
        {
            if (GUI.Button(new Rect(0, 0, Screen.width * 0.1f, Screen.width * 0.1f), ""))
            {
                music.PlayOneShot(beep1);
                open = 0;

            }
            GUI.DrawTexture(new Rect(0, 0, Screen.width * 0.1f, Screen.width * 0.1f), p1);
        }
        if (open == 0)
        {
            if (GUI.Button(new Rect(0, 0, Screen.width * 0.1f, Screen.width * 0.1f), ""))
            {
                music.PlayOneShot(beep1);
                open = 1;
                kai1 = 1;

            }
            GUI.DrawTexture(new Rect(0, 0, Screen.width * 0.1f, Screen.width * 0.1f), p2);



            if (GUI.Button(new Rect(Screen.width * 0.1f, 0, Screen.width * 0.1f, Screen.width * 0.1f), ""))
            {
                music.PlayOneShot(beep1);
                kai2 = 0;
            }
            GUI.DrawTexture(new Rect(Screen.width * 0.1f, 0, Screen.width * 0.1f, Screen.width * 0.1f), p3);

            if (GUI.Button(new Rect(Screen.width * 0.2f, 0, Screen.width * 0.1f, Screen.width * 0.1f), ""))
            {
                music.PlayOneShot(beep1);
                kai1 = 0;


            }
            GUI.DrawTexture(new Rect(Screen.width * 0.2f, 0, Screen.width * 0.1f, Screen.width * 0.1f), p4);

            if (GUI.Button(new Rect(Screen.width * 0.3f, 0, Screen.width * 0.1f, Screen.width * 0.1f), ""))
            {
                music.PlayOneShot(beep1);
                Application.Quit();
            }
            GUI.DrawTexture(new Rect(Screen.width * 0.3f, 0, Screen.width * 0.1f, Screen.width * 0.1f), p5);
        }
        if (kai3 + kai4 != 2)
        {
            kai = 1;
        }
        if (kai == 1)
        {
            if (GUI.Button(new Rect(Screen.width * 0.9f, 0, Screen.width * 0.1f, Screen.width * 0.1f), ""))
            {
                music.PlayOneShot(beep1);

                kai = 0;
                //GetComponent<AudioSource>().mute = true;
                GetComponent<AudioSource>().Stop();
                quanju.yinyue = 0;
                quanju.yinxiao = 0;
                music.volume = 0;
                kai3 = 1;
                kai4 = 1;
                //audio.Stop();
            }
            GUI.DrawTexture(new Rect(Screen.width * 0.9f, 0, Screen.width * 0.1f, Screen.width * 0.1f), p6);
        }

        if (kai == 0 || (kai3 == 1 && kai4 == 1))
        {
            if (GUI.Button(new Rect(Screen.width * 0.9f, 0, Screen.width * 0.1f, Screen.width * 0.1f), ""))
            {
                music.PlayOneShot(beep1);
                kai = 1;
                //GetComponent<AudioSource>().mute = false;
                GetComponent<AudioSource>().Play();
                quanju.yinyue = 1;
                quanju.yinxiao = 1;
                music.volume = 1;
                kai3 = 0;
                kai4 = 0;
                //audio.Play();
            }
            //if(kai3!=0||kai4!=0)
            GUI.DrawTexture(new Rect(Screen.width * 0.9f, 0, Screen.width * 0.1f, Screen.width * 0.1f), p7);
        }

        if (open == 0 && kai2 == 0 && kai3 == 0)
        {
            GUI.DrawTexture(new Rect(Screen.width * 0.446f, Screen.height * 0.367f, Screen.width * 0.266f, Screen.height * 0.108f), p10);
        }
        if (open == 0 && kai2 == 0 && kai4 == 0)
        {
            GUI.DrawTexture(new Rect(Screen.width * 0.446f, Screen.height * 0.485f, Screen.width * 0.266f, Screen.height * 0.11f), p10);
        }


        if (NFC2 == 0)//碰一碰图片显示
        {
            GUI.DrawTexture(new Rect(Screen.width * 0.37f, Screen.height * 0.3f, Screen.width * 0.4f, Screen.height * 0.4f), p13);
            if (Random.Range(0, 2) == 0)
                GUI.DrawTexture(new Rect(Screen.width * 0.42f, Screen.height * 0.35f, Screen.width * 0.3f, Screen.height * 0.3f), p14);
        }
        if (reNFC1 == "N")//扫描成功传进N；
        {
            NFC = 1; NFC2 = 1;//碰一碰图片消失
            GUI.DrawTexture(new Rect(Screen.width * 0.08f, -Screen.height * 0.16f, Screen.width * 0.8f, Screen.height * 1.2f), p8);
            GUI.skin = GUIskin1;
            if (clo)
                GUI.Label(new Rect(Screen.width * 0.2f, Screen.height * 0.35f, Screen.width, Screen.height), "恭喜您成功扫入左键位，\n点击确定进入键位二");
            else
                GUI.Label(new Rect(Screen.width * 0.2f, Screen.height * 0.35f, Screen.width, Screen.height), "恭喜您成功扫入右键位，\n点击确定进入游戏吧");
        }
        if(shangdianopen==0)
        {
            GUI.DrawTexture(new Rect(Screen.width * 0.1f, Screen.height * 0.1f, Screen.width * 0.8f, Screen.height * 0.8f), yifupic);
            GUI.DrawTexture(new Rect(Screen.width * 0.3f, Screen.height * 0.4f, Screen.width * 0.4f, Screen.height * 0.4f), cloth[index]);
            GUI.DrawTexture(new Rect(Screen.width * 0.13f, Screen.height * 0.25f, Screen.width * 0.3f, Screen.height * 0.15f), jinbi);
            GUI.DrawTexture(new Rect(Screen.width * 0.56f, Screen.height * 0.25f, Screen.width * 0.3f, Screen.height * 0.15f), zuanshi);
            if(jinbiopen==0)
            {
                GUI.skin = GUIskin2;
                if (GUI.Button(new Rect(Screen.width * 0.3f, Screen.height * 0.4f, Screen.width * 0.4f, Screen.height * 0.4f), "已获得无限金币资格"))
                {

                }
                StartCoroutine(WaitAndPrint1(1.8F));
            }
            if(zuanshiopen==0)
            {
                GUI.skin = GUIskin2;
                if (GUI.Button(new Rect(Screen.width * 0.3f, Screen.height * 0.4f, Screen.width * 0.4f, Screen.height * 0.4f), "暂无支付接口"))
                {

                }
                StartCoroutine(WaitAndPrint2(1.8F));
            }
            //角色购买

/////////////////////////////////////////////////////////////////////////////////////////////////////////
           /* //PlayerPrefs.SetInt("renwu1", quanju.renwu1);
            quanju.renwu1 = PlayerPrefs.GetInt("renwu1");
            //PlayerPrefs.SetInt("renwu2", quanju.renwu2);
            quanju.renwu2 = PlayerPrefs.GetInt("renwu2");
            //PlayerPrefs.SetInt("renwu3", quanju.renwu3);
            quanju.renwu3 = PlayerPrefs.GetInt("renwu3");
            //PlayerPrefs.SetInt("renwu4", quanju.renwu4);
            quanju.renwu4 = PlayerPrefs.GetInt("renwu4");
            //PlayerPrefs.SetInt("renwu5", quanju.renwu5);
            quanju.renwu5 = PlayerPrefs.GetInt("renwu5");
            //PlayerPrefs.SetInt("renwu6", quanju.renwu6);
            quanju.renwu6 = PlayerPrefs.GetInt("renwu6");
            GUI.skin = GUIskin1;
            if (GUI.Button(new Rect(Screen.width * 0.8f, Screen.height * 0.8f, Screen.width * 0.15f, Screen.height * 0.1f), "购买"))
            {
                if (index == 1 && quanju.renwu1 == 0 && quanju.jinbi >= 10000)
                {
                    quanju.jinbi = quanju.jinbi - 10000;
                    PlayerPrefs.SetInt("JinBi", quanju.jinbi);
                    quanju.renwu1 = 1;
                    PlayerPrefs.SetInt("renwu1", quanju.renwu1);
                }
                if (index == 2 && quanju.renwu2 == 0 && quanju.jinbi >= 20000)
                {
                    quanju.jinbi = quanju.jinbi - 20000;
                    PlayerPrefs.SetInt("JinBi", quanju.jinbi);
                    quanju.renwu2 = 1;
                    PlayerPrefs.SetInt("renwu2", quanju.renwu2);

                }
                if (index == 3 && quanju.renwu3 == 0 && quanju.jinbi >= 30000)
                {
                    quanju.jinbi = quanju.jinbi - 30000;
                    PlayerPrefs.SetInt("JinBi", quanju.jinbi);
                    quanju.renwu3 = 1;
                    PlayerPrefs.SetInt("renwu3", quanju.renwu3);

                }
                if (index == 4 && quanju.renwu4 == 0 && quanju.jinbi >= 40000)
                {
                    quanju.jinbi = quanju.jinbi - 40000;
                    PlayerPrefs.SetInt("JinBi", quanju.jinbi);
                    quanju.renwu4 = 1;
                    PlayerPrefs.SetInt("renwu4", quanju.renwu4);

                }
                if (index == 5 && quanju.renwu5 == 0 && quanju.jinbi >= 50000)
                {
                    quanju.jinbi = quanju.jinbi - 50000;
                    PlayerPrefs.SetInt("JinBi", quanju.jinbi);
                    quanju.renwu5 = 1;
                    PlayerPrefs.SetInt("renwu5", quanju.renwu5);

                }
                if (index == 0 && quanju.renwu6 == 0 && quanju.jinbi >= 5000)
                {
                    quanju.jinbi = quanju.jinbi - 5000;
                    PlayerPrefs.SetInt("JinBi", quanju.jinbi);
                    quanju.renwu6 = 1;
                    PlayerPrefs.SetInt("renwu6", quanju.renwu6);

                }
            }

            if (index == 0)
            {
                if (GUI.Button(new Rect(Screen.width * 0.42f, Screen.height * 0.6f, Screen.width * 0.15f, Screen.height * 0.3f), "小白" + "\n" + "5000$" + "\n" + "已内购"))
                {   }
            }
            if (index == 5)
            {
                if (quanju.renwu5 == 1)
                {
                    if (GUI.Button(new Rect(Screen.width * 0.42f, Screen.height * 0.6f, Screen.width * 0.15f, Screen.height * 0.3f), "小黑" + "\n" + "50000$" + "\n" + "已购买"))
                    {
                        
                    }
                }
                else
                {
                    if (GUI.Button(new Rect(Screen.width * 0.42f, Screen.height * 0.6f, Screen.width * 0.15f, Screen.height * 0.3f), "小黑" + "\n" + "50000$" + "\n" + "未购买"))
                    {
                        
                    }
                }

            }
            if (index == 4)
            {
                if (quanju.renwu4 == 1)
                {
                    if (GUI.Button(new Rect(Screen.width * 0.42f, Screen.height * 0.6f, Screen.width * 0.15f, Screen.height * 0.3f), "小红" + "\n" + "40000$" + "\n" + "已购买"))
                    {
                       
                    }
                }
                else
                {
                    if (GUI.Button(new Rect(Screen.width * 0.42f, Screen.height * 0.6f, Screen.width * 0.15f, Screen.height * 0.3f), "小红" + "\n" + "40000$" + "\n" + "未购买"))
                    {
                        
                    }
                }
            }
            if (index == 3)
            {
                if (quanju.renwu3 == 1)
                {
                    if (GUI.Button(new Rect(Screen.width * 0.42f, Screen.height * 0.6f, Screen.width * 0.15f, Screen.height * 0.3f), "明明" + "\n" + "30000$" + "\n" + "已购买"))
                    {
                       
                    }
                }
                else
                {
                    if (GUI.Button(new Rect(Screen.width * 0.42f, Screen.height * 0.6f, Screen.width * 0.15f, Screen.height * 0.3f), "明明" + "\n" + "30000$" + "\n" + "未购买"))
                    {
                       
                    }
                }

            }
            if (index == 2)
            {
                if (quanju.renwu2 == 1)
                {
                    if (GUI.Button(new Rect(Screen.width * 0.42f, Screen.height * 0.6f, Screen.width * 0.15f, Screen.height * 0.3f), "静静" + "\n" + "20000$" + "\n" + "已购买"))
                    {
                        
                    }
                }
                else
                {
                    if (GUI.Button(new Rect(Screen.width * 0.42f, Screen.height * 0.6f, Screen.width * 0.15f, Screen.height * 0.3f), "静静" + "\n" + "20000$" + "\n" + "未购买"))
                    {
                       
                    }
                }
            }
            if (index == 1)
            {
                if (quanju.renwu1 == 1)
                {
                    if (GUI.Button(new Rect(Screen.width * 0.42f, Screen.height * 0.6f, Screen.width * 0.15f, Screen.height * 0.3f), "小蓝" + "\n" + "10000$" + "\n" + "已购买"))
                    {
                        
                    }
                }
                else
                {
                    if (GUI.Button(new Rect(Screen.width * 0.42f, Screen.height * 0.6f, Screen.width * 0.15f, Screen.height * 0.3f), "小蓝" + "\n" + "10000$" + "\n" + "未购买"))
                    {
                        
                    }
                }
            }*/
///////////////////////////////////////////////////////////////////////////////////////////////////////////
        }
       
        if (quit1 == 0)
        {
            GUI.DrawTexture(new Rect(Screen.width * 0.2f, Screen.height * 0.15f, Screen.width * 0.6f, Screen.height * 0.65f), quitpic);
        }
        if (kai5 == 0)//小脚印触摸图案显示，必须在最后，叠在所有UI最上
        {
            GUI.DrawTexture(new Rect(sx - Screen.width * 0.05f, Screen.height - sy - Screen.height * 0.05f, Screen.width * 0.1f, Screen.width * 0.1f), p11);
            StartCoroutine(WaitAndPrint(0.2F));
        }
       
    }
    IEnumerator WaitAndPrint2(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        zuanshiopen=1;
    }
    IEnumerator WaitAndPrint1(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        jinbiopen = 1;
    }
    IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        kai5 = 1;
    }
    void reNFC(string str)
    {
        reNFC1 = str;
    }
  
}
