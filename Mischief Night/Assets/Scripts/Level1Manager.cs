using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Manager : LevelManager
{
    private void Start()
    {
        GameManager.Instance.Player.SetObjective("Enter the school.");
    }
}
