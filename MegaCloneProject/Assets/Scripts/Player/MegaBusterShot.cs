using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaBusterShot : MonoBehaviour
{
        
    public PlayerController2D player;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }


    public void Shoot()
    {
        //shoot logic  
        
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        animator.Play("ShootAnim");


    }
}
