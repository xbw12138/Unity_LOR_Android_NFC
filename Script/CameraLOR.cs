using UnityEngine;
using System.Collections;
public class CameraLOR : MonoBehaviour {

    GameObject box;
    GameObject camera1;
    GameObject one;
    int speed = 1;
	// Use this for initialization
	void Start () {
        box = GameObject.Find("LenzoPrefab");
        camera1 = GameObject.FindGameObjectWithTag("MainCamera");
        //one.transform.position = box.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        if (quanju.openbomp == 0)
        {
            if (camera1.transform.rotation.y >= 0.2)
            {
                speed = -5;
            }
            if (camera1.transform.rotation.y <= -0.2)
            {
                speed = 5;
            }
            camera1.transform.RotateAround(box.transform.position, Vector3.up, speed);
          
        }
        else
        {
            if (camera1.transform.rotation.y >= 0.35)
            {
                speed = -1;
            }
            if (camera1.transform.rotation.y <= -0.3)
            {
                speed = 1;
            }
            camera1.transform.RotateAround(box.transform.position, Vector3.up, speed);
        }
	}
}
