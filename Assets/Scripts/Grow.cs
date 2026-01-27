using UnityEngine;
using System.Collections;

public class Grow : MonoBehaviour
{

   public int activeGos = 0;
    public float growRate = 3f;
    public bool won;
    public bool isActive = true;
    public float finalTime = 10.0f;
    public float startTime;
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime >= finalTime)
        {
            if(activeGos > 0)
            {
                won = false;
            }
            else
            {
                won = true;
            }
            isActive = false;
            
            GameObject[] los = GameObject.FindGameObjectsWithTag("multiply");
            foreach (GameObject lo in los)
                lo.SetActive(false);
            return;
        }
        GameObject[] gos = GameObject.FindGameObjectsWithTag("multiply");
        foreach (GameObject go in gos)

            go.transform.localScale += new Vector3(.1F, .1f, .1f) * growRate * Time.deltaTime;
       activeGos = gos.Length;
    }
}