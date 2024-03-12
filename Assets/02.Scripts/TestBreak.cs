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
    }
    
    void Update()
    {
        
    }

    private IEnumerator Break_Coroutine()
    {
        yield return new WaitForSeconds(5f);
        _animator.SetBool("Break", true);
        
    }
}
