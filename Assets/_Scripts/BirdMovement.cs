using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BirdMovement : MonoBehaviour {

    [SerializeField] GameObject bird;
    Vector3 initialCameraPosition;
    public GameObject camParentSwitch;
    public bool isbird = true;
	
	void Start () 
    {
        if (isbird)
            StartAnimating();
        else
            AnimateAnimal();
        initialCameraPosition = Camera.main.transform.position;
	}


    void AnimateAnimal()
    {
        camParentSwitch = new GameObject();
        camParentSwitch.transform.position = initialCameraPosition;
        for (int i = 0; i < Camera.main.transform.childCount; i++)
        {
            Camera.main.transform.GetChild(i).transform.SetParent(camParentSwitch.transform);
        }
        Camera.main.transform.DOMove(bird.transform.position - new Vector3(0, 0, 15), 1f, false);
        bird.gameObject.GetComponent<Collider>().enabled = true;
        bird.transform.DORotate(new Vector3(0, -90, 0), 0.75f, RotateMode.Fast).SetEase(Ease.Linear).OnComplete(() =>
        {
            
            bird.transform.DORotate(new Vector3(0, -90, 0), 1.2f, RotateMode.Fast).SetEase(Ease.Linear).OnComplete(() =>
            {
                    Camera.main.transform.DOMove(initialCameraPosition / 5, 1.5f, false).OnComplete(() =>
                      {
                          bird.transform.GetChild(0).GetComponent<Animator>().enabled = true;
                          for (int i = 0; i < camParentSwitch.transform.childCount; i++)
                          {
                              camParentSwitch.transform.GetChild(i).transform.SetParent(Camera.main.transform);
                              Destroy(camParentSwitch);
                          }
                          if (bird.transform.position.x < 0)
                          {
                              bird.transform.DORotate(new Vector3(0, -135, 0), 0.25f).SetEase(Ease.Linear).OnComplete(() =>
                             {
                                 bird.transform.DOMoveX(bird.transform.position.x + 35f, 2f, false).SetEase(Ease.Linear).OnComplete(() =>
                             {
                                  GameManager.instance.UpdateShowCase(LevelSetUp.instance.bossIndex);
                                  PlayerPrefs.SetInt("CrossedLevel1", 1);
                              });
                             });
                          }
                          else
                          {
                              bird.transform.DORotate(new Vector3(0, -45, 0), 0.25f).SetEase(Ease.Linear).OnComplete(() =>
                              {
                                  bird.transform.DOMoveX(bird.transform.position.x - 35f, 2f, false).SetEase(Ease.Linear).OnComplete(() =>
                                  {
                                      GameManager.instance.UpdateShowCase(LevelSetUp.instance.bossIndex);
                                      PlayerPrefs.SetInt("CrossedLevel1", 1);
                                  });
                              });
                          }
                });
            });
        });
    }



    void StartAnimating()
    {
        camParentSwitch = new GameObject();
        camParentSwitch.transform.position = initialCameraPosition;
        for (int i = 0; i < Camera.main.transform.childCount; i++)
        {
            Camera.main.transform.GetChild(i).transform.SetParent(camParentSwitch.transform);
        }
        Camera.main.transform.DOMove(bird.transform.position - new Vector3(0, -2, +10), 1f, false);
        bird.gameObject.GetComponent<Collider>().enabled = true;
        // bird.transform.DOLocalJump(transform.position, 1, 1, 0.5f, false);
        bird.transform.DORotate(new Vector3(0, 180, 0), 0.75f, RotateMode.Fast).SetEase(Ease.Linear).OnComplete(() =>
        {
            bird.transform.DORotate(new Vector3(0, 180, 0), 0.5f, RotateMode.Fast).SetEase(Ease.Linear).OnComplete(() =>
             {
                 bird.GetComponent<Animator>().enabled = true;
                //bird.AddComponent<Rigidbody>();
                Camera.main.transform.DOMove(initialCameraPosition, 1f, false).OnComplete(() =>
                    {
                     for (int i = 0; i < camParentSwitch.transform.childCount; i++)
                     {
                         camParentSwitch.transform.GetChild(i).transform.SetParent(Camera.main.transform);
                         Destroy(camParentSwitch);
                     }
                 });
                 bird.transform.DOMoveY(bird.transform.position.y + 5f, 1f, false);
                 bird.transform.DOMoveZ(bird.transform.position.z - 5f, 1f, false).SetEase(Ease.Linear).OnComplete(() =>
                {
             if (bird.transform.position.x < 0)
             {
                 bird.transform.DORotate(new Vector3(0, 90, 0), 0.25f).SetEase(Ease.Linear).OnComplete(() =>
                 {
                     bird.transform.DOMoveX(bird.transform.position.x + 35f, 2f, false).SetEase(Ease.Linear).OnComplete(() =>
                     {
                         GameManager.instance.UpdateShowCase(LevelSetUp.instance.bossIndex);
                     });
                 });
             }
             else
             {
                 bird.transform.DORotate(new Vector3(0, -90, 0), 0.25f).SetEase(Ease.Linear).OnComplete(() =>
                 {
                     bird.transform.DOMoveX(bird.transform.position.x - 35f, 2f, false).SetEase(Ease.Linear).OnComplete(() =>
                     {
                         GameManager.instance.UpdateShowCase(LevelSetUp.instance.bossIndex);
                     });
                 });
             }
         });
             });
        });
    }
}
