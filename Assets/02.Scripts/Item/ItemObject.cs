using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public enum ItemState
    {
        Idle,
        Trace
    }
    public ItemType ItemType;
    private ItemState _itemState = ItemState.Idle;

    private Transform _player;
    public float EatDistance = 5f;

    private Vector3 _startPosition;
    private const float TRACE_DURATION = 0.3f;
    private float _progress = 0;

    public int Count;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
        _startPosition = transform.position;
    }

    private void Update()
    {
        switch (_itemState)
        {
            case ItemState.Idle:
                Idle();
                break;

            case ItemState.Trace:
                Trace();
                break;
        }
    }
    public void Init()
    {
        _startPosition = transform.position;
        _progress = 0f;
        _traceCoroutine = null;
        _itemState = ItemState.Idle;
    }

    private void Idle()
    {
        float distance = Vector3.Distance(_player.position, transform.position);
        if (distance <= EatDistance)
        {
            _itemState = ItemState.Trace;
        }
    }

    private Coroutine _traceCoroutine;
    private void Trace()
    {
        if (_traceCoroutine == null)
        {
            _traceCoroutine = StartCoroutine(Trace_Coroutine());
        }
    }

    private IEnumerator Trace_Coroutine()
    {
        while (_progress < 0.6)
        {
            _progress += Time.deltaTime / TRACE_DURATION;
            transform.position = Vector3.Slerp(_startPosition, _player.position, _progress);

            yield return null;
        }

        ItemManager.Instance.AddItem(ItemType, Count);
        gameObject.SetActive(false);
    }
}
