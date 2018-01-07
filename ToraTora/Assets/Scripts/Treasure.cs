using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    [SerializeField]
    private Transform destination;
    [SerializeField]
    private float moveSpeed = 20;
    [SerializeField]
    private bool isBig;

    private bool reachTarget;

    private IEnumerator GoToUI()
    {
        Vector2 velocity = 
            (destination.position - transform.position).normalized * moveSpeed;

        transform.SetParent(destination.parent, true);

        while (!reachTarget)
        {
            transform.Translate(velocity * Time.deltaTime);
            yield return null;
        }

        PlaySceneManager.Instance.AddTreasure(isBig);
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(GoToUI());
        }
        else if(collision.CompareTag("TreasurePos"))
        {
            reachTarget = true;
        }
    }
}
