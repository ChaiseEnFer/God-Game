using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
    [SerializeField]
    private Slider _happinessSlider;
    [SerializeField]
    private spawnBuilding buildHandler;
    [SerializeField]
    private TopDownCameraManager camScript;
    private bool _isPaused = false;
    private bool _isAccelerated = false;

    public GameObject SchoolPanel;
    public GameObject BuildPanel;
    public GameObject PInfoPanel;

    /// <summary>
    /// Fonction servant a afficher le panel montrant le choix des metiers
    /// </summary>
    public void DisplaySchoolPanel()
    {
        SchoolPanel.SetActive(true);
    }

    public void DisplayPeopleInfoPanel()
    {
        PInfoPanel.SetActive(true);
    }

    /// <summary>
    /// Fonction servant a cacher le panel montrant le choix des metiers
    /// </summary>
    public void HideSchoolPanel()
    {
        SchoolPanel.SetActive(false);
    }

    public void HidePeopleInfoPanel()
    {
        PInfoPanel.SetActive(false);
    }

    /// <summary>
    /// Permet de sélectionner un personnage aléatoire dans la liste selon le métier de celui ci
    /// </summary>
    public void SelectCharacterByJobInList(int job)
    {
        switch (job)
        {
            case 0:
                if (GameManager.Instance.Wanderers.Count > 0)
                    GameManager.Instance.SelectedCharacter = GameManager.Instance.Wanderers[Random.Range(0, GameManager.Instance.Wanderers.Count -1)];
                break;

            case 1:
                if (GameManager.Instance.FoodHarvesters.Count > 0)
                    GameManager.Instance.SelectedCharacter = GameManager.Instance.FoodHarvesters[Random.Range(0, GameManager.Instance.FoodHarvesters.Count -1)];
                break;

            case 2:
                if (GameManager.Instance.Timbers.Count > 0)
                    GameManager.Instance.SelectedCharacter = GameManager.Instance.Timbers[Random.Range(0, GameManager.Instance.Timbers.Count - 1)];
                break;

            case 3:
                if (GameManager.Instance.Miners.Count > 0)
                    GameManager.Instance.SelectedCharacter = GameManager.Instance.Miners[Random.Range(0, GameManager.Instance.Miners.Count -1)];
                break;

            case 4:
                if (GameManager.Instance.Masons.Count > 0)
                    GameManager.Instance.SelectedCharacter = GameManager.Instance.Masons[Random.Range(0, GameManager.Instance.Masons.Count -1)];
                break;
        }

        buildHandler.CheckForDisplay();
    }

    /// <summary>
    /// Fonction pour mettre le jeu en pause puis le reprendre
    /// </summary>
    public void PauseGame()
    {
        if (!_isPaused)
        {
            _isPaused = true;
            Time.timeScale = 0.01f;
            camScript.CameraSpeed *= 100;
        }
        else
        {
            _isPaused = false;
            Time.timeScale = 1;
            camScript.CameraSpeed /= 100;
        }
    }

    /// <summary>
    /// Permet d'accelerer le jeu
    /// </summary>
    public void AccelerateGame()
    {
        if (!_isAccelerated)
        {
            _isAccelerated = true;
            Time.timeScale = 4f;
        }
        else
        {
            _isAccelerated = false;
            Time.timeScale = 1;
        }
    }

    public void UpdateSlider()
    {
        _happinessSlider.value = GameManager.Instance.ActualHappiness;
    }
}
