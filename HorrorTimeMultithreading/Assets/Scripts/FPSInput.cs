using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    public float speed = 2.0f;
    private CharacterController m_CharacterController; //ссылка на компонент
    public float gravity = -9.8f;
    public int n;
    System.Random rand = new System.Random();
    private bool readynow = true;
    static object locker = new object();

    [SerializeField]
    private bool m_IsWalking;
    [SerializeField]
    private float m_StepInterval;
    [SerializeField]
    private AudioClip[] m_FootstepSounds;


    private bool m_PreviouslyGrounded;
    private AudioSource m_AudioSource;
    private Vector2 m_Input;
    public float moveSpeed = 2;
    public float turnSpeed = 90;
    private Transform _thisTransform;

    // List<Thread> threads = new List<Thread>();
  

    void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();//доступ ко всем компонентам
        m_AudioSource = GetComponent<AudioSource>();
        _thisTransform = transform;
    }

    void Update()
    {
      


        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);//диагональ

        movement.y = gravity;

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);// преобразование от локальных к глобальным координатам
        m_CharacterController.Move(movement);
      
        if (m_CharacterController.velocity.sqrMagnitude > 0 || (m_Input.x != 0 || m_Input.y != 0))
        {
            if (readynow)

            {

                StartCoroutine(PlayFootStepAudio());

            }
        }
    }


    public void ChooseFootStepAudio()
    {
        lock (locker)
        {
            n = rand.Next(1, m_FootstepSounds.Length);
            Vector3 movement3 = new Vector3(2, 0, 8);
        }
    }
    public IEnumerator PlayFootStepAudio()
    {
        readynow = false;
        Thread newThrd = new Thread((ChooseFootStepAudio));
        newThrd.Start();
        m_AudioSource.clip = m_FootstepSounds[n];

        m_AudioSource.PlayOneShot(m_AudioSource.clip);
        yield return new WaitForSeconds(1);
        readynow = true;
    }

    public void FixedUpdate()
    {
        // Рассчитываем позицию
        m_CharacterController.Move(_thisTransform.forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical") +
                        Vector3.down * 10.0f * Time.deltaTime);

        // Рассчитываем поворот
        Quaternion rot = Quaternion.AngleAxis(
            turnSpeed * Time.deltaTime * Input.GetAxis("Horizontal"), Vector3.up);
        _thisTransform.rotation *= rot;
    }
}



