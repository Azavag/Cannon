using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    [SerializeField]
    private Transform characterController;
    [SerializeField]
    private Enemy[] enemies;

    [SerializeField]
    private float startDelay = 2f;

    public bool isStartFabric;


    private void Start()
    {
        if(isStartFabric)
            StartCoroutine(StartDelay());
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(startDelay);
        OrderFollowPlayer();
    }

    private void OrderFollowPlayer()
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.StartFollowPlayer(characterController);
        }
    }
}
