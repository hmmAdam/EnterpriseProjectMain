using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;
    void OnCollisionEnter(Collision collision)
    {
        audioSource.Play();
    }
}
