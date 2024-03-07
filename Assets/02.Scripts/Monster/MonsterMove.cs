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
    public float FindDistance = 10f;
    public float AttackDistance = 3f;
    public float MoveDistance = 40f;
    public const float TOLERANCE = 0.1f;        //목적지 도착 판단하는 오차 범위 상수
    private const float IDLE_DURATION = 3f;
    private float _idleTimer;
    //public Transform PatrolTarget;

    // 몬스터가 이동할 랜덤한 순찰지점을 저장할 리스트
    private List<Vector3> randomPatrolPoints = new List<Vector3>();
    // 현재 몬스터가 이동 중인 랜덤 순찰 지점의 인덱스
    private int currentPatrolIndex = 0;


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
        _navMeshAgent.speed = MoveSpeed;
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

        GenerateRandomPatrolPoints();   // 몬스터 개별 관리
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

        if (randomPatrolPoints != null && _idleTimer >= IDLE_DURATION)
        {
            _idleTimer = 0f;
            Debug.Log("상태 전환: Idle -> Patrol");
            _animator.SetTrigger("IdleToPatrol");
            _currentState = MonsterState.Patrol;
        }

        if (Vector3.Distance(_target.position, transform.position) <= FindDistance)
        {
            Debug.Log("상태 전환: Patrol -> Trace");
            _animator.SetTrigger("PatrolToTrace");
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
        if (randomPatrolPoints.Count == 0)
        {
            GenerateRandomPatrolPoints();
        }

        _navMeshAgent.SetDestination(randomPatrolPoints[currentPatrolIndex]);
        _navMeshAgent.stoppingDistance = 0f;

        if (randomPatrolPoints.Count == 0)
        {
            GenerateRandomPatrolPoints();   //랜덤 순찰 지점이 없다면 새로 생성
        }

        // 현재 몬스터가 이동 중인 랜덤 순찰 지점으로 목적지 설정
        _navMeshAgent.SetDestination(randomPatrolPoints[currentPatrolIndex]);
        // 목적지에 도달하면 상태를 Comeback으로 변경하고 돌아가도록 설정
        if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= TOLERANCE)
        {
            Debug.Log("상태 전환: Patrol -> Comeback");
            _animator.SetTrigger("PatrolToComeback");
            _currentState = MonsterState.Comeback;
        }

        // 플레이어가 근처에 있으면 상태를 Trace로 변경하여 플레이어를 추적
        if (Vector3.Distance(_target.position, transform.position) <= FindDistance)
        {
            Debug.Log("상태 전환: Patrol -> Trace");
            _animator.SetTrigger("PatrolToTrace");
            _currentState = MonsterState.Trace;
        }
    }

    public void GenerateRandomPatrolPoints()   //랜덤 순찰지점 생성
    {
        randomPatrolPoints.Clear();

        // 몬스터 시작 위치를 중심으로 정해진 반경 내에서 랜덤한 순찰 지점을 생성
        for (int i = 0; i < 10; i++)        // 10개의 랜덤 지점을 생성하게 반복
        {
            Vector3 randomPoint = StartPosition + new Vector3(Random.Range(-10f, 50f), 0f, Random.Range(-10f, 50f));
            randomPatrolPoints.Add(randomPoint);        // 생성된 랜덤 지점 리스트에 추가
        }
    }
    private void UpdatePatrolIndex()
    {
        currentPatrolIndex = (currentPatrolIndex + 1) % randomPatrolPoints.Count;   // 다음 랜던 순찰 지점 업데이트
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

            Debug.Log("몬스터 : Damaged -> Trace");
            _animator.SetTrigger("DamagedToTrace");
            _currentState = MonsterState.Trace;
        }


    }


    public void Hit(DamageInfo damage)
    {
        Health -= damage.Amount;
        if (Health <= 0)
        {
            Debug.Log("상태 전환: Any -> Die");

            _animator.SetTrigger($"Die{Random.Range(1, 3)}");
            _currentState = MonsterState.Die;
        }
        else
        {
            Debug.Log("상태 전환: Any -> Damaged");
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
            _attackTimer = 0f;
        }
    }
}