using UnityEngine;

public class PlayerController: MonoBehaviour
{
    // Partículas públicas
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    // Clips de audio públicos
    public AudioClip jumpSound;
    public AudioClip crashSound;

    // Variables privadas
    private AudioSource playerAudio;
    private bool isOnGround = true;
    private bool gameOver = false;

    void Start()
    {
        // Inicializa el componente de audio
        playerAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Reproduce partículas y sonido al saltar
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Reproduce partículas de tierra al aterrizar
            dirtParticle.Play();
            isOnGround = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Reproduce explosión y detiene las partículas de tierra al chocar con un obstáculo
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
            gameOver = true; // Marcar el fin del juego
        }
    }
}
