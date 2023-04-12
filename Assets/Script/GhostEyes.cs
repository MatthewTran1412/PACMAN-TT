using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEyes : MonoBehaviour
{
    public SpriteRenderer spriteRenderer{get;private set;}
    public MoveMent movement{get;private set;}
    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;
    private void Awake() {
        spriteRenderer=GetComponent<SpriteRenderer>();
        movement=GetComponentInParent<MoveMent>();
    }
    private void Update() {
        CheckEyes();
    }
    private void CheckEyes(){
        if(movement.direction == Vector2.up){
            spriteRenderer.sprite= up;
        }
        if(movement.direction == Vector2.down){
            spriteRenderer.sprite= down;
        }
        if(movement.direction == Vector2.left){
            spriteRenderer.sprite= left;
        }
        if(movement.direction == Vector2.right){
            spriteRenderer.sprite= right;
        }
    }
}
