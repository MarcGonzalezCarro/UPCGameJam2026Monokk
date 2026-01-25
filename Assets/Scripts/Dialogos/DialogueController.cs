using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueController : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text personajeText;
    public TMP_Text dialogoText;

    public Animator animator;
    private DialogueLine currentLine;
    private NPCDialogue npcActual;

    [Header("Variables de juego")]
    public bool tieneLibreta;
    public bool minijuegoTutorialDone = false;
    //Mision cual es mi hogar
    public bool firstTimeHogar;
    public int carasHogar;
    public bool carasHogarOk;

    //Mision como pez fuera del agua
    public bool firstTimePez;
    public int carasPez;
    public bool carasPezOk;

    //Mision ladron de kokopilis
    public bool firstTimeKoko;
    public bool carasKokoOk;
    public int carasKoko = 0;
    public int puzzleStatus;

    //Mision muñecas
    public bool firstTimeZari;
    public int muñecas = 0;
    public int partesMuñeca = 0;
    public bool carasZariOk;
    void Update()
    {
        if (currentLine != null && Input.GetKeyDown(KeyCode.Space))
            Next();
    }

    private void Start()
    {
        
    }
    public void StartDialogue(
        string jsonFile,
        string conversacionId,
        NPCDialogue npc
    )
    {
        npcActual = npc;

        DialogueManager.Instance.LoadDialogueFile(jsonFile);

        CheckConversation();

        string inicio = DialogueManager.Instance.GetInicioConversacion(conversacionId, this);

        if (inicio != null)
        {
            ShowLine(inicio);
            animator.ResetTrigger("EndConversation");
            animator.SetTrigger("StartConversation");
        }
            
    }

    public void ShowLine(string id)
    {
        DialogueLine line = DialogueManager.Instance.GetLine(id);
        if (line == null) return;
        if (!CheckCondition(line.condicion)) return;

        currentLine = line;

        personajeText.text = line.personaje;
        dialogoText.text = line.texto;

        Debug.Log(line.texto);
        ExecuteAction(line.accion);
    }

    public void Next()
    {
        if (currentLine == null) return;

        if (!string.IsNullOrEmpty(currentLine.siguiente))
            ShowLine(currentLine.siguiente);
        else
            EndDialogue();
    }

    void EndDialogue()
    {
        personajeText.text = "";
        dialogoText.text = "";
        currentLine = null;

        animator.ResetTrigger("StartConversation");
        animator.SetTrigger("EndConversation");

        switch (npcActual.archivoDialogo)
        {
            case "mision_cual_es_mi_hogar.json":
                firstTimeHogar = false;
                break;
            case "mision_como_pez_fuera_del_agua.json":
                firstTimePez = false;
                break;
            case "mision_ladron_kokopilis.json":
                firstTimeKoko = false;
                break;
        }
        //Mirar si se lanza el minijuego
    }

    // =========================
    // CONDICIONES
    // =========================
    public bool CheckCondition(string condition)
    {
        if (string.IsNullOrEmpty(condition))
            return true;

        if (condition == "tiene_libreta == true")
            return tieneLibreta;

        if (condition == "caras_dueños < 3")
            return carasHogar < 3;

        if (condition == "caras_dueños >= 3")
            return carasHogar >= 3;

        return true;
    }

    // =========================
    // ACCIONES
    // =========================
    void ExecuteAction(string action)
    {
        if (string.IsNullOrEmpty(action))
            return;

        switch (action)
        {
            case "activar_mision_gato":
                Debug.Log("MISIÓN GATO ACTIVADA");
                break;

            case "dar_pista_dueña":
                Debug.Log("PISTA ENTREGADA");
                break;
        }
    }

    public void CheckConversation() {

        switch (npcActual.archivoDialogo)
        {
            case "mision_cual_es_mi_hogar.json":
                if (!firstTimeHogar) {
                    if (npcActual.conversacionActual != "conv_cara_bien") {
                        if (carasHogar < 3)
                        {
                            npcActual.conversacionActual = "conv_falta_caras";
                        }
                        else if (carasHogarOk)
                        {
                            npcActual.conversacionActual = "conv_cara_bien";
                        }
                        else
                        {
                            npcActual.conversacionActual = "conv_cara_mal";
                        }
                    } 
                }
                break;
            case "mision_como_pez_fuera_del_agua.json":
                if (!firstTimePez)
                {
                    if (npcActual.nombre == "Macarena")
                    {
                        if (carasPez != 3)
                        {
                            npcActual.conversacionActual = "conv_sin_dibujos";
                        }
                        else if (carasPezOk)
                        {
                            npcActual.conversacionActual = "conv_dibujos_bien";
                        }
                        else
                        {
                            npcActual.conversacionActual = "conv_dibujos_mal";
                        }
                    }
                }
                break;
            case "mision_ladron_kokopilis.json":
                if (!firstTimeKoko)
                {
                    if (npcActual.conversacionActual == "conv_1_inicio")
                    {
                        npcActual.conversacionActual = "conv_ayuda";
                        //Lanzar minijuego en el EndDialogue
                    }
                    else if (carasKoko < 1)
                    {
                        if (puzzleStatus == 0)
                        {
                            npcActual.conversacionActual = "conv_fallo_puzle";
                        }
                        else if (puzzleStatus == 1)
                        {
                            npcActual.conversacionActual = "conv_acierto_parcial";
                        }
                        else if (puzzleStatus == 2)
                        {
                            npcActual.conversacionActual = "conv_acierto_total";
                        }
                    }
                    else if(npcActual.conversacionActual != "conv_ayuda")
                    {
                        if (carasKokoOk)
                        {
                            npcActual.conversacionActual = "conv_cara_bien";
                        }
                        else {
                            npcActual.conversacionActual = "conv_cara_mal";
                        }
                    }
                }
                break;
            case "mision_munecas.json":
                if (!firstTimeZari)
                {
                    if (npcActual.conversacionActual == "conv_1_inicio" && muñecas < 3)
                    {
                        npcActual.conversacionActual = "conv_sin_munecas";
                    }
                    if (muñecas >= 3 && npcActual.conversacionActual != "conv_con_munecas") {
                        npcActual.conversacionActual = "conv_con_munecas";
                    }
                    if (npcActual.conversacionActual == "conv_con_munecas" && partesMuñeca < 4) {
                        npcActual.conversacionActual = "conv_sin_partes";
                    }
                    if (npcActual.conversacionActual == "conv_con_munecas" && partesMuñeca >= 4)
                    {
                        npcActual.conversacionActual = "conv_con_partes";
                    }
                    if (npcActual.conversacionActual == "conv_con_partes" || npcActual.conversacionActual == "conv_caras_mal") {
                        if (carasZariOk)
                        {
                            npcActual.conversacionActual = "conv_caras_bien";
                        }
                        else {
                            npcActual.conversacionActual = "conv_caras_mal";
                        }
                    }
                }
                break;
        }
    }
}

