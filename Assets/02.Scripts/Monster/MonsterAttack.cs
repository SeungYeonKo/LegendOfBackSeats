using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    private MonsterMove _owner;
  
    void Start()
    {
        _owner = GetComponentInParent<MonsterMove>();
    }

    public void AttackEvent()
    {
        Debug.Log("어택");
        _owner.PlayerAttack();
    }
}
