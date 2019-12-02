using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWinner : MonoBehaviour, IInteractable
{
    [SerializeField] string endingSceneName;

    public void Interact()
    {
        SceneManager.LoadScene(endingSceneName);
    }
}
