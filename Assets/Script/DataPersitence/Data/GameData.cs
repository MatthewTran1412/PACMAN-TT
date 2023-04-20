using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int lives;
    public int score;
    public Vector3 pacmanposition;
    public Vector2 PacmanDirection;
    // public Ghost[] ghosts;
    public Vector3[] GhostPosistion;
    public bool[] isHome;
    public bool[] isChase;
    public bool[] isScatter;
    public bool[] isFrightened;
    public float[] Duration;
    public List<Vector3> pellet;
    public GameData()
    {
        lives=3;
        score=0;
        pacmanposition= new Vector3(0,-9.5f,-5);
        PacmanDirection=Vector2.right;
        GhostPosistion= new Vector3[4];
        GhostPosistion[0]= new Vector3(0f,2.5f,95f);
        GhostPosistion[1]= new Vector3(0f,-0.5f,95f);
        GhostPosistion[2]= new Vector3(2f,-0.5f,95f);
        GhostPosistion[3]= new Vector3(-2f,-0.5f,95f);
        isHome= new bool[]{ false, true, true, true };
        isChase= new bool[]{ false, false, false, false };
        isScatter= new bool[]{ true, false, false, false };
        isFrightened=new bool[]{ false, false, false, false };
        Duration = new float[]{7f,6f,9f,12f};
    }
}
