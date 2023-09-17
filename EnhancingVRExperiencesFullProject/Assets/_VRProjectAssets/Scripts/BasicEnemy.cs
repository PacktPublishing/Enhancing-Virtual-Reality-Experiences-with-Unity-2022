using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    public GameObject projectile;

[Header("AI")]
public NavMeshAgent agent;
public Transform player;
public string playerTag;
public LayerMask whatIsGround, whatIsPlayer;

[Header("Patroling")]
public Vector3 walkPoint;
public bool walkPointSet;
public float walkPointRange;

[Header("Attacking")]
public float timeBetweenAttacks;
bool alreadyAttacked;

[Header("States")]
public float sightRange, attackRange;
public bool playerInSightRange, playerInAttackRange;

[Header("Stats")]
public int health;

private void Awake() 
{
    player = GameObject.Find(playerTag).transform;
    agent = GetComponent<NavMeshAgent>();
}
public void TakeDamage(int damage){
    health -=damage;
    if (health<=0){
        Destroy(gameObject);
    }
}
private void Update() 
{
    playerInSightRange = Physics.CheckSphere(transform.position,sightRange,whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position,attackRange,whatIsPlayer);
if(!playerInSightRange&&!playerInAttackRange)
{
Patroling();
}
if(playerInSightRange&&!playerInAttackRange)
{
ChasePlayer();
}
if(playerInSightRange&&playerInAttackRange)
{
AttackPlayer();
}
}
public void Patroling()
{
if(!walkPointSet){
    SearchWalkPoint();
}
if(walkPointSet){
   agent.SetDestination(walkPoint); 
}

Vector3 distanceToWalkPoint = transform.position - walkPoint;

if(distanceToWalkPoint.magnitude<1f){
    walkPointSet = false;
}


}
public void SearchWalkPoint()
{
float randomZ = Random.Range(-walkPointRange,walkPointRange);
float randomX = Random.Range(-walkPointRange,walkPointRange);
walkPoint = new Vector3(transform.position.x+randomX,transform.position.y,transform.position.z+randomZ);

if (Physics.Raycast(walkPoint,-transform.up,2f,whatIsGround)){
    walkPointSet = true;
}
}
public void ChasePlayer()
{
agent.SetDestination(player.position);
}
public void AttackPlayer()
{
agent.SetDestination(transform.position);
if(!alreadyAttacked){
    
    Rigidbody rb = Instantiate(projectile,transform.position, Quaternion.identity).GetComponent<Rigidbody>();
    
    rb.AddForce(transform.forward*32f,ForceMode.Impulse);
        rb.AddForce(transform.up*8f,ForceMode.Impulse);

    alreadyAttacked=true;
    Invoke(nameof(ResetAttack),timeBetweenAttacks);
}
}

public void ResetAttack(){
    alreadyAttacked= false;
}
 private void OnDrawGizmosSelected() {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position,attackRange);
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(transform.position,sightRange);
    
}
}