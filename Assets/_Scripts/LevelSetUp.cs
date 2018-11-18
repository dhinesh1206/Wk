using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LevelSetUp : MonoBehaviour {

    public static LevelSetUp instance;

    [SerializeField]List<GameObject> cubesInLevel;
    float percentPerCube;

    [Space]
    [Header("Normal Levels")]
    public GameObject ground;
    public float groundPoint,levelPoint;
    public GameObject levelPart;
    public Rigidbody[] defaultRigidBody;

    [Space]
    [Header("Enter the Values Below For Bosslevel")]
    public int bossIndex;
    [SerializeField] bool isBossLevel = false, animateInAnimation;
    public GameObject savingaAnimal;
    public Transform iceCage;
    public GameObject tempIceCageA, tempIceCageB;
    Animator savinaAnimalAnimator;
    Vector3 initialCamPosition;

    [Space]
    [Header("Colour Management")]
    public Color bgColor;
    public Color lightColor;
    public Color cloudColor;

	void Start () 
    {
        initialCamPosition = Camera.main.transform.position;
        percentPerCube = cubesInLevel.Count;
        Invoke("StartLevel",0f);
        if(isBossLevel&& animateInAnimation)
        savinaAnimalAnimator = savingaAnimal.GetComponent<Animator>();
        foreach (var item in defaultRigidBody)
        {
            item.mass = item.mass / 2;
        }
       // Camera.main.backgroundColor = bgColor;
       // GameManager.instance.directionalLight.color = lightColor;
    }

    private void Awake()
    {
        instance = this;
    }

    public void RemoveKnockedObjects(GameObject knockedobject)
    {
        if(cubesInLevel.Contains(knockedobject))
        {
            GameManager.instance.CreateDropSound();
            cubesInLevel.Remove(knockedobject);
            knockedobject.transform.DOScale(0, 1f).OnComplete(() =>
            {
                GameManager.instance.IncrementScrollBar(percentPerCube);
                Destroy(knockedobject);
            });
            if(cubesInLevel.Count == 0)
            {
                if (!isBossLevel)
                {
                    GameManager.instance.gameOver = true;
                    GameManager.instance.StopAllCoroutines();
                    GameManager.instance.rayCastCollider.SetActive(false);
                    GameManager.instance.completionText.text = GetLevelCompletionText(GameManager.instance.waterLimit / GameManager.instance.maxLimit);
                    GameManager.instance.levelCompletedScreen.SetActive(true);
                    GameManager.instance.completionText.transform.localScale = Vector3.zero;
                    GameManager.instance.completionText.transform.DOScale(1, 2.5f).SetEase(Ease.OutBounce).OnComplete(() =>
                     {
                         NextLevel();

                         GameManager.instance.levelCompletedScreen.SetActive(false);
                     });
                }
                else
                {
                    if (animateInAnimation)
                    {
                        AnimateBossLevel();
                    } else
                    {
                        iceCage.gameObject.SetActive(true);
                    }
                       // 

                }
            }
        }
    }

    string GetLevelCompletionText(float waterLevel)
    {
        string newString = "";
        if (waterLevel >= 0.5f)
        {
            newString = GameManager.instance.levelCOmpletionTextcontents[Random.Range(0, 4)];
        }
        else if(waterLevel >= 0.4f && waterLevel < 0.5f)
        {
            newString = GameManager.instance.levelCOmpletionTextcontents[Random.Range(5, 9)];
        }  
        else if (waterLevel >= 0.3f && waterLevel < 0.2f)
        {
            newString = GameManager.instance.levelCOmpletionTextcontents[Random.Range(10, 14)];
        }
        else
        {
            newString = GameManager.instance.levelCOmpletionTextcontents[Random.Range(15, 19)];
        }
        return newString.ToUpper();
    }

    public void NextLevel()
    {
        StopCoroutine(GameManager.instance.RestartMenu());
        GameManager.instance.levelnumber += 1;
        GameManager.instance.CreateLevel();
    }

    public void StartLevel()
    {
        AnimateNormalLevel();
    }

    void AnimateNormalLevel()
    {
        FireWater.instance.gunPrefab.SetActive(false);
        Camera.main.transform.position = new Vector3(0, 2.5f, -4.5f);
        //if (!animateInAnimation)
        //{
            ground.transform.DOMoveY(groundPoint + 2f, 0.5f, false).OnComplete(() =>
            {
                ground.transform.DOMoveY(groundPoint - 1f, 0.3f, false).OnComplete(() =>
                {
                    ground.transform.DOMoveY(groundPoint, 0.1f, false);
                    levelPart.transform.DOMoveY(levelPoint, 0.5f, false).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        Camera.main.transform.DOMove(new Vector3(0, 1.5f, -15), 0.2f);
                        FireWater.instance.gunPrefab.SetActive(true);
                        FireWater.instance.gunPrefab.transform.localPosition = new Vector3(0, -0.66f, 1.8f);
                        Invoke("EnableRigidBody", 0.2f);
                    });
                });
            });
        //} else {
        //    ground.transform.DOMoveY(groundPoint + 2f, 0.5f, false).OnComplete(() =>
        //   {
        //       ground.transform.DOMoveY(groundPoint - 1f, 0.3f, false).OnComplete(() =>
        //       {
        //           ground.transform.DOMoveY(groundPoint, 0.1f, false);
        //           //Camera.main.transform.DOMove(new Vector3(0, 1.5f, -15), 0.2f);
        //           // Invoke("EnableRigidBody", 0.2f);
        //           //AnimateBossLevel();
        //            Invoke("EnableRigidBody", 0.2f);
        //       });
        //   });
        //}
    }

    public void AnimateBossLevel()
    {
        savinaAnimalAnimator.Play("Moving");
        Camera.main.transform.position = initialCamPosition / 3;
        savingaAnimal.transform.DOMoveX(iceCage.transform.position.x + Random.Range(2, -2), 2f);
        savingaAnimal.transform.DOMoveY(savingaAnimal.transform.position.y +2f, 0.5f).OnComplete(() =>
        {
            savingaAnimal.transform.DOMoveY(iceCage.transform.position.y+1.5f, 1.5f).OnComplete(() =>
            {
                Camera.main.transform.DOMove( new Vector3(0, 0, 4), 0.35f);
                savinaAnimalAnimator.Play("Walk");
                savingaAnimal.transform.DOMoveX(iceCage.transform.position.x, 1f).OnComplete(() =>
                {
                    savingaAnimal.transform.DORotate(new Vector3(0, 180, 0), 0.5f).OnComplete(() =>
                    {
                        tempIceCageA.SetActive(true);
                        tempIceCageB.SetActive(true);
                            savinaAnimalAnimator.Play("Idle");
                            tempIceCageA.transform.DOMoveX(iceCage.transform.position.x, 0.75f, false);
                            tempIceCageB.transform.DOMoveX(iceCage.transform.position.x, 0.75f, false).OnComplete(() =>{
                            tempIceCageB.SetActive(false);
                            tempIceCageA.SetActive(false);
                            savingaAnimal.SetActive(false);
                            iceCage.gameObject.SetActive(true);
                            Camera.main.transform.DOMove(initialCamPosition, 1f).OnComplete(() =>
                            {
                                //iceCage.gameObject.SetActive(false);
                                levelPart.transform.DOMoveY(levelPoint, 0.5f, false).SetEase(Ease.Linear).OnComplete(() =>
                                {
                                    iceCage.transform.SetParent(levelPart.transform);
                                    FireWater.instance.gunPrefab.SetActive(true);
                                    Camera.main.transform.DOMove(new Vector3(0, 1.5f, -15), 0.2f);
                                    FireWater.instance.gunPrefab.transform.localPosition = new Vector3(0, -0.66f, 1.8f);
                                   

                                });
                            });
                        });
                    });
                });
            });
        });
    }


    void EnableRigidBody()
    {
        GameManager.instance.levelLoaded = true;
        foreach (Rigidbody item in defaultRigidBody)
        {
            item.isKinematic = false;
        }
    }
}


[System.Serializable]
public class LevelPrefabSets
{
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    public GameObject level4;
    public GameObject level5;
}