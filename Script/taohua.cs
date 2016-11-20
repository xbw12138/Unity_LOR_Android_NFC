using UnityEngine;
using System.Collections;

public class taohua : MonoBehaviour {

 public Texture2D[] ani;
  private int index = 0;
  private int open = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI()
	{
        if(quanju.energy==3)
        {
            open = 0;
        }
        if(open==0)
        {
            //StartCoroutine(WaitAndPrint(1.0F));
            index++;
            index %= ani.Length;

            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), ani[index]);
            if(index==0)
            {
                open = 1;
            }
        }
        
          
	}
    //IEnumerator WaitAndPrint(float waitTime)
    //{
    //    yield return new WaitForSeconds(waitTime);
    //    index++;
    //}
	
}
