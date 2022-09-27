using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject whole;
    public GameObject sliced;


    private Rigidbody fruitRigidbody;// we transfer the velocity of fruit as a whole to individual slices
    private Collider fruitCollider;
    private ParticleSystem juiceParticleEffect;
    private AudioSource bladeSound;

    private void Awake()
    {
        // assigning rigidbody & collider
        fruitRigidbody = GetComponent<Rigidbody>();
        fruitCollider = GetComponent<Collider>();
        juiceParticleEffect = GetComponentInChildren<ParticleSystem>();
        bladeSound = GetComponent<AudioSource>();
    }

    // we get the "direction" from "Blade.cs" (the variable was public)

    private void Slice(Vector3 direction, Vector3 position, float force)
    {
        FindObjectOfType<GameManager>().IncreaseScore();


        // for game objects, "SetActive" instead of ".enabled"
        whole.SetActive(false);
        sliced.SetActive(true);

        fruitCollider.enabled = false; // we don't want to check collision again once its sliced
        juiceParticleEffect.Play();
        bladeSound.Play();

        // gives us the angle that we can roatate our fruit in (tangent)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // radians so convert to degrees

        // we only need to rotate the z axis
        sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // now add force to each of the individual slices ("components" plural bc more than one)
        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody slice in slices)
        {
            // match velocity of slices to the fruit (whole)
            slice.velocity = fruitRigidbody.velocity;

            // "ForceMode.Impulse" because its a one time force we are adding not continuous
            slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        }
    }


    // check for collision with our blade (our blade is a trigger)
    // we need to check that this other collider is a blade
    private void OnTriggerEnter(Collider other)
    {
        // we tagged our blade as the "Player"
        if (other.CompareTag("Player"))
        {
            // getting blade from other that was passed in
            Blade blade = other.GetComponent<Blade>();

            Slice(blade.direction, blade.transform.position, blade.sliceForce);
        }
    }
}
