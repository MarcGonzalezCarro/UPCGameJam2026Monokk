using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    private Dictionary<string, DialogueLine> dialogues =
        new Dictionary<string, DialogueLine>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void LoadDialogueFile(string fileName)
    {
        string path = Path.Combine(
            Application.streamingAssetsPath,
            "Dialogues",
            fileName
        );

        if (!File.Exists(path))
        {
            Debug.LogError("No se encontró el diálogo: " + path);
            return;
        }

        string json = File.ReadAllText(path);
        DialogueData data = JsonUtility.FromJson<DialogueData>(json);

        dialogues.Clear();
        foreach (var line in data.dialogos)
        {
            dialogues[line.id] = line;
        }

        Debug.Log("Diálogos cargados: " + dialogues.Count);
    }

    public DialogueLine GetLine(string id)
    {
        return dialogues.ContainsKey(id) ? dialogues[id] : null;
    }
}
