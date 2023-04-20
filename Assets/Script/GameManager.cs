using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class GameManager : MonoBehaviour,IDataPersistence
{
    public Ghosts[] ghosts;
    public Pacman pacman;
    public Transform pellets;
    public AudioClip EatGhost;
    public AudioClip Die;
    public AudioClip gameover;
    public AudioClip startgame;
    public AudioClip Eatsound;
    public AudioClip Victory;
    public AudioSource ac;
    public GameObject[] livesprite;
    public Text Myscore;
    public Text Highestscore;
    public bool isNewGame;
    public bool isEnd;
    public GameObject replay;
    public GameObject victory;
    public GameObject PauseScene;

    public int ghostMultiplier {get;private set;}=1;
    public int score{get;private set;}
    public int lives {get;private set;}
    public bool isPause;
    private void Start() {
        isNewGame=false;
        isEnd=false;
        isPause=false;
        PauseScene.SetActive(false);
        victory.SetActive(false);
        replay.SetActive(false);
        ac = GetComponent<AudioSource>();
        ac.PlayOneShot(startgame);
        StartCoroutine(WaitforNewGame());
    }
    private void NewGame(){
        SetScore(0);
        SetLives(3);
        NewRound();
        isNewGame=true;
        isEnd=false;
        victory.SetActive(false);
        replay.SetActive(false);
        foreach (GameObject item in livesprite)
        {
            item.SetActive(true);
        }
    }
    public void LoadData(GameData data)
    {
        this.score=data.score;
        this.lives=data.lives;
        pacman.gameObject.transform.position=data.pacmanposition;
        if(data.PacmanDirection==Vector2.up)
            pacman.gameObject.transform.rotation=Quaternion.Euler(0,0,90);
        else if(data.PacmanDirection==Vector2.down)
            pacman.gameObject.transform.rotation=Quaternion.Euler(0,0,-90);
        else if(data.PacmanDirection==Vector2.left)
            pacman.gameObject.transform.rotation=Quaternion.Euler(0,0,0);
        else if(data.PacmanDirection==Vector2.right)
            pacman.gameObject.transform.rotation=Quaternion.Euler(0,0,180);
        pacman.movement.SetDirection(data.PacmanDirection);
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].gameObject.transform.position=data.GhostPosistion[i];
            ghosts[i].home.enabled=data.isHome[i];
            ghosts[i].chase.enabled=data.isChase[i];
            ghosts[i].scatter.enabled=data.isScatter[i];
            ghosts[i].frigtened.enabled=data.isFrightened[i];
            if(ghosts[i].home.enabled)
                ghosts[i].home.Enable(data.Duration[i]);
            else if(ghosts[i].chase.enabled)
                ghosts[i].chase.Enable(data.Duration[i]);
            else if(ghosts[i].scatter.enabled)
                ghosts[i].scatter.Enable(data.Duration[i]);
            else if(ghosts[i].frigtened.enabled)
                ghosts[i].frigtened.Enable(data.Duration[i]);
        }
        foreach(Transform pellet in pellets)
        {
            if (data.pellet.Contains(pellet.transform.position))
            {
                pellet.gameObject.SetActive(false);
            }
            else
            {
                pellet.gameObject.SetActive(true);
            }
        }
        foreach(GameObject item in livesprite)
            item.SetActive(false);
        for (int i = 0; i < lives; i++)
        {
            livesprite[i].SetActive(true);
        }
    }
    public void SaveData(ref GameData data){
        data.score=this.score;
        data.lives=this.lives;
        data.pacmanposition=pacman.gameObject.transform.position;
        data.PacmanDirection=pacman.movement.direction;
        for (int i = 0; i < ghosts.Length; i++)
        {
            data.GhostPosistion[i]=ghosts[i].gameObject.transform.position;
            data.isHome[i]=ghosts[i].home.enabled;
            data.isChase[i]=ghosts[i].chase.enabled;
            data.isScatter[i]=ghosts[i].scatter.enabled;
            data.isFrightened[i]=ghosts[i].frigtened.enabled;
            if(ghosts[i].home.enabled)
                data.Duration[i]=ghosts[i].home.timeleft;
            else if(ghosts[i].chase.enabled)
                data.Duration[i]=ghosts[i].chase.timeleft;
            else if(ghosts[i].scatter.enabled)
                data.Duration[i]=ghosts[i].scatter.timeleft;
            else if(ghosts[i].frigtened.enabled)
                data.Duration[i]=ghosts[i].frigtened.timeleft;
        }
        foreach(Transform pellet in pellets)
        {
            if(!pellet.gameObject.activeSelf)
                data.pellet.Add(pellet.gameObject.transform.position);
        }
    }
    private void Update() {
        Myscore.text="Score:\t"+score;
        Highestscore.text="Highest Score:\t"+PlayerPrefs.GetInt("HighestScore",0);
        if(score>PlayerPrefs.GetInt("HighestScore",0))
            PlayerPrefs.SetInt("HighestScore",score);
        if(isEnd==true)
            StartCoroutine(WaitforNewGame());
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(isPause==false)
            {
                OpenPause();
            }
            else if(isPause==true)
            {
                ClosePause();
            }
        }
    }
    public void OpenPause(){
        isPause=true;
        Time.timeScale=0f;
        PauseScene.SetActive(true);
    }
    public void ClosePause()
    {
        isPause=false;
        Time.timeScale=1f;
        PauseScene.SetActive(false);
    }
    public void QuitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying=false;
    #else
        Application.Quit();
    #endif
    }
    private void NewRound()
    {
        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }
        ResetGhostMultiplier();
        for (int i = 0; i < ghosts.Length; i++)
        {
            this.ghosts[i].ResetState();
        }
        ResetState();
    }
    public void ResetState()
    {
        this.pacman.ResetState();
    }
    private void GameOver(){
        for (int i = 0; i < ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }
        this.pacman.gameObject.SetActive(false);
        ac.PlayOneShot(gameover);
        isNewGame=false;
        isEnd=true;
        replay.SetActive(true);
    }
    private void SetScore(int score){
        this.score=score;
    }
    private void SetLives(int lives)
    {
        this.lives=lives;
    }
    public void GhostEaten(Ghosts ghost)
    {
        ac.PlayOneShot(EatGhost);
        SetScore(this.score + ghost.points*ghostMultiplier);
        this.ghostMultiplier++;
    }
    public void PacmanEaten()
    {
        this.pacman.gameObject.SetActive(false);
        SetLives(this.lives-1);
        if(this.lives<=0)
        {
            GameOver();
            isEnd=true;
        }
        else {
            livesprite[this.lives-1].SetActive(false);
            ac.PlayOneShot(Die);
            Invoke(nameof(ResetState),3.0f);
        }
    }
    // public void checksprite()
    // {
        // livesprite[this.lives-1].SetActive(false);
    // }
    public void PelletEaten(Pellet pellet){
        pellet.gameObject.SetActive(false);
        ac.PlayOneShot(Eatsound);
        SetScore(score+pellet.point);
        if(!HasRemainningPellets())
        {
            this.pacman.gameObject.SetActive(false);
            ac.PlayOneShot(Victory);
            victory.SetActive(true);
            for (int i = 0; i < ghosts.Length; i++)
            {
                ghosts[i].gameObject.SetActive(false);
            }
            isEnd=true;
        }
    }
    public void PowerPellet(PowerPellet powerpellet){
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].frigtened.Enable(powerpellet.duration);
        }
        PelletEaten(powerpellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier),powerpellet.duration);
    }
    private bool HasRemainningPellets()
    {
        foreach (Transform pellet in this.pellets)
        {
            if(pellet.gameObject.activeSelf){
                return true;
            }
        }
        return false;
    }
    private void ResetGhostMultiplier(){
        ghostMultiplier=1;
    }
    IEnumerator WaitforNewGame(){
        yield return new WaitForSeconds(4.336f);
        NewGame();
    }
}
