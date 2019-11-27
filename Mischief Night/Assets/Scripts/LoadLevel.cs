using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Placeholder level transition script.
/// Loads the given level on player collision
/// </summary>
public class LoadLevel : MonoBehaviour
{
    [SerializeField] string sceneName;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
            SceneManager.LoadScene(sceneName);
    }
}
