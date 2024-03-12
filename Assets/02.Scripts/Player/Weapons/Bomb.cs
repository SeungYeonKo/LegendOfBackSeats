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
                foreach (Collider collider in _colliders)
                {
                    IHitable hitableObject;
                    if (collider.TryGetComponent<IHitable>(out hitableObject))
                    {
                        hitableObject.Hit(Damage);
                        Debug.Log(hitableObject);
                    }
                }
        this.gameObject.SetActive(false);
    }
}
