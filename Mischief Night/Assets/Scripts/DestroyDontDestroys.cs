using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDontDestroys : MonoBehaviour
{
    private void Awake()
    {
        var player = FindObjectOfType<Player>();
        if (player)
            Destroy(player.gameObject);

        var gm = FindObjectOfType<GameManager>();
        if (gm)
            Destroy(gm.gameObject);

        var dm = FindObjectOfType<DimensionManager>();
        if (dm)
            Destroy(dm.gameObject);

        var cm = FindObjectOfType<CameraManager>();
        if (cm)
            Destroy(cm.gameObject);

        // Find it via the post processing components
        // Differentiates between menu cameras
        var gameCam = FindObjectOfType<UnityEngine.Rendering.PostProcessing.PostProcessVolume>();
        if (gameCam)
            Destroy(gameCam.gameObject);
    }
}
