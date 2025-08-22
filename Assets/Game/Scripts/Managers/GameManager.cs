using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int balasHandGun, balasEscopeta, balasRifle;
    public Vector3 posicionPlayer;
    public string nombreGuardado;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void SalvarDatos()
    {
        posicionPlayer = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    public void Iniciar()
    {
        
    }

}
