using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Gun : MonoBehaviour
{

    public float damage = 10f;
    public float range = 1000f;
    public Text scoreText;    

    private int score;
    public int points;
    public int whalepoints;

    public Camera fpsCam;

    public ParticleSystem muzzleFlash;

    public GameObject kappale;   
    public GameObject pyöriväKappale;

    public Text winText;


    

    void Start()
    {
        score = 0;
        scoreText.text = "Score:" + score;
    }

    void Update()
    {
        


        if (Input.GetMouseButtonDown(1))
        {
            Shoot();
        }

    }

    void Shoot ()
    {

        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
                score += points;
                scoreText.text = "Score:" + score;

                GameObject instantiated;

                instantiated = Instantiate(kappale, hit.point, Quaternion.identity);
                instantiated.GetComponent<Kappale>().relativeTransform =hit.transform.gameObject.transform.InverseTransformPoint(hit.point);
                instantiated.GetComponent<Kappale>().rotationParent = hit.transform.gameObject;

                //kappale.GetComponent<Projectiler>().paikka = hit.point;
            }

            if (hit.transform.tag == "Deduction")
            {

                score -= points;
                scoreText.text = "Score:" + score;

            }

            if (hit.transform.tag == "Whale")
            {

                score -= whalepoints;
                scoreText.text = "Score:" + score;

            }

            if (score == 200)
            {
                winText.gameObject.SetActive(true);
            }

        }
    }

}
