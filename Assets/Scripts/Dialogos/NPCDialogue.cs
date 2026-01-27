using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public string archivoDialogo;
    public string nombre;
    public string conversacionActual = "conv_1";
    public GameObject interactUI;
    private bool jugadorCerca;

    void Update()
    {
        if (jugadorCerca && interactUI.activeInHierarchy == false)
        {
            interactUI.SetActive(true);
        }
        else if(!jugadorCerca && interactUI.activeInHierarchy == true)
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

        controller.StartDialogue(
            archivoDialogo,
            conversacionActual,
            this
        );
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
