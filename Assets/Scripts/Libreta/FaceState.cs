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

    public string noteA;
    public string noteB;
    public string noteC;

    public bool noteAUnlocked;
    public bool noteBUnlocked;
    public bool noteCUnlocked;

    public FacePage(string name)
    {
        pageName = name;
        faceState = new FaceState();

        noteA = "";
        noteB = "";
        noteC = "";

        noteAUnlocked = false;
        noteBUnlocked = false;
        noteCUnlocked = false;
    }
}

[System.Serializable]
public class FaceRecipe
{
    public string faceName;

    public Sprite ojos;
    public Sprite nariz;
    public Sprite boca;
    public Sprite cejas;
}
