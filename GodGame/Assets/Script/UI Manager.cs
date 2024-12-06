using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private bool isDisplayed = false; // vérifie l'etat d'activité du panel schooldisplay
    private bool isPaused = false;
    private bool isAccelerated = false;

    public GameObject SchoolPanel;

    /// <summary>
    /// Fonction servant a afficher le panel montrant le choix des metiers
    /// </summary>
    public void DisplaySchoolPanel()
    {
        isDisplayed = true;
        SchoolPanel.SetActive(true);
    }

    /// <summary>
    /// Fonction servant a cacher le panel montrant le choix des metiers
    /// </summary>
    public void HideSchoolPanel()
    {
        isDisplayed = false;
        SchoolPanel.SetActive(false);
    }

    /// <summary>
    /// Permet de sélectionner un personnage aléatoire dans la liste selon le métier de celui ci
    /// </summary>
    public void SelectCharacterByJobInList(int job)
    {
        switch (job)
        {
            case 0:
            //GameManager.Instance.selectedCharacter = ListeDuJobEnQuestion(Random.Range(0, ListeDuJobEnQuestion.Count() -1));
                break;

            case 1:
            //GameManager.Instance.selectedCharacter = ListeDuJobEnQuestion(Random.Range(0, ListeDuJobEnQuestion.Count() -1));
                break;

            case 2:
            //GameManager.Instance.selectedCharacter = ListeDuJobEnQuestion(Random.Range(0, ListeDuJobEnQuestion.Count() -1));
                break;

            case 3:
            //GameManager.Instance.selectedCharacter = ListeDuJobEnQuestion(Random.Range(0, ListeDuJobEnQuestion.Count() -1));
                break;

            case 4:
            //GameManager.Instance.selectedCharacter = ListeDuJobEnQuestion(Random.Range(0, ListeDuJobEnQuestion.Count() -1));
                break;
        }
    }

    /// <summary>
    /// Fonction pour mettre le jeu en pause puis le reprendre
    /// </summary>
    public void PauseGame()
    {
        if (!isPaused)
        {
            isPaused = true;
            Time.timeScale = 0.01f;
        }
        else
        {
            isPaused = false;
            Time.timeScale = 1;
        }
    }

    /// <summary>
    /// Permet d'accelerer le jeu
    /// </summary>
    public void AccelerateGame()
    {
        if (!isAccelerated)
        {
            isAccelerated = true;
            Time.timeScale = 2f;
        }
        else
        {
            isAccelerated = false;
            Time.timeScale = 1;
        }
    }
}
