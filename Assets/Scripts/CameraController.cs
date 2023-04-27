using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;

    private Vector3 offset;//camera and player distance
    public float speed = 12.0f; //player speed
    public float forceSpd = 9.0f; //player charge speed
    private float force = 0.0f; //player已經蓄了多少力 的大小

    public float highestSpd = 50f;

    public float distance = 6.0f; //攝影機 離 母球 距離 初始值
    public float xSpeed = 120.0f; //the speed of mouse moving left and right
    public float ySpeed = 120.0f; //the speed of mouse moving up and down

    public float yMinLimit = -20f;  //滑鼠上下 轉仰角 下限
    public float yMaxLimit = 80f;   //滑鼠上下 轉仰角 上限

    public float distanceMin = 1.5f;  //滾輪 拉 攝影機 離 母球 距離下限
    public float distanceMax = 15f;  //滾輪 拉 攝影機 離 母球 距離上限

    private Rigidbody rbody;

    float x = 0.0f;
    float y = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //攝影機位置 - 母球位置 = 相對位置
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
            y = ClampAngle(y, yMinLimit, yMaxLimit);  //限制 仰角傾仰範圍
            //繞 Y軸 是 繞球轉，繞X軸 是 傾仰

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            distance = Mathf.Clamp(// 限制 滾輪 拉 遠近 移動範圍
                distance - Input.GetAxis("Mouse ScrollWheel") * 5,
                distanceMin, distanceMax);
            // (沿Z軸 前後移動）

            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            offset = rotation * negDistance; //依新角度，距離 重新算相對位置
            transform.rotation = rotation; // 攝影機 新角度
        }
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Lt Button up!");
        }

        // 攝影機新位置 = 新相對位置 + 母球位置
        transform.position = player.transform.position + offset;

        if (Input.GetMouseButton(1))//Mouse Rt
        {
            Debug.Log("Rt Button down!");//testing

            force += Time.deltaTime * forceSpd;// 大小和時間成正比
            if (force >= highestSpd)
            {
                force = highestSpd;
            }

            Debug.Log("Force:  " + force);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            Debug.Log("Rt Button up!");

            //眼睛看的方向the direction of camera(eye)：
            // Camera.main.transform.forward
            Vector3 movement = Camera.main.transform.forward;
            movement.y = 0.0f;      // no vertical movement 不上下移動
            //力量模式impulse:衝力，speed：初速大小
            rbody.AddForce(movement * speed * force, ForceMode.Impulse);
            force = 0.0f;  // 力量用盡歸0，準備下次重新蓄力
        }

    }
    public static float ClampAngle(float angle, float min, float max)
    {// 用上下限 夾值
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;

        return Mathf.Clamp(angle, min, max);
    }

   
}
