using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostChase : GhostBehavior
{
    private void OnDisable() {
        this.ghost.scatter.Enable();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        Node node = other.GetComponent<Node>();
        if(node!=null && this.enabled && !this.ghost.frigtened.enabled)
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;
            foreach (Vector2 availableDirection in node.availableDirections)
            {
                Vector3 newposition = this.transform.position + new Vector3(availableDirection.x,availableDirection.y,0);
                float distance = (ghost.target.position-newposition).sqrMagnitude;
                if(distance <minDistance)
                {
                    direction = availableDirection;
                    minDistance=distance;
                }
            }
            ghost.movement.SetDirection(direction);
        }
    }
}
