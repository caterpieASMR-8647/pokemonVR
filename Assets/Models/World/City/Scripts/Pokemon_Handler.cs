using System;
using Oculus.Interaction.Samples;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Pokemon_Handler : MonoBehaviour
{
    private int frameCounter = 0;
    
    [SerializeField]
    private GameObject[] pokemons;
    
    private NavMeshAgent[] agents;
    
    private void Awake()
    {
        foreach (var pokemon in pokemons)
        {
            Instantiate( pokemon, RandomNavMeshLocation(10f), Quaternion.identity );
        }
    }

    private void Start()
    {
        agents = FindObjectsByType<NavMeshAgent>();
        foreach (var agent in agents)
        {
            agent.autoRepath = true;
            agent.SetDestination( RandomNavMeshLocation( 4f ) );
        }
    }

    // 40 ticks per second
    private void FixedUpdate()
    {
        frameCounter++;

        switch ( frameCounter )
        {
            case 39:
                CheckAgent(0);
                frameCounter = 0;
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
        
        // foreach ( var pokemon in pokemons )
        // {
        //     var agent = pokemon.GetComponent<NavMeshAgent>();
        //     if ( agent.destination == agent.transform.position || agent.velocity.magnitude <= 0.1f )
        //     {
        //         agent.SetDestination( RandomNavMeshLocation( 4f ) );
        //         Debug.Log( agent.destination );
        //     }
        // }
    }

    public Vector3 RandomNavMeshLocation( float radius )
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
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
        
        if ( agents[index].destination == agents[index].transform.position || agents[index].velocity.magnitude <= 0.1f )
        {
            agents[index].SetDestination( RandomNavMeshLocation( 4f ) );
            Debug.Log( agents[index].destination );
        }
    }
}
