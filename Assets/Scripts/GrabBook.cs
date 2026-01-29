using UnityEngine;

public class GrabBook : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject book;
 
    public GameObject lol;
    public GameObject text;

    bool touched;
    void Start()
    {
        book.SetActive(false);
        text.SetActive(false);
    }
    private void Update()
    {
        if(touched==true && Input.GetKeyDown(KeyCode.E))
        {

            book.SetActive(true);
            lol.SetActive(false);
            text.SetActive(false);


        }
    }
    // Update is called once per frame
    private void OnTriggerEnter()
    {
        touched = true;
        text.SetActive(true);
    }
    private void OnTriggerExit()
    {
        touched = false;
        text.SetActive(false);
    }
}
