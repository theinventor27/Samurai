using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Make sure to include this namespace


public class XPCount : MonoBehaviour
{
    public GameObject xpText;
    public int currentXP;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {



        currentXP = player.GetComponent<PlayerAttributes>().xp;
        xpText.GetComponent<UnityEngine.UI.Text>().text = currentXP.ToString();

    }
}
