using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    private PlayerArrowFireAbility _arrowAttackScript;
    void Start()
    {
        _arrowAttackScript = GetComponentInParent<PlayerArrowFireAbility>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!_arrowAttackScript.isActiveAndEnabled)
        {
            GetComponent<Renderer>().enabled = false;
        }
        else
        {
            GetComponent<Renderer>().enabled = true;
        }
    }
}
