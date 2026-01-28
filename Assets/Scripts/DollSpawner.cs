using TMPro;
using UnityEngine;

public class DollSpawner : MonoBehaviour
{
    [Header("Slots Muñecas")]
    public Transform[] dollSlots;

    [Header("Slots Partes")]
    public Transform[] dollPartSlots;

    [Header("Prefabs")]
    public GameObject dollPrefab;
    public GameObject dollPartPrefab;

    [Header("Sprites")]
    public Sprite[] dollSprites;
    public Sprite[] dollPartSprites;

    public string[] pageName;
    public int[] noteIndex;

    [Header("Interactable Settings")]
    public GameObject interactionUI;
    public TMP_Text interactionUIText;
    public KeyCode interactionKey = KeyCode.E;
    public bool dollsSpawned = false;
    public bool dollPartSpawned = false;

    void Start()
    {
        
    }

    public void SpawnDolls()
    {
        if (!dollsSpawned) {
            for (int i = 0; i < dollSlots.Length && i < dollSprites.Length; i++)
            {
                GameObject doll = Instantiate(
                    dollPrefab,
                    dollSlots[i].position,
                    dollSlots[i].rotation
                );

                SetupSprite(doll, dollSprites[i]);
                SetupInteractable(
                    doll,
                    "Recoger muñeca", true
                );
            }
            dollsSpawned = true;
        }
    }

    public void SpawnDollParts()
    {
        if (!dollPartSpawned) {
            for (int i = 0; i < dollPartSlots.Length && i < dollPartSprites.Length; i++)
            {
                GameObject part = Instantiate(
                    dollPartPrefab,
                    dollPartSlots[i].position,
                    dollPartSlots[i].rotation
                );

                SetupSprite(part, dollPartSprites[i]);
                SetupAction(part, pageName[i], noteIndex[i]);
                SetupInteractable(
                    part,
                    "Recoger parte de muñeca", false
                );
            }
            dollPartSpawned = true;
        }
    }

    void SetupSprite(GameObject obj, Sprite sprite)
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.sprite = sprite;
    }

    void SetupInteractable(GameObject obj, string interactionText, bool muneca)
    {
        Interactable interactable = obj.GetComponent<Interactable>();
        if (interactable == null) return;

        // Asignación de TODAS las variables públicas
        interactable.interactionKey = interactionKey;
        interactable.interactionText = interactionText;
        interactable.interactionUI = interactionUI;
        interactable.interactionUIText = interactionUIText;

        // Evento limpio
        interactable.onInteract.RemoveAllListeners();
        if (muneca)
        {
            interactable.onInteract.AddListener(OnMunecaInteracted);
        }
        else {
            interactable.onInteract.AddListener(OnParteInteracted);
        }
        
    }

    void OnMunecaInteracted()
    {
        FindFirstObjectByType<DialogueController>().AddCaras("mision_munecas.json");
    }
    void OnParteInteracted()
    {
        FindFirstObjectByType<DialogueController>().AddParte();
    }
    void SetupAction(GameObject obj, string pageName, int noteIndex)
    {
        obj.GetComponent<Interactable>().pageName = pageName;
        obj.GetComponent<Interactable>().noteIndex = noteIndex - 1;
    }
}
