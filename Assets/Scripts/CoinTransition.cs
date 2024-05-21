using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTransition : MonoBehaviour
{
    public bool goUp;
    public AudioSource Audio;
    public AudioClip CoinCollector;

    // Start is called before the first frame update
    void Start()
    {
        Audio = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (goUp == true)
        {
            transform.Rotate(0, 0, 0);
            transform.Translate(0, 0.04f, 0);
        }
        else
        {
            transform.Rotate(0, 0, 1f);
        }
    }
    void OnTriggerEnter(Collider other) 
    { 
      
        if (other.gameObject.tag == "Player")
        { 
            goUp = true;
            Audio.PlayOneShot (CoinCollector, 1);
        }
    }
}
