using UnityEngine;
using UnityEngine.UI;

public class FaceItemUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    private FaceItemData data;
    private FacePreviewController preview;

    public void Init(FaceItemData newData, FacePreviewController previewController)
    {
        data = newData;
        preview = previewController;
        icon.sprite = data.icon;
    }

    public void OnClick()
    {
        preview.ApplyItem(data);
        Debug.Log("Item clickao" + data.ToString());
    }
}
