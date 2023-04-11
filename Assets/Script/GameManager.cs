using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public Ghosts[] ghosts;
    public Pacman pacman;
    public Transform pellets;

    public int ghostMultiplier {get;private set;}=1;
    public int score{get;private set;}
    public int lives {get;private set;}
    private void Start() {
        NewGame();
    }
    private void NewGame(){
        SetScore(0);
        SetLives(3);
        NewRound();
    }
    private void Update() {
        if(this.lives<=0 && Input.anyKeyDown)
            NewGame();
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
        SetScore(this.score + ghost.points*ghostMultiplier);
        this.ghostMultiplier++;
    }
    public void PacmanEaten()
    {
        this.pacman.gameObject.SetActive(false);
        SetLives(this.lives-1);
        if(this.lives<=0)
            GameOver();
        else 
            Invoke(nameof(ResetState),3.0f);
    }
    public void PelletEaten(Pellet pellet){
        pellet.gameObject.SetActive(false);
        SetScore(score+pellet.point);
        if(!HasRemainningPellets())
        {
            this.pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound),3.0f);
        }
    }
    public void PowerPellet(PowerPellet powerpellet){
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
}
