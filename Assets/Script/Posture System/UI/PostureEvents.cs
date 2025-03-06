using UnityEngine;
using System;

public class PostureEvents : MonoBehaviour
{
    public static PostureEvents Instance { get; private set; }

    public event Action<PostureGauge> OnPostureBreak; 
    public event Action OnDeathblowAvailable;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TriggerPostureBreak(PostureGauge postureGauge)
    {
        Debug.Log($"{postureGauge.gameObject.name} broke posture!");
        OnPostureBreak?.Invoke(postureGauge);
    }

    public void TriggerDeathblow()
    {
        Debug.Log("Deathblow Available!");
        OnDeathblowAvailable?.Invoke();
    }
}
