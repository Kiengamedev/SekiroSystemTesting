using UnityEngine;

[CreateAssetMenu(fileName = "PostureModifierSO", menuName = "Posture System/PostureModifierSO")]
public class PostureModifierSO : ScriptableObject
{
    [Header("Posture Multipliers")]
    public float recoveryMultiplier = 1.0f;
    public float damageResistance = 0.0f;

    //[Header("Special Effects")]
    
}
