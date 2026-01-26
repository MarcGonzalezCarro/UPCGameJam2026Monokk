using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interactable : MonoBehaviour
{
    [Header("Interacción")]
    public KeyCode interactionKey = KeyCode.E;
    public string interactionText = "Pulsa E";

    [Header("UI")]
    public GameObject interactionUI;
    public TMP_Text interactionUIText;

    [Header("Acción")]
    public UnityEngine.Events.UnityEvent onInteract;

    private bool playerNearby = false;

    void Start()
    {
        if (interactionUI != null)
            interactionUI.SetActive(false);
    }

    void Update()
    {
        if (!playerNearby) return;

        if (Input.GetKeyDown(interactionKey))
        {
            Interact();
        }
    }

    void Interact()
    {
        onInteract.Invoke();
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerNearby = true;

        if (interactionUI != null)
        {
            interactionUI.SetActive(true);
            if (interactionUIText != null)
                interactionUIText.text = interactionText;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerNearby = false;

        if (interactionUI != null)
            interactionUI.SetActive(false);
    }
}