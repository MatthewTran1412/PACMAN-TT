using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFrigtened : GhostBehavior
{
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;

    public bool eaten{get;private set;}

    public override void Enable(float duration)
    {
        base.Enable(duration);
        this.body.enabled=false;
        this.eyes.enabled=false;
        this.blue.enabled=true;
        this.white.enabled=false;
        Invoke(nameof(Flash),duration/2.0f);
    }
    public override void Disable() {
        base.Disable();
        this.body.enabled=true;
        this.eyes.enabled=true;
        this.blue.enabled=false;
        this.white.enabled=false;
    }
    private void Flash() {
        if(!eaten)
        {
            blue.enabled=false;
            white.enabled=true;
            white.GetComponent<AnimatedSprite>().Restart();    
        }
    }
    private void OnEnable() {
        ghost.movement.speedMultiplier=0.5f;
    }
    private void OnDisable() {
        ghost.movement.speedMultiplier=1.0f;
        eaten=false;
    }
    private void Eaten(){
        eaten=true;
        Vector3 position = ghost.home.inside.position;
        position.z=ghost.transform.position.z;
        ghost.transform.position=position;

        ghost.home.Enable(duration);
        this.body.enabled=false;
        this.eyes.enabled=true;
        this.blue.enabled=false;
        this.white.enabled=false;
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Pacman")){
            if(this.enabled)
                Eaten();
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        Node node = other.GetComponent<Node>();
        if(node!=null && this.enabled)
        {
            Vector2 direction = Vector2.zero;
            float maxDistance = float.MinValue;
            foreach (Vector2 availableDirection in node.availableDirections)
            {
                Vector3 newposition = this.transform.position + new Vector3(availableDirection.x,availableDirection.y,0);
                float distance = (ghost.target.position-newposition).sqrMagnitude;
                if(distance >maxDistance)
                {
                    direction = availableDirection;
                    maxDistance=distance;
                }
            }
            ghost.movement.SetDirection(direction);
        }
}
}
