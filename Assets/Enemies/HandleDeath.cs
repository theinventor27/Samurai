using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleDeath : MonoBehaviour
{
    public float Health = 100;
    public int XP;
    public GameObject enemySprite;
    public GameObject xpParticleSystemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if (Health <= 0)
        {
            InstantiateParticleSystemPrefab(0);
            Destroy(enemySprite);
        }
    }

    public void InstantiateParticleSystemPrefab(int xp)
    {
        Instantiate(xpParticleSystemPrefab, new Vector3(enemySprite.transform.position.x, enemySprite.transform.position.y, 0), Quaternion.identity);
    }

}
    