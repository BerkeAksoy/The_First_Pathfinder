using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SkeletonManager : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float movementSpeed;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform playerTransform;

    // Attack variables
    [SerializeField] private float attackRadius;        
    public AIPath aiPath;


    



    // Start is called before the first frame update
    void Start()
    {
        // Find the player object using its tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerTransform = playerObject.transform;
        animator.SetBool("isWalking", true);
        
    }

    void Update()
    {        
        

        //if (aiPath.desiredVelocity.x >= 0.01f)
        //{
        //    transform.localScale = new Vector3(1f, 1f, 1f);
        //} else if (aiPath.desiredVelocity.x <= -0.01f)
        //{
        //    transform.localScale = new Vector3(-1f, 1f, 1f);
        //}
    }

    // Skeleton movement and attack code here
    void FixedUpdate()
    {
        // Calculate the direction vector towards the player
        Vector2 directionToPlayer = playerTransform.position - transform.position;

        // Move the Skeleton towards the player if they are within the attack radius
        if (directionToPlayer.magnitude <= attackRadius)
        {

            animator.SetTrigger("attack");
            // TODO: Add code here to damage the player
        }
    }
}

