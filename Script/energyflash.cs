﻿using UnityEngine;
using System.Collections;

public class energyflash : MonoBehaviour
{
    public Material[] ani;
    private int index = 0;
    private int open = 1;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (quanju.energy == 3)
        {
            flash();
        }
        else
        {
            index = 0;
            GetComponent<Renderer>().enabled = false;
        }
    }

    public void flash()
    {
        index++;
        index %= ani.Length;
        GetComponent<Renderer>().enabled = true;
        GetComponent<Renderer>().material = ani[index];
    }

}
