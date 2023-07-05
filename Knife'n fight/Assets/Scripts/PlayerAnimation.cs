using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private CharacterController controller;
    public ParticleSystem footsteps;
    public AudioSource walkingsound;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Get the speed of the player's movement
        float speed = controller.velocity.magnitude;

        // Set the animation speed to match the player'
        animator.SetFloat("MovementSpeed", speed);
        // If the player isn't moving, stop the animation and particle system
        if (speed == 0)
        {
            animator.Play("Move.Wait");
            if (footsteps.isPlaying)
            {
                footsteps.Stop();
                walkingsound.Stop();
            }
        }
        else
        {
            animator.speed = 1;
            // If the player is moving, play the particle system
            if (!footsteps.isPlaying)
            {
                footsteps.Play();
                walkingsound.Play();
            }
        }
    }
}
