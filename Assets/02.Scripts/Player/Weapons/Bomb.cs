using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BombState
{
    Carry,
    Placed
}
public class Bomb : MonoBehaviour
{
    public BombState state { get; set; }
    public float BurstingExplosionRadius = 3f;
    private Collider[] _colliders = new Collider[10];
    public int Damage = 10;

    void OnEnable()
    {
        state = BombState.Carry;
    }
    void Update()
    {
        
    }
    public void ExplodeBomb()
    {

        int layer = (LayerMask.GetMask("Monster") | LayerMask.GetMask("Player") | LayerMask.GetMask("Breakable"));
        int count = Physics.OverlapSphereNonAlloc(transform.position, BurstingExplosionRadius, _colliders, layer);
        Debug.Log(count);
        for (int i = 0; i < count; i++)
        {
            Collider c = _colliders[i];
            IHitable hitableObject = c.GetComponent<IHitable>();
            if (hitableObject != null)
            {
                hitableObject.Hit(Damage);
                Debug.Log(hitableObject);
            }
        }
        this.gameObject.SetActive(false);
    }
}
