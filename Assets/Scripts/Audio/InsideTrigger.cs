using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.PlayBGM("Inside");
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.PlayBGM("Outside");
        }
    }
}
