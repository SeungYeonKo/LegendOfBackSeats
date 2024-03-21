using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EndingTriggerEvent : MonoBehaviour
{
    public GameObject[] EffectsToTurnOn;
    public GameObject[] EffectsAlreadyOn;
    private BoxCollider _triggerCollider;
    public float Effect_Gap = 1f;
    public AudioSource EndingBGM;

    private void Awake()
    {
        _triggerCollider = GetComponent<BoxCollider>();
    }
    void Start()
    {

        foreach (var effect in EffectsToTurnOn)
        {
            effect.SetActive(false);
        }
        foreach (var effect in EffectsAlreadyOn)
        {
            effect.SetActive(true);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        // MainBGM 스크립트를 불러옵니다. 이 예제에서는 MainBGM이라는 이름의 스크립트 클래스를 가정합니다.
        MainBGM mainBGM = Camera.main.GetComponent<MainBGM>();

        if (other.CompareTag("Player"))
        {
            if (mainBGM != null)
            {
                // MainBGM 스크립트 내의 MainBGMSound AudioSource를 멈춥니다.
                mainBGM.MainBGMSound.Stop();
                EndingBGM.Play();
            }
            Debug.Log("Ending Event Trigger");
            _triggerCollider.enabled = false;
          
            // Ending
            // ending effect Coroutine


            StartCoroutine(EndingEffect_Coroutine());
        }
    }
    private IEnumerator EndingEffect_Coroutine()
    {
        yield return new WaitForSeconds(Effect_Gap);
        for (int i = 0;i< EffectsToTurnOn.Length;i++)
        {
            EffectsToTurnOn[i].SetActive(true);
            yield return new WaitForSeconds(Effect_Gap);
        }
       
       

        EndingBGM.Play();
        Gamemanager.Instance.OnEnding();
        Gamemanager.Instance.PlayableDirector.Play(Gamemanager.Instance.TimeLines[2]);
    }
}
