using UnityEngine;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour
{
    public string archivoDialogo;
    public string nombre;
    public string conversacionActual;
    public GameObject interactUI;
    private bool jugadorCerca;
    public bool checkConversation;
    public Sprite sprite;

    void Update()
    {
        if (jugadorCerca && interactUI.activeInHierarchy == false)
        {
            interactUI.SetActive(true);
        }
        else if (!jugadorCerca && interactUI.activeInHierarchy == true)
        {
            interactUI.SetActive(false);
        }
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            Interactuar();
        }
    }

    void Interactuar()
    {
        DialogueController controller =
            FindFirstObjectByType<DialogueController>();
        if (checkConversation)
        {
            controller.StartDialogue(
                archivoDialogo,
                conversacionActual,
                this
            );
        }
        else
        {

            switch (nombre)
            {
                case "NPC1":
                    controller.StartDialogueNow(
                    archivoDialogo,
                    conversacionActual,
                    this, 1
                );
                    break;
                case "NPC2":
                    controller.StartDialogueNow(
                    archivoDialogo,
                    conversacionActual,
                    this, 2
                );
                    break;
                case "NPC3":
                    controller.StartDialogueNow(
                    archivoDialogo,
                    conversacionActual,
                    this, 3
                );
                    break;

            }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            jugadorCerca = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            jugadorCerca = false;
    }
}
