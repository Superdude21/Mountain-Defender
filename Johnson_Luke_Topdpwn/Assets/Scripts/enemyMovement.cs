using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    private AudioSource mySpeaker;
    public AudioClip flyingsound1;
    public AudioClip flyingsound2;
    public AudioClip explosionSound;
    public float detectionRadius = 3;
    public float movementSpeed = 5;
    public bool canMove = false;
    public bool movementDirection = false;
    public bool isFollowing = false;

    public Transform playerTarget;
    private Rigidbody2D myRB;
    private CircleCollider2D detectionZone; 
    private Vector2 up;
    private Vector2 down;
    private Vector2 zero;

    // Start is called before the first frame update
    void Start()
    {
        up = new Vector2(0, movementSpeed);
        down = new Vector2(0, -movementSpeed);
        zero = new Vector2(0, 0);

        playerTarget = GameObject.Find("playerSprite").transform;

        myRB = GetComponent<Rigidbody2D>();
        mySpeaker = GetComponent<AudioSource>();
        detectionZone = GetComponent<CircleCollider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        detectionZone.radius = detectionRadius;

        if (isFollowing == false)
            myRB.velocity = zero;

        else if (isFollowing == true)
        {
            Vector3 lookPos = playerTarget.position - transform.position;
            float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
            myRB.rotation = angle;
            lookPos.Normalize();

            myRB.MovePosition(transform.position + (lookPos * movementSpeed * Time.deltaTime));
        }

         if (canMove == true)
        {

                if (movementDirection == true)
                    myRB.velocity = up;

                else if (movementDirection == false)
                    myRB.velocity = down;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Bullet"))
        {
            Destroy(collision.gameObject);
            mySpeaker.clip = explosionSound;
            mySpeaker.Play();
            FindObjectOfType<GameManager>().playerKillCount++;
            Destroy(this.gameObject, 0.1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("player"))
            isFollowing = true;

        if (collision.gameObject.name == "trigger1")
            movementDirection = false;

        else if (collision.gameObject.name == "trigger2")
            movementDirection = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("player"))
            isFollowing = false;
    }
}
