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

    void OnEnable()
    {
        state = BombState.Carry;
    }
    void Update()
    {
        
    }
    public void ExplodeBomb()
    {

            this.gameObject.SetActive(false);
    }
}
