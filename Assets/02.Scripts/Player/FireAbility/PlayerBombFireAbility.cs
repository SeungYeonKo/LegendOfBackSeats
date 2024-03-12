using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public enum BombFireStage
{
    Neutral,
    Carry,
}
public class PlayerBombFireAbility : MonoBehaviour
{
    private BombFireStage _currentStage;
    private Animator _animator;
    public bool IsCarrying;
    public bool IsThrown;
    private PlayerArrowFireAbility _arrowFireAbility;
    private MeleeAttackAbility _meleeAttackAbility;
    public Transform BombPosition;
    public GameObject Bomb;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _arrowFireAbility = GetComponent<PlayerArrowFireAbility>();
        _meleeAttackAbility = GetComponent<MeleeAttackAbility>();
    }
    void Start()
    {
        _currentStage = BombFireStage.Neutral;
        IsCarrying = false;
    }
    void Update()
    {
        switch (_currentStage)
        {
            case BombFireStage.Neutral:
                Neutral();
                break;
            case BombFireStage.Carry:
                CarryBomb();
                break;

        }
    }
    void Neutral()
    {
        _meleeAttackAbility.enabled = true;
        _arrowFireAbility.enabled = true;

        if (Input.GetKeyDown(KeyCode.Tab) && !IsThrown)
        {
            _currentStage = BombFireStage.Carry;
            _animator.SetBool("Carry", true);
            SpawnBomb();
            Debug.Log("Neutral -> Carry");
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && IsThrown)
        {
            ExplodeBomb();
            _currentStage = BombFireStage.Neutral;

        }
    }
    void CarryBomb()
    {
        IsCarrying = true;
        _meleeAttackAbility.enabled = false;
        _arrowFireAbility.enabled = false;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _currentStage = BombFireStage.Neutral;
            PutBack();
            _animator.SetBool("Carry", false);
            Debug.Log("Carry -> Putback -> Neutral");
        }
        if (Input.GetMouseButtonDown(0))
        {
            _currentStage = BombFireStage.Neutral;
            ThrowBomb();
            Debug.Log("Carry -> Throw -> Neutral");
            _animator.SetBool("Carry", false);
            _animator.SetTrigger("Throw");
        }
    }
    void SpawnBomb()
    {
        if (!Bomb.gameObject.activeSelf)
        {
            Bomb.SetActive(true);
            //Bomb.transform = BombPosition;
        }
    }
    void ThrowBomb()
    {
        IsThrown = true;
        IsCarrying = false;
        // 던지고 나서 Neutral로 transition
        // 오브젝트 던지기

    }
    void ExplodeBomb()
    {
        Bomb.SetActive(false);
        IsThrown = false;
        Debug.Log("Explode");
    }
    void PutBack()
    {
        IsCarrying = false;
        Debug.Log("Putback -> Neutral");
    }

    

}
 