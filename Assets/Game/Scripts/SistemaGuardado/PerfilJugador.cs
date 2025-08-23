using UnityEngine;

[System.Serializable]
public class PerfilJugador
{
    public int balasHandGun, balasEscopeta, balasRifle;

    public float posX, posY, posZ;

    public string nivel;

    public string[] armasObtenidas = new string[3];

    public PerfilJugador()
    {
        balasHandGun = GameManager.Instance.balasHandGun;
        balasEscopeta = GameManager.Instance.balasEscopeta;
        balasRifle = GameManager.Instance.balasRifle;


        armasObtenidas = GameManager.Instance.armasObtenidasID;

        posX = GameManager.Instance.posX;
        posY = GameManager.Instance.posY;
        posZ = GameManager.Instance.posZ;
    }
}
