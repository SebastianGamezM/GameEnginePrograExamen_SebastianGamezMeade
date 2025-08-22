using System.Collections;
using UnityEngine;

public class Pistola : Armas
{

    private void Start()
    {
        municionActual = municionMax;
    }
    public override void Shoot()
    {
        if (estaRecargando || enCooldown || municionActual <= 0) return;

        GameObject balaClon = Instantiate(balaPrefab, shootPoint.position, Quaternion.identity);
        balaClon.GetComponent<Rigidbody>().AddForce(shootPoint.forward * fuerzaDisparo);
        balaClon.GetComponent<Bala>().daño = daño;

        Destroy(balaClon, 5);
        municionActual--;
        UiManager.instance.textoBalas.text = municionActual + "/" + municionMax;
        AudioManager.instance.Play("DisparoPistola");
        StartCoroutine(FireCooldown());
    }

    public override IEnumerator Reload()
    {
        municionActual = 0;
        estaRecargando = true;

        if (GameManager.instance.balasHandGun <= 0)
        {
            UiManager.instance.textoBalas.text = "0";
            estaRecargando = false;
            yield break;
        }
        UiManager.instance.textoBalas.text = "Recargando";

        AudioManager.instance.Play("RecargarPistola");

        yield return new WaitForSeconds(tiempoRecarga);

        int municionQueRecargar = GameManager.instance.balasHandGun >= municionMax ? municionMax : GameManager.instance.balasHandGun;
        GameManager.instance.balasHandGun -= municionQueRecargar;
        municionActual = municionQueRecargar;
        estaRecargando = false;
        AudioManager.instance.Stop("RecargarPistola");
        UiManager.instance.textoBalas.text = municionActual + "/" + municionMax;
    }

    public override IEnumerator FireCooldown()
    {
        enCooldown = true;
        yield return new WaitForSeconds(fireRate);
        enCooldown = false;
    }
}
