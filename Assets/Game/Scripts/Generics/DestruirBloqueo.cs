using System.Linq;
using UnityEngine;

public class DestruirBloqueo : MonoBehaviour
{
    public GameObject padre;
    void Update()
    {
        if (padre.transform.childCount == 0)
        {
            GameManager.Instance.objetosEliminados.Add(name);
            Destroy(gameObject);
        }
    }
}
