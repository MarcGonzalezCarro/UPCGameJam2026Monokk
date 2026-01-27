using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

[System.Serializable]
public class NPCMinigameDialogue
{
    public string npcId;

    [Header("IDs de conversación")]
    public string fallo = "conv_fallo_puzle";
    public string parcial = "conv_acierto_parcial";
    public string total = "conv_acierto_total";

    [Header("Máx. victorias")]
    public int maxWins = 3;
}
public class DialogueController : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text personajeTextDer;
    public TMP_Text dialogoTextDer;
    public TMP_Text personajeTextIzq;
    public TMP_Text dialogoTextIzq;

    public Animator animator;

    public GameObject fondoDer;
    public GameObject fondoIzq;

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
    public int puzzleKokoStatus;

    //Mision muñecas
    public bool firstTimeZari = true;
    public bool muñecasEntregadas = false;
    public bool partesEntregadas = false;
    public int muñecas = 0;
    public int partesMuñeca = 0;
    public bool carasZariOk;

    private string conversacion= "";
    private bool minigame;
    private bool minigameResult;

    [Header("Minijuegos")]
    public List<NPCMinigameDialogue> minigameDialogues = new();
    public List<NPCMinigameProgress> minigameProgress = new();
    public FacePagesManager facePages;

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

        string inicio = DialogueManager.Instance.GetInicioConversacion(npc.conversacionActual, this);

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


        personajeTextDer.text = line.personaje;
        personajeTextIzq.text = line.personaje;
        GetComponent<TypewriterEffect>().ShowText(line.texto);

        if (line.personaje == "PROTA")
        {
            fondoDer.SetActive(true);
            fondoIzq.SetActive(false);
        }
        else {
            fondoDer.SetActive(false);
            fondoIzq.SetActive(true);
        }

        //Debug.Log(line.texto);
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
        personajeTextDer.text = "";
        dialogoTextDer.text = "";
        personajeTextIzq.text = "";
        dialogoTextIzq.text = "";
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
            case "mision_munecas.json":
                firstTimeZari = false;
                break;
        }
        //Lanza el minijuego
        if (minigame) {
            //minigameResult = LanzaMinijuego();
            MiniGameManager.Instance.StartMiniGame(OnMinigameFinished);
        }
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

        minigame = false;

        switch (npcActual.archivoDialogo)
        {
            case "mision_cual_es_mi_hogar.json":
                if (!firstTimeHogar)
                {
                    if (carasHogar < 3)
                    {
                        npcActual.conversacionActual = "conv_falta_caras";
                        return;
                    }

                    if (carasHogarOk)
                    {
                        npcActual.conversacionActual = "conv_cara_bien";
                        return;
                    }

                    // Si no tiene todas las caras pero tampoco ok
                    npcActual.conversacionActual = "conv_cara_mal";
                    return;
                }
                break;
                
            case "mision_como_pez_fuera_del_agua.json":
                if (!firstTimePez && npcActual.nombre == "Macarena")
                {
                    if (carasPez != 3)
                    {
                        npcActual.conversacionActual = "conv_sin_dibujos";
                        return;
                    }

                    if (carasPezOk)
                    {
                        npcActual.conversacionActual = "conv_dibujos_bien";
                        return;
                    }

                    // Si tiene 3 caras pero no están correctas
                    npcActual.conversacionActual = "conv_dibujos_mal";
                    return;
                }
                break;
            case "mision_ladron_kokopilis.json":
                if (!firstTimeKoko)
                {
                    var progress = GetProgress(npcActual.nombre);
                    var config = GetMinigameDialogue(npcActual.nombre);

                    if (progress.wins < config.maxWins)
                    {
                        // Todavía está en el minijuego
                        ResolveMinigameConversation();
                        return;
                    }

                    // FASE 2: COMPARAR CARAS
                    if (FindFirstObjectByType<GameManager>().CheckFaceByName("Ladrón"))
                    {
                        npcActual.conversacionActual = "conv_cara_bien";
                    }
                    else
                    {
                        npcActual.conversacionActual = "conv_cara_mal";
                    }

                    return;
                }
                break;
            case "mision_munecas.json":

                if (!firstTimeZari)
                {
                    if (muñecas < 3)
                    {
                        Debug.Log("muñecas < 3 pero en realidad son " + muñecas);
                        conversacion = "conv_sin_munecas";
                        return;
                    }


                    if (muñecas >= 3 && !muñecasEntregadas)
                    {
                        Debug.Log("Tienes las muñecas");
                        muñecasEntregadas = true;
                        conversacion = "conv_con_munecas";
                        return;
                    }


                    if (partesMuñeca < 4)
                    {
                        conversacion = "conv_sin_partes";
                        return;
                    }


                    if (!partesEntregadas)
                    {
                        conversacion = "conv_con_partes";
                        partesEntregadas = true;
                        return;
                    }


                    if (!FindFirstObjectByType<GameManager>().CheckFaceByName("Muñeca1") || !FindFirstObjectByType<GameManager>().CheckFaceByName("Muñeca2") || !FindFirstObjectByType<GameManager>().CheckFaceByName("Muñeca3"))
                    {
                        conversacion = "conv_caras_mal";
                        return;
                    }


                    conversacion = "conv_caras_bien";
                }
                else {
                    conversacion = "conv_1_inicio";
                }
                break;
        }
    }

    public void AddCaras(string mision)
    {
        switch (mision)
        {
            case "mision_cual_es_mi_hogar.json":
                carasHogar++;
                break;
            case "mision_como_pez_fuera_del_agua.json":
                carasPez++;
                break;
            case "mision_ladron_kokopilis.json":
                carasKoko++;
                break;
            case "mision_munecas.json":
                muñecas++;
                break;
        }
    }

    public void AddParte()
    {
        partesMuñeca++;
    }

    public void ChangePuzzleStatus(string name, int status)
    {
        switch (name)
        {
            case "Koko":
                puzzleKokoStatus = status;
                break;
        }
    }

    void OnMinigameFinished(bool won)
    {
        string npcId = npcActual.nombre;
        var progress = GetProgress(npcId);
        var config = GetMinigameDialogue(npcId);

        if (!won || progress.wins >= config.maxWins)
            return;

        progress.wins++;

        facePages.UnlockNote(
            npcId,
            progress.wins - 1
        );

       
        if (progress.wins == config.maxWins)
        {
            Debug.Log("Minijuego completado para " + npcId);
            
        }
    }


    NPCMinigameProgress GetProgress(string npcId)
    {
        var progress = minigameProgress.Find(p => p.npcId == npcId);
        if (progress == null)
        {
            progress = new NPCMinigameProgress
            {
                npcId = npcId,
                wins = 0
            };
            minigameProgress.Add(progress);
        }
        return progress;
    }
    NPCMinigameDialogue GetMinigameDialogue(string npcId)
    {
        return minigameDialogues.Find(d => d.npcId == npcId);
    }

    void ResolveMinigameConversation()
    {
        string npcId = npcActual.nombre;

        var config = GetMinigameDialogue(npcId);
        if (config == null)
        {
            Debug.LogWarning("NPC sin configuración de minijuego: " + npcId);
            return;
        }

        var progress = GetProgress(npcId);

        if (progress.wins == 0)
        {
            npcActual.conversacionActual = config.fallo;
        }
        else if (progress.wins < config.maxWins)
        {
            npcActual.conversacionActual = config.parcial;
        }
        else
        {
            npcActual.conversacionActual = config.total;
        }

        minigame = progress.wins < config.maxWins;
    }
}

