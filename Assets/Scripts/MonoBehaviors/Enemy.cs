using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    float hitPoints;
    public int damageStength;
    Coroutine coroutine = null;

    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        while (true)
        {
            hitPoints -= damage;
            if (hitPoints <= float.Epsilon)
            {
                KillCharacter();
                break;
            }

            if (interval > float.Epsilon)
            {
                yield return new WaitForSeconds(interval);
            } 
            else
            {
                break;
            }
        }
    }

    public override void ResetCharacter()
    {
        hitPoints = startingHitPoints;
    }

    private void OnEnable()
    {
        ResetCharacter();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (coroutine == null)
            {
                //StartCoroutine 실행된 적이 없다.
                coroutine = StartCoroutine(player.DamageCharacter(damageStength, 1.0f));
            }
        }
    }

    private void OnCollisionExit2D(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }
}
