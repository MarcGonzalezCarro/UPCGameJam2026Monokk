using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    private Dictionary<string, DialogueLine> dialogos = new();
    private Dictionary<string, Conversacion> conversaciones = new();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
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

        dialogos.Clear();
        conversaciones.Clear();

        foreach (var d in data.dialogos)
            dialogos[d.id] = d;

        foreach (var c in data.conversaciones)
            conversaciones[c.id] = c;

        //Debug.Log("Diálogos: " + dialogos.Count);
        //Debug.Log("Conversaciones: " + conversaciones.Count);
    }

    public DialogueLine GetLine(string id)
    {
        return dialogos.ContainsKey(id) ? dialogos[id] : null;
    }

    public string GetInicioConversacion(string conversacionId, DialogueController controller)
    {
        if (!conversaciones.ContainsKey(conversacionId))
            return null;

        Conversacion conv = conversaciones[conversacionId];

        if (!controller.CheckCondition(conv.condicion))
            return null;

        return conv.inicio;
    }
}
