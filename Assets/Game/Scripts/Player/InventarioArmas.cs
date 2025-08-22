using UnityEngine;

public class InventarioArmas : MonoBehaviour
{
    public Transform spawnArma;

    public GameObject[] armasObtenidas = new GameObject[3];

    private Armas armaEquipada;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && armasObtenidas[0] != null) EquiparArma(0);
        if (Input.GetKeyDown(KeyCode.Alpha2) && armasObtenidas[1] != null) EquiparArma(1);
        if (Input.GetKeyDown(KeyCode.Alpha3) && armasObtenidas[2] != null) EquiparArma(2);

        if (Input.GetKeyDown(KeyCode.R) && armaEquipada != null)
        {
            StartCoroutine(armaEquipada.Reload());
        }

        if (armaEquipada != null && Input.GetMouseButton(0))
        {
            armaEquipada.Shoot();
        }
    }

    public void RecogerArma(GameObject armaPrefab)
    {
        for (int i = 0; i < armasObtenidas.Length; i++)
        {
            if (armasObtenidas[i] == null)
            {
                GameObject armaClon = Instantiate(
                    armaPrefab,
                    spawnArma.position,
                    spawnArma.rotation,
                    spawnArma
                );
                armaClon.SetActive(false);

                armasObtenidas[i] = armaClon;
                break;
            }
        }
    }

    private void EquiparArma(int armaIndex)
    {
        if (armaEquipada != null)
        {
            if (armaEquipada.estaRecargando || armaEquipada.enCooldown) return;

            armaEquipada.gameObject.SetActive(false);

            if (armaEquipada == armasObtenidas[armaIndex].GetComponent<Armas>())
            {
                UiManager.instance.imagenArma.sprite = null;
                UiManager.instance.textoBalas.text = "";
                armaEquipada = null;
                Debug.Log("Desesquipar");
                return;
            }
        }

        if (armasObtenidas[armaIndex] != null)
        {
            armasObtenidas[armaIndex].SetActive(true);
            armaEquipada = armasObtenidas[armaIndex].GetComponent<Armas>();

            UiManager.instance.imagenArma.sprite = armaEquipada.imagenUI;
            UiManager.instance.textoBalas.text = armaEquipada.municionActual + "/" + armaEquipada.municionMax;

            AudioManager.instance.Play("RecogerArma");
        }
    }
}

