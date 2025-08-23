using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inicio : MonoBehaviour
{
    public GameObject menu;
    private bool menuActivo;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameManager.Instance.bloquearInputs = true;
        AlternarMenu();
    }
    public void AlternarMenu()
    {
        menuActivo = !menuActivo;
        menu.SetActive(menuActivo);
        GameManager.Instance.bloquearInputs = menuActivo;

        if (menuActivo)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    public void IniciarElJuego()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Nivel");
    }

    public void ContinuarPartida()
    {
        GameManager.Instance.CargarGuardado(GameManager.Instance.nombreGuardado);
        AlternarMenu();
    }

    public void Salir()
    {
        Application.Quit();
    }
}
