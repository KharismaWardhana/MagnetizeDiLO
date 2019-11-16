using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float pullForce = 100f;
    public float rotateSpeed = 360f;
    
    private GameObject closestTower;
    private GameObject hookedTower;
    private Rigidbody2D _playerRig2D;
    private UIController uiControl;
    private AudioSource myAudio;
    private bool isCrashed = false;
    private bool isPulled = false;
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        _playerRig2D = this.gameObject.GetComponent<Rigidbody2D>();
        myAudio = this.gameObject.GetComponent<AudioSource>();
        startPosition = this.gameObject.transform.position;

        uiControl = GameObject.Find("Canvas").GetComponent<UIController>();
    }

    public void setPulled(Vector3 pullDirection, float newPullForce, float distance)
    {
        _playerRig2D.AddForce(pullDirection * newPullForce);

        //Angular velocity
        _playerRig2D.angularVelocity = -rotateSpeed / distance;
        isPulled = true;
    }

    public void releasePull()
    {
        isPulled = false;
    }

    void FixedUpdate()
    {
        if (isCrashed)
        {
            if (!myAudio.isPlaying)
            {
                //Restart scene
                restartPosition();
            }
        }
        else 
        {
            //Move the object
            _playerRig2D.velocity = -transform.up * moveSpeed;  
        }

        // if (Input.GetKeyDown(KeyCode.Z) && !isPulled)
        // {
        //     Debug.Log("checked ONN ==> Z");
        //     if (closestTower != null && hookedTower == null)
        //     {
        //         hookedTower = closestTower;
        //     }

        //     if (hookedTower)
        //     {
        //         float distance = Vector2.Distance(transform.position, hookedTower.transform.position);

        //         //Gravitation toward tower
        //         Vector3 pullDirection = (hookedTower.transform.position - transform.position).normalized;
        //         float newPullForce = Mathf.Clamp(pullForce / distance, 20, 50);
        //         _playerRig2D.AddForce(pullDirection * newPullForce);

        //         //Angular velocity
        //         _playerRig2D.angularVelocity = -rotateSpeed / distance;
        //         isPulled = true;
        //     }
        // }
    
        // if (Input.GetKeyUp(KeyCode.Z))
        // {
        //     Debug.Log("checked Off Z");
        //     isPulled = false;
        //     hookedTower = null;
        // }
    }
    // Update is called once per frame
    void Update()
    {
      
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Tower")
        {
            closestTower = collision.gameObject;
        
            //Change tower color back to green as indicator
            // collision.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            //Hide game object
            if (!isCrashed)
            {
                //Play SFX
                myAudio.Play();
                _playerRig2D.velocity = new Vector3(0f, 0f, 0f);
                _playerRig2D.angularVelocity = 0f;
                isCrashed = true;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Goal")
        {
            Debug.Log("Levelclear!");
            uiControl.endGame();
        }
    }
    
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (isPulled) return;
    
        if (collision.gameObject.tag == "Tower")
        {
            closestTower = null;
    
            //Change tower color back to normal
            // collision.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public void restartPosition()
    {
        //Set to start position
        this.transform.position = startPosition;

        //Restart rotation
        this.transform.rotation = Quaternion.Euler(0f, 0f, 90f);

        //Set isCrashed to false
        isCrashed = false;

        if (closestTower)
        {
            closestTower.GetComponent<SpriteRenderer>().color = Color.white;
            closestTower = null;
        }
            
    }

}
