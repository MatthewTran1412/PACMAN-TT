using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveMent))]
public class Pacman : MonoBehaviour,IDataPersistence
{
    Vector2 movedirection;
    public MoveMent movement{get;private set;}
    AudioSource ac;
    private void Awake() {
        movement=GetComponent<MoveMent>();
        ac=GetComponent<AudioSource>();
    }
    private void Update() {
        if(FindObjectOfType<GameManager>().isNewGame==true && FindObjectOfType<GameManager>().isPause==false)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                movement.SetDirection(Vector2.up);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                movement.SetDirection(Vector2.down);
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
                movement.SetDirection(Vector2.left);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                movement.SetDirection(Vector2.right);
            }
            float angle = Mathf.Atan2(movement.direction.y,movement.direction.x);
            transform.rotation=Quaternion.AngleAxis(angle*Mathf.Rad2Deg,Vector3.forward);
        }
    }
    public void ResetState()
    {
        gameObject.SetActive(true);
        movement.ResetState();
    }
    public void LoadData(GameData data)
    {
        this.transform.position= data.pacmanposition;
    }
    public void SaveData(ref GameData data)
    {
        data.pacmanposition=this.transform.position;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Pellet"))
            ac.enabled=true;
        if(other.gameObject.layer != LayerMask.NameToLayer("Pellet"))
            ac.enabled=false;
    }
}
