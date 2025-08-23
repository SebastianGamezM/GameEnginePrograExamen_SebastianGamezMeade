using UnityEngine;
using System.Collections;

public abstract class Armas : MonoBehaviour
{
    public string id;

    [Header("Stats")]
    public int municionMax;
    public int municionActual;
    public float tiempoRecarga;
    public float fireRate;
    public int daño;
    public float fuerzaDisparo;


    [Header("References")]
    public GameObject balaPrefab;
    public Transform shootPoint;
    public Sprite imagenUI;

    public bool estaRecargando = false;
    public bool enCooldown = false;

    public abstract void Shoot();

    public abstract IEnumerator Reload();

    public abstract IEnumerator FireCooldown();

}
