using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;

    private Vector3 offset;//camera and player distance
    public float speed = 12.0f; //player speed
    public float forceSpd = 9.0f; //player charge speed
    private float force = 0.0f; //player�w�g�W�F�h�֤O ���j�p

    public float highestSpd = 50f;

    public float distance = 6.0f; //��v�� �� ���y �Z�� ��l��
    public float xSpeed = 120.0f; //the speed of mouse moving left and right
    public float ySpeed = 120.0f; //the speed of mouse moving up and down

    public float yMinLimit = -20f;  //�ƹ��W�U ����� �U��
    public float yMaxLimit = 80f;   //�ƹ��W�U ����� �W��

    public float distanceMin = 1.5f;  //�u�� �� ��v�� �� ���y �Z���U��
    public float distanceMax = 15f;  //�u�� �� ��v�� �� ���y �Z���W��

    private Rigidbody rbody;

    float x = 0.0f;
    float y = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //��v����m - ���y��m = �۹��m
        offset = transform.position - player.transform.position;

        Vector3 angles = transform.eulerAngles; //the angle of camera
        x = angles.y;
        y = angles.x;

        rbody = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.position = player.transform.position + offset;

        
        //mouse control
        if (Input.GetMouseButton(0))//Mouse Lt, activate after pressing
        {
            Debug.Log("Lt Button down!");//testing

            x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            y = ClampAngle(y, yMinLimit, yMaxLimit);  //���� �����ɥ��d��
            //¶ Y�b �O ¶�y��A¶X�b �O �ɥ�

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            distance = Mathf.Clamp(// ���� �u�� �� ���� ���ʽd��
                distance - Input.GetAxis("Mouse ScrollWheel") * 5,
                distanceMin, distanceMax);
            // (�uZ�b �e�Ჾ�ʡ^

            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            offset = rotation * negDistance; //�̷s���סA�Z�� ���s��۹��m
            transform.rotation = rotation; // ��v�� �s����
        }
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Lt Button up!");
        }

        // ��v���s��m = �s�۹��m + ���y��m
        transform.position = player.transform.position + offset;

        if (Input.GetMouseButton(1))//Mouse Rt
        {
            Debug.Log("Rt Button down!");//testing

            force += Time.deltaTime * forceSpd;// �j�p�M�ɶ�������
            if (force >= highestSpd)
            {
                force = highestSpd;
            }

            Debug.Log("Force:  " + force);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            Debug.Log("Rt Button up!");

            //�����ݪ���Vthe direction of camera(eye)�G
            // Camera.main.transform.forward
            Vector3 movement = Camera.main.transform.forward;
            movement.y = 0.0f;      // no vertical movement ���W�U����
            //�O�q�Ҧ�impulse:�ĤO�Aspeed�G��t�j�p
            rbody.AddForce(movement * speed * force, ForceMode.Impulse);
            force = 0.0f;  // �O�q�κ��k0�A�ǳƤU�����s�W�O
        }

    }
    public static float ClampAngle(float angle, float min, float max)
    {// �ΤW�U�� ����
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;

        return Mathf.Clamp(angle, min, max);
    }

   
}
