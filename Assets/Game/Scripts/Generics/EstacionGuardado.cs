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
