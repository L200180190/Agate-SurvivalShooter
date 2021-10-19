using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    //PlayerShooting playerShooting;
    bool isDead;                                                
    bool damaged;

    void Awake()
    {
        // Mendapat reference component
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();

        //playerShooting = GetComponentInChildren<PlayerShooting>();
        currentHealth = startingHealth;
    }

    void Update()
    {
        // Apabila terkena damage
        if (damaged)
        {
            // Rubah warna gambar jadi value dari flashColour
            damageImage.color = flashColour;
        }
        else
        {
            // Fading out damage image
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed*Time.deltaTime);
        }

        // Set damage jadi false
        damaged = false;
    }

    // Function untuk dapat damage
    public void TakeDamage(int amount)
    {
        damaged = true;

        // Mengurangi health
        currentHealth -= amount;

        // Rubah tampilan heartslider
        healthSlider.value = currentHealth;

        // Mainkan suara saat kena damage
        playerAudio.Play();

        // Panggil method Death() jika health <= 0 dan belum mati
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;

        //playerShooting.DisableEffects();

        // Set Trigger pada animasi Die
        anim.SetTrigger("Die");

        // Mainkan suara efek saat mati
        playerAudio.clip = deathClip;
        playerAudio.Play();

        // Matikan script PlayerMovement
        playerMovement.enabled = false;

        //playerShooting.enabled = false;
    }

    public void RestartLevel()
    {
        // Load ulang scene dg index 0 pada build setting
        SceneManager.LoadScene(0);
    }
}
