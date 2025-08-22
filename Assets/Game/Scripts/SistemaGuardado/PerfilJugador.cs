using UnityEngine;

[System.Serializable]
public class PerfilJugador
{
    public int balasHandGun, balasEscopeta, balasRifle;

    public Vector3 posicionPlayer;

    public string nivel;

    public PerfilJugador()
    {
        balasHandGun = GameManager.Instance.balasHandGun;
        balasHandGun = GameManager.Instance.balasEscopeta;
        balasHandGun = GameManager.Instance.balasRifle;

        posicionPlayer = GameManager.Instance.posicionPlayer;
    }
}
