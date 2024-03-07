using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackAbility : MonoBehaviour
{
    private Animator _playerAnimator;
    public GameObject MeleeWeapon;
    private Sword _sword;

    void Start()
    {
        _playerAnimator = GetComponent<Animator>();
        _sword = MeleeWeapon.GetComponent<Sword>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _playerAnimator.SetTrigger("Attack");
            _sword.Use();
        }
    }
}
