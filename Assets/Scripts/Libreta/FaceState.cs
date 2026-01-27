using UnityEngine;

[System.Serializable]
public class FaceState
{
    public Sprite ojos;
    public Sprite nariz;
    public Sprite boca;
    public Sprite cejas;
}

[System.Serializable]
public class FacePage
{
    public string pageName;
    public FaceState faceState;

    public FacePage(string name)
    {
        pageName = name;
        faceState = new FaceState();
    }
}
