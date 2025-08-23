using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class Enemigo : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    private PlayerMove playerScript;
    private Animator animator;

    [Header("Stats")]
    public int daño = 10;
    public int vida = 20;

    [Header("Detección")]
    [SerializeField] private float radio = 10f;
    [SerializeField] private LayerMask mask;

    private bool playerEncontrado;
    private bool cooldown = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        playerScript = player.GetComponent<PlayerMove>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float velocidadActual = agent.velocity.magnitude;
        animator.SetBool("Moving", velocidadActual > 0.1f);

        DetectarJugador();

        if (playerEncontrado)
        {
            PerseguirJugador();
        }
        AtacarJugador();
    }

    private void DetectarJugador()
    {
        playerEncontrado = Physics.CheckSphere(transform.position, radio, mask);
    }

    private void PerseguirJugador()
    {
        if (!agent.isOnNavMesh) return;

        agent.stoppingDistance = 1;
        agent.SetDestination(player.position);
    }

    private void AtacarJugador()
    {
        if (Vector3.Distance(transform.position, player.position) < 2f && !cooldown)
        {
            animator.SetTrigger("Attack");
            Debug.Log("Golpeo al player");
            DañarPlayer();
        }
    }

    public void RecibirDaño(int daño)
    {
        animator.SetTrigger("Damage");
        vida -= daño;
        StartCoroutine(DetenerMeshDaño(0.5f));

        if (vida <= 0)
        {
            StartCoroutine(Muerte());
        }
    }
    private IEnumerator DetenerMeshDaño(float tiempo)
    {
        agent.isStopped = true;
        agent.SetDestination(transform.position);
        yield return new WaitForSeconds(tiempo);
        PerseguirJugador();
        agent.isStopped = false;
    }


    private IEnumerator Muerte()
    {
        agent.isStopped = true;
        animator.SetTrigger("Die");
        agent.SetDestination(transform.position);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        GameManager.Instance.objetosEliminados.Add(name);
        Destroy(gameObject);
    }
    private void DañarPlayer()
    {
        GameManager.Instance.bloquearInputs = true;
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = new Vector3(999, 999, 999);
        player.GetComponent<CharacterController>().enabled = true;
        UiManager.instance.pantallaDerrota.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radio);
    }
}
