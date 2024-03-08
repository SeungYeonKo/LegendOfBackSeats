using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public enum MonsterState
{
    Idle,
    Patrol,
    Trace,
    Attack,
    Comeback,
    Damaged,
    Die
}

public class MonsterMove : MonoBehaviour, IHitable
{
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private MonsterState _currentState = MonsterState.Idle;

    // 체력
    public int Health;
    public int MaxHealth = 10;
    public Slider HealthSliderUI;

    // 이동
    public float MoveSpeed = 4f;
    public Vector3 StartPosition;

    // 공격
    public int Damage = 3;
    public const float AttackDelay = 0.8f;
    private float _delayTimer = 0f;

    // AI
    private Transform _target;
    public float FindDistance = 10f;
    public float AttackDistance = 3f;
    public float MoveDistance = 40f;
    public const float TOLERANCE = 0.1f;        //목적지 도착 판단하는 오차 범위 상수
    private const float IDLE_DURATION = 3f;
    private float _idleTimer;
    //public Transform PatrolTarget;

    // 랜덤하게 순찰지점 설정
    private Vector3 Destination;
    public float MovementRange = 30f;  // 순찰 범위

    // 넉백
    private Vector3 _knockbackStartPosition;
    private Vector3 _knockbackEndPosition;
    private const float KNOCKBACK_DURATION = 0.1f;
    private float _knockbackProgress = 0f;
    public float KnockbackPower = 1.2f;


    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        Destination = _navMeshAgent.transform.position;
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _delayTimer = 0f;
        StartPosition = transform.position;
        Init();
    }
    public void Init()
    {
        _idleTimer = 0f;
        Health = MaxHealth;
    }

    void Update()
    {
        HealthSliderUI.value = (float)Health / (float)MaxHealth;

        switch (_currentState)
        {
            case MonsterState.Idle:
                Idle();
                break;

            case MonsterState.Trace:
                Trace();
                break;

            case MonsterState.Patrol:
                Patrol();
                break;

            case MonsterState.Comeback:
                Comeback();
                break;

            case MonsterState.Attack:
                Attack();
                break;

            case MonsterState.Damaged:
                Damaged();
                break;

            case MonsterState.Die:
                Die();
                break;
        }
    }

    private void Idle()
    {
        _idleTimer += Time.deltaTime;

        if (_idleTimer >= IDLE_DURATION)
        {
            _idleTimer = 0f;
            Debug.Log("Monster : Idle -> Patrol");
            _animator.SetTrigger("IdleToPatrol");
            _currentState = MonsterState.Patrol;
        }

        if (Vector3.Distance(_target.position, transform.position) <= FindDistance && _idleTimer >= IDLE_DURATION / 2)
        {
            Debug.Log("Monster: Idle -> Trace");
            _animator.SetTrigger("IdleToTrace");
            _currentState = MonsterState.Trace;
        }


    }

    private void Trace()
    {
        Vector3 dir = _target.transform.position - this.transform.position;
        dir.y = 0;
        dir.Normalize();

        _navMeshAgent.stoppingDistance = AttackDistance;

        _navMeshAgent.destination = _target.position;

        if((Vector3.Distance(_target.position, transform.position) >= FindDistance))
        {
            Debug.Log("Monster : Trace -> Comeback");
            _animator.SetTrigger("TraceToComeback");
            _currentState = MonsterState.Comeback;
        }
    }

    private void Patrol()
    {
        if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= TOLERANCE)
        {
            MoveToRandomPosition();
        }

        // 플레이어가 감지 범위 내에 있으면 상태를 Trace로 변경하여 플레이어를 추적
        if (Vector3.Distance(_target.position, transform.position) <= FindDistance)
        {
            Debug.Log("Monster : Patrol -> Trace");
            _animator.SetTrigger("PatrolToTrace");
            _currentState = MonsterState.Trace;
        }

        // 추가: Patrol 상태에서 일정 시간 대기 후 Comeback으로 전환
        if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= TOLERANCE)
        {
            StartCoroutine(WaitAndComeback());
        }
    }

    private IEnumerator WaitAndComeback()
    {
        yield return new WaitForSeconds(2f);  // 대기 시간 조절 필요
        Debug.Log("Monster : Patrol -> Comeback");
        _animator.SetTrigger("PatrolToComeback");
        _currentState = MonsterState.Comeback;
    }

    private void MoveToRandomPosition()
    {
        // 일정 범위 내에서 랜덤한 위치로 이동
        Vector3 randomDirection = Random.insideUnitSphere * MovementRange;
        randomDirection += _navMeshAgent.destination;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, MovementRange, NavMesh.AllAreas);
        Vector3 targetPosition = hit.position;
        _navMeshAgent.SetDestination(targetPosition);
        Destination = targetPosition;
    }

    private void Comeback()
    {
        Vector3 dir = StartPosition - this.transform.position;
        dir.y = 0;
        dir.Normalize();

        _navMeshAgent.stoppingDistance = TOLERANCE;

        _navMeshAgent.destination = StartPosition;

        if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= TOLERANCE)
        {
            Debug.Log("Monster : Comeback -> idle");
            _animator.SetTrigger("ComebackToIdle");

            _currentState = MonsterState.Idle;
        }
    }

    private void Attack()
    {
        if (Vector3.Distance(_target.position, transform.position) > AttackDistance)
        {
            _delayTimer = 0f;
            Debug.Log("Monster: Attack -> Trace");
            _animator.SetTrigger("AttackToTrace");

            _currentState = MonsterState.Trace;
            return;
        }
        _delayTimer += Time.deltaTime;
        if (_delayTimer >= AttackDelay)
        {
            _animator.SetTrigger("Attack");
        }
    }
    private void Damaged()
    {
        // 넉백
        if (_knockbackProgress == 0)
        {
            _knockbackStartPosition = transform.position;

            Vector3 dir = transform.position - _target.position;
            dir.y = 0;
            dir.Normalize();

            _knockbackEndPosition = transform.position + dir * KnockbackPower;
        }
        _knockbackProgress += Time.deltaTime / KNOCKBACK_DURATION;
        transform.position = Vector3.Lerp(_knockbackStartPosition, _knockbackEndPosition, _knockbackProgress);
        _animator.SetTrigger("Damaged");
        Debug.Log("Monster : 넉백!");

        if (_knockbackProgress > 1)
        {
            _knockbackProgress = 0f;

            Debug.Log("Monster : Damaged -> Trace");
            _animator.SetTrigger("DamagedToTrace");
            _currentState = MonsterState.Trace;
        }
    }

    public void Hit(int amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            Debug.Log("Monster : Any -> Die");

            _animator.SetTrigger("Die");
            _currentState = MonsterState.Die;
        }
        else
        {
            Debug.Log("Monster : Any -> Damaged");
            _animator.SetTrigger("Damaged");
            _currentState = MonsterState.Damaged;
        }
    }

    private Coroutine _dieCoroutine;
    private void Die()
    {
        // 죽을때 아이템 생성
        //ItemObjectFactory.Instance.MakePercent(transform.position);
        if (_dieCoroutine == null)
        {
            Debug.Log("Monster : Any -> Die");

            _animator.SetTrigger("Die");
            _currentState = MonsterState.Die;
            _dieCoroutine = StartCoroutine(Die_Coroutine());
        }
    }
    private IEnumerator Die_Coroutine()
    {
        _navMeshAgent.isStopped = true;
        _navMeshAgent.ResetPath();

        HealthSliderUI.gameObject.SetActive(false);

        yield return new WaitForSeconds(2f);

        Destroy(gameObject);

        // 죽을때 아이템 생성
        //ItemObjectFactory.Instance.MakePercent(transform.position);
    }
    public void PlayerAttack()
    {
        IHitable playerHitable = _target.GetComponent<IHitable>();
        if (playerHitable != null)
        {
            Debug.Log("때렸다!");

            DamageInfo damageInfo = new DamageInfo(DamageType.Normal, Damage);
            playerHitable.Hit(Damage);
            _delayTimer = 0f;
        }
    }
}