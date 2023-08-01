using UnityEngine;

public class ParticleCollider : MonoBehaviour
{
    public ParticleSystem particleSystem;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 0)
        {
            Renderer groundRenderer = other.GetComponent<Renderer>();
            Material groundMaterial = groundRenderer.material;
            Renderer ParticleRender = particleSystem.GetComponent<Renderer>();
            ParticleRender.material = groundMaterial;
            particleSystem.Play();
        }
    }

    private void Update()
    {
        // Keep the Particle System always oriented upward
        particleSystem.transform.up = Vector3.up;
    }
}

