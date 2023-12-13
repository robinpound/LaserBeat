using System.Collections;
using UnityEngine;
using Liminal.Core.Fader;

public class ScreenFadeToBlack : MonoBehaviour
{
    [Header("Fade to black settings")]
    [Range(0.1f, 5f)] [SerializeField] private float fadeDuration = 3f;
    [Range(0.1f, 1f)] [SerializeField] private float fadeTimer = .1f;

    void OnEnable() => LaserGunRayCast.OnFinalTargetHit += FadeToBlack;
    void OnDisable() => LaserGunRayCast.OnFinalTargetHit -= FadeToBlack;
    private void FadeToBlack()
    {
        StartCoroutine(FadeToBlackTimer(fadeTimer));
    }

    private IEnumerator FadeToBlackTimer(float timer)
    {
        var fader = ScreenFader.Instance;
        while (true)
        {
            yield return new WaitForSeconds(timer);
            fader.FadeTo(Color.black, fadeDuration);
        }
    }
}
