using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public enum BombFireStage
{
    Neutral,
    Carry,
}
public class PlayerBombFireAbility : MonoBehaviour
{
    public AudioSource BoomSound;

    private BombFireStage _currentStage;
    private Animator _animator;
    private bool _isCarrying;
    private bool _isThrown;
    private bool _isBombPrepared;
    public float CoolTime = 5.0f;

    private PlayerArrowFireAbility _arrowFireAbility;
    private MeleeAttackAbility _meleeAttackAbility;
    [Header("Bomb Reload Slider")]
    public Image BombReloadSlider;

    [Header ("Bomb Put Position")]
    public Transform BombPosition;
    public Transform BombPutdownPosition;

    public GameObject BombObject;
    private Rigidbody _bombRigidBody;
    private Bomb _bomb;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _arrowFireAbility = GetComponent<PlayerArrowFireAbility>();
        _meleeAttackAbility = GetComponent<MeleeAttackAbility>();
        _bombRigidBody = BombObject.GetComponent<Rigidbody>();
        _bomb = BombObject.GetComponent<Bomb>();
    }
    void Start()
    {
        _currentStage = BombFireStage.Neutral;
        _isCarrying = false;
        _isBombPrepared = true;
        BombReloadSlider.fillAmount = 0;
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

        if (Input.GetKeyDown(KeyCode.Tab) && !_isThrown && _isBombPrepared)
        {
            _currentStage = BombFireStage.Carry;
            _animator.SetBool("Carry", true);
            //SpawnBomb();
            // Debug.Log("Neutral -> Carry");
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && _isThrown)
        {
            ExplodeBomb();
            _currentStage = BombFireStage.Neutral;

        }
    }
    void CarryBomb()
    {
        _isCarrying = true;
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
           // ThrowBomb();
            Debug.Log("Carry -> Throw -> Neutral");
            _animator.SetBool("Carry", false);
            _animator.SetTrigger("Throw");
        }
        else if (Input.GetMouseButtonDown(1))
        {
            _currentStage = BombFireStage.Neutral;
            PutDown();
            Debug.Log("Carry -> Putdown -> Neutral");
            _animator.SetBool("Carry", false);
            _animator.SetTrigger("Putdown");
        }
    }
    void SpawnBomb()
    {
        if (!BombObject.gameObject.activeSelf)
        {
            _bombRigidBody.isKinematic = true;

            BombObject.SetActive(true);
            BombObject.transform.SetParent(BombPosition);
            BombObject.transform.position = BombPosition.position;
        }
    }
    void ThrowBomb()
    {
        _isThrown = true;
        _isCarrying = false;
        _bombRigidBody.isKinematic = false;
        BombObject.transform.SetParent(null);
        Vector3 throwingDir = transform.forward;
        throwingDir += new Vector3(0, 0.5f, 0);
        _bombRigidBody.AddForce(throwingDir* 5f, ForceMode.Impulse);
        // 던지고 나서 Neutral로 transition
        // 오브젝트 던지기

    }
    void ExplodeBomb()
    {
        _bomb.ExplodeBomb();
        _isThrown = false;
        BoomSound.Play();
        Debug.Log("Explode");
        _isBombPrepared = false;
        BombReloadSlider.fillAmount = 0;
        StartCoroutine(BombPrepareTime_Coroutine(CoolTime));
    }
    private IEnumerator BombSliderValue_Coroutine(float cooltime)
    {
        while (BombReloadSlider.fillAmount < 1)
        {
            BombReloadSlider.fillAmount += Time.deltaTime/5f;
            yield return null;
        }
        BombReloadSlider.fillAmount = 0;

    }
    private IEnumerator BombPrepareTime_Coroutine(float cooltime)
    {
        StartCoroutine(BombSliderValue_Coroutine(CoolTime));
        yield return new WaitForSeconds(cooltime);
        _isBombPrepared = true;
    }
    void PutBack()
    {
        _isCarrying = false;
        BombObject.SetActive(false);
        Debug.Log("Putback -> Neutral");
    }
    void PutDown()
    {
        _isCarrying = false;
        _isThrown = true;

        BombObject.transform.SetParent(null);
        BombObject.transform.position = BombPutdownPosition.position;
        _bombRigidBody.isKinematic = false;
    }
    

}
 