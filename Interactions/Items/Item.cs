using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    private GameObject player;
    public override KeyCode inputKey => KeyCode.Mouse0;
    public bool usable = false;

    public Color particleColor = Color.white;
    public bool particleOn = true;
    
    private ParticleSystem particleSystem;
    
    public override void Interact(GameObject interactor)
    {
        if (interactor.CompareTag("Player"))
        {
            player = interactor;
            Inventory inventory = interactor.transform.Find("Inventory").GetComponent<Inventory>();

            if (inventory)
            {
                gameObject.transform.position = Vector3.zero;
                gameObject.transform.rotation = Quaternion.identity;
                inventory.Add(gameObject);
            }
        }
    }

    public virtual void Use() {}

    public void Start()
    {
        particleSystem = gameObject.AddComponent<ParticleSystem>();
        var main = particleSystem.main;
        main.startColor = particleColor;
        main.startSize = 0.1f;
        main.startSpeed = 0.5f;
        main.loop = true;

        var emission = particleSystem.emission;
        emission.rateOverTime = 10;

        var shape = particleSystem.shape;
        shape.shapeType = ParticleSystemShapeType.Sphere;
        shape.radius = 0.1f;

        var particleMaterial = new Material(Shader.Find("Particles/Standard Unlit"));
        particleMaterial.SetColor("_Color", particleColor);

        var renderer = particleSystem.GetComponent<ParticleSystemRenderer>();
        renderer.material = particleMaterial;

        particleSystem.Play();
    }

    public void Update()
    {
        if (interactable && !usable && !particleOn)
        {
            particleSystem.Play();
            particleOn = true;
        }

        else if (!interactable && usable && particleOn)
        {
            particleSystem.Stop();
            particleOn = false;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && usable)
        {
            Use();
        }
    }

    public GameObject GetPlayer() => player;
}