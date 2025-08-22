using UnityEngine;
using UnityEngine.SceneManagement;

public class CargaInicio : MonoBehaviour
{
    private PerfilJugador perfil;

    public void InicioJuego(string nombreGuardado)
    {
        GameManager.Instance.nombreGuardado = nombreGuardado;

        perfil = SistemaGuardado.CargarPartida();

        if (perfil != null)
        {
            GameManager.Instance.balasEscopeta = perfil.balasEscopeta;
            GameManager.Instance.balasHandGun = perfil.balasHandGun;
            GameManager.Instance.balasRifle = perfil.balasRifle;
            GameManager.Instance.Iniciar();
        }
    }
}