using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using AttackMode = Weapon.AttackMode;
using Debug = UnityEngine.Debug;

public class CharacterController : MonoBehaviour
{
    // Enums
    enum Status
    {
        Stopped,
        Moving,
        Aiming,
        Attacking
    }

    // GameObjects
    public GameObject player;
    public GameObject moveCursor;
    public GameObject targetCursor;
    
    // Components
    Animator anim;

    // Move ����
    Vector3 playerDestination = Vector3.zero;
    Vector3 targetDirection = Vector3.zero;

    // Status ����
    Status mode = Status.Stopped;
    Status prevMode = Status.Stopped;
    AttackMode attackMode = AttackMode.Basic;

    Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        anim = player.GetComponent<Animator>();

        weapon = player.GetComponent<Weapon>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))    // RMB
        {
            playerDestination = GetRaycastHitpoint();
            playerDestination.y = 0.0f;

            if (mode == Status.Aiming)
            {
                QuitAim();
            }

            Move();
        }

        if (Input.GetMouseButtonDown(0))    // LMB
        {
            if (mode == Status.Aiming)
            {
                QuitAim();
                Fire();
            }
        }

        if (Input.GetKeyDown("a"))
        {
            SwitchMode(Status.Aiming);
            attackMode = AttackMode.Basic;
        }

        if (Input.GetKeyDown("1"))
        {
            SwitchMode(Status.Aiming);
            attackMode = AttackMode.Cannon;
        }

        switch (mode)
        {
            case Status.Moving:

                // When player moved onto the point
                if (Vector3.Distance(player.transform.position, playerDestination) < 0.05f)
                {
                    Stop();
                }
                else
                {
                    Move();
                }

                break;

            case Status.Stopped:

                break;

            case Status.Aiming:
                Stop();
                Aim();

                break;
        }

    }

    ///
    ///

    void Stop()
    {
        anim.SetBool("isRunning", false);
        moveCursor.SetActive(false);

        SwitchMode(Status.Stopped);
    }

    void Move()
    {
        anim.SetBool("isRunning", true);

        playerDestination.y = 0.55f;
        targetDirection = playerDestination - player.transform.position;
        
        moveCursor.transform.position = playerDestination;
        moveCursor.SetActive(true);

        player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(targetDirection), Time.deltaTime * 8.0f);
        player.transform.position = Vector3.MoveTowards(player.transform.position, playerDestination, Time.deltaTime * 5.0f);

        SwitchMode(Status.Moving);
    }

    void QuitAim()
    {
        targetCursor.SetActive(false);

        SwitchMode(Status.Stopped);
    }

    void Aim()
    {

        Vector3 targetPosition = GetRaycastHitpoint();
        targetPosition.y = 0.55f;

        targetCursor.transform.position = targetPosition;
        targetCursor.SetActive(true);

        player.transform.LookAt(targetPosition);


        // position to fire the projectile
        Vector3 firePosition = Vector3.MoveTowards(player.transform.position, targetCursor.transform.position, 0.5f);
        firePosition.y += 1.5f;

        if (attackMode == AttackMode.Cannon)
        {
            // �̸�����
             weapon.PreviewCannonballTrace();

            if (Input.GetKey("q"))
            {
                // �߻簢 ���
                Debug.Log("�߻簢 ���");
            }
            else if (Input.GetKey("e"))
            {
                // �߻簢 �ϰ�
                Debug.Log("�߻簢 �ϰ�");
            }

        }

        SwitchMode(Status.Aiming);

    }

    void Fire()
    {
       player.GetComponent<Weapon>().Fire(attackMode);
    }



    ///
    ///

    Vector3 GetRaycastHitpoint()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit h;
        Physics.Raycast(r, out h);

        return h.point;
    }

    void SwitchMode(Status modeChangeTo)
    {
        prevMode = mode;
        mode = modeChangeTo;
    }
}
