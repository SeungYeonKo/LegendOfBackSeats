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
        MeleeArea = GetComponent<BoxCollider>();
        MeleeArea.enabled = false;
    }
    public void Use()
    {
        StartCoroutine(Attack_Coroutine());

    }

    private IEnumerator Attack_Coroutine()
    {
        yield return new WaitForSeconds(0.4f);
        MeleeArea.enabled = true;
        Debug.Log("Sword Collider enabled");
        yield return new WaitForSeconds(0.3f);
        Debug.Log("Sword Collider disabled");
        MeleeArea.enabled = false;
        yield return null;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            IHitable hitMonster = other.GetComponent<IHitable>();
            if (hitMonster != null)
            {
                hitMonster.Hit(Damage);
                Debug.Log(other);
            }
        }
    }
}
