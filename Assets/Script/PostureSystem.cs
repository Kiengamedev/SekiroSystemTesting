using UnityEngine;
using System.Collections;
using System;
using CodeMonkey;

public class PostureSystem : MonoBehaviour, IPostureSystem
{
    [Header("Posture Configuration")]
    public PostureData postureData; // Reference to ScriptableObject

    private float currentPosture;
    private bool isPostureBroken;
    private Coroutine postureRecoveryCoroutine;

    public float MaxPosture => postureData.maxPosture;
    public float CurrentPosture
    {
        get => currentPosture;
        set => currentPosture = Mathf.Clamp(value, 0, MaxPosture);
    }
    public bool IsPostureBroken => isPostureBroken;

    public void TakePostureDamage(float amount, bool canBreak)
    {
        if (isPostureBroken && canBreak) return;

        CurrentPosture += amount;
        Debug.Log($"{gameObject.name} took {amount} Posture damage. Current: {CurrentPosture}/{MaxPosture}");

        if (CurrentPosture >= MaxPosture && canBreak)
        {
            BreakPosture();
        }
        else
        {
            StartPostureRecoveryTimer();
        }
    }

    public void BreakPosture()
    {
        isPostureBroken = true;
        Debug.Log($"{gameObject.name} Posture Broken!");
        StartCoroutine(AutoRecoverPostureAfterDelay());

    }

    public void ResetPosture()
    {
        isPostureBroken = false;

        CurrentPosture = 0;

        Debug.Log($"{gameObject.name} Posture Reset.");
    }

    public void RecoverPosture(float amount)
    {
        if (!isPostureBroken)
        {
            CurrentPosture -= amount;
        }
            
        //Debug.Log($"{gameObject.name} recovered {amount} Posture.");
    }


    private void StartPostureRecoveryTimer()
    {
        if (postureRecoveryCoroutine != null)
        {
            StopCoroutine(postureRecoveryCoroutine);
        }
        postureRecoveryCoroutine = StartCoroutine(PostureRecoveryRoutine());
    }

    private IEnumerator PostureRecoveryRoutine()
    {
        yield return new WaitForSeconds(postureData.postureDecayDelay);
        while (CurrentPosture > 0)
        {
            RecoverPosture(postureData.postureRecoveryRate * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator AutoRecoverPostureAfterDelay()
    {
        yield return new WaitForSeconds(postureData.autoRecoveryAfterBreakTime); // Delay before auto-recover
        if (isPostureBroken) // Ensure Finisher wasn't executed before this timer
        {
            Debug.Log($"{gameObject.name} Posture recovering automatically...");
            StartPostureRecoveryTimer();
            isPostureBroken = false;
        }
    }
}
