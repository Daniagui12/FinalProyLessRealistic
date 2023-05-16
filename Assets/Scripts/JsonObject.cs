using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JsonObject
{
    public AudiosIndividuales[] audios;
    public PreguntasIndividuales[] preguntas;
    public ObjetosIndividuales[] objetos;
}

[System.Serializable]
public class AudiosIndividuales
{
    public string source;
}


[System.Serializable]
public class PreguntasIndividuales
{
    public string pregunta;
    public string source;
}


[System.Serializable]
public class ObjetosIndividuales
{
    public string nombre;
}
