using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed=10;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public bool powerUp;
    private float powerupStrength = 15;
    public GameObject powerupIndicator;
    // Start is called before the first frame update
    void Start()
    {
        //powerupIndicator = GameObject.Find("Powerup Indicator"); NEDEN BÖYLE OLMUYODA PUBLÝC YAPIP ÝSPECTOR DEN ATAMA YAPMAMIZ GEREKÝYOR
        focalPoint = GameObject.Find("Focal Point");
        playerRb= GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        powerupIndicator.transform.position = transform.position;
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PowerUp"))
        {
            powerupIndicator.SetActive(true);/*gameObject olmadan da oluyor. ama kursta gameObject. ekliyor setactive den önce*/
            powerUp= true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCoroutin());
        }
    }
    IEnumerator PowerUpCoroutin()
    {
        yield return new WaitForSeconds(7);
        powerUp= false;
        powerupIndicator.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
        Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
        if (collision.gameObject.CompareTag("Enemy") &&powerUp)
        {
            Debug.Log("Collided with " + collision.gameObject.name + "with powerup set to " + powerUp);
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }
}
