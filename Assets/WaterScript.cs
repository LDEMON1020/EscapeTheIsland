using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    public PlayerControllersecound player;
    public bool inWater;
    private void Awake()
    {
        player = FindObjectOfType<PlayerControllersecound>();
    }
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            inWater = !inWater;
            player.gravity = -2.2f;
            player.jumpPower = 0.2f;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inWater = !inWater;
            player.gravity = -9.81f;
            player.jumpPower = 1f;
        }
    }
}
