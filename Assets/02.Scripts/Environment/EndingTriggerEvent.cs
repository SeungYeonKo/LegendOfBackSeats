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
        if (other.CompareTag("Player"))
        {
            Debug.Log("Ending Event Trigger");
            // Ending
            // ending effect Coroutine
            Gamemanager.Instance.OnCutScene();
            Gamemanager.Instance.PlayableDirector.Play(Gamemanager.Instance.TimeLines[2]);

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

    }
}
