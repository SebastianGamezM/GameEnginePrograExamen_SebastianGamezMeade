using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int balasHandGun, balasEscopeta, balasRifle;
    public string nombreGuardado;
    public GameObject[] armasObtenidas = new GameObject[3];
    public string[] armasObtenidasID = new string[3];

    public ArmasPrefab[] armasID = new ArmasPrefab[3];

    public float posX, posY, posZ;
    private PerfilJugador perfilJugador;

    public List<string> objetosEliminados = new List<string>();

    public bool bloquearInputs;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GuardarPartida();
    }

    public void GuardarPartida()
    {
        armasObtenidas = FindAnyObjectByType<InventarioArmas>().armasObtenidas;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        posX = player.transform.position.x;
        posY = player.transform.position.y; 
        posZ = player.transform.position.z;
        SalvarArmasPorID();
        
        SistemaGuardado.GuardarPartida();
    }
    public void CargarGuardado(string saveName)
    {
        nombreGuardado = saveName;
        perfilJugador = SistemaGuardado.CargarPartida();

        if (perfilJugador != null)
        {
            balasHandGun = perfilJugador.balasHandGun;
            balasEscopeta = perfilJugador.balasEscopeta;
            balasRifle = perfilJugador.balasRifle;

            armasObtenidasID = perfilJugador.armasObtenidas;

            posX = perfilJugador.posX;
            posY = perfilJugador.posY;
            posZ = perfilJugador.posZ;

            RecuperarArmas();

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = new Vector3(posX, posY, posZ);
            player.GetComponent<CharacterController>().enabled = true;

            objetosEliminados = perfilJugador.objetosEliminados;
            EliminarObjetosEliminados();    

            UiManager.instance.pantallaDerrota.SetActive(false);
            UiManager.instance.pantallaVictoria.SetActive(false);
        }

    }

    private void EliminarObjetosEliminados()
    {
        for (int i = 0; i < objetosEliminados.Count; i++) 
        {
            GameObject objeto = GameObject.Find(objetosEliminados[i]);
            Destroy(objeto);
        }
    }

    private void RecuperarArmas()
    {
        InventarioArmas inventario = FindAnyObjectByType<InventarioArmas>();

        for (int i = 0; i < armasObtenidasID.Length; i++)
        {
            if (!string.IsNullOrEmpty(armasObtenidasID[i]))
            {
                GameObject prefab = armasID[BuscarID(armasObtenidasID[i])].prefab;
                GameObject armaClon = Instantiate(prefab, inventario.spawnArma.position, inventario.spawnArma.rotation, inventario.spawnArma);
                armaClon.SetActive(false);
                inventario.armasObtenidas[i] = armaClon;
            }
        }
    }


    private void CargarArmas()
    {
        InventarioArmas invenario = FindAnyObjectByType<InventarioArmas>();
        for(int i = 0; i < armasObtenidasID.Length; i++)
        {
            invenario.armasObtenidas[i] = armasID[BuscarID(armasObtenidasID[i])].prefab;
        }
    }

    private int BuscarID(string ID)
    {
       for(int i = 0;i < armasID.Length;i++)
       {
            if(armasID[i].ID == ID) 
                return i;
       }
        return 0;
    }

    private void SalvarArmasPorID()
    {
        for(int i = 0; i < armasObtenidas.Length; i++)
        {
            if (armasObtenidas[i] != null && armasObtenidas[i].GetComponent<Armas>() != null)
            {
                string armaID = armasObtenidas[i].GetComponent<Armas>().id;
                armasObtenidasID[i] = armaID;
                Debug.Log("Arma " + armaID + " salvada en el espacio " + i);
            }
           
        }
    }

}
