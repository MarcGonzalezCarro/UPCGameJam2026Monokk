using UnityEngine;

public class AudioWhenActive : MonoBehaviour
{
   public AudioSource laughs;
   public GameObject canvas;
    float time;
    bool firsttime = true;
    void Start()
    {
        time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float timeNow = Time.time;
        if (timeNow - time > 35 && firsttime) {

            laughs.Play();
            firsttime = false;
        
        }
        
    }
}
