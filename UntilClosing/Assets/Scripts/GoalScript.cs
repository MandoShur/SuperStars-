using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    public UnityEvent OnTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player") //if player collides game wins, look at gameManager for more details
        {
            OnTrigger?.Invoke();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Debug.Log("goal triggered");
            
        }
    }
}
