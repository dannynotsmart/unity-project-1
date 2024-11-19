using System.Collections;
using UnityEngine;

public class Ingot : Interactable
{
    private GameObject gameManager;
    public override KeyCode inputKey => KeyCode.Mouse0;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        
        var particleSystem = gameObject.AddComponent<ParticleSystem>();

        var main = particleSystem.main;
        main.startColor = Color.yellow;
        main.startSize = 0.1f;
        main.startSpeed = 0.5f;
        main.loop = true;

        var emission = particleSystem.emission;
        emission.rateOverTime = 10;

        var shape = particleSystem.shape;
        shape.shapeType = ParticleSystemShapeType.Sphere;
        shape.radius = 0.1f;

        var particleMaterial = new Material(Shader.Find("Particles/Standard Unlit"));
        particleMaterial.SetColor("_Color", Color.yellow);

        var renderer = particleSystem.GetComponent<ParticleSystemRenderer>();
        renderer.material = particleMaterial;

        particleSystem.Play();
    }

    public override void Interact(GameObject interactor)
    {
        GameManager gM = gameManager.GetComponent<GameManager>();

        if (gM)
        {
            gM.ingotsCollected++;
            Destroy(transform.parent.gameObject);
        }
    }
}