using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public Camera mainCam;
    public Light directionalLight;
    [Space(10)]
    [Header("UiPart")]
    public GameObject mainMenu;
    public GameObject retryMenu;
    public GameObject showCaseImage;
    public GameObject levelCompletedScreen;
    public Image retryCountDown;
    public GameObject noThanksButton;
    public Text fromLevel, toLevel;
    public Image waterLevelIndicator, progressBar;
    public GameObject fuelIndicator;
    public GameObject scrollArea;
    public Text completionText;
    public string[] levelCOmpletionTextcontents;

    [Space]
    [Header("Level Controller")]
    public GameObject gun;
    public GameObject rayCastCollider;
    public List<LevelPrefabSets> levelPrefabSets;
    public GameObject[] levelPrefabs;
    public int[] levelwater;
    public float waterLimit, maxLimit;
    GameObject currentLevel;
   
    [HideInInspector] public  int levelnumber,nextRandomIndex; public bool gameOver;
    [HideInInspector] public bool levelLoaded;

    [Space]
    [Header("ParticalSystem")]
    public GameObject waterPartical;
    public GameObject explositionPartical;
    public GameObject firePartical;

    [Space]
    [Header("3D Models")]
    public GameObject[] animalModels;

    [Space]
    [Header("Color Manager")]
    public Color showCaseScrollObjectsBg;
    public GameObject[] scrollingScreenImages;


    [Space]
    [Header("Audio Management")]
    [Header("Audio Clips")]
    public AudioClip waveAudio;
    public AudioClip[] fallinginwaterSound;
    public AudioClip bombAudio;
    public AudioClip[] showCaseStrikeAudio;

    [Header("Audio Source")]
    public AudioSource waveaudioSource;
    public AudioSource fallinginWaterAudioSource;
    public AudioSource showCaseAudioSource;
    public AudioSource bombAudioSource;

    [Space]
    [Header("Mass Manager")]
    public List<float> massManagerList;

    int deathLevel;
    public enum PickUpEnum
    {
        Water,
        Bomb
    }

    public enum MassTypes
    {
        VeryLarge,
        Large,
        Medium,
        Small,
        VerySmall
    }

    private void Awake()
    {
        Facebook.Unity.FB.Init();
        Application.targetFrameRate = 60;
        instance = this;
    }

    public void CheatClear()
    {
        if(levelnumber<3)
        LevelSetUp.instance.NextLevel();
    }

    private void Start()
    {
        PlayerPrefs.DeleteAll();
        CheckShowCaseClearedAnimals();
        //UpdateShowCase(11);
        Invoke("CreateWaveAudioSound", Random.Range(4, 10));
       mainCam.enabled = true;
        fromLevel.text = "1";
        toLevel.text = "2";
        //levelnumber = 0;
        levelnumber = PlayerPrefs.GetInt("LevelCompleted", 0) * 5;
        deathLevel = levelnumber + Random.Range(6, 9);
        CreateLevel();
        //print(levelnumber);


    }

    public void PlayButtonPressed()
    {
        rayCastCollider.SetActive(true);
        mainMenu.SetActive(false);
        FireWater.instance.CreateLevel();
        gameOver = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    void CreateWaveAudioSound()
    {
        waveaudioSource.clip = waveAudio;
        waveaudioSource.Play();
        Invoke("CreateWaveAudioSound", Random.Range(4, 10));
    }

    public void CreateLevelFromSavedData()
    {
        
    }

    public void GameOver()
    {
        if (!gameOver)
        {
            retryCountDown.fillAmount = 1;
            noThanksButton.transform.localScale = Vector3.zero;
            noThanksButton.transform.DOScale(1, 1.5f).SetEase(Ease.InOutExpo);
            gameOver = true;
        }
    }

    public void PlayBombSound()
    {
        bombAudioSource.clip = bombAudio;
        bombAudioSource.Play();
    }

    public void CreateLevel()
    {
        // if(levelnumber > 0)
        //rayCastCollider.SetActive(true);
        waterLimit = 0;
        maxLimit = 0;
       // print(levelnumber);
        if ((levelnumber) % 5 == 0 && levelnumber > 1)
        {
            fromLevel.text = (((levelnumber ) / 5) + 1).ToString();
            toLevel.text = (((levelnumber ) / 5) + 2).ToString();
            PlayerPrefs.SetInt("LevelCompleted", levelnumber / 5);
            //print(levelnumber);
            // currentLevel = Instantiate(levelPrefabSets[nextRandomIndex].level1);
        }

        //CancelInvoke();
        int startingLevel = PlayerPrefs.GetInt("LevelCompleted", 0);
        print(startingLevel);
        if (startingLevel == 0)
        {
            nextRandomIndex = 0;
        }
        else
        {
            if(levelnumber%5 ==0)
            nextRandomIndex = levelnumber / 5;
        }

        levelLoaded = false;
        if (currentLevel)
        {
            FireWater.instance.CreateLevel();
            Destroy(currentLevel);
            gameOver = false;
        }

        print(levelnumber);
        float levelNumber = (levelnumber ) % 5;
        if (nextRandomIndex < 17)
        {
            if (levelNumber == 1)
            {
                progressBar.fillAmount = 0.20f;
                currentLevel = Instantiate(levelPrefabSets[nextRandomIndex].level2);
            }
            else if (levelNumber == 2)
            {
                progressBar.fillAmount = 0.40f;
                currentLevel = Instantiate(levelPrefabSets[nextRandomIndex].level3);
            }
            else if (levelNumber == 3)
            {
                progressBar.fillAmount = 0.60f;
                currentLevel = Instantiate(levelPrefabSets[nextRandomIndex].level4);
            }
            else if (levelNumber == 4)
            {
                progressBar.fillAmount = 0.80f;
                currentLevel = Instantiate(levelPrefabSets[nextRandomIndex].level5);
            }
            else
            {
                progressBar.fillAmount = 0.0f;
                currentLevel = Instantiate(levelPrefabSets[nextRandomIndex].level1);
            }
        } else 
        {
            if (levelNumber == 1)
            {
                progressBar.fillAmount = 0.20f;

            }
            else if (levelNumber == 2)
            {
                progressBar.fillAmount = 0.40f;

            }
            else if (levelNumber == 3)
            {
                progressBar.fillAmount = 0.60f;

            }
            else if (levelNumber == 4)
            {
                progressBar.fillAmount = 0.80f;

            }
            else
            {
                progressBar.fillAmount = 0.0f;

            }
            currentLevel = Instantiate(levelPrefabs[Random.Range(0, levelPrefabs.Length)]);
        }
       

        //maxLimit = levelwater[levelnumber];
        //waterLimit = levelwater[levelnumber];
        if (deathLevel != levelnumber)
        {
            foreach (Rigidbody item in currentLevel.GetComponent<LevelSetUp>().defaultRigidBody)
            {
                if (item.gameObject.GetComponent<MassManager>())
                {
                    if (item.gameObject.GetComponent<MassManager>())
                    {
                        if (item.gameObject.GetComponent<MassManager>().massTypes == MassTypes.VeryLarge)
                        {
                            maxLimit += 0.5f;
                            waterLimit += 0.5f;
                        }
                        else if (item.gameObject.GetComponent<MassManager>().massTypes == MassTypes.Large)
                        {
                            maxLimit += 0.4f;
                            waterLimit += 0.4f;
                        }
                        else if (item.gameObject.GetComponent<MassManager>().massTypes == MassTypes.Medium)
                        {
                            maxLimit += 0.3f;
                            waterLimit += 0.3f;
                        }
                        else if (item.gameObject.GetComponent<MassManager>().massTypes == MassTypes.Small)
                        {
                            maxLimit += 0.2f;
                            waterLimit += 0.2f;
                        }
                        else if (item.gameObject.GetComponent<MassManager>().massTypes == MassTypes.VerySmall)
                        {
                            maxLimit += 0.1f;
                            waterLimit += 0.1f;
                        }

                    }
                }
            }
        }
        else 
        {
            foreach (Rigidbody item in currentLevel.GetComponent<LevelSetUp>().defaultRigidBody)
            {
                if (item.gameObject.GetComponent<MassManager>())
                {
                    if (item.gameObject.GetComponent<MassManager>().massTypes == MassTypes.VeryLarge)
                    {
                        maxLimit += 0.2f;
                        waterLimit += 0.2f;
                    }
                    else if (item.gameObject.GetComponent<MassManager>().massTypes == MassTypes.Large)
                    {
                        maxLimit += 0.1f;
                        waterLimit += 0.1f;
                    }
                    else if (item.gameObject.GetComponent<MassManager>().massTypes == MassTypes.Medium)
                    {
                        maxLimit += 0.04f;
                        waterLimit += 0.04f;
                    }
                    else if (item.gameObject.GetComponent<MassManager>().massTypes == MassTypes.Small)
                    {
                        maxLimit += 0.03f;
                        waterLimit += 0.03f;
                    }
                    else if (item.gameObject.GetComponent<MassManager>().massTypes == MassTypes.VerySmall)
                    {
                        maxLimit += 0.01f;
                        waterLimit += 0.01f;
                    }
                }
            }
            deathLevel = deathLevel + Random.Range(5,8);
        }

        FireWater.instance.waterAmount = 1;
        UpdateWaterIndicator();
    }

    void GetIndex()
    {
        nextRandomIndex = 1;
        int randomIndex =  levelPrefabSets.IndexOf(levelPrefabSets[Random.Range(1, levelPrefabSets.Count)]);
        if(PlayerPrefs.GetInt("levelPrefabSet"+nextRandomIndex,0)==0)
        {
            nextRandomIndex = randomIndex;
        } else 
        {
            GetIndex();
        }
        //return a;
    }

    public void IncrementScrollBar(float scrollValue)
    {
        progressBar.fillAmount = progressBar.fillAmount + ((1 * 0.20f) / scrollValue);
    }

    public void WaterPickUp(Vector3 position)
    {
        waterLimit = 12.5f;
        maxLimit = 20;
        //waterLevelIndicator.DOFillAmount(waterLimit / maxLimit, 0.5f);
        UpdateWaterIndicator();
    }

    public void ContinueCurrentLevel()
    {
        retryMenu.SetActive(false);
        DOTween.KillAll();
        gameOver = false;
        waterLimit += 15;
    }

    private void CheckShowCaseClearedAnimals()
    {
        for (int i = 0; i < scrollingScreenImages.Length; i++)
        {
            GameObject currentImage = scrollingScreenImages[i];
            string crossedDetails = PlayerPrefs.GetString("Animal" + i, "Pending");
            if (crossedDetails == "Cleared")
            {
                currentImage.transform.GetChild(1).gameObject.SetActive(true);
                currentImage.transform.GetChild(2).gameObject.SetActive(false);
                currentImage.transform.GetChild(3).gameObject.SetActive(true);
                currentImage.transform.GetChild(3).GetComponent<Image>().DOFade(1, 0.2f);
                currentImage.transform.GetChild(4).gameObject.SetActive(false);
            }
        }
    }

    public void CreateDropSound()
    {
        if(!fallinginWaterAudioSource.isPlaying)
        {
            fallinginWaterAudioSource.clip = fallinginwaterSound[Random.Range(0, fallinginwaterSound.Length)];
            fallinginWaterAudioSource.Play();
        }
    }

    public void UpdateShowCase(int animalNumber)
    {
        //animalNumber -= 1;
        PlayerPrefs.SetInt("levelPrefabSet" + nextRandomIndex, 1);
        if(animalNumber <12)
        {
            scrollArea.GetComponent<ScrollRect>().verticalNormalizedPosition = 1f;
        }  else 
        {
            scrollArea.GetComponent<ScrollRect>().verticalNormalizedPosition = 0f;
        }
        showCaseImage.SetActive(true);
        Color objColor = scrollingScreenImages[animalNumber].transform.GetChild(3).GetComponent<Image>().color;
        objColor.a = 0;
        scrollingScreenImages[animalNumber].transform.GetChild(3).GetComponent<Image>().color = objColor;
        scrollingScreenImages[animalNumber].transform.GetChild(3).transform.localScale = Vector3.one * 10;
        scrollingScreenImages[animalNumber].transform.GetChild(3).gameObject.SetActive(true);
        scrollingScreenImages[animalNumber].transform.GetChild(3).transform.DOScale(1, 0.5f).SetEase(Ease.Linear);
        showCaseAudioSource.clip = showCaseStrikeAudio[Random.Range(0, showCaseStrikeAudio.Length)];
        scrollingScreenImages[animalNumber].transform.GetChild(3).GetComponent<Image>().DOFade(1, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            
            scrollingScreenImages[animalNumber].transform.GetChild(4).GetComponent<Image>().fillAmount = 0;
            scrollingScreenImages[animalNumber].transform.GetChild(4).gameObject.SetActive(false);
            scrollingScreenImages[animalNumber].transform.GetChild(4).GetComponent<Image>().DOFillAmount(1, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
            {
                scrollingScreenImages[animalNumber].transform.GetChild(1).transform.localScale = Vector3.one;
                scrollingScreenImages[animalNumber].transform.GetChild(1).gameObject.SetActive(true);
                scrollingScreenImages[animalNumber].transform.GetChild(1).transform.DOScale(1, 0.5f).OnComplete(() => {
                    scrollingScreenImages[animalNumber].transform.GetChild(1).transform.DOScale(1, 0.5f).OnComplete(() =>
                    {
                        PlayerPrefs.SetString("Animal" + animalNumber, "Cleared");
                        LevelSetUp.instance.NextLevel();
                        showCaseImage.SetActive(false);
                        CheckShowCaseClearedAnimals();
                    });
                });
            });
        });
    }

    public void UpdateWaterIndicator()
    {
        float percent = waterLimit / maxLimit;
        float difference = 1 - percent;
        float crossedAngle = difference * 180;
        float remainingAngle =  90 - crossedAngle;
        fuelIndicator.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -remainingAngle));

        //print(waterLimit);
        if (waterLimit <= 0 && !gameOver)
        {
            gameOver = true;
            StopCoroutine(RestartMenu());
            StartCoroutine(RestartMenu());
        }
    }

    public IEnumerator RestartMenu()
    {
        yield return new WaitForSeconds(6f);
        gameOver = true;
        retryMenu.SetActive(true);
        rayCastCollider.SetActive(false);
    }


}
