using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.UI;

public class MenuAnimator : MonoBehaviour
{
    public GameObject logo;
    public Image logoImage;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StartSequence();
        }
        if (Input.GetMouseButtonDown(0))
        {
           logoImage.DOFade(0, 0.1f);
           logo.transform.DOScale(new Vector3(0.25f, 5, 1), 0.1f);
            
        }
    }

    void StartSequence()
    {
        var logoStart = DOTween.Sequence();
        logoStart.Append(logo.transform.DOMove(new Vector3(1000, 8000, 0), 0.5f).From());
        logoStart.Insert(0.25f, logo.transform.DOScale(new Vector3(2f, 0.2f, 1), 0.25f));
        logoStart.Append(logo.transform.DOScale(new Vector3(1, 1, 1), 0.25f));
        logoStart.Insert(0, logoImage.DOFade(1, 0.3f));

        logoStart.Append(logo.transform.DOScale(1.1f, 2).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo));
        //logoStart.Insert(0, logo.transform.DOPunchScale(new Vector3(-0.8f, 0.8f, 1), 0.25f, 1, 1));
        //logoStart.Insert(0.25f, logo.transform.DOPunchScale(new Vector3(0, -0.5f, 1), 0.25f, 1, 1));
    }
}
