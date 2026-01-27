using UnityEngine;

public class AudioWhenClose : MonoBehaviour
{
    public AudioSource audioSource;
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

       audioSource.Play();
      


    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        audioSource.Stop();

    }


 
}
