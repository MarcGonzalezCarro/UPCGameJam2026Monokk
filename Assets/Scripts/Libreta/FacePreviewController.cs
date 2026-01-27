using UnityEngine;
using UnityEngine.UI;

public class FacePreviewController : MonoBehaviour
{
    [Header("Slots")]
    public Image eyesSlot;
    public Image noseSlot;
    public Image mouthSlot;
    public Image eyeBrowSlot;

    public void ApplyItem(FaceItemData data)
    {
        switch (data.partType)
        {
            case FacePartType.Ojos:
                SetSlot(eyesSlot, data.sprite);
                break;

            case FacePartType.Nariz:
                SetSlot(noseSlot, data.sprite);
                break;

            case FacePartType.Boca:
                SetSlot(mouthSlot, data.sprite);
                break;

            case FacePartType.Cejas:
                SetSlot(eyeBrowSlot, data.sprite);
                break;
        }
    }

    void SetSlot(Image slot, Sprite sprite)
    {
        slot.sprite = sprite;
        slot.enabled = sprite != null;
    }
    public FaceState GetCurrentState()
    {
        return new FaceState
        {
            ojos = eyesSlot.sprite,
            nariz = noseSlot.sprite,
            boca = mouthSlot.sprite,
            cejas = eyeBrowSlot.sprite
        };
    }

    public void ApplyState(FaceState state)
    {
        SetSlot(eyesSlot, state.ojos);
        SetSlot(noseSlot, state.nariz);
        SetSlot(mouthSlot, state.boca);
        SetSlot(eyeBrowSlot, state.cejas);
    }
}
