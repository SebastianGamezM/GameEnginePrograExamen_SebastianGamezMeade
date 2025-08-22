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
    }

    public static PerfilJugador CargarPartida()
    {
        string path = Application.dataPath + GameManager.Instance.nombreGuardado;

        if (File.Exists(path))
        {
            // Se crea un flujo de informacion con la direccion y accion
            FileStream stream = new FileStream(path, FileMode.Open);

            // Creamos una variable de formateo binario
            BinaryFormatter formatter = new BinaryFormatter();

            // Descomprimir archivo
            PerfilJugador perfil = formatter.Deserialize(stream) as PerfilJugador;

            // Se cierra el flujo de la informacion
            stream.Close();

            return perfil;
        }
        else
        {
            Debug.Log("No se encontro archivo");

            return null;
        }
    }
}
