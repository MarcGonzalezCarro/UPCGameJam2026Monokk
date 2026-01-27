using UnityEngine;
using System;
using System.Collections;
using Random = UnityEngine.Random;

[System.Serializable]
public class NPCMinigameProgress
{
    public string npcId;
    public int wins;
}


public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager Instance;

    [Header("Prefabs y Spawn")]
    public GameObject clickablePrefab;
    public Transform spawnArea; // Un empty que define el centro del spawn
    public Vector2 spawnRange = new Vector2(5f, 5f);

    [Header("Minijuego Settings")]
    public int clicksToWin = 15;
    public float duration = 10f;

    private int currentClicks = 0;
    private float timer = 0f;
    private bool isRunning = false;

    private System.Action<bool> onFinishedCallback;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
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
            SpawnPrefab();
            yield return new WaitForSeconds(0.3f); // cada 0.3s spawn
        }
    }

    void SpawnPrefab()
    {
        Vector3 pos = spawnArea.position + new Vector3(
            Random.Range(-spawnRange.x, spawnRange.x),
            Random.Range(-spawnRange.y, spawnRange.y),
            0f
        );

        Instantiate(clickablePrefab, pos, Quaternion.identity);
    }

    IEnumerator TimerCountdown()
    {
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            yield return null;
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

        // Destruye todos los prefabs existentes
        foreach (var obj in FindObjectsOfType<ClickablePrefab>())
        {
            Destroy(obj.gameObject);
        }

        bool won = currentClicks >= clicksToWin;

        // Llama al DialogueController
        FindFirstObjectByType<DialogueController>().OnMinigameFinished(won);
    }
}
