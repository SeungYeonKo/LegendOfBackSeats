using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    // todo: 플레이어 체력 데이터 불러와서 체력 변수 1당 하트 1개 생성
    public ThirdPersonController Playerinfo;
    private int _previousHealth;
    private int _currentHealth;
    private int _maxHealth;
    private Image _heartImage;

    private void Awake()
    {
        Playerinfo = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
        _heartImage = GetComponentInChildren<Image>();
    }
    void Start()
    {
        _maxHealth = Playerinfo.MaxHealth; // initialize
        _previousHealth = Playerinfo.CurrentHealth; // store initial value
    }

    void LateUpdate()
    {
        if (_previousHealth != Playerinfo.CurrentHealth)
        {
            RefreshUI();
            _previousHealth = Playerinfo.CurrentHealth; // update previous value
        }
    }
    void RefreshUI()
    {
        _currentHealth = Playerinfo.CurrentHealth;
        _heartImage.fillAmount = (float)_currentHealth / _maxHealth;

    }
}
