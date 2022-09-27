using UnityEngine;

public class Blade : MonoBehaviour
{
    private Camera mainCamera;
    private Collider bladeCollider;
    private TrailRenderer bladeTrail;
    private bool slicing;

    // we only need public read
    // private set so only this function can change it but public get so others can read
    public Vector3 direction { get; private set; }
    public float sliceForce = 5f; 
    public float minSliceVelocity = 0.01f; 

    private void Awake()
    {
        mainCamera = Camera.main; 
        bladeCollider = GetComponent<Collider>();
        bladeTrail = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        StopSlicing();
    }

    private void OnDisable()
    {
        StopSlicing();
    }

    private void Update()
    {
        // when mouse is pressed, we start slicing
        if (Input.GetMouseButtonDown(0)){
            StartSlicing();
        }else if (Input.GetMouseButtonUp(0)) // when released, we stop slicing
        {
            StopSlicing();
        }
        else
        {
            ContinueSlicing();
        }
    }

    private void StartSlicing()
    {
        // converting mouse position that is in screenspace and need to convert this to world space
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        newPosition.z = 0f; // 3D game but plays like a 2D game so set z to 0

        transform.position = newPosition;

        slicing = true;
        bladeCollider.enabled = true;
        bladeTrail.enabled = true;
        bladeTrail.Clear();
    }

    private void StopSlicing()
    {
        slicing = false;
        bladeCollider.enabled = false;
        bladeTrail.enabled = false; 
    }

    // when we continue slicing we just need to update the position of our blade
    private void ContinueSlicing()
    {
        // converting mouse position that is in screenspace and need to convert this to world space
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        newPosition.z = 0f; // 3D game but plays like a 2D game so set z to 0

        // transform.position is current position so newPosition - currentPosition = direction
        direction = newPosition - transform.position;

        // determine how fast it's moving (velocity); velocity is distance / Time 
        // direction.magnitude == how long our vector is
        // Time.deltaTime == how much time has elapsed since the previous frame
        float velocity = direction.magnitude / Time.deltaTime;

        // collider only enabled when velocity is greater than min slice velocity
        bladeCollider.enabled = velocity > minSliceVelocity;


        // update blade position to newPosition
        transform.position = newPosition;
    }
}
