using UnityEngine;

[CreateAssetMenu(fileName = "PostureDataSO", menuName = "Posture System/PostureDataSO")]
public class PostureDataSO : ScriptableObject
{
    [Header("Posture Attributes")]
    public float maxPosture = 100f;
    public float recoveryRate = 5f;
    public float recoveryDelay = 1f;

    [Header("Posture Damage")]
    public float blockPostureDamage = 10f;
    public float deflectPostureDamage = 5f;
    public float deflectDamageToAttacker = 15f;

    [Header("Posture Mechanics")]
    public bool canPostureBreak = true;
    public bool hasDeathblow = true;
}
