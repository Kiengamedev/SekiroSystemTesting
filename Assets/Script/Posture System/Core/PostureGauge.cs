using UnityEngine;
using System;
using System.Collections;

public class PostureGauge : MonoBehaviour, IPosture
{
    [SerializeField] private PostureDataSO postureData;
    [SerializeField] private PostureModifierSO postureModifier;

    private float currentPosture;
    private bool isPostureBroken = false;

    public event Action OnPostureBreak;
    public event Action OnPostureRecover;
    public event Action OnPostureChanged; 

    public float CurrentPosture => currentPosture;
    public float MaxPosture => postureData.maxPosture;

    private void Start()
    {
        currentPosture = 0f;
    }

    public void AddPosture(float amount, bool canBreakPosture)
    {
        if (isPostureBroken) return;

        float adjustedAmount = amount * (1f - postureModifier.damageResistance);
        currentPosture += adjustedAmount;
        OnPostureChanged?.Invoke();

        if (currentPosture >= postureData.maxPosture)
        {
            currentPosture = postureData.maxPosture;

            if (canBreakPosture)
            {
                isPostureBroken = true;
                Debug.Log($"{gameObject.name} Posture Broken!");
                OnPostureBreak?.Invoke();
            }
            
        }
    }


    public void ReducePosture(float amount)
    {
        if (!isPostureBroken)
        {
            float adjustedAmount = amount * postureModifier.recoveryMultiplier;
            currentPosture = Mathf.Max(0, currentPosture - adjustedAmount);

            //Debug.Log($"{gameObject.name} Posture Reduced: {currentPosture}/{MaxPosture}");

            if (currentPosture == 0)
            {
                Debug.Log($"{gameObject.name} Posture Fully Recovered!");
                OnPostureRecover?.Invoke();
            }

        }
    }

    public bool IsPostureBroken() => isPostureBroken;

    public bool IsAtMaxPosture()
    {
        return currentPosture >= postureData.maxPosture;
    }

    public void ResetPosture()
    {
        currentPosture = 0;
        isPostureBroken = false; 

        //Debug.Log($"{gameObject.name} Posture Reset to 0 After Deathblow!");

        OnPostureRecover?.Invoke(); 
    }


    public void LostPosture(bool forceBreak)
    {
        if (forceBreak || !isPostureBroken) 
        {
            Debug.Log($"{gameObject.name} has lost posture! Resetting immediately.");
            isPostureBroken = true;
            ResetPosture();
        }
    }


}
