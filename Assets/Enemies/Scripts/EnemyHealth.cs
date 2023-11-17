using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 30;
    [SerializeField] private int health;    


    public int GetHealth() { return health; }



    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;        
    }


    public void Damage(int damage)
    {
        
        if(damage > 0)
        {
            health -= damage;
            StartCoroutine(HitAnimation());            
        }
            

        if (health <= 0)
        {
            health = 0;            
            Destroy(gameObject);
        }
    }


    IEnumerator HitAnimation()
    {
        GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        yield return new WaitForSeconds(0.5f);
        GetComponent<Renderer>().material.SetColor("_Color", Color.white);

    }
}
