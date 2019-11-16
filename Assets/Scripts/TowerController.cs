using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;

    void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        float distance = Vector2.Distance(player.transform.position, transform.position);

        //Gravitation toward tower
        Vector3 pullDirection = (transform.position - player.transform.position).normalized;
        float newPullForce = Mathf.Clamp(player.pullForce / distance, 20, 50);
        player.setPulled(pullDirection, newPullForce, distance);
    }

    void OnMouseUp() 
    {
        player.releasePull();   
        GetComponent<SpriteRenderer>().color = Color.white; 
    }
}
