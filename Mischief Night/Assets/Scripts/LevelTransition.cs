using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CameraAnimator))]
public class LevelTransition : MonoBehaviour
{
    [SerializeField] string nextLevel;

    CameraAnimator cameraAnim;

    private void Awake()
    {
        cameraAnim = GetComponent<CameraAnimator>();
    }
    string currentLevel;
    bool started = false;
    private void Update()
    {
        if (!started && Input.GetKeyDown(KeyCode.Y))
        {
            started = true;

            currentLevel = SceneManager.GetActiveScene().name;
            var op = SceneManager.LoadSceneAsync(nextLevel, LoadSceneMode.Additive);
            op.completed += SetActive;

            cameraAnim.StartAnimation(FinishTransition);
        }
    }

    private void SetActive(AsyncOperation op)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(nextLevel));
    }

    private void FinishTransition()
    {
        var op = SceneManager.UnloadSceneAsync(currentLevel);

        op.completed += GameManager.Instance.StartLevel;
    }
}
