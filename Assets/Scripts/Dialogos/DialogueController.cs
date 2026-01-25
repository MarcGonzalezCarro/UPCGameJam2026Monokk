using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [Header("UI")]
    public Text personajeText;
    public Text dialogoText;

    private DialogueLine currentLine;

    public bool tieneLibreta;
    public int carasDueños;

    public void StartDialogue(string jsonFile, string startId)
    {
        DialogueManager.Instance.LoadDialogueFile(jsonFile);
        ShowLine(startId);
    }

    public void ShowLine(string id)
    {
        DialogueLine line = DialogueManager.Instance.GetLine(id);
        if (line == null) return;

        if (!CheckCondition(line.condicion)) return;

        currentLine = line;

        personajeText.text = line.personaje;
        dialogoText.text = line.texto;

        ExecuteAction(line.accion);
    }

    public void Next()
    {
        if (currentLine == null) return;

        if (!string.IsNullOrEmpty(currentLine.siguiente))
        {
            ShowLine(currentLine.siguiente);
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        personajeText.text = "";
        dialogoText.text = "";
        currentLine = null;
    }

    // =========================
    // CONDICIONES
    // =========================
    bool CheckCondition(string condition)
    {
        if (string.IsNullOrEmpty(condition))
            return true;

        if (condition == "tiene_libreta == true")
            return tieneLibreta;

        if (condition == "caras_dueños < 3")
            return carasDueños < 3;

        return true;
    }

    // =========================
    // ACCIONES
    // =========================
    void ExecuteAction(string action)
    {
        if (string.IsNullOrEmpty(action)) return;

        switch (action)
        {
            case "coger_libreta":
                tieneLibreta = true;
                break;

            case "activar_puzle_dibujo":
                Debug.Log("PUZLE DIBUJO ACTIVADO");
                break;

            case "activar_mision_hogar":
                Debug.Log("MISIÓN ACTIVADA");
                break;
        }
    }
}

