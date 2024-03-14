using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackAbility : MonoBehaviour
{
    private Animator _playerAnimator;
    public GameObject MeleeWeapon;
    private PlayerArrowFireAbility _bowFireAbility;

    private Sword _sword;

    void Start()
    {
        _playerAnimator = GetComponent<Animator>();
        _sword = MeleeWeapon.GetComponent<Sword>();
        _bowFireAbility = GetComponent<PlayerArrowFireAbility>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && MeleeWeapon.gameObject.activeSelf == true)
        {
            _playerAnimator.SetTrigger("Attack");
        }

        if (_bowFireAbility.IsAiming)
        {
            MeleeWeapon.SetActive(false);
        }
        else if (!_bowFireAbility.IsAiming)
        {
            MeleeWeapon.SetActive(true);
        }

    }
    void SetActiveSword()
    {
        _sword.EnableTrail();
    }
    void SetDisableSword()
    {
        _sword.DisableTrail();
    }
}
