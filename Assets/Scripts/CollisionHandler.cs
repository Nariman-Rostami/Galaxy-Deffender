using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem explosionParticle;
    [SerializeField] float reLoadDelay = 1;
    PlayerControler playerControler;
    void OnTriggerEnter(Collider other)
    {
        playerControler = GetComponent<PlayerControler>();  
        StartCrashSequence(reLoadDelay);       
    }
    void StartCrashSequence(float delay)
    {
        playerControler.enabled = false;
        PlayExplosionParticle();
        DisableMeshRenderers(transform);
        Invoke("ReloadLevel",delay);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void DisableMeshRenderers(Transform parent)
    {
        MeshRenderer[] meshRenderers = parent.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer renderer in meshRenderers)
        {
            renderer.enabled = false;
        }
    }
    void PlayExplosionParticle() {explosionParticle.Play();}
}
