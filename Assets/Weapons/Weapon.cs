using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{

    public int damage;    


    public void setDamage(int damage)
    {
        this.damage = damage;
    }

    public void getTransformPlayer()
    {
        GetComponentInParent<Transform>().parent = transform;
    }

    //public float getDirection()
    //{
    //    return GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAnimation>().getDirection();
    //}

    public LayerMask getLayer() { return LayerMask.GetMask("Enemies"); }

    public abstract void Attack();

    public abstract void UpdatePosition(float direction);


    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
            collision.gameObject.GetComponent<EnemyHealth>().Damage(damage);
    }


}
