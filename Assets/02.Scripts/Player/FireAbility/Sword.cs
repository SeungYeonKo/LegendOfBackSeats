using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public int Damage = 2;
    // public float Rate;
    public BoxCollider MeleeArea;
    public TrailRenderer Traileffect;

    private void Start()
    {
        Damage = 2;
        MeleeArea = GetComponent<BoxCollider>();
        Traileffect = GetComponentInChildren<TrailRenderer>();
        MeleeArea.enabled = false;
        Traileffect.enabled = false;

    }
    public void Use()
    {
        StartCoroutine(Attack_Coroutine());

    }

    private IEnumerator Attack_Coroutine()
    {
        yield return new WaitForSeconds(0.1f);
        MeleeArea.enabled = true;
        Traileffect.enabled = true;
        Debug.Log("Sword Collider enabled");
        yield return new WaitForSeconds(0.25f);
        Traileffect.enabled = false;
        MeleeArea.enabled = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            IHitable hitMonster = other.GetComponent<IHitable>();
            if (hitMonster != null)
            {
                Debug.Log("hit");

                hitMonster.Hit(Damage);
            }

        }
    }
}