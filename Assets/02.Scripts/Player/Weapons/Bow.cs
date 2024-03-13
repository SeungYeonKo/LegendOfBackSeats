using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    private PlayerArrowFireAbility _arrowAttackScript;
    void Start()
    {
        _arrowAttackScript = GetComponent<PlayerArrowFireAbility>();

    }

    // Update is called once per frame
    void Update()
    {
 /*       if (!_arrowAttackScript.isActiveAndEnabled)
        {
            this.gameObject.SetActive(false);
        }*/
    }
}
