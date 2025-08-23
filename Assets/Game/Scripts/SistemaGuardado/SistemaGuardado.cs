using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SistemaGuardado
{
    public static void GuardarPartida()
    {
        string path = Application.dataPath + GameManager.Instance.nombreGuardado;

        FileStream stream = new FileStream(path, FileMode.Create);

        PerfilJugador perfil = new PerfilJugador();

        BinaryFormatter formatter = new BinaryFormatter();

        formatter.Serialize(stream, perfil);

        stream.Close();

        Debug.Log("Partida guardada en " + path);
    }

    public static PerfilJugador CargarPartida()
    {
        string path = Application.dataPath + GameManager.Instance.nombreGuardado;

        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);

            BinaryFormatter formatter = new BinaryFormatter();

            PerfilJugador perfil = formatter.Deserialize(stream) as PerfilJugador;

            stream.Close();

            Debug.Log("Partida cargada " + path);

            return perfil;
        }
        else
        {
            Debug.Log("No se encontro archivo");

            return null;
        }
    }
}
