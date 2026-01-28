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
    public enum HogarState
    {
        PrimerDialogo,
        BuscandoCaras,
        CheckCara,
        Completada
    }

    public HogarState hogarState = HogarState.PrimerDialogo;
    //Mision cual es mi hogar
    public int objetosCasas;
    public bool carasHogarOk;

    public enum PezState
    {
        PrimerDialogo,
        EsperandoDibujos,
        CheckCaras,
        Completada
    }

    public PezState pezState = PezState.PrimerDialogo;
    //Mision como pez fuera del agua
    public int npc1Wins;
    public int npc2Wins;
    public int npc3Wins;
    public int dibujosConseguidos;
    public int carasPez;
    public bool carasPezOk;

    //Mision ladron de kokopilis

    public enum KokoState
    {
        PrimerDialogo,
        Ayuda,
        Minijuego,
        CheckCara
    }
    public KokoState kokoState = KokoState.PrimerDialogo;
    public int kokoWins = 0; // cuántas veces has ganado el minijuego
    public int kokoMaxWins = 3;

    //Mision muñecas
    public enum ZariState
    {
        PrimerDialogo,
        BuscandoMuñecas,
        BuscandoPartes,
        CheckCaras,
        Completada
    }
    public ZariState zariState = ZariState.PrimerDialogo;


    public bool muñecasEntregadas = false;
    public bool partesEntregadas = false;
    public int muñecas = 0;
    public int partesMuñeca = 0;
    public bool carasZariOk;

    private string conversacion = "";
    public bool minigame;
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
    public void StartDialogueNow(
        string jsonFile,
        string conversacionId,
        NPCDialogue npc, int tipo
    )
    {
        npcActual = npc;

        if (tipo == 1)
        {
            minigame = true;
        }

        DialogueManager.Instance.LoadDialogueFile(jsonFile);

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


        personajeTextDer.text = line.personaje;
        personajeTextIzq.text = line.personaje;
        GetComponent<TypewriterEffect>().ShowText(line.texto);

        if (line.personaje == "PROTA")
        {
            fondoDer.SetActive(true);
            fondoIzq.SetActive(false);
        }
        else
        {
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

        //Lanza el minijuego
        if (minigame)
        {
            if (MiniGameUIManager.Instance != null)
                MiniGameUIManager.Instance.StartMiniGame(OnMinigameFinished);
            else
                Debug.LogError("MiniGameManager no encontrado");
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
            return objetosCasas < 3;

        if (condition == "caras_dueños >= 3")
            return objetosCasas >= 3;

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

    public void CheckConversation()
    {

        minigame = false;

        switch (npcActual.archivoDialogo)
        {
            case "mision_cual_es_mi_hogar.json":
                switch (hogarState)
                {
                    case HogarState.PrimerDialogo:
                        npcActual.conversacionActual = "conv_1_inicio";
                        hogarState = HogarState.BuscandoCaras;
                        FindFirstObjectByType<CasasSpawner>().SpawnItems();
                        break;

                    case HogarState.BuscandoCaras:
                        if (objetosCasas < 12)
                        {
                            npcActual.conversacionActual = "conv_falta_caras";
                        }
                        else
                        {
                            hogarState = HogarState.CheckCara;
                            CheckConversation();
                        }
                        break;

                    case HogarState.CheckCara:
                        if (FindFirstObjectByType<GameManager>().CheckFaceByName("Casa Marrón"))
                        {
                            npcActual.conversacionActual = "conv_cara_bien";
                            hogarState = HogarState.Completada;
                        }
                        else
                        {
                            npcActual.conversacionActual = "conv_cara_mal";
                        }
                        break;
                }
                break;

            case "mision_como_pez_fuera_del_agua.json":
                switch (pezState)
                {
                    case PezState.PrimerDialogo:
                        npcActual.conversacionActual = "conv_1_inicio";
                        pezState = PezState.EsperandoDibujos;
                        break;

                    case PezState.EsperandoDibujos:
                        if (dibujosConseguidos < 3)
                        {
                            npcActual.conversacionActual = "conv_sin_dibujos";
                        }
                        else
                        {
                            pezState = PezState.CheckCaras;
                            CheckConversation();
                        }
                        break;

                    case PezState.CheckCaras:
                        if (FindFirstObjectByType<GameManager>().CheckFaceByName("Macarena"))
                        {
                            npcActual.conversacionActual = "conv_dibujos_bien";
                            pezState = PezState.Completada;
                        }
                        else
                        {
                            npcActual.conversacionActual = "conv_dibujos_mal";
                        }
                        break;
                }
                break;
            case "mision_ladron_kokopilis.json":
                switch (kokoState)
                {
                    case KokoState.PrimerDialogo:
                        npcActual.conversacionActual = "conv_1_inicio";
                        kokoState = KokoState.Ayuda; // siguiente vez que hables irá a conv_ayuda
                        minigame = false;
                        break;

                    case KokoState.Ayuda:
                        npcActual.conversacionActual = "conv_ayuda";
                        minigame = true; // al terminar este diálogo se lanzará el minijuego
                        break;

                    case KokoState.Minijuego:
                        // Mientras no se hayan conseguido las 3 notas, sigue lanzando el minijuego
                        npcActual.conversacionActual = "conv_ayuda";
                        minigame = true;
                        break;

                    case KokoState.CheckCara:
                        if (FindFirstObjectByType<GameManager>().CheckFaceByName("Ladrona"))
                            npcActual.conversacionActual = "conv_cara_bien";
                        else
                            npcActual.conversacionActual = "conv_cara_mal";

                        minigame = false;
                        break;
                }
                break;
            case "mision_munecas.json":

                switch (zariState)
                {
                    case ZariState.PrimerDialogo:
                        npcActual.conversacionActual = "conv_1_inicio";
                        zariState = ZariState.BuscandoMuñecas;
                        FindFirstObjectByType<GameManager>().GetComponent<DollSpawner>().SpawnDolls();
                        break;

                    case ZariState.BuscandoMuñecas:
                        if (muñecas < 3)
                            npcActual.conversacionActual = "conv_sin_munecas";
                        else
                        {
                            npcActual.conversacionActual = "conv_con_munecas";
                            zariState = ZariState.BuscandoPartes;
                            FindFirstObjectByType<GameManager>().GetComponent<DollSpawner>().SpawnDollParts();
                        }
                        break;

                    case ZariState.BuscandoPartes:
                        if (partesMuñeca < 12)
                            npcActual.conversacionActual = "conv_sin_partes";
                        else
                        {
                            npcActual.conversacionActual = "conv_con_partes";
                            zariState = ZariState.CheckCaras;
                        }
                        break;

                    case ZariState.CheckCaras:
                        if (FindFirstObjectByType<GameManager>().CheckFaceByName("Muñeca1") && FindFirstObjectByType<GameManager>().CheckFaceByName("Muñeca2") && FindFirstObjectByType<GameManager>().CheckFaceByName("Muñeca3"))
                        {
                            npcActual.conversacionActual = "conv_caras_bien";
                            zariState = ZariState.Completada;
                        }
                        else
                        {
                            npcActual.conversacionActual = "conv_caras_mal";
                        }
                        break;
                }
                break;
        }
    }

    public void AddCaras(string mision)
    {
        switch (mision)
        {
            case "mision_cual_es_mi_hogar.json":
                objetosCasas++;
                break;
            case "mision_como_pez_fuera_del_agua.json":
                carasPez++;
                break;
            case "mision_ladron_kokopilis.json":
                //carasKoko++;
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

    public void OnMinigameFinished(bool won)
    {

        switch (npcActual.nombre)
        {
            case "NPC1":
                Debug.Log(npcActual.nombre);
                if (won)
                {
                    Debug.Log("Ganaste");
                    npc1Wins++;
                    facePages.UnlockNote("Entrevista1", npc1Wins - 1);
                    if (npc1Wins < 3)
                    {
                        Debug.Log("TEst");
                        StartDialogueNow("mision_como_pez_fuera_del_agua.json", "conv_acierto_parcial", npcActual, -1);
                    }
                    else
                    {
                        Debug.Log("TEst4");
                        StartDialogueNow("mision_como_pez_fuera_del_agua.json", "conv_acierto_total", npcActual, -1);
                    }

                }
                else
                {
                    npcActual.conversacionActual = "conv_fallo_puzle";
                }
                minigame = false;

                break;
            case "NPC2":
                if (won)
                {
                    npc2Wins++;
                    facePages.UnlockNote("Entrevista2", npc2Wins - 1);
                    if (npc2Wins < 3)
                    {
                        StartDialogueNow("mision_ladron_kokopilis.json", "conv_acierto_parcial", npcActual, -1);
                    }
                    else
                    {
                        StartDialogueNow("mision_ladron_kokopilis.json", "conv_acierto_total", npcActual, -1);
                    }

                }
                else
                {
                    npcActual.conversacionActual = "conv_fallo_puzle";
                }
                minigame = false;

                break;
            case "NPC3":
                if (won)
                {
                    npc3Wins++;
                    facePages.UnlockNote("Entrevista3", npc3Wins - 1);
                    if (npc3Wins < 3)
                    {
                        StartDialogueNow("mision_ladron_kokopilis.json", "conv_acierto_parcial", npcActual, -1);
                    }
                    else
                    {
                        StartDialogueNow("mision_ladron_kokopilis.json", "conv_acierto_total", npcActual, -1);
                    }

                }
                else
                {
                    npcActual.conversacionActual = "conv_fallo_puzle";
                }
                minigame = false;

                break;
            case "Coco":
                if (won)
                {
                    kokoWins++;
                    facePages.UnlockNote("Ladrona", kokoWins - 1);
                    if (kokoWins < 3)
                    {
                        StartDialogueNow("mision_ladron_kokopilis.json", "conv_acierto_parcial", npcActual, -1);
                    }
                    else
                    {
                        StartDialogueNow("mision_ladron_kokopilis.json", "conv_acierto_total", npcActual, -1);
                    }

                }
                else
                {
                    npcActual.conversacionActual = "conv_fallo_puzle";
                }

                // Si ya consiguió las 3 notas
                if (kokoWins >= kokoMaxWins)
                    kokoState = KokoState.CheckCara;
                else
                    kokoState = KokoState.Minijuego;

                minigame = false;

                break;

        }


    }

    public void OnMinigameFinishedNPC1(bool won)
    {
        Debug.Log("sjfsijfo");

    }
    public void OnMinigameFinishedNPC2(bool won)
    {

    }
    public void OnMinigameFinishedNPC3(bool won)
    {

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

    public void ItemUnlockNote(string pageName, int noteIndex)
    {
        facePages.UnlockNote(pageName, noteIndex);
    }


}
