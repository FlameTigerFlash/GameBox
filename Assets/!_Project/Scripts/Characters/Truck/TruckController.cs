using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

enum TruckStates {INCOMING, WAITING, LEAVING };

public class TruckController : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _waitPoint;
    [SerializeField] private Transform _finishPoint;

    [SerializeField] private NavMeshAgent _navigator;

    [SerializeField] private float _delay = 10f;


    public UnityEvent ServiceFinishedEvent;
    public UnityEvent JourneyFinishedEvent;

    private TruckStates _currentState;

    private void FixedUpdate()
    {
        if (!_navigator.pathPending && _navigator.remainingDistance <= _navigator.stoppingDistance)
        {
            if (_currentState == TruckStates.INCOMING)
            {
                WaitTime();
            }
            else if (_currentState == TruckStates.LEAVING)
            {
                FinishJourney();
            }
        }
    }

    public void StartJourney()
    {
        GoToStart();
        DriveToDestination();
    }

    private void FinishJourney()
    {
        JourneyFinishedEvent.Invoke();
        GoToStart();
        gameObject.SetActive(false);
    }

    private void GoToStart()
    {
        transform.position = _startPoint.position;
        transform.rotation = _startPoint.rotation;
    }

    private void DriveToDestination()
    {
        _currentState = TruckStates.INCOMING;
        _navigator.SetDestination(_waitPoint.position);
    }

    private void LeaveMap()
    {
        ServiceFinishedEvent.Invoke();
        _currentState = TruckStates.LEAVING;
        _navigator.SetDestination(_finishPoint.position);
    }

    private void WaitTime()
    {
        _currentState = TruckStates.WAITING;
        StartCoroutine(WaitForWasteCollection(_delay));
    }

    private IEnumerator WaitForWasteCollection(float delay)
    {
        yield return new WaitForSeconds(delay);
        LeaveMap();
    }
}
