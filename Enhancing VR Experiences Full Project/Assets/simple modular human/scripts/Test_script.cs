using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Test_script : MonoBehaviour
{
    


    NavMeshAgent agent;
   public Animator ani;
   public GameObject aim_point;

    public bool execute_walking;
    public bool execute_sitting;
    public bool execute_stealing;
    public bool execute_picking_up;

    public bool execute_running;

    public float walk_speed;
    public float run_speed;

    Vector3 sitting_position;
    Vector3 sitting_Rotation;

    Vector3 stealing_position;
    Vector3 stealing_Rotation;


  

    public bool walk;
    public bool run;
    public bool sit;
    public bool steal;
    public bool pick_up;

   public bool destermine_new_aim;



    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();

        sitting_position = new Vector3(0,0,0);
        sitting_Rotation = new Vector3(180,-90,-90);

        stealing_position = new Vector3(0,0.04185915f,-0.07200003f);
        stealing_Rotation = new Vector3(0,180,0);

        way_points.Clear();
        Sitting_points.Clear();
        Stealing_points.Clear();
        pick_up_points.Clear();

        GameObject[] waypointsFind = GameObject.FindGameObjectsWithTag("waypoint");
        GameObject[] SittingpointsFind = GameObject.FindGameObjectsWithTag("sittingpoint");
        GameObject[] stealingpointsFind = GameObject.FindGameObjectsWithTag("stealingpoint");
        GameObject[] pick_up_pointsFind = GameObject.FindGameObjectsWithTag("pickuppoint");

        foreach(GameObject g in waypointsFind)
        {
            way_points.Add(g);
        }
        foreach (GameObject g in SittingpointsFind)
        {
            Sitting_points.Add(g);
        }
        foreach (GameObject g in stealingpointsFind)
        {
            Stealing_points.Add(g);
        }
        foreach (GameObject g in pick_up_pointsFind)
        {
            pick_up_points.Add(g);
        }


    }

    bool in_sitting;
    bool in_stealing;
    bool in_pickup;

    Coroutine sitting_start;
    Coroutine stealing_start;
    Coroutine pickup_start;

    public GameObject crowbar;

    IEnumerator sitting_down ()
    {
        yield return new WaitForSeconds(0);

        transform.parent = aim_point.transform;
       

      

        Destroy(agent);

        ani.SetInteger("legs", 3);
        ani.SetInteger("arms", 3);

        transform.localPosition = sitting_position;
        transform.localEulerAngles = sitting_Rotation;



        yield return new WaitForSeconds(5);

        agent =  gameObject.AddComponent<NavMeshAgent>();


        in_sitting = false;
        destermine_new_aim = false;
        transform.parent = null;

        StopCoroutine(sitting_start);
    }


    IEnumerator stealing_execute()
    {
        yield return new WaitForSeconds(0);
        crowbar.SetActive(true);
        transform.parent = aim_point.transform;
        transform.localPosition = stealing_position;
        transform.localEulerAngles = stealing_Rotation;

        ani.SetInteger("legs", 5);
        ani.SetInteger("arms", 22);

        yield return new WaitForSeconds(5);
        crowbar.SetActive(false);
        in_stealing = false;
        destermine_new_aim = false;
        transform.parent = null;

        StopCoroutine(stealing_start);
    }


    IEnumerator pickup_execute()
    {
        yield return new WaitForSeconds(0);



        ani.SetInteger("legs", 32);
        ani.SetInteger("arms", 32);


        yield return new WaitForSeconds(2);

        in_pickup = false;
        destermine_new_aim = false;
        

        StopCoroutine(pickup_start);
    }

    public bool ready;

    void Update()
    {

        if(!ready)
        {
            return;
        }

     
        if(!destermine_new_aim)
        {
            int what_to_choose = UnityEngine.Random.Range(0, 5);

           

            walk = false;
            run = false;
            sit = false;
            steal = false;
            pick_up = false;



            if(what_to_choose == 0)
            {
                walk = true;

                int Which_point = UnityEngine.Random.Range(0, way_points.Count);
                aim_point = way_points[Which_point].gameObject;
                destermine_new_aim = true;
            }
            if (what_to_choose == 1)
            {
                run = true;

                int Which_point = UnityEngine.Random.Range(0, way_points.Count );
                aim_point = way_points[Which_point].gameObject;
                destermine_new_aim = true;
            }
            if (what_to_choose == 2)
            {
                sit = true;

                int Which_point = UnityEngine.Random.Range(0, Sitting_points.Count );
                aim_point = Sitting_points[Which_point].gameObject;
                destermine_new_aim = true;
            }
            if (what_to_choose == 3)
            {
                steal = true;

                int Which_point = UnityEngine.Random.Range(0, Stealing_points.Count );
                aim_point = Stealing_points[Which_point].gameObject;
                destermine_new_aim = true;
            }
            if (what_to_choose == 4)
            {
                pick_up = true;

                int Which_point = UnityEngine.Random.Range(0, pick_up_points.Count );
                aim_point = pick_up_points[Which_point].gameObject;
                destermine_new_aim = true;
            }

        }
        if (destermine_new_aim)
        {
            if (walk)
            {

                if (Vector3.Distance(transform.position,aim_point.transform.position) > 0.25f)
                {
                   
                    agent.speed = walk_speed;
                    agent.SetDestination(aim_point.transform.position);
                    ani.SetInteger("arms", 1);
                    ani.SetInteger("legs", 1);
                }

                if (Vector3.Distance(transform.position, aim_point.transform.position) < 0.25f)
                {
                    agent.speed = 0;

                    ani.SetInteger("arms", 5);
                    ani.SetInteger("legs", 5);

                    destermine_new_aim = false;
                }

            }
            if(run)
            {

                if (Vector3.Distance(transform.position, aim_point.transform.position) > 0.25f)
                {
                    Debug.Log("going to run");
                    agent.speed = run_speed;
                    agent.SetDestination(aim_point.transform.position);
                    ani.SetInteger("arms", 2);
                    ani.SetInteger("legs", 2);
                }

                if (Vector3.Distance(transform.position, aim_point.transform.position) < 0.25f)
                {
                    agent.speed = 0;

                    ani.SetInteger("arms", 5);
                    ani.SetInteger("legs", 5);

                    destermine_new_aim = false;
                }

            }
            if(sit && !in_sitting)
            {

                if (Vector3.Distance(transform.position, aim_point.transform.position) > 0.25f)
                {
                    
                    agent.speed = walk_speed;
                    agent.SetDestination(aim_point.transform.position);
                    ani.SetInteger("arms", 1);
                    ani.SetInteger("legs", 1);
                }

                if (Vector3.Distance(transform.position, aim_point.transform.position) < 0.25f)
                {
                    agent.speed = 0;


                    if(!in_sitting)
                    {
                        in_sitting = true;

                        sitting_start = StartCoroutine(sitting_down());
                    }

                }

            }
            if(steal && !in_stealing)
            {

                if (Vector3.Distance(transform.position, aim_point.transform.position) > 0.25f)
                {
                 
                    agent.speed = walk_speed;
                    agent.SetDestination(aim_point.transform.position);
                    ani.SetInteger("arms", 1);
                    ani.SetInteger("legs", 1);
                }

                if (Vector3.Distance(transform.position, aim_point.transform.position) < 0.25f)
                {
                    agent.speed = 0;

                 

                    if (!in_stealing)
                    {
                        in_stealing = true;

                        stealing_start = StartCoroutine(stealing_execute());
                    }

                }


            }
            if(pick_up && !in_pickup)
            {
                if (Vector3.Distance(transform.position, aim_point.transform.position) > 0.25f)
                {
                    
                    agent.speed = walk_speed;
                    agent.SetDestination(aim_point.transform.position);
                    ani.SetInteger("arms", 1);
                    ani.SetInteger("legs", 1);
                }

                if (Vector3.Distance(transform.position, aim_point.transform.position) < 0.25f)
                {
                    agent.speed = 0;

                   

                    if (!in_pickup)
                    {
                        in_pickup = true;

                        pickup_start = StartCoroutine(pickup_execute());
                    }

                }
            }


        }


        






    }




    public List<GameObject> way_points = new List<GameObject>();
    public List<GameObject> Sitting_points = new List<GameObject>();
    public List<GameObject> Stealing_points = new List<GameObject>();
    public List<GameObject> pick_up_points = new List<GameObject>();

  


}
