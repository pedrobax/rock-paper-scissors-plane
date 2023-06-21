using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TypeHUD : MonoBehaviour
{
    public RectTransform typeUITransform, rockImage, paperImage, scissorsImage, rockToPaperImage, paperToScissorsImage, scissorsToRockImage;
    float currentRotation = 0;

    void Start()
    {
        ChangeTypeUI(3);
    }
    public void ChangeTypeUI(int type)
    {
        switch(type)
        {
            case 0:
                currentRotation += 120;
                typeUITransform.DORotate(new Vector3(typeUITransform.transform.rotation.x, typeUITransform.transform.rotation.y, currentRotation), 0.15f);
                rockImage.DORotate(new Vector3(0, 0, 360), 0.15f);
                paperImage.DORotate(new Vector3(0, 0, 360), 0.15f);
                scissorsImage.DORotate(new Vector3(0, 0, 360), 0.15f);
                rockImage.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.15f);
                paperImage.DOScale(new Vector3(1, 1, 1), 0.15f);
                break;
            case 1:
                currentRotation -= 120;
                typeUITransform.DORotate(new Vector3(typeUITransform.transform.rotation.x, typeUITransform.transform.rotation.y, currentRotation), 0.15f);
                rockImage.DORotate(new Vector3(0, 0, 360), 0.15f);
                paperImage.DORotate(new Vector3(0, 0, 360), 0.15f);
                scissorsImage.DORotate(new Vector3(0, 0, 360), 0.15f);
                rockImage.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.15f);
                scissorsImage.DOScale(new Vector3(1, 1, 1), 0.15f);
                break;
            case 2:

                currentRotation -= 120;
                typeUITransform.DORotate(new Vector3(typeUITransform.transform.rotation.x, typeUITransform.transform.rotation.y, currentRotation), 0.15f);
                rockImage.DORotate(new Vector3(0, 0, 360), 0.15f);
                paperImage.DORotate(new Vector3(0, 0, 360), 0.15f);
                scissorsImage.DORotate(new Vector3(0, 0, 360), 0.15f);
                rockImage.DOScale(new Vector3(1, 1, 1), 0.15f);
                paperImage.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.15f);
                break;
            case 3:
                currentRotation += 120;
                typeUITransform.DORotate(new Vector3(typeUITransform.transform.rotation.x, typeUITransform.transform.rotation.y, currentRotation), 0.15f);
                rockImage.DORotate(new Vector3(0, 0, 360), 0.15f);
                paperImage.DORotate(new Vector3(0, 0, 360), 0.15f);
                scissorsImage.DORotate(new Vector3(0, 0, 360), 0.15f);
                paperImage.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.15f);
                scissorsImage.DOScale(new Vector3(1, 1, 1), 0.15f);
                break;
            case 4:
                currentRotation += 120;
                typeUITransform.DORotate(new Vector3(typeUITransform.transform.rotation.x, typeUITransform.transform.rotation.y, currentRotation), 0.15f);
                rockImage.DORotate(new Vector3(0, 0, 360), 0.15f);
                paperImage.DORotate(new Vector3(0, 0, 360), 0.15f);
                scissorsImage.DORotate(new Vector3(0, 0, 360), 0.15f);
                scissorsImage.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.15f);
                rockImage.DOScale(new Vector3(1, 1, 1), 0.15f);
                break;
            case 5:
                currentRotation -= 120;
                typeUITransform.DORotate(new Vector3(typeUITransform.transform.rotation.x, typeUITransform.transform.rotation.y, currentRotation), 0.15f);
                rockImage.DORotate(new Vector3(0, 0, 360), 0.15f);
                paperImage.DORotate(new Vector3(0, 0, 360), 0.15f);
                scissorsImage.DORotate(new Vector3(0, 0, 360), 0.15f);
                scissorsImage.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.15f);
                paperImage.DOScale(new Vector3(1, 1, 1), 0.15f);
                break;
        }
        
    }
}
