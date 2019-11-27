/*
 * Author: Colton Campbell (B00693513)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Manager : MonoBehaviour
{
    [Header("Required References")]
    [SerializeField] List<Collectable> collectables = new List<Collectable>();
    [SerializeField] List<DimensionPortal> portals = new List<DimensionPortal>();
    [SerializeField] GameObject collectablePreEffect;
    [SerializeField] GameObject collectableActivationEffect;
    [SerializeField] GameObject collectablesCompleteEffect;
    [SerializeField] GameObject mineEntraceBlocker;

    // Ensure effects and entrace blockage are in proper states
    private void Awake()
    {
        collectablePreEffect.SetActive(true);
        collectablesCompleteEffect.SetActive(false);
        collectableActivationEffect.SetActive(false);
        mineEntraceBlocker.SetActive(true);
    }

    private void Start()
    {
        foreach (var c in collectables)
        {
            c.gameObject.SetActive(false);
            c.OnCollect += OnCollection;
        }

        foreach (var p in portals)
            p.gameObject.SetActive(false);
    }

    public void ActivateCollectables()
    {
        foreach (var c in collectables)
            if (c.Dimension == DimensionManager.Instance.CurrentDimension)
                c.gameObject.SetActive(true);

        foreach (var p in portals)
        {
            p.gameObject.SetActive(true);
            p.SetDimension(DimensionManager.Instance.CurrentDimension);
        }   

        collectablePreEffect.SetActive(false);
        collectableActivationEffect.SetActive(true);
    }

    private void OnCollection(Collectable collectable)
    {
        if (collectable && collectables.Contains(collectable))
            collectables.Remove(collectable);

        if (collectables.Count <= 0)
            collectablesCompleteEffect.SetActive(true);
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
