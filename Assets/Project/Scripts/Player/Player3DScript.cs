using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player3DScript : MonoBehaviour
{
    [SerializeField]
    Joystick joystick;
    [SerializeField]
    Slider throttle;

    public GameObject playerDestroyPrefab;
    public AudioClip playerExplosionSound;

    //[SerializeField]
    //float playerExplosionDurationSound = 1.5f;

    [SerializeField]
    float horizontalSpeed;
    [SerializeField]
    public float thrustSpeed;
    [SerializeField]
    float heightSpeed;
    [SerializeField]
    Vector3 moveForward;

    float horizontalMove;
    float verticalMove;

    float ZDistance { get; set; }
    Vector3 NewPos { get; set; }

    Vector3 lastPos;
    public Vector3 currentVel;

    // Use this for initialization
    void Awake()
    {
        lastPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PositionHandler();
        MoveHandler();
        ScreenBounds();
        JoystickHandler();
    }

    void PositionHandler()
    {
        currentVel = GetComponent<Rigidbody>().transform.position - lastPos;
        lastPos = GetComponent<Rigidbody>().transform.position;
    }


    void MoveHandler()
    {
        moveForward = new Vector3(0f, 0f, throttle.value * thrustSpeed * Time.fixedDeltaTime);
        NewPos = transform.position + moveForward;
        GetComponent<Rigidbody>().MovePosition(NewPos);
    }

    void JoystickHandler()
    {
        horizontalMove = joystick.Horizontal * horizontalSpeed * Time.fixedDeltaTime;
        verticalMove = joystick.Vertical * heightSpeed * Time.fixedDeltaTime;

        if (horizontalMove != 0 || verticalMove != 0)
            GetComponent<Rigidbody>().velocity = new Vector3(horizontalMove, verticalMove, 0f);

        else
            GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    void ScreenBounds()
    {
        //Calculating the cameras z position from the player
        ZDistance = Camera.main.transform.position.z - transform.position.z;
        
        float upLimit = Camera.main.ScreenToWorldPoint(new Vector3(
    transform.position.x,
    Screen.height/2,
    -ZDistance / (Mathf.Cos(Camera.main.transform.localEulerAngles.x * Mathf.Deg2Rad))
    )).y;

        if (transform.position.y > upLimit)                                                                 //upLimit causes a "jump" of the ship into the ground (the value of the variable upLimit changes from -1 to -2)
        {
            transform.position = new Vector3(transform.position.x, upLimit, NewPos.z);
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyBullet" || other.tag == "Cliff" || other.tag == "Enemy" || other.tag == "Obstacle" || other.tag == "Ground" || other.tag == "Tree")
        {
            GameObject treeExplosionInstance = Instantiate(playerDestroyPrefab);
            treeExplosionInstance.transform.position = transform.position;
            AudioSource.PlayClipAtPoint(playerExplosionSound, transform.position);

            Destroy(treeExplosionInstance, 1.5f);
            //Destroy(playerExplosionSound, playerExplosionDurationSound);
            Destroy(gameObject);
        }
    }*/
}
