using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHome : GhostBehavior
{
    [SerializeField]public Transform inside;
    [SerializeField]public Transform outside;

    private void OnEnable() {
        StopAllCoroutines();
    }
    private void OnDisable() {
        if(gameObject.activeSelf)
            StartCoroutine(Exit());
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(enabled && other.gameObject.layer==LayerMask.NameToLayer("Obstacle"))
            ghost.movement.SetDirection(-ghost.movement.direction);
    }
    private IEnumerator Exit(){
        this.ghost.scatter.Enable();
        this.ghost.movement.SetDirection(Vector2.up,true);
        this.ghost.movement.rigidbody.isKinematic = true;
        this.ghost.movement.enabled=false;

        Vector3 position = transform.position;
        float duration = 0.5f;
        float elapsed=0.0f;

        while(elapsed<duration)
        {
            Vector3 newposition = Vector3.Lerp(position,this.inside.position,elapsed/duration);
            newposition.z=position.z;
            this.ghost.transform.position = newposition;
            elapsed+=Time.deltaTime;
            yield return null;
        }
        elapsed=0.0f;
        while(elapsed<duration)
        {
            Vector3 newposition = Vector3.Lerp(this.inside.position,this.outside.position,elapsed/duration);
            newposition.z=position.z;
            this.ghost.transform.position = newposition;
            elapsed+=Time.deltaTime;
            yield return null;
        }

        ghost.movement.SetDirection(new Vector2(Random.value < 0.5f ? -1f : 1f, 0f), true);
        ghost.movement.rigidbody.isKinematic = false;
        ghost.movement.enabled = true;
    }
}
