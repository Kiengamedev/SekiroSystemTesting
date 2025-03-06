using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PostureController : MonoBehaviour
{
    [Header("Posture Management")]
    private Dictionary<PostureGauge, PostureDamageSystem> postureSystems = new Dictionary<PostureGauge, PostureDamageSystem>();

    private void Start()
    {
        // Auto-register posture gauges & damage systems dynamically
        PostureGauge[] gauges = FindObjectsOfType<PostureGauge>();
        PostureDamageSystem[] systems = FindObjectsOfType<PostureDamageSystem>();

        foreach (var gauge in gauges) RegisterPosture(gauge);
        foreach (var system in systems) RegisterDamageSystem(system);
    }

    public void RegisterPosture(PostureGauge posture)
    {
        if (!postureSystems.ContainsKey(posture))
        {
            postureSystems.Add(posture, null); // Initially, posture does not have a linked damage system
            posture.OnPostureBreak += () => HandlePostureBreak(posture);
        }
    }

    public void RegisterDamageSystem(PostureDamageSystem damageSystem)
    {
        PostureGauge gauge = postureSystems.Keys.FirstOrDefault(p => p.gameObject == damageSystem.gameObject);
        if (gauge != null)
        {
            postureSystems[gauge] = damageSystem; // Automatically links the correct posture gauge to the correct damage system
        }
    }

    public void ApplyBlockDamage(PostureGauge defender)
    {
        if (postureSystems.TryGetValue(defender, out PostureDamageSystem damageSystem) && damageSystem != null)
        {
            damageSystem.TakeBlockDamage(defender);
            Debug.Log($"{defender.gameObject.name} took block posture damage.");
        }
    }

    public void ApplyDeflectDamage(PostureGauge defender, PostureGauge attacker)
    {
        if (postureSystems.TryGetValue(defender, out PostureDamageSystem damageSystem) && damageSystem != null)
        {
            damageSystem.TakeDeflectDamage(defender, attacker);
            Debug.Log($"{defender.gameObject.name} took deflect posture damage.");
        }
    }

    private void HandlePostureBreak(PostureGauge posture)
    {
        Debug.Log($"{posture.gameObject.name}'s posture is broken!");
        PostureEvents.Instance.TriggerPostureBreak(posture);
    }

    public void HandleDeathblow(PostureGauge enemyPosture)
    {
        if (enemyPosture == null) return;
        Debug.Log($"{enemyPosture.gameObject.name} received a Deathblow! Resetting posture...");
        enemyPosture.ResetPosture();
    }
}
