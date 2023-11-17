using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    private Animator swordAnimator;
    private Collider2D collider;
    private SpriteRenderer spriteRenderer;

    
    [SerializeField] private GameObject swordTrail;

    

    private void Awake()
    {        
        swordAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        setDamage(10);
    }


    override public void Attack(){        

        spriteRenderer.enabled = true;     
        collider.enabled = true;

        swordAnimator.SetTrigger("Attack");
        swordTrail.SetActive(true);        
               
        StartCoroutine(DisableSword());
    }

    IEnumerator DisableSword(){
        yield return new WaitForSeconds(0.5f);
        swordTrail.SetActive(false);
        collider.enabled = false;
        spriteRenderer.enabled = false;
    }



    public override void UpdatePosition(float direction)
    {
        //right
        if (direction == 0)
            transform.localPosition = new Vector3(-0.25f, 0.05f, 0);
        //up
        else if (direction == 90)
            transform.localPosition = new Vector3(-0.2f, 0.2f, 0);
        //left
        else if (direction == 180)
            transform.localPosition = new Vector3(0.1f, 0.1f, 0.1f);
        //down
        else if (direction == -90)
            transform.localPosition = new Vector3(-0.25f, -0.4f, 0.1f);
    }
}
