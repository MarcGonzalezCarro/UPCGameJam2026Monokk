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
        //startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive) return;

        if (Time.time - startTime >= finalTime)
        {
            won = activeGos <= 0;
            isActive = false;

            GameObject[] los = GameObject.FindGameObjectsWithTag("multiply");
            foreach (GameObject lo in los)
                lo.SetActive(false);

            return;
        }

        GameObject[] gos = GameObject.FindGameObjectsWithTag("multiply");
        foreach (GameObject go in gos)
            go.transform.localScale += Vector3.one * growRate * Time.deltaTime;

        activeGos = gos.Length;
    }

    public void StartGame()
    {
        startTime = Time.time;
        won = false;
        isActive = true;
    }
}