using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PostureUI : MonoBehaviour
{
    [SerializeField] private PostureGauge postureGauge;
    [SerializeField] private Image postureBar;
    //[SerializeField] private GameObject staggerEffect;
    //[SerializeField] private GameObject deathblowPrompt;

    private void Start()
    {
        //postureGauge.OnPostureBreak += ShowStaggerEffect;
        //postureGauge.OnPostureRecover += HideStaggerEffect;
    }

    private void Update()
    {
        postureBar.fillAmount = postureGauge.CurrentPosture / postureGauge.MaxPosture;

        //if (postureGauge.CurrentPosture >= postureGauge.MaxPosture * 0.8f)
        //{
        //    StartCoroutine(FlashPostureBar());
        //}
    }

    //private void ShowStaggerEffect()
    //{
    //    staggerEffect.SetActive(true);
    //}

    //private void HideStaggerEffect()
    //{
    //    staggerEffect.SetActive(false);
    //}

    //private IEnumerator FlashPostureBar()
    //{
    //    while (postureGauge.CurrentPosture >= postureGauge.MaxPosture * 0.8f)
    //    {
    //        postureBar.color = Color.red;
    //        yield return new WaitForSeconds(0.2f);
    //        postureBar.color = Color.white;
    //        yield return new WaitForSeconds(0.2f);
    //    }
    //}

    //public void ShowDeathblowPrompt()
    //{
    //    deathblowPrompt.SetActive(true);
    //}

    //public void HideDeathblowPrompt()
    //{
    //    deathblowPrompt.SetActive(false);
    //}
}
