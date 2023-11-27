using UnityEngine;

public class ParticleInfo : MonoBehaviour
{
    public GameObject player;
    private ParticleSystem ps;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");


        ps = GetComponent<ParticleSystem>();
        
        var trigger = ps.trigger;
        trigger.enabled = true;
        trigger.SetCollider(0, player.transform);
        
        

    }
}