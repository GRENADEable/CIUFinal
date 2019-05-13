using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMechanic : MonoBehaviour
{
    public GameObject match;
    public delegate void SendEvents();
    public static event SendEvents onFleeEnemy;
    public static event SendEvents onChasePlayer;
    public AudioSource litMatchstickAud;

    public bool lightOn;
    [SerializeField]
    private Collider col;
    [SerializeField]
    private int matchesCount;
    private Animator courageAnim;

    void Start()
    {
        courageAnim = GetComponent<Animator>();
    }

    void OnDisable()
    {
        courageAnim.SetBool("isWithTorch", false);
    }

    void Update()
    {
        //  Picking up a match comes here
        if (Input.GetKeyDown(KeyCode.F) && !lightOn && matchesCount > 0 && !match.activeSelf)
        {
            match.SetActive(true);
            lightOn = true;
            courageAnim.SetBool("isWithTorch", true);
            litMatchstickAud.Play();
        }
        else if (Input.GetKeyDown(KeyCode.F) && lightOn)
        {
            match.SetActive(false);
            lightOn = false;
            courageAnim.SetBool("isWithTorch", false);
            litMatchstickAud.Stop();
        }

        if (match.activeSelf)
        {
            if (onFleeEnemy != null)
                onFleeEnemy();
        }
        else if (!match.activeSelf)
        {
            if (onChasePlayer != null)
                onChasePlayer();
        }

        if (col != null && Input.GetKeyDown(KeyCode.E))
        {
            matchesCount = 1;
            Destroy(col.gameObject);
            col = null;
        }
    }

    public void GiveMatchStick()
    {
        matchesCount = 1;
        Debug.LogWarning("Matchstick Aquired");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Matchstick")
        {
            col = other;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Matchstick")
        {
            col = null;
        }
    }
}
