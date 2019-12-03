/*
 * Author: Colton Campbell (B00693513)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Manager : LevelManager
{
    [SerializeField] AudioSource audioSource;

    [Header("Required References")]
    [SerializeField] List<Collectable> collectables = new List<Collectable>();
    [SerializeField] List<DimensionPortal> portals = new List<DimensionPortal>();
    [SerializeField] GameObject collectablePreEffect;
    [SerializeField] GameObject collectableActivationEffect;
    [SerializeField] GameObject collectablesCompleteEffect;
    [SerializeField] GameObject mineEntraceBlocker;
    [SerializeField] GameObject lowfiLevel1;
    [SerializeField] GameObject levelTransitionZone;

    [Header("Objectives")]
    [SerializeField] string startObjective;
    [SerializeField] string collectObjective;
    [SerializeField] string returnObjective;
    [SerializeField] string nextLevelObjective;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        collectablePreEffect.SetActive(true);
        collectablesCompleteEffect.SetActive(false);
        collectableActivationEffect.SetActive(false);
        mineEntraceBlocker.SetActive(true);
        levelTransitionZone.SetActive(false);

        foreach (var c in collectables)
        {
            c.gameObject.SetActive(false);
            c.OnCollect += OnCollection;
        }

        foreach (var p in portals)
            p.gameObject.SetActive(false); 
    }

    public override void StartLevel()
    {
        base.StartLevel();
        lowfiLevel1.SetActive(true);
        GameManager.Instance.Player.SetObjective(startObjective);
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

        GameManager.Instance.Player.SetObjective(collectObjective);
    }

    private void OnCollection(Collectable collectable)
    {
        if (collectable && collectables.Contains(collectable))
            collectables.Remove(collectable);

        if (collectables.Count <= 0)
        {
            GameManager.Instance.Player.SetObjective(returnObjective);
            collectablesCompleteEffect.SetActive(true);
        }
    }

    public void TryActivateAltar(Altar altar)
    {
        if (collectables.Count <= 0)
        {
            audioSource.Play();
            mineEntraceBlocker.SetActive(false);
            levelTransitionZone.SetActive(true);
            altar.Activate();

            foreach (var p in portals)
                Destroy(p.gameObject);

            DimensionManager.Instance.SetDimension(Dimension.OVERWORLD);
            GameManager.Instance.Player.SetObjective(nextLevelObjective);
        }
    }
}
