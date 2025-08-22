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
    public int da�o = 10;
    public int vida = 20;

    [Header("Detecci�n")]
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
            Da�arPlayer();
        }
    }

    public void RecibirDa�o(int da�o)
    {
        animator.SetTrigger("Damage");
        vida -= da�o;
        StartCoroutine(DetenerMeshDa�o(0.5f));

        if (vida <= 0)
        {
            StartCoroutine(Muerte());
        }
    }
    private IEnumerator DetenerMeshDa�o(float tiempo)
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

        Destroy(gameObject);
    }
    private void Da�arPlayer()
    {
        //player.position = new Vector3(999, 999, 999);
        //StartCoroutine(GameManager.instance.Derrota());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radio);
    }
}
