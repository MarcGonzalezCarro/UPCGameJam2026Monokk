using UnityEngine;
using System;

[System.Serializable]
public class NPCMinigameProgress
{
    public string npcId;
    public int wins;
}
public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager Instance;

    public Grow grow;
    public Multiply multiply;
    public Timer timer;

    private Action<bool> onGameFinished;

    void Awake()
    {
        Instance = this;
    }

    public void StartMiniGame(Action<bool> callback)
    {
        onGameFinished = callback;

        grow.StartGame();
        multiply.StartGame();
        timer.StartGame();
    }

    // Llamar cuando termine el minijuego
    public void FinishMiniGame(bool won)
    {
        onGameFinished?.Invoke(won);
    }
}
