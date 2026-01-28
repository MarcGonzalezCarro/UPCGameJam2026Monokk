using System.Collections;
using UnityEngine;

[System.Serializable] public class NPCMinigameProgress { public string npcId; public int wins; }

public class MiniGameUIManager : MonoBehaviour
{
    public static MiniGameUIManager Instance;

    [Header("UI Prefab y Canvas")]
    public GameObject clickablePrefab; // el Button prefab
    public RectTransform spawnArea; // un RectTransform que define el área donde spawnean

    [Header("Settings")]
    public int clicksToWin = 15;
    public float duration = 10f;
    public float spawnInterval = 0.3f;

    private int currentClicks = 0;
    private float timer = 0f;
    private bool isRunning = false;

    private System.Action<bool> onFinishedCallback;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void StartMiniGame(System.Action<bool> onFinished)
    {
        if (isRunning) return;

        onFinishedCallback = onFinished;
        currentClicks = 0;
        timer = duration;
        isRunning = true;

        StartCoroutine(SpawnLoop());
        StartCoroutine(TimerCountdown());
    }

    IEnumerator SpawnLoop()
    {
        while (isRunning)
        {
            SpawnButton();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnButton()
    {
        GameObject buttonGO = Instantiate(clickablePrefab);

        // IMPORTANTÍSIMO
        buttonGO.transform.SetParent(spawnArea, false);

        RectTransform rect = buttonGO.GetComponent<RectTransform>();

        Vector2 randomPos = new Vector2(
            Random.Range(spawnArea.rect.xMin, spawnArea.rect.xMax),
            Random.Range(spawnArea.rect.yMin, spawnArea.rect.yMax)
        );

        rect.anchoredPosition = randomPos;
        rect.localScale = Vector3.one;
    }

    IEnumerator TimerCountdown()
    {
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            yield return null;

            if (currentClicks >= clicksToWin)
                break; // si ya ganó, termina antes
        }

        EndMiniGame();
    }

    public void RegisterClick()
    {
        if (!isRunning) return;

        currentClicks++;
        if (currentClicks >= clicksToWin)
        {
            EndMiniGame();
        }
    }

    void EndMiniGame()
    {
        if (!isRunning) return;

        isRunning = false;

        // Destruye todos los botones restantes
        foreach (Transform child in spawnArea)
        {
            Destroy(child.gameObject);
        }

        bool won = currentClicks >= clicksToWin;

        // Llama al DialogueController
        FindObjectOfType<DialogueController>().OnMinigameFinished(won);
    }
}
