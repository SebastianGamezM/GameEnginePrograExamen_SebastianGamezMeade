using UnityEngine;
using System.Collections;

public class Shotgun : Armas
{
    private void Start()
    {
        municionActual = municionMax;
    }

    public override void Shoot()
    {
        if (estaRecargando || enCooldown || municionActual <= 0) return;

        int balasPorDisparo = 5;
        for (int i = 0; i < balasPorDisparo; i++)
        {
            GameObject balaClon = Instantiate(balaPrefab, shootPoint.position, shootPoint.rotation);
            Vector3 spread = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
            balaClon.GetComponent<Rigidbody>().AddForce((shootPoint.forward + spread) * fuerzaDisparo);
            balaClon.GetComponent<Bala>().daño = daño;
            Destroy(balaClon, 5);
        }

        municionActual--;
        UiManager.instance.textoBalas.text = municionActual + "/" + municionMax;
        AudioManager.instance.Play("DisparoEscopeta");
        StartCoroutine(FireCooldown());

        if (estaRecargando)
        {
            StopCoroutine("Reload");
            estaRecargando = false;
            AudioManager.instance.Stop("RecargarEscopeta");
        }
    }

    public override IEnumerator Reload()
    {
        if (municionActual >= municionMax) yield break;

        estaRecargando = true;
        UiManager.instance.textoBalas.text = "Recargando";

        while (municionActual < municionMax && GameManager.Instance.balasEscopeta > 0)
        {
            AudioManager.instance.Play("RecargarEscopeta");
            yield return new WaitForSeconds(tiempoRecarga); 
            municionActual++;
            GameManager.Instance.balasEscopeta--;
            UiManager.instance.textoBalas.text = municionActual + "/" + municionMax;
        }

        estaRecargando = false;
        AudioManager.instance.Stop("RecargarEscopeta");
    }

    public override IEnumerator FireCooldown()
    {
        enCooldown = true;
        yield return new WaitForSeconds(fireRate);
        enCooldown = false;
    }
}
