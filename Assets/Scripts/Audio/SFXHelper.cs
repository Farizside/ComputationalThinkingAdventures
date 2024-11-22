using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXHelper : MonoBehaviour
{
    public void PlaySFX(string name)
    {
        AudioManager.Instance.PlaySFX(name);
    }
}
