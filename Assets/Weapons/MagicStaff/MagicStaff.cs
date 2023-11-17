using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicStaff : MonoBehaviour
{
    [SerializeField]
    private GameObject Ball;
    [SerializeField]
    private float launchForce;
    [SerializeField]
    private Transform shotPoint;
    [SerializeField]
    private float time;
    [SerializeField]
    private Animator StaffAnimator;
    [SerializeField]
    private GameObject MagicStaffGlow;
    [SerializeField]
    private float timeDisableGlow;

    // Update is called once per frame
    void Update()
    {
        Vector2 bowPosition = transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - bowPosition;
        transform.right = direction;

        if(Input.GetMouseButtonDown(0) ){
            MagicStaffGlow.SetActive(true);
            StartCoroutine(Shoot());
            StartCoroutine(DisableMagicStaffGlow());
        }
    }

    IEnumerator DisableMagicStaffGlow(){
        yield return new WaitForSeconds(timeDisableGlow);
        MagicStaffGlow.SetActive(false);  
    }

    IEnumerator Shoot(){
        GameObject newBall = Instantiate(Ball, shotPoint.position, shotPoint.rotation);
        newBall.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
        yield return new WaitForSeconds(time);
        Destroy (newBall);
    }

    bool isPlaying(Animator anim, string stateName)
    {
        if (StaffAnimator.GetCurrentAnimatorStateInfo(0).IsName("MagicStaff") && StaffAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            return true;
        }else{
            return false;
        }
    }


}
