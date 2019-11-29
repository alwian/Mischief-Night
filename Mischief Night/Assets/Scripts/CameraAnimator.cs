using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct PathSegment
{
    public Transform transform;
    public float segmentTime;
}

public class CameraAnimator : MonoBehaviour
{
    [Header("Path")]
    [SerializeField] PathSegment[] pathSegments;

    

    new Camera camera;

    private void Awake()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            StartAnimation();
    }

    public void StartAnimation()
    {
        GameManager.Instance.Player.DisableControl();
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        camera.transform.position = pathSegments[0].transform.position;
        for (int i=0; i<pathSegments.Length - 1; i++)
        {
            float timer = 0f;
            float segmentLength = pathSegments[i].segmentTime;
            while (timer < segmentLength)
            {
                var prog = timer / segmentLength;

                camera.transform.position = Vector3.Lerp(pathSegments[i].transform.position, pathSegments[i + 1].transform.position, prog);
                camera.transform.rotation= Quaternion.Slerp(pathSegments[i].transform.rotation, pathSegments[i + 1].transform.rotation, prog);

                timer += Time.deltaTime;
                yield return null;
            }
        }

        FinishAnimation();
    }

    private void FinishAnimation()
    {
        GameManager.Instance.Player.EnableControl();
    }
}
