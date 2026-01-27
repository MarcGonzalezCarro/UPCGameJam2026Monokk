using UnityEngine;

public class onButtonMusic : MonoBehaviour
{
    public AudioSource clickSound;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        
    }
    public void OnButtonClick()
    {
        clickSound.Play();
    }
}
