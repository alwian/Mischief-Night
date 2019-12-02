using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Required References")]
    [SerializeField] GameObject mainScreen;
    [SerializeField] GameObject levelSelectScreen;

    [Header("Main Screen Buttons")]
    [SerializeField] Button playButton;
    [SerializeField] Button levelSelectButton;

    [Header("Level Select Buttons")]
    [SerializeField] Button level1Button;
    [SerializeField] Button level2Button;
    [SerializeField] Button level3Button;
    [SerializeField] Button backButton;

    [Header("Scene Names")]
    [SerializeField] string level1Name;
    [SerializeField] string level2Name;
    [SerializeField] string level3Name;

    private void Awake()
    {
        playButton.onClick.AddListener(Level1);
        level1Button.onClick.AddListener(Level1);
        level2Button.onClick.AddListener(Level2);
        level3Button.onClick.AddListener(Level3);

        levelSelectButton.onClick.AddListener(OpenLevelSelect);
        backButton.onClick.AddListener(OpenMainScreen);
    }


    private void OpenLevelSelect()
    {
        mainScreen.SetActive(false);
        levelSelectScreen.SetActive(true);
    }

    private void OpenMainScreen()
    {
        mainScreen.SetActive(true);
        levelSelectScreen.SetActive(false);
    }

    private void Level1()
    {
        SceneManager.LoadScene(level1Name);
    }
    private void Level2()
    {
        SceneManager.LoadScene(level2Name);
    }
    private void Level3()
    {
        SceneManager.LoadScene(level3Name);
    }
}
