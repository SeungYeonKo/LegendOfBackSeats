using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBreak : MonoBehaviour, IHitable
{
    public Animator _animator;

    void Start()
    {
       // StartCoroutine(Break_Coroutine());
    }
    public void Hit(int amount)
    {
        _animator.SetBool("Break", true);
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
