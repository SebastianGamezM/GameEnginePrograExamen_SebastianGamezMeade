using UnityEngine;

public class Bala : MonoBehaviour
{

    private GameObject colObj;
    public int daño;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemigo>().RecibirDaño(daño);
            AudioManager.instance.Play("ImpactoDisparoEnemigo");
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            AudioManager.instance.Play("ImpactoDisparoTierra");
            Destroy(gameObject,2);
        }
    }
}