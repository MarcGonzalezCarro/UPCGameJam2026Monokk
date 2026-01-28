using TMPro;
using UnityEngine;

public class CasasSpawner : MonoBehaviour
{
    [Header("Slots Items")]
    public Transform[] ItemSlots;

    [Header("Prefabs")]
    public GameObject ItemPrefab;

    [Header("Sprites")]
    public Sprite[] ItemSprites;

    [Header("Notas")]
    public string[] pageName;
    public int[] noteIndex;

    [Header("Interactable Settings")]
    public GameObject interactionUI;
    public TMP_Text interactionUIText;
    public KeyCode interactionKey = KeyCode.E;
    public bool itemSpawned = false;

    void Start()
    {

    }

    public void SpawnItems()
    {
        if (!itemSpawned)
        {
            for (int i = 0; i < ItemSlots.Length && i < ItemSprites.Length; i++)
            {
                GameObject item = Instantiate(
                    ItemPrefab,
                    ItemSlots[i].position,
                    ItemSlots[i].rotation
                );

                SetupSprite(item, ItemSprites[i]);
                SetupAction(item, pageName[i], noteIndex[i]);
                SetupInteractable(
                    item,
                    "Recoger Item"
                );
            }
            itemSpawned = true;
        }
    }


    void SetupSprite(GameObject obj, Sprite sprite)
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.sprite = sprite;
    }
    void SetupAction(GameObject obj, string pageName, int noteIndex)
    {
        obj.GetComponent<Interactable>().pageName = pageName;
        obj.GetComponent<Interactable>().noteIndex = noteIndex - 1;
    }

    void SetupInteractable(GameObject obj, string interactionText)
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
        
        interactable.onInteract.AddListener(OnItemInteracted);
        

    }

    void OnItemInteracted()
    {
        FindFirstObjectByType<DialogueController>().AddCaras("mision_cual_es_mi_hogar.json");
    }
}
