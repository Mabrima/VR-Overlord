using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    int health = 3;
    [SerializeField]
    int hits = 0;
    [SerializeField]
    float speed = 0.0001f;
    [SerializeField]
    TextMesh counter;

    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed);
    }

    public void TakeDamage(int damage)
    {
        if (counter != null)
        {
            hits++;
            counter.text = "" + hits;
        }
        health -= damage;
        if (health <= 0)
        {
            StartCoroutine(Dying());
        }
        else
        {
            animator.SetTrigger("Hit");
        }
    }

    IEnumerator Dying()
    {
        animator.SetBool("Death", true);
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
        yield return null;
    }
}
