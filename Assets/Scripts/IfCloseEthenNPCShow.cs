using UnityEngine;

public class IfCloseEthenNPCShow : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject npc1;
    public GameObject npc2;
    public GameObject npc3;
    public GameObject npc4;
    public GameObject npc5;
    public GameObject npc6;
    public GameObject npc7;

    bool trigger = false;

    void Start()
    {
        npc1.SetActive(false);
        npc2.SetActive(false);
        npc3.SetActive(false);
        npc4.SetActive(false);
        npc5.SetActive(false);
        npc6.SetActive(false);
        npc7.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (trigger == true && Input.GetKeyDown(KeyCode.E)) {



            npc1.SetActive(true);
            npc2.SetActive(true);
            npc3.SetActive(true);
            npc4.SetActive(true);
            npc5.SetActive(true);
            npc6.SetActive(true);
            npc7.SetActive(true);



        }
    }
    private void OnTriggerEnter(Collider other)
    {
        trigger = true;
    }
    private void OnTriggerExit(Collider other)
    {
        trigger = false;
    }
}
