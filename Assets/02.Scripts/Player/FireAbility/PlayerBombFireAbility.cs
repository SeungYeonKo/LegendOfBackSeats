using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public enum BombFireStage
{
    Neutral,
    Carry,
    Throw,
    Putback
}
public class PlayerBombFireAbility : MonoBehaviour
{
    private BombFireStage _currentStage;

    void Start()
    {
        _currentStage = BombFireStage.Neutral;
    }
    void Update()
    {
        switch (_currentStage)
        {
            case BombFireStage.Neutral:
                break;
            case BombFireStage.Carry:

                    CarryBomb();


                break;
            case BombFireStage.Throw:
                ThrowBomb();
                break;
            case BombFireStage.Putback:
                PutBack();
                break;

        }
    }
    void CarryBomb()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _currentStage = BombFireStage.Throw;
        }
    }
    void ThrowBomb()
    {
        // 던지고 나서 Neutral로 transition
    }
    void PutBack()
    {

    }

    

}
 