using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghosts : MonoBehaviour
{
    public MoveMent movement{get;private set;}
    public GhostHome home{get;private set;}
    public GhostScater scatter {get;private set;}
    public GhostChase chase{get;private set;}
    public GhostFrigtened frigtened {get;private set;}
    public GhostBehavior initialBeahavior;
    public Transform target;
    public int points =200;
    private void Awake() {
        movement=GetComponent<MoveMent>();
        home=GetComponent<GhostHome>();
        scatter=GetComponent<GhostScater>();
        chase=GetComponent<GhostChase>();
        frigtened=GetComponent<GhostFrigtened>();
    }
    private void Start() {
        if(FindObjectOfType<GameManager>().isNewGame==true)
            ResetState();
    }
    public void ResetState(){
        gameObject.SetActive(true);
        movement.ResetState();

        frigtened.Disable();
        chase.Disable();
        if(scatter!=initialBeahavior)
            scatter.Disable();
        else if(scatter==initialBeahavior)
            scatter.Enable();
        if(home!=initialBeahavior)
            home.Disable();
        else if(home==initialBeahavior)
            home.Enable();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Pacman")){
            if(frigtened.enabled)
                FindObjectOfType<GameManager>().GhostEaten(this);
            else
                FindObjectOfType<GameManager>().PacmanEaten();
        }
    }
}
