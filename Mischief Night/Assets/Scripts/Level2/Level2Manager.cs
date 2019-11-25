using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Manager : MonoBehaviour
{
    [Header("Required References")]
    [SerializeField] List<Collectable> collectables = new List<Collectable>();
    [SerializeField] GameObject collectableActivationEffect;
    [SerializeField] GameObject mineEntraceBlocker;

    private void Awake()
    {
        foreach (var c in collectables)
        {
            c.gameObject.SetActive(false);
            c.OnCollect += OnCollection;
        }
    }

    public void ActivateCollectables()
    {
        foreach (var c in collectables)
            c.gameObject.SetActive(true);
        collectableActivationEffect.SetActive(true);
    }

    private void OnCollection(Collectable collectable)
    {
        if (collectable && collectables.Contains(collectable))
            collectables.Remove(collectable);
    }

    public void TryActivateAltar(Altar altar)
    {
        if (collectables.Count <= 0)
        {
            mineEntraceBlocker.SetActive(false);
            altar.Activate();
        }
    }
}
