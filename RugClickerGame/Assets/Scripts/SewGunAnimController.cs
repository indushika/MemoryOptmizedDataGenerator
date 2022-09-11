using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SewGunAnimController : MonoBehaviour
{
    private Animator animator;
    public static event Action SewAnimationEnded; 

    void Start()
    {
        SewGun.Sew += OnSew; 
        animator = GetComponent<Animator>(); 
    }

    private void OnSew()
    {
        animator.Play("SewAnimation"); 
    }  

    public void OnSewEnd()
    {
        SewAnimationEnded?.Invoke(); 
    }
}
