using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clothing : MonoBehaviour
{


    public GameObject skin_head;
    public GameObject skin_body;
  

    public GameObject cigarette;
    public GameObject crowbar;
    public GameObject fireaxe;
    public GameObject glock;
    public GameObject phone;
    

    public GameObject beard_a;
    public GameObject beard_b;
    public GameObject beard_c;
    public GameObject beard_d;

    public GameObject hair_a;
    public GameObject hair_b;
    public GameObject hair_c;
    public GameObject hair_d;
    public GameObject hair_e;

    public GameObject cap;
    public GameObject cap2;
    public GameObject cap3;
    public GameObject chain1;
    public GameObject chain2;
    public GameObject chain3;
    
    public GameObject banker_suit;

    public GameObject cock_suit;
    public GameObject cock_suit_hat;

    public GameObject farmer_suit;
    public GameObject farmer_suit_hat;

    public GameObject fireman_suit;
    public GameObject fireman_suit_hat;

    public GameObject mechanic_suit;
    public GameObject mechanic_suit_hat;

    public GameObject nurse_suit;

    public GameObject police_suit;
    public GameObject police_suit_hat;

    public GameObject roober_suit;
    public GameObject roober_suit_hat;

    public GameObject security_guard_suit;
    public GameObject security_guard_suit_hat;

    public GameObject seller_suit;

    public GameObject worker_suit;
    public GameObject worker_suit_hat;

    public GameObject glasses;
    public GameObject jacket;
    public GameObject pullover;
    public GameObject scarf;
    public GameObject shirt;

    public GameObject shoes1;
    public GameObject shoes2;
    public GameObject shoes3;

    public GameObject shortpants;
    public GameObject t_shirt;
    public GameObject tank_top;
    public GameObject trousers;


  
    
    public Texture[] skin_textures;

    public Texture[] beard_textures;

    public Texture[] hair_a_textures;
    public Texture[] hair_b_textures;
    public Texture[] hair_c_textures;
    public Texture[] hair_d_textures;
    public Texture[] hair_e_textures;

    public Texture[] cap_textures;
    public Texture[] cap2_textures;
    public Texture[] cap3_textures;
    public Texture[] chain1_textures;
    public Texture[] chain2_textures;
    public Texture[] chain3_textures;

    public Texture[] banker_suit_texture;

    public Texture cock_suit_texture;
    

    public Texture farmer_suit_texture;
    

    public Texture fireman_suit_texture;
    

    public Texture mechanic_suit_texture;
   

    public Texture nurse_suit_texture;

    public Texture police_suit_texture;


    public Texture roober_suit_texture;


    public Texture security_guard_suit_texture;
   

    public Texture seller_suit_texture;

    public Texture worker_suit_texture;


    public Texture[] glasses_texture;
    public Texture[] jacket_textures;
    public Texture[] pullover_textures;
    public Texture[] scarf_textures;
    public Texture[] shirt_textures;

    public Texture[] shoes1_textures;
    public Texture[] shoes2_textures;
    public Texture[] shoes3_textures;

    public Texture[] shortpants_textures;
    public Texture[] t_shirt_textures;
    public Texture[] tank_top_textures;
    public Texture[] trousers_textures;

    public Animator ani;


    public bool show_run;
   

   

    bool hat;







   



    Coroutine coroutine_random_clothing;

    IEnumerator start_random_clothing()
    {
        yield return new WaitForSeconds(0);

        // disapear all cloth, for a new run

        hat = true;

        hair_a.SetActive(false);
        hair_b.SetActive(false);
        hair_c.SetActive(false);
        hair_d.SetActive(false);
        hair_e.SetActive(false);

        beard_a.SetActive(false);
        beard_b.SetActive(false);
        beard_c.SetActive(false);
        beard_d.SetActive(false);

        cap.SetActive(false);
        cap2.SetActive(false);
        cap3.SetActive(false);

        chain1.SetActive(false);
        chain2.SetActive(false);
        chain3.SetActive(false);

        banker_suit.SetActive(false);

        cock_suit.SetActive(false);
        cock_suit_hat.SetActive(false);

        farmer_suit.SetActive(false);
        farmer_suit_hat.SetActive(false);

        fireman_suit.SetActive(false);
        fireman_suit_hat.SetActive(false);

        mechanic_suit.SetActive(false);
        mechanic_suit_hat.SetActive(false);

        nurse_suit.SetActive(false);

        police_suit.SetActive(false);
        police_suit_hat.SetActive(false);

        roober_suit.SetActive(false);
        roober_suit_hat.SetActive(false);

        security_guard_suit.SetActive(false);
        security_guard_suit_hat.SetActive(false);

        seller_suit.SetActive(false);

        worker_suit.SetActive(false);
        worker_suit_hat.SetActive(false);

        glasses.SetActive(false);

        jacket.SetActive(false);

        pullover.SetActive(false);

        scarf.SetActive(false);

        shirt.SetActive(false);

        shoes1.SetActive(false);

        shoes2.SetActive(false);

        shoes3.SetActive(false);

        shortpants.SetActive(false);

        t_shirt.SetActive(false);

        tank_top.SetActive(false);

        trousers.SetActive(false);








      

        // determining skin color

        int skin_color = UnityEngine.Random.Range(0, 6);

        skin_head.GetComponent<Renderer>().materials[0].mainTexture = skin_textures[skin_color];
        skin_body.GetComponent<Renderer>().materials[0].mainTexture = skin_textures[skin_color];



        // determining male or female
        int male_female = UnityEngine.Random.Range(0, 2);

   
        
       
        // does a hat fit for the hair
        

        // determining haircolor
        int hairColor = UnityEngine.Random.Range(0, 4);    // 0 = dark  1 = brown  2 = blonde
        
        // male
        if(male_female == 0)
        {
         

            hat = true;

            // choose hair type   hair_a , hair_b  , hair_e
            int hair = UnityEngine.Random.Range(0, 3);

            if (hair == 0)
            {
                hair_a.SetActive(true);

                // 0 = full hair    1 = under cut
                int hair_cut = UnityEngine.Random.Range(0, 2);
                hat = true;
            

                if (hairColor == 0)
                {
                    if (hair_cut == 0)
                    {
                        hair_a.GetComponent<Renderer>().materials[0].mainTexture = hair_a_textures[0];
                    }
                    if (hair_cut == 1)
                    {
                        hair_a.GetComponent<Renderer>().materials[0].mainTexture = hair_a_textures[1];
                    }
                }
                if (hairColor == 1)
                {
                    if (hair_cut == 0)
                    {
                        hair_a.GetComponent<Renderer>().materials[0].mainTexture = hair_a_textures[2];
                    }
                    if (hair_cut == 1)
                    {
                        hair_a.GetComponent<Renderer>().materials[0].mainTexture = hair_a_textures[3];
                    }

                }
                if (hairColor == 2)
                {
                    if (hair_cut == 0)
                    {
                        hair_a.GetComponent<Renderer>().materials[0].mainTexture = hair_a_textures[4];
                    }
                    if (hair_cut == 1)
                    {
                        hair_a.GetComponent<Renderer>().materials[0].mainTexture = hair_a_textures[5];
                    }

                }


            }

            if (hair == 1)
            {
                hair_b.SetActive(true);
                hat = false;

                // 0 = full hair    1 = under cut
                int hair_cut = UnityEngine.Random.Range(0, 2);



                if (hairColor == 0)
                {
                    if (hair_cut == 0)
                    {
                        hair_b.GetComponent<Renderer>().materials[0].mainTexture = hair_b_textures[0];
                    }
                    if (hair_cut == 1)
                    {
                        hair_b.GetComponent<Renderer>().materials[0].mainTexture = hair_b_textures[5];
                    }
                }
                if (hairColor == 1)
                {
                    if (hair_cut == 0)
                    {
                        hair_b.GetComponent<Renderer>().materials[0].mainTexture = hair_b_textures[1];
                    }
                    if (hair_cut == 1)
                    {
                        hair_b.GetComponent<Renderer>().materials[0].mainTexture = hair_b_textures[3];
                    }

                }
                if (hairColor == 2)
                {
                    if (hair_cut == 0)
                    {
                        hair_b.GetComponent<Renderer>().materials[0].mainTexture = hair_b_textures[2];
                    }
                    if (hair_cut == 1)
                    {
                        hair_b.GetComponent<Renderer>().materials[0].mainTexture = hair_b_textures[4];
                    }

                }



            }

            if (hair == 2)
            {
                hair_e.SetActive(true);
                hat = false;
               



                if (hairColor == 0)
                {
                    
                        hair_e.GetComponent<Renderer>().materials[0].mainTexture = hair_e_textures[0];
                    
                   
                }
                if (hairColor == 1)
                {
                   
                        hair_e.GetComponent<Renderer>().materials[0].mainTexture = hair_e_textures[1];
                    
                   

                }
                if (hairColor == 2)
                {
                   
                        hair_e.GetComponent<Renderer>().materials[0].mainTexture = hair_e_textures[2];
                    
                   

                }


            }

        }
        // female
        if(male_female == 1)
        {
            
            hat = false;

            // choose hair type   hair_c , hair_d
            int hair = UnityEngine.Random.Range(0, 2);


            if(hair == 0)
            {
                hat = false;
                hair_c.SetActive(true);

                if(hairColor == 0)
                {
                    hair_c.GetComponent<Renderer>().materials[0].mainTexture = hair_c_textures[0];
                }
                if (hairColor == 1)
                {
                    hair_c.GetComponent<Renderer>().materials[0].mainTexture = hair_c_textures[1];
                }
                if (hairColor == 2)
                {
                    hair_c.GetComponent<Renderer>().materials[0].mainTexture = hair_c_textures[2];
                }
            }
            if (hair == 1)
            {
                hat = false;
                hair_d.SetActive(true);

                if (hairColor == 0)
                {
                    hair_d.GetComponent<Renderer>().materials[0].mainTexture = hair_d_textures[0];
                }
                if (hairColor == 1)
                {
                    hair_d.GetComponent<Renderer>().materials[0].mainTexture = hair_d_textures[1];
                }
                if (hairColor == 2)
                {
                    hair_d.GetComponent<Renderer>().materials[0].mainTexture = hair_d_textures[2];
                }
            }
        }

        // determining beard
        if(male_female == 0)
        {
            int percent = UnityEngine.Random.Range(0, 100);

            if(percent > 0 && percent < 50)
            {
                // none beard
            }
            if (percent > 50 && percent < 70)
            {
                // beard a
                beard_a.SetActive(true);

                if(hairColor == 0)
                {
                    beard_a.GetComponent<Renderer>().materials[0].mainTexture = beard_textures[0];
                }
                if (hairColor == 1)
                {
                    beard_a.GetComponent<Renderer>().materials[0].mainTexture = beard_textures[1];
                }
                if (hairColor == 2)
                {
                    beard_a.GetComponent<Renderer>().materials[0].mainTexture = beard_textures[2];
                }
            }
            if (percent > 70 && percent < 80)
            {
                // beard b

                beard_b.SetActive(true);

                if (hairColor == 0)
                {
                    beard_b.GetComponent<Renderer>().materials[0].mainTexture = beard_textures[0];
                }
                if (hairColor == 1)
                {
                    beard_b.GetComponent<Renderer>().materials[0].mainTexture = beard_textures[1];
                }
                if (hairColor == 2)
                {
                    beard_b.GetComponent<Renderer>().materials[0].mainTexture = beard_textures[2];
                }
            }
            if (percent > 80 && percent < 90)
            {
                // beard c

                beard_c.SetActive(true);

                if (hairColor == 0)
                {
                    beard_c.GetComponent<Renderer>().materials[0].mainTexture = beard_textures[0];
                }
                if (hairColor == 1)
                {
                    beard_c.GetComponent<Renderer>().materials[0].mainTexture = beard_textures[1];
                }
                if (hairColor == 2)
                {
                    beard_c.GetComponent<Renderer>().materials[0].mainTexture = beard_textures[2];
                }
            }
            if (percent > 90 && percent < 100)
            {
                // beard d

                beard_d.SetActive(true);

                if (hairColor == 0)
                {
                    beard_d.GetComponent<Renderer>().materials[0].mainTexture = beard_textures[0];
                }
                if (hairColor == 1)
                {
                    beard_d.GetComponent<Renderer>().materials[0].mainTexture = beard_textures[1];
                }
                if (hairColor == 2)
                {
                    beard_d.GetComponent<Renderer>().materials[0].mainTexture = beard_textures[2];
                }
            }
        }


        // determining complet suits or normal cloth
        int suit_or_cloth = UnityEngine.Random.Range(0, 2);

        // determing      suit/ normal cloth to wear


        if (suit_or_cloth == 0)
        {
            // suits

            int which_suit = UnityEngine.Random.Range(0,11);




            // bankersuit    0
            // cocksuit      1
            // farmersuit    2
            // firemansuit   3
            // mechanicsuit  4
            // nursesuit     5
            // policesuit    6
            // roobersuit    7
            // securitysuit  8
            // sellersuit    9
            // workersuit    10


            // banker suit
            if (which_suit == 0)
            {
                banker_suit.SetActive(true);

                int which_texture = UnityEngine.Random.Range(0, 7);

               
                banker_suit.GetComponent<Renderer>().materials[0].mainTexture = banker_suit_texture[which_texture];
                
            }
            // cock suit
            if(which_suit == 1)
            {
                cock_suit.SetActive(true);

                cock_suit.GetComponent<Renderer>().materials[0].mainTexture = cock_suit_texture;

                if(hat)
                {
                    cock_suit_hat.SetActive(true);
                    cock_suit_hat.GetComponent<Renderer>().materials[0].mainTexture = cock_suit_texture;
                }
            }
            // farmer suit
            if (which_suit == 2)
            {
                farmer_suit.SetActive(true);

                farmer_suit.GetComponent<Renderer>().materials[0].mainTexture = farmer_suit_texture;

                if (hat)
                {
                    farmer_suit_hat.SetActive(true);
                    farmer_suit_hat.GetComponent<Renderer>().materials[0].mainTexture = farmer_suit_texture;
                }
            }
            // fireman suit
            if (which_suit == 3)
            {
                fireman_suit.SetActive(true);

                fireman_suit.GetComponent<Renderer>().materials[0].mainTexture = fireman_suit_texture;

                if (hat)
                {
                    fireman_suit_hat.SetActive(true);

                    fireman_suit_hat.GetComponent<Renderer>().materials[0].mainTexture = fireman_suit_texture;
                }
            }
            // mechanic suit
            if (which_suit == 4)
            {
                mechanic_suit.SetActive(true);

                mechanic_suit.GetComponent<Renderer>().materials[0].mainTexture = mechanic_suit_texture;

                if (hat)
                {
                    mechanic_suit_hat.SetActive(true);

                    mechanic_suit_hat.GetComponent<Renderer>().materials[0].mainTexture = mechanic_suit_texture;
                }
            }
            // nurse suit
            if (which_suit == 5)
            {
                nurse_suit.SetActive(true);

                nurse_suit.GetComponent<Renderer>().materials[0].mainTexture = nurse_suit_texture;

               
            }
            // police suit
            if (which_suit == 6)
            {
                police_suit.SetActive(true);

                police_suit.GetComponent<Renderer>().materials[0].mainTexture = police_suit_texture;

                if (hat)
                {
                    police_suit_hat.SetActive(true);

                    police_suit_hat.GetComponent<Renderer>().materials[0].mainTexture = police_suit_texture;
                }
            }
            // robber suit
            if (which_suit == 7)
            {
                roober_suit.SetActive(true);

                roober_suit.GetComponent<Renderer>().materials[0].mainTexture = roober_suit_texture;

              
                    roober_suit_hat.SetActive(true);

                    roober_suit_hat.GetComponent<Renderer>().materials[0].mainTexture = roober_suit_texture;


                hair_a.SetActive(false);
                hair_b.SetActive(false);
                hair_c.SetActive(false);
                hair_d.SetActive(false);
                hair_e.SetActive(false);

                beard_a.SetActive(false);
                beard_b.SetActive(false);
                beard_c.SetActive(false);
                beard_d.SetActive(false);
            }
            // security guard suit
            if (which_suit == 8)
            {
                security_guard_suit.SetActive(true);

                security_guard_suit.GetComponent<Renderer>().materials[0].mainTexture = security_guard_suit_texture;

                if (hat)
                {
                    security_guard_suit_hat.SetActive(true);

                    security_guard_suit_hat.GetComponent<Renderer>().materials[0].mainTexture = security_guard_suit_texture;
                }
            }
            // seller suit
            if (which_suit == 9)
            {
                seller_suit.SetActive(true);

                seller_suit.GetComponent<Renderer>().materials[0].mainTexture = seller_suit_texture;

            }
            // worker suit
            if (which_suit == 10)
            {
                worker_suit.SetActive(true);

                worker_suit.GetComponent<Renderer>().materials[0].mainTexture = worker_suit_texture;

                if (hat)
                {
                    worker_suit_hat.SetActive(true);

                    worker_suit_hat.GetComponent<Renderer>().materials[0].mainTexture = worker_suit_texture;
                }
            }




        }
        if (suit_or_cloth == 1)
        {
            // normal cloth

            int shoes = UnityEngine.Random.Range(0, 3);

            if(shoes == 0)
            {
                shoes1.SetActive(true);

                int shoes1_texture = UnityEngine.Random.Range(0, 8);

                shoes1.GetComponent<Renderer>().materials[0].mainTexture = shoes1_textures[shoes1_texture];

            }

            if (shoes == 1)
            {
                shoes2.SetActive(true);

                int shoes2_texture = UnityEngine.Random.Range(0, 7);

                shoes2.GetComponent<Renderer>().materials[0].mainTexture = shoes2_textures[shoes2_texture];

            }

            if (shoes == 2)
            {
                shoes3.SetActive(true);

                int shoes3_texture = UnityEngine.Random.Range(0, 6);

                shoes3.GetComponent<Renderer>().materials[0].mainTexture = shoes3_textures[shoes3_texture];

            }


            int glasses_percentage = UnityEngine.Random.Range(0, 100);

            if(glasses_percentage < 20)
            {
                glasses.SetActive(true);

                int texture_choose = UnityEngine.Random.Range(0, 6);

                glasses.GetComponent<Renderer>().materials[0].mainTexture = glasses_texture[texture_choose];
            }

            int chain = UnityEngine.Random.Range(0, 3);

            if(chain == 0)
            {
                chain1.SetActive(true);

                int textures = UnityEngine.Random.Range(0, 4);

                chain1.GetComponent<Renderer>().materials[0].mainTexture = chain1_textures[textures];

            }
            if(chain == 1)
            {
                chain2.SetActive(true);

                int textures = UnityEngine.Random.Range(0, 3);

                chain2.GetComponent<Renderer>().materials[0].mainTexture = chain2_textures[textures];

            }
            if(chain == 2)
            {
                chain3.SetActive(true);

                int textures = UnityEngine.Random.Range(0, 3);

                chain3.GetComponent<Renderer>().materials[0].mainTexture = chain3_textures[textures];

            }

            int scarfPercentage = UnityEngine.Random.Range(0, 100);

            if(scarfPercentage < 20)
            {
                scarf.SetActive(true);

                int textures = UnityEngine.Random.Range(0, 11);

                scarf.GetComponent<Renderer>().materials[0].mainTexture = scarf_textures[textures];
            }

            int which_trouser = UnityEngine.Random.Range(0, 2);

            // trousers
            if(which_trouser == 0)
            {
                trousers.SetActive(true);

                int texture = UnityEngine.Random.Range(0, 15);

                trousers.GetComponent<Renderer>().materials[0].mainTexture = trousers_textures[texture];
                
            }
            // short pants
            if (which_trouser == 1)
            {
                shortpants.SetActive(true);


                int texture = UnityEngine.Random.Range(0, 11);

                shortpants.GetComponent<Renderer>().materials[0].mainTexture = shortpants_textures[texture];


            }


            // upper bosy cloth :   0 = pullover  1 = shirt    2 = t_shirt    3 = tanktop
            int upper_cloth = UnityEngine.Random.Range(0, 4);

            
            if(upper_cloth == 0)
            {
                pullover.SetActive(true);

                int texture = UnityEngine.Random.Range(0, 17);

                pullover.GetComponent<Renderer>().materials[0].mainTexture = pullover_textures[texture];
            }

            if (upper_cloth == 1)
            {
                shirt.SetActive(true);

                int texture = UnityEngine.Random.Range(0, 14);

                shirt.GetComponent<Renderer>().materials[0].mainTexture = shirt_textures[texture];
            }
            if (upper_cloth == 2)
            {
                t_shirt.SetActive(true);

                int texture = UnityEngine.Random.Range(0, 21);

                t_shirt.GetComponent<Renderer>().materials[0].mainTexture = t_shirt_textures[texture];
            }
            if (upper_cloth == 3)
            {
                tank_top.SetActive(true);

                int texture = UnityEngine.Random.Range(0, 11);

                tank_top.GetComponent<Renderer>().materials[0].mainTexture = tank_top_textures[texture];
            }



        }




        yield return new WaitForSeconds(5);

        StopCoroutine(coroutine_random_clothing);
        coroutine_random_clothing = StartCoroutine(start_random_clothing());
        
    }


    void Update()
    {
        if(!show_run)
        {
            show_run = true;

            coroutine_random_clothing = StartCoroutine(start_random_clothing());

          
        }
        





    }
}
