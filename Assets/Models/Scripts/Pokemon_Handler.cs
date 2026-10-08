using System;
using System.Collections;
using NUnit.Framework.Constraints;
using Oculus.Interaction.Samples;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Pokemon_Handler : MonoBehaviour
{
    private int tickCounter = 0;

    [SerializeField] private float randomRadius = 20f;
    [SerializeField] private bool onlyCaptured = false;
    
    [SerializeField]
    private GameObject[] pokemons;
    
    private NavMeshAgent[] agents;
    private Animator[] animators;
    private const string IsMoving = "IsMoving";
    private const string Scratch = "Scratch";
    private bool[] waitForCoroutine;
    
    private void Awake()
    {
        if (!onlyCaptured)
        {
            foreach (var pokemon in pokemons)
            {
                Instantiate( pokemon, RandomNavMeshLocation(15f), Quaternion.identity );
            }
        }
        else
        {
            
        }
    }

    private void Start()
    {
        agents = FindObjectsByType<NavMeshAgent>();
        animators = FindObjectsByType<Animator>();
        foreach (var agent in agents)
        {
            agent.autoRepath = true;
            agent.SetDestination( RandomNavMeshLocation( randomRadius ) );
        }
        
        waitForCoroutine = new bool[agents.Length];
        for (int i = 0; i < waitForCoroutine.Length; i++)
        {
            waitForCoroutine[i] = true;
        }
    }

    // 40 ticks per second
    private void FixedUpdate()
    {
        tickCounter++;

        switch ( tickCounter )
        {
            case 39:
                tickCounter = 0;
                CheckAgent(0);
                break;
            case 9:
                CheckAgent(1);
                break;
            case 19:
                CheckAgent(2);
                break;
            case 29:
                CheckAgent(3);
                break;
            default:
                break;
        }
    }

    public Vector3 RandomNavMeshLocation( float radius )
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection = transform.position - randomDirection;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if ( NavMesh.SamplePosition( randomDirection, out hit, radius, 1 ) ) {
            finalPosition = hit.position;            
        }
        return finalPosition;
    }

    public void CheckAgent(int index)
    {
        if (agents.Length < index + 1) return;
        
        if ( ( agents[index].destination == agents[index].transform.position || agents[index].velocity.magnitude <= 0.1f )
            && waitForCoroutine[index] )
        {
            waitForCoroutine[index] = false;
            animators[index].SetBool( IsMoving, waitForCoroutine[index] );
            StartCoroutine(WaitRandomSeconds(index));
            agents[index].SetDestination( RandomNavMeshLocation( randomRadius ) );
        }
    }

    private IEnumerator WaitRandomSeconds( int index )
    {
        yield return new WaitForSeconds( Random.Range( 1f, 5f ) );
        waitForCoroutine[index] = true;
        animators[index].SetBool( IsMoving, waitForCoroutine[index] );
    }
}
