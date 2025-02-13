using UnityEngine;

public class DeflectSystem : MonoBehaviour
{
    private PostureSystem postureSystem;

    private void Start() { postureSystem = GetComponent<PostureSystem>(); }

    public void AttemptDeflect(bool isPerfectTiming, PostureSystem enemyPosture)
    {
        if (isPerfectTiming)
        {
            Debug.Log("Perfect Deflect! Enemy takes Posture Damage.");
            enemyPosture.TakePostureDamage(postureSystem.postureData.deflectEnemyPostureDamage * postureSystem.postureData.deflectPostureDamageMultiplier, true); // Enemy CAN break
        }

        // Player still takes posture damage but it NEVER breaks
        float damageTaken = postureSystem.postureData.deflectPlayerPostureDamage * postureSystem.postureData.deflectPostureDamageMultiplier;
        postureSystem.TakePostureDamage(damageTaken, false); // Player CANNOT break
    }
}
