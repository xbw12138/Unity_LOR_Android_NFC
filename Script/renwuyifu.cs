using UnityEngine;
using System.Collections;

public class renwuyifu : MonoBehaviour {
    public Material[] ani;
  
	// Use this for initialization
	void Start () {
        quanju.zongcloth = ani.Length;
    }
	
	// Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().enabled = true;
        GetComponent<Renderer>().material = ani[quanju.cloth];
    }
}
