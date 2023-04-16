using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private LightSource lightSource;

    private int isWalkingHash;

    Vector2 currentMovement;
    bool movementPressed;

    [Header("Movement configuration")]
    [SerializeField] float movementSpeed;

    [Header("Light reduction")]
    [SerializeField] float lightReduction;
    [SerializeField] float timeBetweenLightReduction;
    float timeLeftLightReduction;

    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Gameplay.Movement.performed += ctx =>
        {
            currentMovement = ctx.ReadValue<Vector2>();
            movementPressed = currentMovement.sqrMagnitude != 0;
        };


        isWalkingHash = Animator.StringToHash("isWalking");
    }

    void Start()
    {

    }

    void Update()
    {
        //input
        if (playerInput.Gameplay.Movement.WasReleasedThisFrame())
        {
            movementPressed = false;
        }

        //movement
        HandleMovement();

        //light

        if (timeLeftLightReduction <= 0)
        {
            timeLeftLightReduction = timeBetweenLightReduction;
            lightSource.ChangeCurrentSize(-lightReduction);
        }
        timeLeftLightReduction -= Time.deltaTime;
    }

    void HandleMovement()
    {
        bool isWalking = animator.GetBool(isWalkingHash);

        if (movementPressed && !isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }
        else if (!movementPressed && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
        }

        if (movementPressed)
        {
            transform.position += new Vector3(currentMovement.x * Time.deltaTime * movementSpeed, currentMovement.y * Time.deltaTime * movementSpeed, 0);
        }
    }
    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

}
