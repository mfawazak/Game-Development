using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Car : MonoBehaviour
{
    public Text fuelText;
    private Rigidbody rb;
    public float speed = 10f;
    public float burstSpeedMultiplier = 2f;
    public float burstDuration = 2f;
    private bool burstAvailable = true;

    public float fuelValue = 500;
    // Start is called before the first frame update
    void Start()
    {
        fuelText.text = "Fuel is" + fuelValue.ToString();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(fuelValue <= 0)
        {
            speed = 0;
            fuelValue = 0;
        }
                
        // Movement
        float moveHorizontal = Input.GetAxis("Horizontal") * speed;
        float moveVertical = Input.GetAxis("Vertical") * speed;
        
        Vector3 movement = new Vector3(moveHorizontal, rb.velocity.y, moveVertical);
        
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector3(movement.x, rb.velocity.y, speed);
            fuelValue--;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector3(movement.x, rb.velocity.y, -speed);
            fuelValue--;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector3(-speed, rb.velocity.y, movement.z);
            fuelValue--;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector3(speed, rb.velocity.y, movement.z);
            fuelValue--;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        // Speed Burst
        if (Input.GetKeyDown(KeyCode.Space) && burstAvailable)
        {
            StartCoroutine(SpeedBurst());
        }

    }
    IEnumerator SpeedBurst()
    {
        float originalSpeed = speed;
        speed *= burstSpeedMultiplier;
        burstAvailable = false;
        yield return new WaitForSeconds(burstDuration);
        speed = originalSpeed;
        fuelValue =- 10;
        burstAvailable = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fuel"))
        {
            fuelValue += 100;
            fuelText.text = "Fuel is" + fuelValue.ToString();
            Destroy(other.gameObject);
        }
    }
}
