using UnityEngine;

public class RecogerBalas : MonoBehaviour
{
    public TipoBala tipoDeBala;
    public int balasQueDa;
    public enum TipoBala
    {
        handGun,
        rifle,
        shotGun
    }

    private void DarBalas()
    {
        switch (tipoDeBala)
        {
            case TipoBala.handGun:
                GameManager.instance.balasHandGun += balasQueDa;
                break;

            case TipoBala.rifle:
                GameManager.instance.balasRifle += balasQueDa;
                break;

            case TipoBala.shotGun:
                GameManager.instance.balasEscopeta += balasQueDa;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {           
            DarBalas();
            AudioManager.instance.Play("DarBalas");
            Destroy(gameObject);
        }
    }
}
