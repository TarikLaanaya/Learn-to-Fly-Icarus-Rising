using UnityEngine;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    private float gravity;
    private float maxSpeed;
    private float speedFactor;
    private float deceleration;
    private float currentSpeed;
    private float boostStrength;
    private bool boosting;

    [SerializeField] float rotationSpeed = 200f;
    [SerializeField] Transform modelTransform;
    private Rigidbody2D rb;
    private float rotationInput;
    [HideInInspector] public bool isAlive = true;
    private bool isStarted = false;
    [SerializeField] private GameObject cutSceneObj;
    [SerializeField] private Renderer modelRenderer;
    [SerializeField] private Renderer jetpackModelRenderer;
    private UpgradesHandler upgradesHandler;
    [SerializeField] private AudioSource jetPackAudioSource;
    private float rocketAudioClipLength;
    [SerializeField] private AudioSource windAudio;
    [SerializeField] private float speedToPitchMultiplier; 
    [SerializeField] private GameObject[] flames;
    private GameObject currentFlame;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = new Vector3(0, SceneManager.instance.gameManager.playerStartHeight, transform.position.z);

        isStarted = false;
        rb.gravityScale = 0;
        modelRenderer.enabled = false;
        jetpackModelRenderer.enabled = false;

        rocketAudioClipLength = jetPackAudioSource.clip.length;

        // --- Set Upgrade Values --- //#

        upgradesHandler = GetComponent<UpgradesHandler>();

        // Get the current upgrades
        int currentWingUpgrade = SceneManager.instance.gameManager.GetCurrentWingUpgrade();
        int currentBoostUpgrade = SceneManager.instance.gameManager.GetCurrentBoostUpgrade();

        // Grab the correct struct that has the current upgrade details
        UpgradesHandler.WingUpgrade wingStruct = upgradesHandler.wingUpgrades[currentWingUpgrade];
        UpgradesHandler.BoostUpgrade boostStruct = upgradesHandler.boostUpgrades[currentBoostUpgrade];

        // Wings
        gravity = wingStruct.gravity;
        maxSpeed = wingStruct.maxSpeed;
        speedFactor = wingStruct.speedFactor;
        deceleration = wingStruct.deceleration;

        // Boost
        boostStrength = boostStruct.boostStrength;

        currentFlame = flames[0];

        if (currentBoostUpgrade > 2)
        {
            currentFlame = flames[1];
        }

    }

    void Update()
    {
        rotationInput = Input.GetAxis("Horizontal");

        Debug.DrawRay(transform.position, rb.linearVelocity, Color.green);

        // Cutscene logic
        if (!isStarted)
        {
            if (cutSceneObj == null)
            {
                isStarted = true;
                rb.gravityScale = 1;
                modelRenderer.enabled = true;
                jetpackModelRenderer.enabled = true;
                return;
            }

            transform.position = cutSceneObj.transform.position;
        }

        if (!isAlive || !isStarted) return;

        if (Input.GetKey(KeyCode.Space))
        {
            boosting = true;
            currentFlame.SetActive(true);

            if (!jetPackAudioSource.isPlaying)
            {
                // Play Rocket Audio at Random Timestamp
                float randomStartTime = Random.Range(0.0f, rocketAudioClipLength);
                jetPackAudioSource.time = randomStartTime;
                jetPackAudioSource.Play();
            }
        }
        else
        {
            boosting = false;
            jetPackAudioSource.Stop();
            currentFlame.SetActive(false);
        }

        windAudio.pitch = Mathf.Clamp(rb.linearVelocity.magnitude * speedToPitchMultiplier, 0.8f, 3f);
    }

    void FixedUpdate()
    {
        if (!isAlive || !isStarted) { rb.linearVelocity = Vector2.zero; return; }

        // --- ROTATION --- //

        Rotate();

        // --- GLIDE MOVEMENT --- //

        GlideMovement();
    }

    void Rotate()
    {
        // Clamp rotation
        if (modelTransform.rotation.z > .7f && rotationInput < 0)
        {
            return;
        }
        else if (modelTransform.rotation.z < -.7f && rotationInput > 0)
        {
            return;
        }

        modelTransform.Rotate(0, 0, -rotationInput * rotationSpeed * Time.deltaTime); // Rotate based on the input * rotation speed
    }

    void GlideMovement()
    {
        float addedSpeed = 0;

        // Dont add speed when pointed up
        if (modelTransform.rotation.z > 0)
        {
            addedSpeed = 0;
            currentSpeed -= deceleration * Time.deltaTime; // Deceleration (only when pointed up)
        }
        else if (modelTransform.rotation.z < 0)
        {
            addedSpeed = speedFactor * Mathf.Clamp(modelTransform.rotation.z / -0.7f, .5f, 1f); // Reduce speed factor based on how steep the angle is
        }

        // Boost
        if (boosting)
        {
            addedSpeed += boostStrength * Time.deltaTime;
        }

        // --- Apply Calculated Values --- //

        currentSpeed += addedSpeed * Time.deltaTime; // Adjust current speed based on the added speed
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed); // Clamp the speed

        rb.linearVelocity = modelTransform.right * currentSpeed + Vector3.up * -gravity; // Set velocity and add gravity
    }
}