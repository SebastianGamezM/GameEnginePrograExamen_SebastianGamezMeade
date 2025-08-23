using System.Collections;
using UnityEngine;

public class EstacionGuardado : MonoBehaviour
{
    public float radio;
    public LayerMask playerLayer;
    private bool enCooldown;
    private void Update()
    {
        if (PlayerDentro() && Input.GetKeyDown(KeyCode.E) && !enCooldown)
        {
            GuardarPartida();
        }
        if(PlayerDentro() && !enCooldown)
        {
            UiManager.instance.guardarPartidaTexto.text = "Presiona \"E\" para guardar la partida";
        }
        else if(!enCooldown) 
        {
            UiManager.instance.guardarPartidaTexto.text = "";
        }
 
    }

    private void GuardarPartida()
    {
        GameManager.Instance.GuardarPartida();
        StartCoroutine(GuardadoVisual());
    }
    private bool PlayerDentro()
    {
        Collider[] colisiones = Physics.OverlapBox(gameObject.transform.position, new Vector3(radio, radio, radio), Quaternion.identity, playerLayer);

        foreach (Collider col in colisiones)
        {
            if (col.CompareTag("Player"))
                return true;
        }

        return false;
    }

    private IEnumerator GuardadoVisual()
    {
        enCooldown = true;
        UiManager.instance.guardarPartidaTexto.text = "Guardando partida...";
        yield return new WaitForSeconds(2);
        UiManager.instance.guardarPartidaTexto.text = "Partida guardada";
        yield return new WaitForSeconds(1);
        enCooldown = false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, new Vector3(radio, radio, radio));
    }
  #endif
}
