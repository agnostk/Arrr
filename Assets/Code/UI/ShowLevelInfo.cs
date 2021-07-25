using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShowLevelInfo : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        GameManager.Instance.onStartGame += () => canvasGroup.DOFade(1f, 0.5f);
    }
}
