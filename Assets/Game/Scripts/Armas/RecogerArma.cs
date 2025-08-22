using UnityEngine;

public class RecogerArma : MonoBehaviour
{
    public GameObject armaQueDa;


    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            FindAnyObjectByType<InventarioArmas>().RecogerArma(armaQueDa);
            AudioManager.instance.Play("RecogerArma");
            Destroy(gameObject);
        }
    }
}
