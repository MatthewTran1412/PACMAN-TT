using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class MoveMent : MonoBehaviour
{
    public float speed=8.0f;
    public float speedMultiplier=1.0f;
    public Vector2 initialDirection;
    public LayerMask obstacleLayer;
    public new Rigidbody2D rigidbody{get; private set;}
    public Vector2 direction{get;private set;}
    public Vector2 nextdirection{get;private set;}
    public Vector3 startingPosition{get;private set;}
    private void Awake()
    {
        rigidbody=GetComponent<Rigidbody2D>();
        startingPosition=transform.position;
    }
    private void Start() {
        ResetState();
    }
    public void ResetState()
    {
        speedMultiplier=1.0f;
        direction=initialDirection;
        nextdirection=Vector2.zero;
        transform.position=startingPosition;
        this.rigidbody.isKinematic=false;
        enabled=true;
    }
    private void Update() {
        if(nextdirection!=Vector2.zero){
            SetDirection(nextdirection);
        }
    }
    private void FixedUpdate() {
        Vector2 position=rigidbody.position;
        Vector2 translation=direction*speed*speedMultiplier*Time.fixedDeltaTime;
        rigidbody.MovePosition(position+translation);
    }
    public void SetDirection(Vector2 direction, bool forced = false)
    {
        if(!Occupied(direction)|| forced)
        {
            this.direction=direction;
            this.nextdirection=Vector2.zero;
        }
        else{
            this.nextdirection=direction;
        }
    }
    private bool Occupied(Vector2 direction)
    {
        RaycastHit2D hit =Physics2D.BoxCast(transform.position,Vector2.one*0.75f,0.0f,direction,1.5f,obstacleLayer);
        return hit.collider !=null;
    }
}
