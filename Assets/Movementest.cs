using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movementest : MonoBehaviour
{
    public float speed;
    public Animator anim;
    public Camera maincamera;
    private Rigidbody rb;

    //camera variaveis 
    public float velocidadecamera;
    public float velocidaderotacaocamera;
    public Vector3 CameraOffset;

    //jump variaveis
    public float jumph;
    public bool isground = true;
    public float jumpforce;
    private Vector3 jump;

    //ataque variaveis
    public bool isattack;
    public float AttackCooldown;


    float InputX, InputZ;
    Vector3 direcao;
    // Start is called before the first frame update
    void Start()
    {
       

    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0f, jumph, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");
        direcao = new Vector3(InputX, 0, InputZ);
 

        if(InputX != 0 || InputZ != 0)
        {
            var camerarot = maincamera.transform.rotation;
            camerarot.x = 0;
            camerarot.z = 0;


            transform.Translate(0, 0, speed * Time.deltaTime); 
            anim.SetBool( "run", true );
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direcao)*camerarot, 5 * Time.deltaTime);
        }
        if (InputX == 0 && InputZ == 0) {
            anim.SetBool("run", false);
        }
        maincamera.transform.rotation = Quaternion.Lerp(maincamera.transform.rotation, transform.rotation, velocidaderotacaocamera * Time.deltaTime);

        
        if (Input.GetKeyDown(KeyCode.Space) && isground)
        {
            rb.AddForce(jump * jumpforce, ForceMode.Impulse);
            //anim.SetBool("Jump", true);
            isground = false;
        }
        if (Input.GetMouseButtonDown(0)){
            attack();
          
        }
        
    }

    IEnumerator ResetAttackCooldown()
    {
        isattack = true;
        yield return new WaitForSeconds(AttackCooldown);
    }
    public void attack()
    {
        isattack = false;
        anim.SetTrigger("Hit");
        StartCoroutine(ResetAttackCooldown());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground"){
            isground = true;
            //anim.SetBool("Jump", false);
        }
    }
    private void LateUpdate()
    {
        var pos = transform.position - maincamera.transform.forward * CameraOffset.z + maincamera.transform.up * CameraOffset.y + maincamera.transform.right * CameraOffset.x;
        maincamera.transform.position = Vector3.Lerp(maincamera.transform.position, pos, velocidadecamera    * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        
    }
}
