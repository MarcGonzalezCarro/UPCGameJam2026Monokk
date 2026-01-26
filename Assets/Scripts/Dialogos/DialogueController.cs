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
    public int puzzleKokoStatus;

    //Mision muñecas
    public bool firstTimeZari = true;
    public bool muñecasEntregadas = false;
    public bool partesEntregadas = false;
    public int muñecas = 0;
    public int partesMuñeca = 0;
    public bool carasZariOk;

    private string conversacion= "";
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

        string inicio = DialogueManager.Instance.GetInicioConversacion(conversacion, this);

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
            case "mision_munecas.json":
                firstTimeZari = false;
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
                        if (puzzleKokoStatus == 0)
                        {
                            npcActual.conversacionActual = "conv_fallo_puzle";
                        }
                        else if (puzzleKokoStatus == 1)
                        {
                            npcActual.conversacionActual = "conv_acierto_parcial";
                        }
                        else if (puzzleKokoStatus == 2)
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


                    if (!carasZariOk)
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
}

