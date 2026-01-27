

using UnityEngine;

public class Multiply : MonoBehaviour
{
    public GameObject prefab;
    public float time;
    public float interval;
    public Canvas canvas;
    public float finalTime = 10.0f;
    public float startTime;
    public bool isActive = true;

    void Start()
    {
        //startTime = Time.time;
        //time = Time.time;
    }


    void Update()
    {

        if (!isActive)
        {
            return;
        }
        if (Time.time - startTime >= finalTime)
        {
            isActive = false;
            return;
        }
        
        float timeNow = Time.time;

        if (timeNow - time >= interval && isActive)
        {
            RectTransform canvasRect = canvas.GetComponent<RectTransform>();
         


            float x = Random.Range(
            -canvasRect.rect.width / 2,
            canvasRect.rect.width / 2
            );


            float y = Random.Range(
            -canvasRect.rect.height / 2,
            canvasRect.rect.height / 2
            );


          
            GameObject button = Instantiate(prefab, canvas.transform);


          
            RectTransform buttonRect = button.GetComponent<RectTransform>();
            buttonRect.anchoredPosition = new Vector2(x, y);

            if(!button.activeSelf && isActive)
            {
                button.SetActive(true);
            }

            time = Time.time;
        }

    }
    public void StartGame()
    {
        startTime = Time.time;
        time = Time.time;
        isActive = true;
    }
}