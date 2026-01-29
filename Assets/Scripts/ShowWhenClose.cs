using UnityEngine;

public class ShowWhenClose : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject text;

    private void Start()
    {
        text.SetActive(false);
    }

    private void Update()
    {
        if (text.activeSelf && Input.GetKeyDown(KeyCode.E)) {

            text.SetActive(false);
        
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        text.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        text.SetActive(false);
    }
}
