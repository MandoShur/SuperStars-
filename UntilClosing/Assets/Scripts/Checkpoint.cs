using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Checkpoint : MonoBehaviour
{
    //i know this is stupid and inefficient but idc its tuesday bl;afuuasdfy

    public UnityEvent onTrigger;

    private void OnTriggerEnter(Collider other) //basically the exact same thing as goalscript except it calls something different and doesnt unlock cursor
    {
        if(other.gameObject.tag == "Player")
        {
            onTrigger?.Invoke();
            Debug.Log("checkpoint triggered");
        }
    }
}
