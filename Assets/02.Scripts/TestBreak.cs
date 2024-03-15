using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBreak : MonoBehaviour, IHitable
{
    public Animator _animator;
    private BoxCollider _collider;

    void Start()
    {

    }
    public void Hit(int amount)
    {
        _animator.SetBool("Break", true);
        _collider = GetComponent<BoxCollider>();
        _collider.enabled = false;
        // todo: 카메라 2초간 비춰주기

    }
    
    void Update()
    {
        
    }

}
