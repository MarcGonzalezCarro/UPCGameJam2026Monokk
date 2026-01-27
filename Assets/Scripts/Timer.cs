using UnityEngine;


public class Timer : MonoBehaviour
{
    public GameObject gameObjectToScale;
    public float finalTime = 10.0f;

    public bool isActive = false;
    private float startTime;
    private Vector3 initialScale;


    void Start()
    {
        //startTime = Time.time;
        //initialScale = gameObjectToScale.transform.localScale;
    }


    void Update()
    {
        if (!isActive) return;

        float elapsed = Time.time - startTime;

        if (elapsed >= finalTime)
        {
            gameObjectToScale.transform.localScale =
                new Vector3(0, initialScale.y, initialScale.z);
            isActive = false;
            return;
        }

        float newX = Mathf.Lerp(initialScale.x, 0, elapsed / finalTime);
        gameObjectToScale.transform.localScale =
            new Vector3(newX, initialScale.y, initialScale.z);
    }
    public void StartGame()
    {
        startTime = Time.time;
        isActive = true;
    }
}