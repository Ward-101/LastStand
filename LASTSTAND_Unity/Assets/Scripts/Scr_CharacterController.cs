using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CharacterController : MonoBehaviour
{
    [SerializeField] private int bulletDamage = 1;
    [SerializeField] private float specialChargeTime = 1f;
    [SerializeField] private float aimDelay = 0.2f;
    [SerializeField] private float moveMargin = 0.1f;
    [SerializeField] private float shootRate = 0.5f;


    [SerializeField] private float aimAndFireInput;
    [SerializeField] private bool meleeInput;
    [SerializeField] private bool specialInput;

    private Vector2 startTouchPos = Vector2.zero;
    private float currentInputTime = 0f;
    private float currentShootTime = 0f;
    private bool lockInAimState = false;
    private bool chargeIsReady = false;

    public LineRenderer LineRenderer;
    public static Scr_CharacterController instance = null;


    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Update()
    {
        GetInput();
        ActionManager();
    }

    private void ActionManager()
    {
        if (aimAndFireInput != 0)
        {
            Rotation();
            Shoot();
        }

        if (meleeInput)
        {
            Melee();
        }

        if (specialInput)
        {
            Special();
        }
        
    }

    private void GetInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (startTouchPos == Vector2.zero)
            {
                startTouchPos = touch.position;
            }

            Vector2 currentTouchPos = touch.position;
            currentInputTime += Time.deltaTime;

            if (touch.phase == TouchPhase.Began)
            {
                meleeInput = true;
            }
            else
            {
                meleeInput = false;
            }

            if (((currentTouchPos.x > startTouchPos.x + moveMargin || currentTouchPos.x < startTouchPos.x - moveMargin || currentTouchPos.y > startTouchPos.y + moveMargin || currentTouchPos.y < startTouchPos.y - moveMargin) 
                && currentInputTime >= aimDelay) || lockInAimState)
            {
                Vector2 rotationVec = (currentTouchPos - startTouchPos).normalized;
                aimAndFireInput = Vector2.SignedAngle(rotationVec, Vector2.up);
            }
            else
            {
                aimAndFireInput = 0;
            }

        }
        else
        {
            startTouchPos = Vector2.zero;
            currentInputTime = 0f;
            aimAndFireInput = 0;
            lockInAimState = false;
        }
    }

    private void Rotation()
    {
        transform.eulerAngles = new Vector3(0f, aimAndFireInput, 0f);
    }

    private void Shoot()
    {
        if (currentShootTime > shootRate)
        {
            GameObject bullet = Scr_ObjectPooler.instance.GetPooledObject();
            if (bullet != null)
            {
                bullet.transform.position = transform.position;
                bullet.transform.rotation = transform.rotation;
                bullet.GetComponent<Scr_BulletBehavior>().bulletDamage = bulletDamage;
                bullet.SetActive(true);
            }
            currentShootTime = 0f;
        }
        else
        {
            currentShootTime += Time.deltaTime;
        }
    }

    private void Melee()
    {

    }

    private void Special()
    {

    }

}
