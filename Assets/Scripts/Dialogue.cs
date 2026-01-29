using UnityEngine;
[System.Serializable]
public class Dialogue
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    [TextArea(3,10)]
    public string[] sentences;
}
