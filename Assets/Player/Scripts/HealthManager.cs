using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthManager : MonoBehaviour
{

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int health;
    [SerializeField] private int maxShield = 100;
    [SerializeField] private int shield;
    
    [SerializeField] private Material shieldMaterial;
    [SerializeField] private Material parryMaterial;    


    //shield
    private GameObject shieldObject;
    private Renderer shieldRenderer;

    //animation
    private PlayerAnimation playerAnimation;
    private Collider playerCollider;
    private Rigidbody playerRigidbody;    

    //audio
    private AudioSource parryAudio;

    private LayerMask enemyLayer;


    private bool shieldActive = false;
    private bool shieldIsCooldown = false;
    private float shieldCooldownTimer = 0f;
    private Coroutine shieldCoroutine;    


    public int GetHealth() { return health; }

    public int GetShield() { return shield; }

    public bool GetShieldIsCooldown() { return shieldIsCooldown; }

    public float GetShieldCooldownTimer() { return shieldCooldownTimer; }



    private void Awake()
    {
        shieldObject = transform.Find("Shield").gameObject;
        playerAnimation = GetComponent<PlayerAnimation>();
        playerCollider = GetComponent<Collider>();
        playerRigidbody = GetComponent<Rigidbody>();        
        shieldRenderer = shieldObject.GetComponent<Renderer>();
        parryAudio = GetComponent<AudioSource>();

        enemyLayer = LayerMask.GetMask("Enemies");
    }


    // Start is called before the first frame update
    void Start()
    {        
        health = maxHealth;
        shield = maxShield;
                  
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1) && !shieldIsCooldown)
        {
            shieldActive = true;
            shieldRenderer.enabled = true;
            shieldCoroutine = StartCoroutine(ShieldCoroutine());
        }
        if (Input.GetKeyUp(KeyCode.Mouse1) && !shieldIsCooldown)
        {
            StopCoroutine(shieldCoroutine);
            DisableShield();            
        }
    }

    private void DisableShield()
    {        
        shieldActive = false;
        shieldIsCooldown = true;
        shieldCooldownTimer = 5f;
        shieldRenderer.enabled = false;
        StartCoroutine(ShieldCooldownCoroutine());
    }


    private void OnCollisionEnter(Collision collision)
    {   
        if(collision.gameObject.CompareTag("Enemy"))
        {            
            if(shieldActive)
            {
                StartCoroutine(ParryCoroutine()); 
            } else
            {
                Damage(10);
                playerRigidbody.isKinematic = true;
                playerCollider.enabled = false;
                StartCoroutine(DisableCollisionCoroutine());
            }            
        }
    }


    //coroutine called when player is damaged
    IEnumerator DisableCollisionCoroutine()
    {
        
        yield return new WaitForSeconds(1f);

        /* - TODO - animation player hit */

        playerCollider.enabled = true;
        playerRigidbody.isKinematic = false;

    }



    IEnumerator ShieldCoroutine()
    {       
        
        while(shield > 0)
        {
            shieldObject.transform.localScale -= new Vector3(0.02f, 0.02f, 0.02f);

            shield -= 5;                                //50 iterations -> 5 seconds
            yield return new WaitForSeconds(0.1f);
        }

        
        DisableShield();                
        StartCoroutine(playerAnimation.StunCoroutine(3f));        
    }

    IEnumerator ShieldCooldownCoroutine()
    {
        while(shieldCooldownTimer > 0)
        {
            shieldCooldownTimer -= 0.1f;
            if(shield < 100)
                shield += 2;

            yield return new WaitForSeconds(0.1f);
        }
        shieldObject.transform.localScale = new Vector3(2f, 2f, 2f);


        shieldIsCooldown = false;        
        
    }


    IEnumerator ParryCoroutine()
    {
        shieldRenderer.material = parryMaterial;
        parryAudio.Play();
        yield return new WaitForSeconds(0.5f);
        shieldRenderer.material = shieldMaterial;
    }


    


    private void Damage(int damage)
    {        
        health -= damage;

        if(health <= 0)
        {
            health = 0;
            Debug.Log("you died");
        }
    }
}
