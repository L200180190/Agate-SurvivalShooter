using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    //EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;


    void Awake ()
    {
        // Cari game object dg tag "Player"
        player = GameObject.FindGameObjectWithTag ("Player");

        // Mendaoat komponen player health
        playerHealth = player.GetComponent <PlayerHealth> ();

        //enemyHealth = GetComponent<EnemyHealth>();

        // Mendaoat komponen animator 
        anim = GetComponent <Animator> ();
    }

    // Callback function jika ada object masuk dalam trigger
    void OnTriggerEnter (Collider other)
    {
        // Set player dlm range
        if(other.gameObject == player && other.isTrigger == false)
        {
            playerInRange = true;
        }
    }

    // Callback function jika object keluar dri trigger
    void OnTriggerExit (Collider other)
    {
        // Set player not in range
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }


    void Update ()
    {
        timer += Time.deltaTime;

        if(timer >= timeBetweenAttacks && playerInRange/* && enemyHealth.currentHealth > 0*/)
        {
            Attack ();
        }

        // trigger animasi PlayerDead jika darah player <= 0 
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger ("PlayerDead");
        }
    }


    void Attack ()
    {
        // Reset Timer
        timer = 0f;

        // Take damage
        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage (attackDamage);
        }
    }
}
