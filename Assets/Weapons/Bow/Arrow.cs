using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Weapon
{

    private void Awake()
    {
        setDamage(5);
    }

    public override void Attack() {  }

    public override void UpdatePosition(float direction)
    {
        
    }


}
