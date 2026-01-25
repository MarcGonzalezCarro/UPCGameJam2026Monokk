using UnityEngine;

public class FaceItemsPanel : MonoBehaviour
{
    public Transform gridParent;
    public FaceItemUI itemPrefab;
    public FaceItemData[] itemsData;
    public FacePreviewController previewController;

    void Start()
    {
        foreach (var data in itemsData)
        {
            var item = Instantiate(itemPrefab, gridParent);
            item.Init(data, previewController);
        }

        DialogueManager.Instance.LoadDialogueFile("perro_inicio.json");

        var line = DialogueManager.Instance.GetLine("perro_01");
        Debug.Log(line != null ? line.texto : "NO SE ENCONTRÓ EL DIÁLOGO");
    }
}
