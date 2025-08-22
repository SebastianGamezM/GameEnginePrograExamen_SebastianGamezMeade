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
    }

    private void GuardarPartida()
    {
        GameManager.Instance.SalvarDatos();
    }
    private bool PlayerDentro()
    {
        Collider[] colisiones = Physics.OverlapBox(gameObject.transform.position, new Vector3(radio, radio, radio), Quaternion.identity, playerLayer);
        return colisiones[0].CompareTag("Player");
    }

    private IEnumerator GuardadoVisual()
    {
        enCooldown = true;
        yield return new WaitForSeconds(2);
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
