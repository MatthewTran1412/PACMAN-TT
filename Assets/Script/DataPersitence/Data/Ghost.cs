using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost 
{
    public Vector3 position;
    public bool isChase;
    public bool isHome;
    public bool isScatter;
    public bool isFrightened;

    public Ghost(){
        position= Vector3.zero;
        isChase=false;
        isHome=false;
        isScatter=false;
        isFrightened=false;
    }
}
