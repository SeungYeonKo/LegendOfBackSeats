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

public class MonsterMove : MonoBehaviour
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
    private float _attackTimer = 0f;

    // AI
    private Transform _target;
    public float FindDistance = 1f;
    public float AttackDistance = 3f;
    public float MoveDistance = 40f;
    public const float TOLERANCE = 0.1f;
    private const float IDLE_DURATION = 3f;
    private float _idleTimer;
    public Transform PatrolTarget;

    // 넉백
    private Vector3 _knockbackStartPosition;
    private Vector3 _knockbackEndPosition;
    private const float KNOCKBACK_DURATION = 0.1f;
    private float _knockbackProgress = 0f;
    public float KnockbackPower = 1.2f;


    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = MoveSpeed;

        _animator = GetComponent<Animator>();
       /* if (_animator != null)
        {
            Debug.Log("뭐임진짜");
        }
        else
        {
            Debug.Log("Animator 컴포넌트가 연결되어 있지 않습니다. MonsterType_1 게임 오브젝트에 Animator를 추가하세요.");
        }*/
        _target = GameObject.FindGameObjectWithTag("Player").transform;

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

        switch(_currentState)
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
        }
    }

    private void Idle()
    {
        _idleTimer += Time.deltaTime;

        if (PatrolTarget != null && _idleTimer >= IDLE_DURATION)
        {
            _idleTimer = 0f;
            Debug.Log("상태 전환: Idle -> Patrol");
            _animator.SetTrigger("IdleToPatrol");
            _currentState = MonsterState.Patrol;
        }

        if (Vector3.Distance(_target.position, transform.position) <= FindDistance)
        {
            Debug.Log("상태 전환: Idle -> Trace");
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
    }

    private void Patrol()
    {
        _navMeshAgent.stoppingDistance = 0f;
        _navMeshAgent.SetDestination(PatrolTarget.position);

        if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= TOLERANCE)
        {
            Debug.Log("상태 전환: Patrol -> Comeback");
            _animator.SetTrigger("PatrolToComeback");
            _currentState = MonsterState.Comeback;
        }

        if (Vector3.Distance(_target.position, transform.position) <= FindDistance)
        {
            Debug.Log("상태 전환: Patrol -> Trace");
            _animator.SetTrigger("PatrolToTrace");
            _currentState = MonsterState.Trace;
        }
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
            Debug.Log("상태 전환: Comeback -> idle");
            _animator.SetTrigger("ComebackToIdle");

            _currentState = MonsterState.Idle;
        }
        if (Vector3.Distance(StartPosition, transform.position) <= TOLERANCE)
        {
            Debug.Log("상태 전환: Comeback -> idle");
            _animator.SetTrigger("ComebackToIdle");

            _currentState = MonsterState.Idle;
        }
    }

    private void Attack()
    {
        if (Vector3.Distance(_target.position, transform.position) > AttackDistance)
        {
            _attackTimer = 0f;
            Debug.Log("상태 전환: Attack -> Trace");
            _animator.SetTrigger("AttackToTrace");

            _currentState = MonsterState.Trace;
            return;
        }
        _attackTimer += Time.deltaTime;
        if (_attackTimer >= AttackDelay)
        {
            _animator.SetTrigger("Attack");
        }
    }

    private void Damaged()
    {
        if (_knockbackProgress == 0)
        {
            _knockbackStartPosition = transform.position;

            Vector3 dir = transform.position - _target.position;
            dir.y = 0;
            dir.Normalize();

            _knockbackEndPosition = transform.position + dir * KnockbackPower;
        }

        _knockbackProgress += Time.deltaTime / KNOCKBACK_DURATION;

        // 2-2. Lerp를 이용해 넉백하기
        transform.position = Vector3.Lerp(_knockbackStartPosition, _knockbackEndPosition, _knockbackProgress);

        if (_knockbackProgress > 1)
        {
            _knockbackProgress = 0f;

            Debug.Log("상태 전환: Damaged -> Trace");
            _animator.SetTrigger("DamagedToTrace");
            _currentState = MonsterState.Trace;
        }
    }
    private void Die()
    {
        // 죽을때 아이템 생성
        //ItemObjectFactory.Instance.MakePercent(transform.position);

        Destroy(gameObject);
    }

    public void Hit(DamageInfo damage)
    {
        Health -= damage.Amount;
        if (Health <= 0)
        {
            Die();
        }
        else
        {
            Debug.Log("상태 전환: Any -> Damaged");
            _animator.SetTrigger("Damaged");
            _currentState = MonsterState.Damaged;
        }
    }

    public void PlayerAttack()
    {
        IHitable playerHitable = _target.GetComponent<IHitable>();
        if (playerHitable != null)
        {
            Debug.Log("때렸다!");

            DamageInfo damageInfo = new DamageInfo(DamageType.Normal, Damage);
            playerHitable.Hit(damageInfo);
            _attackTimer = 0f;
        }
    }
}
