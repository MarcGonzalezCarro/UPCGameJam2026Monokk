using System;

[Serializable]
public class DialogueLine
{
    public string id;
    public string personaje;
    public string texto;
    public string condicion;
    public string siguiente;
    public string accion;
}

[Serializable]
public class Conversacion
{
    public string id;
    public string inicio;
    public string condicion;
}

[Serializable]
public class DialogueData
{
    public string escena;
    public Conversacion[] conversaciones;
    public DialogueLine[] dialogos;
}
