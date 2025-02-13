using UnityEngine;

[CreateAssetMenu(fileName = "PostureData", menuName = "Scriptable Objects/PostureData")]
public class PostureData : ScriptableObject
{
    [Header("Posture Core Settings")]
    public float maxPosture = 100f;       // Max Posture before breaking
    public float postureRecoveryRate = 5f; // Passive Posture regeneration per second
    public float postureDecayDelay = 2f;  // Time before Posture starts recovering

    [Header("Posture Break Settings")]
    public float postureBreakResetTime = 2f; // Time after which broken posture resets
    public float autoRecoveryAfterBreakTime = 4f; // Time before posture automatically recovers

    [Header("Guard & Deflect Settings")]
    public float guardPostureDamageMultiplier = 1f; // Posture damage multiplier when blocking
    public float deflectPostureDamageMultiplier = 0.3f; // Reduced Posture damage when deflecting
    public float deflectEnemyPostureDamage = 20f; // Posture damage to enemy on successful deflect
    public float deflectPlayerPostureDamage = 5f; // Posture damage taken when failed deflect

    [Header("Posture Recovery Settings")]
    public bool enablePassiveRecovery = true; // Enable or disable passive Posture recovery
    public float passiveRecoveryRate = 5f; // Rate at which Posture recovers when idle

}
