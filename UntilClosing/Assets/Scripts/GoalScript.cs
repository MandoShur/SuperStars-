using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    public UnityEvent OnTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            OnTrigger?.Invoke();
            Debug.Log("goal triggered");
            
        }
    }
}
