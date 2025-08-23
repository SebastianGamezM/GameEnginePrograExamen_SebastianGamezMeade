using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public TextMeshProUGUI textoBalas;
    public Image imagenArma;
    public TextMeshProUGUI guardarPartidaTexto;
    public GameObject pantallaDerrota;
    public GameObject pantallaVictoria;
    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        pantallaDerrota.SetActive(false);
        pantallaVictoria.SetActive(false);
        imagenArma.sprite = null;
        textoBalas.text = "";
        guardarPartidaTexto.text = "";
    }
}
