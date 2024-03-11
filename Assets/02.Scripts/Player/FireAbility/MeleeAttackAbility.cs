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
        if (Input.GetMouseButtonDown(0) && MeleeWeapon.gameObject.activeSelf == true)
        {
            _playerAnimator.SetTrigger("Attack");
        }

        if (Input.GetMouseButton(1))
        {
            MeleeWeapon.SetActive(false);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            MeleeWeapon.SetActive(true);
        }

    }
    void UseSwordForAnimator()
    {
        _sword.Use();
    }
}
