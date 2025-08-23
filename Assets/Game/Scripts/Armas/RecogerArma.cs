using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class RecogerArma : MonoBehaviour
{
    public GameObject armaQueDa;


    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            InventarioArmas inventario = FindAnyObjectByType<InventarioArmas>();
            inventario.RecogerArma(armaQueDa);
            AudioManager.instance.Play("RecogerArma");
            GameManager.Instance.objetosEliminados.Add(name);
            Destroy(gameObject);
        }
    }
}
