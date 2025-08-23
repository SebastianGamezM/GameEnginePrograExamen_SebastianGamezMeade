using UnityEngine;

public class TrofeoFinal : MonoBehaviour
{
    public GameObject pantallaVictoria;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.Instance.bloquearInputs = true;
            pantallaVictoria.SetActive(true);
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = new Vector3(999, 999, 999);
            player.GetComponent<CharacterController>().enabled = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Destroy(gameObject);
        }
    }
}
