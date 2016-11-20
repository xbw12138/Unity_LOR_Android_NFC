using UnityEngine;
using System.Collections;

public class leftorrightUI : MonoBehaviour {

    //任务动画控制
    protected Animator animator;
    private int an = 0;
    private int bn = 0;
    private int con = 0;
    //音频
    public AudioClip beep;
    public AudioClip beep1;
    public AudioClip beep2;
    public AudioClip beep3;

    public AudioSource music = null;

    public Texture2D hongzuo;
    public Texture2D hongyou;
    public Texture2D lanzuo;
    public Texture2D lanyou;
    public Texture2D bombL;
    public Texture2D bombR;

    public Texture2D lose;

    public GUISkin GUIskin;
    public GUISkin GUIskin1;
    public GUISkin GUIskin2;
    public GUISkin GUIskin3;
    public GUISkin GUIskin4;//得分字体
    private int randompic = 0;
    private int randompos = 1;

    ////镜头旋转
    //GameObject box;
    //GameObject camera1;
    //int speed = 1;

    public Texture2D load_write;    //进度条底纹

    public Texture2D []load_yellow;   //进度条
    public float loading = 0f;

    //状态栏
    public Texture2D piclife;
    public Texture2D picenergy;
    public Texture2D picshield;
    public Texture2D lifepic;
    public Texture2D energypic;
    public Texture2D shieldpic;
    public Texture2D back;

    //judge退出游戏
    public int quit1 = 1;
    public Texture2D quitpic;

    //暂停
    private int puase = 0;
    public Texture2D zanting;
    public Texture2D start;
    public Texture2D puasepic;
    //游戏积分
    private int grade = 0;
    private int max = 0;
    //游戏不敢应扣分处理开关；
    private int open1 = 1;
    //游戏能量积累重置
    private int ci = 0;
    //游戏暴击积攒力量
    private float li = 0;
    //NFC接口
    private string name = "S";
    private string Left = "P";
    private string Right = "R";
    private string kongzhi = "W";
    //NFC开关
    private int NFCopen = 1;
    private int NFCopen2 = 0;
    //测试版按钮
    public Texture2D anniu1;
    public Texture2D anniu2;
    //
    private int jinbixianshi = 0;
    private int first = 1;
	// Use this for initialization
	void Start () {
        music = gameObject.AddComponent<AudioSource>();
        quanju.zongfen = PlayerPrefs.GetInt("score");
        quanju.cloth= PlayerPrefs.GetInt("cloth");
        if(quanju.yinxiao==1)
        {
            music.volume = 1;
        }
        else
        {
            music.volume = 0;
        }
        if(quanju.yinyue==1)
        {
            GetComponent<AudioSource>().volume = 1;
        }
        else
        {
            GetComponent<AudioSource>().volume = 0;
        }
        Time.timeScale = 1;
        //获取角色控件
        animator = GameObject.Find("LenzoPrefab").GetComponent<Animator>();
        ////镜头旋转获取镜头
        //box = GameObject.FindGameObjectWithTag("Player");
        //camera1 = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
        ////镜头旋转控制及限制
        //if (camera1.transform.rotation.y >= 0.35)
        //{
        //    speed = -1;
        //}
        //if (camera1.transform.rotation.y <= -0.3)
        //{
        //    speed = 1;
        //}
        //camera1.transform.RotateAround(box.transform.position, Vector3.up, speed);
        //游戏暂停
        if (puase == 1)
        {
            Time.timeScale = 0;
            NFCopen = 0;
            GetComponent<AudioSource>().Pause();
        }
        else
        {
            Time.timeScale = 1;
            NFCopen = 1;
            
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Home))
        {
            quit1 = 0;
        }
        //游戏感应时间控制以及箭头随机
        if (loading >= 100)
        {
            randompic = Random.Range(0, 10);
            randompos = Random.Range(1, 9);
            kongzhi = "W";
            if (randompos % 2 == 0) randompos = 1;
            else randompos = 8;
            open1 = 1;
            loading = 0;
        }
        else
        {
            if(grade>=max)
                max=grade;
            if(quanju.model==0)//nfc模式
                loading += Time.deltaTime * (5 + max / 300 * 3);//游戏速度增长
            if (quanju.model == 1)
                loading += Time.deltaTime * (5 + max / 150 * 3);//游戏速度增长
        }

        if(quanju.openbomp==0)
        {
            quanju.shield -= Time.deltaTime*2;
            li = quanju.shield;
            if (quanju.shield <= 1)
            {
                quanju.openbomp = 1;
                li = 0;
            }

        }

        if(an-bn==-4)
        {
            animator.SetBool("Lose1", true);
            an = 0; bn = 0;
        }
        if (an - bn == -2)
        {
            animator.SetBool("Left1", true);
            an = 0; bn = 0;
        }
        if (an - bn == 0)
        {
            animator.SetBool("Right1", true);
            an = 0; bn = 0;
        }
        
        if(ci==15)
        {

            animator.SetBool("Jump1", false);
       
            if (!music.isPlaying&&con==0)
            {
                music.PlayOneShot(beep3);
                con = 1;
            }
            
        }
        else
        {
            animator.SetBool("Jump1", true);
            con = 0;
        }
        //角色动作控制
        if(an==1)
        {
            animator.SetBool("Lose1", false);
            bn = 5;
            animator.SetBool("Left1", true);
            animator.SetBool("Right1", true);
        }
        if(an==2)
        {
            animator.SetBool("Lose1", true);
            animator.SetBool("Left1", false);
            bn = 4;
            animator.SetBool("Right1", true);
        }
        if(an==3)
        {
            animator.SetBool("Lose1", true);
            animator.SetBool("Left1", true);
            animator.SetBool("Right1", false);
            bn = 3;
        }

	}
    //NFC感应接口
    void NFC(string str)
    {
        if (quanju.model == 0)
        {
            if (NFCopen == 1&&NFCopen2==0)
                name = str;
        }
    }
    
    void OnGUI()
    {
        //测试按钮//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (quanju.model == 1)
        {
            if (NFCopen == 1)
            {
                GUI.skin = GUIskin;
                if (GUI.Button(new Rect(Screen.width * 0.0f, Screen.height * 0.35f, Screen.width * 0.2f, Screen.height * 0.5f), ""))
                {
                    name = "P";
                }
                GUI.DrawTexture(new Rect(Screen.width * 0.03f, Screen.height * 0.5f, Screen.width * 0.15f, Screen.height * 0.2f), anniu1);

                if (GUI.Button(new Rect(Screen.width * 0.8f, Screen.height * 0.35f, Screen.width * 0.2f, Screen.height * 0.5f), ""))
                {
                    name = "R";
                }
                GUI.DrawTexture(new Rect(Screen.width * 0.8f, Screen.height * 0.5f, Screen.width * 0.15f, Screen.height * 0.2f), anniu2);
            }
        }
        //积分显示///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        GUI.skin = GUIskin2;
        GUI.Label(new Rect(Screen.width * 0.25f, Screen.height * 0.13f, Screen.width * 0.1f, Screen.height * 0.1f), (int)grade + " Score");
        //记录保护次数///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        quanju.shield = li;
        if (quanju.shield >= 20 && quanju.openbomp == 1)
        {
            quanju.shield = 20;
            quanju.openbomp = 0;
        }
        //记录能量次数//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        quanju.energy = ci/5;
        if (quanju.energy >= 3)
            quanju.energy = 3;
        if(quanju.life!=5&&quanju.energy==3&&ci==16)
        {
            quanju.life += 1;
            quanju.energy = 0;
            ci = 0;
        }
        //游戏结束///////////////////////////////////////////////////////////////////////////////////////////////////
        if (quanju.life == 0)
        {
            NFCopen = 0;
            GetComponent<AudioSource>().Pause();
            if (quit1 == 1)
            {
                if (first == 1)
                {
                    first = 0;
                    jinbixianshi = 1;
                }
                GUI.skin = GUIskin;
                if (GUI.Button(new Rect(Screen.width * 0.3f, Screen.height * 0.52f, Screen.width * 0.4f, Screen.height * 0.1f), ""))
                {
                    //分享到微博
                    quanju.share = 0;
                    Application.CaptureScreenshot("Screenshot.png");//截取积分截图
                }
                if (GUI.Button(new Rect(Screen.width * 0.31f, Screen.height * 0.685f, Screen.width * 0.1f, Screen.height * 0.1f), ""))
                {
                    //再来一次
                    quanju.life = 5;
                    quanju.shield = 0;
                    li = 0;
                    Time.timeScale = 1;
                    grade = 0;
                    NFCopen = 1;
                    GetComponent<AudioSource>().Play();
                    Application.LoadLevel("GAME1");
                }
                if (GUI.Button(new Rect(Screen.width * 0.6f, Screen.height * 0.685f, Screen.width * 0.1f, Screen.height * 0.1f), ""))
                {
                    //退出游戏
                    quit1 = 0;
                }
                if (quanju.life == 0 || quit1 == 0)
                {
                    GUI.skin = GUIskin1;
                    if (GUI.Button(new Rect(0, 0, Screen.width, Screen.height), ""))
                    {
                        //遮罩
                    }
                }
                GUI.DrawTexture(new Rect(Screen.width * 0.2f, Screen.height * 0.15f, Screen.width * 0.6f, Screen.height * 0.65f), lose);
                //分数显示
                GUI.skin = GUIskin4;
                GUI.Label(new Rect(Screen.width * 0.5f, Screen.height * 0.35f, Screen.width * 0.2f, Screen.height * 0.2f), (int)grade + "");
                //高分显示///////////////////////////////
                if (grade >= quanju.zongfen)
                {
                    GUI.skin = GUIskin4;
                    quanju.zongfen = grade;
                    PlayerPrefs.SetInt("score", quanju.zongfen);
                    GUI.Label(new Rect(Screen.width * 0.5f, Screen.height * 0.42f, Screen.width * 0.2f, Screen.height * 0.2f), (int)quanju.zongfen + "");
                }
                else
                {
                    GUI.skin = GUIskin4;
                    GUI.Label(new Rect(Screen.width * 0.5f, Screen.height * 0.42f, Screen.width * 0.2f, Screen.height * 0.2f), (int)quanju.zongfen + "");
                }
                Time.timeScale = 0;
            }
        }
        //游戏结束关闭NFC感应//////////////////////////////////////////////////////
        if (quanju.life == 0)
        {
            NFCopen2 = 1;
        }
        else
        {
            NFCopen2 = 0;
        }
        //游戏退出///////////////////////////////////////////////////////////////
        if (quit1 == 0)
        {
            Time.timeScale = 0;
            NFCopen = 0;
            GetComponent<AudioSource>().Pause();
            GUI.skin = GUIskin;
            if (GUI.Button(new Rect(Screen.width * 0.37f, Screen.height * 0.53f, Screen.width * 0.1f, Screen.height * 0.18f), ""))
            {
                //取消退出游戏
                quit1 = 1;
                NFCopen = 1;
                GetComponent<AudioSource>().Play();
            }
            if (GUI.Button(new Rect(Screen.width * 0.52f, Screen.height * 0.53f, Screen.width * 0.1f, Screen.height * 0.18f), ""))
            {
                //确认退出游戏
                Application.Quit();
            }
            if (quanju.life == 0 || quit1 == 0)
            {
                GUI.skin = GUIskin1;
                if (GUI.Button(new Rect(0, 0, Screen.width, Screen.height), ""))
                {
                    //遮罩
                }
            }
            GUI.DrawTexture(new Rect(Screen.width * 0.2f, Screen.height * 0.15f, Screen.width * 0.6f, Screen.height * 0.65f), quitpic);
        }

        ///////////////////////////////////////////////////////////按钮遮罩
      
        //暂停//////////////////////////////////////////////////////
        GUI.skin = GUIskin;
        if (GUI.Button(new Rect(Screen.width * 0.9f, 0, Screen.width * 0.1f, Screen.width * 0.1f), ""))
        {
            if (puase == 0)
            {
                puase = 1;
                GetComponent<AudioSource>().Pause();
            }
            else
            {
                puase = 0;
                GetComponent<AudioSource>().Play();
            }
        }

        if (puase == 1)
        {
            if (GUI.Button(new Rect(Screen.width * 0.33f, Screen.height * 0.45f, Screen.width * 0.1f, Screen.height * 0.16f), ""))
            {
                GetComponent<AudioSource>().Play();
                if (puase == 0)
                    puase = 1;
                else
                    puase = 0;
            }
            if (GUI.Button(new Rect(Screen.width * 0.455f, Screen.height * 0.45f, Screen.width * 0.1f, Screen.height * 0.16f), ""))
            {
                Application.LoadLevel("Menu");
            }
            if (GUI.Button(new Rect(Screen.width * 0.585f, Screen.height * 0.45f, Screen.width * 0.1f, Screen.height * 0.16f), ""))
            {
                quanju.cloth=(quanju.cloth+1)%quanju.zongcloth;
                PlayerPrefs.SetInt("cloth", quanju.cloth);
            }
            GUI.skin = GUIskin1;
            if (GUI.Button(new Rect(0, 0, Screen.width, Screen.height), ""))
            {

            }
        }

        
        //时间条显示
        if (loading <= 100)
        {
            int i=0;
            float blood_width = Screen.width *0.8f* loading / 100;
            if(loading>=70)
            {
                i = 1;
            }
            //进度条底纹
            GUI.DrawTexture(new Rect(Screen.width * 0.1f, Screen.height * 0.93f, Screen.width * 0.8f, Screen.height * 0.1f), load_write);
            //进度条
            GUI.DrawTexture(new Rect(Screen.width * 0.1f, Screen.height * 0.93f, blood_width, Screen.height * 0.1f), load_yellow[i]);
        }
        //游戏控制规则////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //5.当手中为蓝色牌子时，则选择牌子上字样的方向；
        //6.当手中为红色牌子时，则选择牌子上字样相反的方向；
        //7.当手中为炸弹牌子时，则选择牌子显示方向相反的方向；
        if (randompic == 0 || randompic == 4)
        {
            GUI.DrawTexture(new Rect(Screen.width * 0.1f * randompos, Screen.height * 0.7f, Screen.width * 0.15f, Screen.height * 0.15f), hongzuo);
            if(name==Left&&name!="S")
            {
                an = 1;
                    quanju.energy = 0;
                    ci = 0;
                if (quanju.life != 0&&quanju.energy!=3)
                    quanju.life -= 1;

                name = "S";
                kongzhi = "SW";
                music.PlayOneShot(beep1);
                if(!music.isPlaying)
                {
                    loading = 100;
                }
            }
            else if (name == Right&&name != "S")
            {
                an = 3;
                grade += 10;
                if (quanju.openbomp == 0)
                {

                    grade += 10;
                }
                else
                    li += 1;
                ci += 1;
                
                name = "S";
                kongzhi = "SW";
                loading = 100;
                music.PlayOneShot(beep);
            }
            else if (loading >= 100 &&kongzhi=="W")
            {
                
                if (quanju.life != 0&&open1==1)
                {
                    an = 1;
                    music.PlayOneShot(beep2);
                    ci = 0;
                    quanju.energy = 0;
                    quanju.life -= 1;
                    open1 = 0;
                }
                    
            }

        }
        if (randompic == 1 || randompic == 5)
        {
            GUI.DrawTexture(new Rect(Screen.width * 0.1f * randompos, Screen.height * 0.7f, Screen.width * 0.15f, Screen.height * 0.15f), lanzuo);
            if (name == Right && name != "S")
            {
                an = 1;
                ci = 0;
                quanju.energy = 0;
                if (quanju.life != 0 && quanju.energy != 3)
                    quanju.life -= 1;
                name = "S";
                kongzhi = "SW";
                music.PlayOneShot(beep1);
                if (!music.isPlaying)
                {
                    loading = 100;
                }
            }
            else if (name == Left && name != "S")
            {
                an = 2;
                grade += 10;
                if (quanju.openbomp == 0)
                {

                    grade += 10;
                }
                else
                    li += 1;
                ci += 1;
                
                name = "S";
                kongzhi = "SW";
                loading = 100;
                music.PlayOneShot(beep);
            }
            else if (loading >= 100 && kongzhi == "W")
            {
                if (quanju.life != 0 && open1 == 1)
                {
                    an = 1;
                    music.PlayOneShot(beep2);
                    ci = 0;
                    quanju.energy = 0;
                    quanju.life -= 1;
                    open1 = 0;
                }

            }
        }
        if (randompic == 2 || randompic == 6)
        {
            GUI.DrawTexture(new Rect(Screen.width * 0.1f * randompos, Screen.height * 0.7f, Screen.width * 0.15f, Screen.height * 0.15f), hongyou);
            if (name == Right && name != "S")
            {
                an = 1;
                ci = 0;
                quanju.energy = 0;
                if (quanju.life != 0 && quanju.energy != 3)
                    quanju.life -= 1;
                name = "S";
                kongzhi = "SW";
                music.PlayOneShot(beep1);
                if (!music.isPlaying)
                {
                    loading = 100;
                }
            }
            else if (name == Left && name != "S")
            {
                an = 2;
                grade += 10;
                if (quanju.openbomp == 0)
                {

                    grade += 10;
                }
                else
                    li += 1;
                ci += 1;
                
                name = "S";
                kongzhi = "SW";
                loading = 100;
                music.PlayOneShot(beep);
            }
            else if (loading >= 100 && kongzhi == "W")
            {
                if (quanju.life != 0 && open1 == 1)
                {
                    an = 1;
                    music.PlayOneShot(beep2);
                    ci = 0;
                    quanju.energy = 0;
                    quanju.life -= 1;
                    open1 = 0;
                }

            }
        }
        if (randompic == 3 || randompic == 7)
        {
            GUI.DrawTexture(new Rect(Screen.width * 0.1f * randompos, Screen.height * 0.7f, Screen.width * 0.15f, Screen.height * 0.15f), lanyou);
            if (name == Left && name != "S")
            {
                an = 1;
                ci = 0;
                quanju.energy = 0;
                if (quanju.life != 0 && quanju.energy != 3)
                    quanju.life -= 1;
                name = "S";
                kongzhi = "SW";
                music.PlayOneShot(beep1);
                if (!music.isPlaying)
                {
                    loading = 100;
                }
            }
            else if (name == Right && name != "S")
            {
                an = 3;
                grade += 10;
                if (quanju.openbomp == 0)
                {

                    grade += 10;
                }
                else
                    li += 1;
                ci += 1;
                
                name = "S";
                kongzhi = "SW";
                loading = 100;
                music.PlayOneShot(beep);
            }
            else if (loading >= 100 && kongzhi == "W")
            {
                if (quanju.life != 0 && open1 == 1)
                {
                    an = 1;
                    music.PlayOneShot(beep2);
                    ci = 0;
                    quanju.energy = 0;
                    quanju.life -= 1;
                    open1 = 0;
                }

            }
        }

        if (randompic == 8)
        {
            GUI.DrawTexture(new Rect(Screen.width * 0.1f * randompos, Screen.height * 0.7f, Screen.width * 0.15f, Screen.height * 0.15f), bombL);
            if(randompos==1)
            {
                if (name == Left && name != "S")
                {
                    an = 1;
                    ci = 0;
                    quanju.energy = 0;
                    if (quanju.life != 0 && quanju.energy != 3)
                        quanju.life -= 1;
                    name = "S";
                    kongzhi = "SW";
                    music.PlayOneShot(beep1);
                    if (!music.isPlaying)
                    {
                        loading = 100;
                    }
                }
                else if (name == Right && name != "S")
                {
                    an = 3;
                    grade += 10;
                    if (quanju.openbomp == 0)
                    {

                        grade += 10;
                    }
                    else
                        li += 1;
                    ci += 1;
                
                    name = "S";
                    kongzhi = "SW";
                    loading = 100;
                    music.PlayOneShot(beep);
                }
                else if (loading >= 100 && kongzhi == "W")
                {
                    if (quanju.life != 0 && open1 == 1)
                    {
                        an = 1;
                        music.PlayOneShot(beep2);
                        ci = 0;
                        quanju.energy = 0;
                        quanju.life -= 1;
                        open1 = 0;
                    }

                }
            }
            if(randompos==8)
            {
                if (name == Right && name != "S")
                {
                    an = 1;
                    ci = 0;
                    quanju.energy = 0;
                    if (quanju.life != 0 && quanju.energy != 3)
                        quanju.life -= 1;
                    name = "S";
                    kongzhi = "SW";
                    music.PlayOneShot(beep1);
                    if (!music.isPlaying)
                    {
                        loading = 100;
                    }
                }
                else if (name == Left && name != "S")
                {
                    an = 2;
                    grade += 10;
                    if (quanju.openbomp == 0)
                    {

                        grade += 10;
                    }
                    else
                        li += 1;
                    ci += 1;
                
                    name = "S";
                    kongzhi = "SW";
                    loading = 100;
                    music.PlayOneShot(beep);
                }
                else if (loading >= 100 && kongzhi == "W")
                {
                    if (quanju.life != 0 && open1 == 1)
                    {
                        an = 1;
                        music.PlayOneShot(beep2);
                        ci = 0;
                        quanju.energy = 0;
                        quanju.life -= 1;
                        open1 = 0;
                    }

                }
            }

        }
        if (randompic == 9)
        {
            GUI.DrawTexture(new Rect(Screen.width * 0.1f * randompos, Screen.height * 0.7f, Screen.width * 0.15f, Screen.height * 0.15f), bombR);
            if (randompos == 1)
            {
                if (name == Left && name != "S")
                {
                    an = 1;
                    ci = 0;
                    quanju.energy = 0;
                    if (quanju.life != 0 && quanju.energy != 3)
                        quanju.life -= 1;
                    name = "S";
                    kongzhi = "SW";
                    music.PlayOneShot(beep1);
                    if (!music.isPlaying)
                    {
                        loading = 100;
                    }
                }
                else if (name == Right && name != "S")
                {
                    an = 3;
                    grade += 10;
                    if (quanju.openbomp == 0)
                    {

                        grade += 10;
                    }
                    else
                        li += 1;
                    ci += 1;
                
                    name = "S";
                    kongzhi = "SW";
                    loading = 100;
                    music.PlayOneShot(beep);
                }
                else if (loading >= 100 && kongzhi == "W")
                {
                    if (quanju.life != 0 && open1 == 1)
                    {
                        an = 1;
                        music.PlayOneShot(beep2);
                        ci = 0;
                        quanju.energy = 0;
                        quanju.life -= 1;
                        open1 = 0;
                    }

                }
            }
            if (randompos == 8)
            {
                if (name == Right && name != "S")
                {
                    an = 1;
                    ci = 0;
                    quanju.energy = 0;
                    if (quanju.life != 0 && quanju.energy != 3)
                        quanju.life -= 1;
                    name = "S";
                    kongzhi = "SW";
                    music.PlayOneShot(beep1);
                    if (!music.isPlaying)
                    {
                        loading = 100;
                    }
                }
                else if (name == Left && name != "S")
                {
                    an = 2;
                    grade += 10;
                    if (quanju.openbomp == 0)
                    {

                        grade += 10;
                    }
                    else
                        li += 1;
                    ci += 1;
                
                    name = "S";
                    kongzhi = "SW";
                    loading = 100;
                    music.PlayOneShot(beep);
                }
                else if (loading >= 100 && kongzhi == "W")
                {
                    if (quanju.life != 0 && open1 == 1)
                    {
                        an = 1;
                        music.PlayOneShot(beep2);
                        ci = 0;
                        quanju.energy = 0;
                        quanju.life -= 1;
                        open1 = 0;
                    }

                }
            }

        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //状态栏，血条，保护，能量
        float life_width = Screen.width * quanju.life / 30;
        float energy_width = Screen.width * quanju.energy / 18;
        float shield_width = Screen.width * quanju.shield/125;
        GUI.DrawTexture(new Rect(-Screen.width * 0.2f, -Screen.height * 0.1f, Screen.width * 2, Screen.height * 0.25f), back);
        GUI.DrawTexture(new Rect(Screen.width * 0.0f, Screen.height * 0.01f, Screen.width * 0.25f, Screen.height * 0.15f), piclife);
        GUI.DrawTexture(new Rect(Screen.width * 0.31f, Screen.height * 0.01f, Screen.width * 0.25f, Screen.height * 0.15f), picenergy);
        GUI.DrawTexture(new Rect(Screen.width * 0.6f, Screen.height * 0.01f, Screen.width * 0.25f, Screen.height * 0.15f), picshield);
        GUI.DrawTexture(new Rect(Screen.width * 0.07f, Screen.height * 0.057f, life_width, Screen.height * 0.062f), lifepic);
        GUI.DrawTexture(new Rect(Screen.width * 0.375f, Screen.height * 0.05f, energy_width, Screen.height * 0.06f), energypic);
        GUI.DrawTexture(new Rect(Screen.width * 0.67f, Screen.height * 0.055f, shield_width, Screen.height * 0.05f), shieldpic);
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //暂停UI
        if (puase == 0)
            GUI.DrawTexture(new Rect(Screen.width * 0.9f, 0, Screen.width * 0.1f, Screen.width * 0.1f), zanting);
        else
        {
            GUI.DrawTexture(new Rect(Screen.width * 0.9f, 0, Screen.width * 0.1f, Screen.width * 0.1f), start);
            GUI.DrawTexture(new Rect(Screen.width * 0.2f, Screen.height * 0.2f, Screen.width * 0.6f, Screen.height * 0.6f), puasepic);
        }
        //游戏结束   
    } 
}
