using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public float maxHitPoints;
    public float startingHitPoints;

    public virtual void KillCharacter()
    {
        Destroy(gameObject);
    }

    public abstract IEnumerator DamageCharacter(int damage, float interval);
    public abstract void ResetCharacter();

    public virtual IEnumerator FlickerCharacter()
    {
        GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(0.1f);

        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
