/*
 * Authors: Colton Campbell (B00693513)
 *          Amanda... (B00...)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class HellHound : DimensionedObject
{
    public AudioClip[] audioClips;
    private AudioSource audioSource;

    Player target;
    NavMeshAgent agent;
    Animator anim;

    Dimension myDimension;

    [SerializeField] float damage = 45f;
    [SerializeField] float attackCooldown = 1.5f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(StartAudio());

        agent = GetComponent<NavMeshAgent>();
        target = GameManager.Instance.Player;

        agent.speed *= Random.Range(1f, 1.25f);

        anim = gameObject.GetComponent<Animator>();

        myDimension = DimensionManager.Instance.CurrentDimension;
    }

    private void Update()
    {
        agent.SetDestination(target.transform.position);
        Animate();
    }

    private IEnumerator StartAudio()
    {
        while (true)
        {
            if (Random.Range(0,4) == 0)
            {
                audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
                audioSource.Play();
                
            } else
            {
                yield return new WaitForSeconds(5);
            }
            
        }
    }

    private void Animate()
    {
        anim.SetBool("Move", true);
    }

    float attackTimer;
    private void OnCollisionEnter(Collision collision)
    {
        if (attackTimer > Time.time)
            return;

        var damagable = collision.collider.GetComponentInParent<IDamagable>();
        if (damagable.IsNull() == false)
        {
            anim.SetBool("Attack", true);
            attackTimer = Time.time + attackCooldown;
            damagable.Damage(damage);
        }
    }

    protected override void SetOverworld()
    {
        this.gameObject.SetActive(myDimension == Dimension.OVERWORLD);
    }

    protected override void SetUpsideDown()
    {
        this.gameObject.SetActive(myDimension == Dimension.UPSIDE_DOWN);
    }
}
