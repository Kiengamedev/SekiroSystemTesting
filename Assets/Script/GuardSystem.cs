using UnityEngine;

public class GuardSystem : MonoBehaviour
{
    private PostureSystem postureSystem;
    private bool isGuarding = false;

    private void Start()
    {
        postureSystem = GetComponent<PostureSystem>();
    }

    public void StartGuarding()
    {
        isGuarding = true;
    }

    public void StopGuarding()
    {
        isGuarding = false;
    }

    public void ReceiveAttack(float basePostureDamage)
    {
        if (isGuarding)
        {
            float damageTaken = basePostureDamage * postureSystem.postureData.guardPostureDamageMultiplier;
            Debug.Log($"Blocked Attack! Taking {damageTaken} Posture Damage.");
            postureSystem.TakePostureDamage(damageTaken, true);
        }
        else
        {
            Debug.Log($"Unblocked Attack! Taking FULL Posture Damage.");
            postureSystem.TakePostureDamage(basePostureDamage, true);
        }
    }

    public bool IsGuarding()
    {
        return isGuarding;
    }
}
