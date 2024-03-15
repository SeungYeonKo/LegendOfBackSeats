using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    private MonsterMove _owner;
  
    void Start()
    {
        _owner = GetComponent<MonsterMove>();
    }

    public void AttackEvent()
    {
        Debug.Log("어택");
        if(_owner.MonsterType == MonsterType.Type1)
        {
            _owner.PlayerAttack();
        }
        
    }
}
