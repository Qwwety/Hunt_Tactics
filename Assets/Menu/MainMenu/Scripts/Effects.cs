using UnityEngine;
using DG.Tweening;

public class Effects : MonoBehaviour
{
    private CanvasGroup CanvasGroup;

    void Start()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
        FadeInEffect();
    }


    private void FadeInEffect()
    {
        CanvasGroup.DOFade(1, 3);
    }
    
}
