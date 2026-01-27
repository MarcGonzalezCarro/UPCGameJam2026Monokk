using UnityEngine;

public enum FacePartType { Ojos, Nariz, Boca, Cejas }

[CreateAssetMenu(menuName = "Face Customization/Item")]
public class FaceItemData : ScriptableObject
{
    public string id;
    public FacePartType partType;
    public Sprite sprite; 
    public Sprite icon;   
}
