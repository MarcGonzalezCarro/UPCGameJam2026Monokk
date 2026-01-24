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
    }
}
