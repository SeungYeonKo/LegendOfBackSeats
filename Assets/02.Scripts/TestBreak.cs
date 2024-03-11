using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBreak : MonoBehaviour
{
    public Animator _animator;

    void Start()
    {
        StartCoroutine(Break_Coroutine());
    }

    void Update()
    {
        
    }

    private IEnumerator Break_Coroutine()
    {
        yield return new WaitForSeconds(5f);
        _animator.SetTrigger("Breaking Wall");
        
    }
}
