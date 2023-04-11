using UnityEngine;

public class GhostScater : GhostBehavior
{
    private void OnDisable() {
        this.ghost.chase.Enable();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        Node node = other.GetComponent<Node>();
        if(node!=null && this.enabled && !this.ghost.frigtened.enabled)
        {
            int index = Random.Range(0,node.availableDirections.Count);
            if(node.availableDirections[index] == -ghost.movement.direction&& node.availableDirections.Count>1)
            {
                index++;

                if(index >=node.availableDirections.Count)
                    index=0;
            }
            ghost.movement.SetDirection(node.availableDirections[index]);
        }
    }
}
