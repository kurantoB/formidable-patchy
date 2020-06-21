using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPersist : MonoBehaviour
{
    public string soundTrack;

    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("AudioSource").Length > 1)
        {
            Destroy(transform.gameObject);
        } else
        {
            DontDestroyOnLoad(transform.gameObject);
        }
    }
}
