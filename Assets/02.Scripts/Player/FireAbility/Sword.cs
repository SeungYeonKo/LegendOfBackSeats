using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public int Damage = 2;
    // public float Rate;
    public BoxCollider MeleeArea;
    public TrailRenderer Traileffect;
    private MeleeAttackAbility _meleeAttack;

    private void Start()
    {
        _meleeAttack = GetComponentInParent<MeleeAttackAbility>();
        Damage = 2;
        MeleeArea = GetComponent<BoxCollider>();
        Traileffect = GetComponentInChildren<TrailRenderer>();
        MeleeArea.enabled = false;
        Traileffect.enabled = false;

    }
    public void EnableTrail()
    {
        Traileffect.Clear();
        MeleeArea.enabled = true;
        Traileffect.enabled = true;
    }
    public void DisableTrail()
    {
        //Traileffect.Clear();
        MeleeArea.enabled = false;
        Traileffect.enabled = false;
 
    }
    private void Update()
    {
        if (!_meleeAttack.isActiveAndEnabled)
        {
            this.gameObject.SetActive(false);
        }
    }

/*    private IEnumerator AutoDisable_Coroutine(float timer)
    {
        yield return new WaitForSeconds(timer);
        if (MeleeArea.enabled && Traileffect.enabled)
        {
            MeleeArea.enabled = false;
            Traileffect.enabled = false;
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            IHitable hitMonster = other.GetComponent<IHitable>();
            if (hitMonster != null)
            {
                hitMonster.Hit(Damage);
            }

        }
    }
}