using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PostureDamageSystem : MonoBehaviour
{
    [SerializeField] private PostureDataSO postureData;

    public void TakeBlockDamage(PostureGauge defender)
    {
        defender.AddPosture(postureData.blockPostureDamage, true);

        if (defender.IsAtMaxPosture())
        {
            defender.LostPosture(true);
        }
        
    }

    public void TakeDeflectDamage(PostureGauge defender, PostureGauge attacker)
    {
        defender.AddPosture(postureData.deflectPostureDamage, false);

        if (attacker.TryGetComponent(out PostureGauge attackerPosture))
        {
            attackerPosture.AddPosture(postureData.deflectDamageToAttacker, true);
        }

        if (attackerPosture.IsAtMaxPosture()) // ✅ Ensure enemy breaks posture when full
        {
            Debug.Log($"{attacker.gameObject.name} should break posture now!");
            attackerPosture.LostPosture(false);
        }
    }
}
