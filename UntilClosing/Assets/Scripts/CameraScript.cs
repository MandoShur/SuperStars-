using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] Transform camPos;

    // Update is called once per frame
    void Update()
    {
        transform.position = camPos.transform.position;
        transform.rotation = camPos.transform.rotation;
    }
}
