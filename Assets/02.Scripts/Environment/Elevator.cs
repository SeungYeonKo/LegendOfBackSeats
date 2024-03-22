using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Elevator : MonoBehaviour
{
    public Transform TargetFloor;
    // private bool _isMoving = false;
    public bool IsEndingScene = false;
    public bool TriggerActivated = false;

    private void Update()
    {
        if (TriggerActivated)
        {
            MoveElevator();
        }
    }

    private void MoveElevator()
    {
       // _isMoving = true;
        Vector3 targetPosition = TargetFloor.position;
        StartCoroutine(MoveToPosition(targetPosition, 2f));
    }
    private IEnumerator MoveToPosition(Vector3 targetPosition, float timeToMove)
    {
        Vector3 currentPos = transform.position;
        float elapsedTime = 0;

        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(currentPos, targetPosition, (elapsedTime / timeToMove));
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        transform.position = targetPosition;
       // _isMoving = false;
        TriggerActivated = false;
    }
    
}

