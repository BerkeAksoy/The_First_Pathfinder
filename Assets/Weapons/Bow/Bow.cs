using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    [SerializeField] private GameObject Arrow;
    [SerializeField] private float launchForce;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private float time;
    [SerializeField] private Animator BowAnimator;
    private SpriteRenderer spriteRenderer;



    private void Awake()
    {
        BowAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();                
    }



    override public void Attack(){
        spriteRenderer.enabled = true;
        BowAnimator.SetTrigger("isBowAttacking");
        if (!isPlaying(BowAnimator, "flex"))
        {
            StartCoroutine(AttackCoroutine());
        }
    }    


    IEnumerator AttackCoroutine()
    {
        GameObject newArrow = Instantiate(Arrow, shotPoint.position, shotPoint.rotation);
        newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
        yield return new WaitForSeconds(1f);
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(time);
        Destroy(newArrow);        
    }


    bool isPlaying(Animator anim, string stateName)
    {
        if (BowAnimator.GetCurrentAnimatorStateInfo(0).IsName("flex") && BowAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            return true;
        }else{
            return false;
        }
    }



    public override void UpdatePosition(float direction)
    {
        
    }

}
