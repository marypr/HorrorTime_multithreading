using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class AISceleton : MonoBehaviour
{

    public float speed = 3.0f;
    public float obstacleRange = 5.0f;
    private bool _alive;
    public Animator anim;
    GameObject player;
    private AudioSource m_AudioSource;
    System.Random rand = new System.Random();
    private bool readynow = true;
    static object locker = new object();
    [SerializeField]
    private AudioClip[] m_EnemySounds;
    public int n;


    void Start()
    {
        anim = GetComponent<Animator>();
        _alive = true;
        player = GameObject.FindGameObjectWithTag("Player");
        m_AudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

        if (_alive)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.SphereCast(ray, 0.75f, out hit))
            {
                if (hit.distance < obstacleRange)
                {
                    float angle = Random.Range(-110, 110);
                    // anim.SetBool("isWalking", true);
                    transform.Rotate(0, angle, 0);

                }
            }

            if (player != null)
            {
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance < 1.5f)
                {
                    if (readynow)

                    {

                        StartCoroutine(PlayFootStepAudio());

                    }
                }



            }
        }
       
    }
    
    public void SetAlive(bool alive)
    {
        _alive = alive;
    }

    public void ChooseFootStepAudio()
    {
        lock (locker)
        {
            n = rand.Next(0, (m_EnemySounds.Length-1));
            Debug.Log(n);
            Vector3 movement3 = new Vector3(2, 0, 8);
        }
    }
    public IEnumerator PlayFootStepAudio()
    {
        readynow = false;
        Thread newThrd = new Thread((ChooseFootStepAudio));
        newThrd.Start();
        m_AudioSource.clip = m_EnemySounds[n];

        m_AudioSource.PlayOneShot(m_AudioSource.clip);
        yield return new WaitForSeconds(1);
        readynow = true;
    }
}
