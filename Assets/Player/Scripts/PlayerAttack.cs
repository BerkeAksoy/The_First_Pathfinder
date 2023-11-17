using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private Transform weapons;
    //private List<GameObject> allWeapons;
    private GameObject currentWeapon;
    private GameObject currentWeapon2;



    /* - Weapons Prefab List - */
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject bow;






    private void Awake()
    {        

        weapons = GameObject.FindGameObjectWithTag("Weapons").transform;

        currentWeapon = sword;
        currentWeapon2 = bow;        

    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            Weapon script = currentWeapon.GetComponent<Weapon>();

            script.Attack();
            
        }

        //change weapon
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            Weapon script = currentWeapon2.GetComponent<Weapon>();

            script.Attack();
        }

    }


}
