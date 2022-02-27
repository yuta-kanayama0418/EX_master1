using UnityEngine;

public class move_box : MonoBehaviour
{
    private bool move = false;
    private float speed = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (!data_util.test_flg && move)
        {
            switch (data_util._vehicle_movement)
            {
                case data_util.VehicleMovement.acc:
                    speed += data_util.whill_acc * (data_util.mag - 1) * Time.deltaTime;
                    this.transform.position += new Vector3(0, 0, speed) * Time.deltaTime;
                    break;
                case data_util.VehicleMovement.dec:
                    speed -= data_util.whill_acc * (data_util.mag - 1) * Time.deltaTime;
                    this.transform.position += new Vector3(0, 0, speed) * Time.deltaTime;
                    break;
                case data_util.VehicleMovement.eq_acc:
                    this.transform.position += new Vector3(0, 0, data_util.eq_whillspeed * (data_util.mag - 1)) * Time.deltaTime;
                    break;
                case data_util.VehicleMovement.eq_dec:
                    this.transform.position -= new Vector3(0, 0, data_util.eq_whillspeed * (data_util.mag - 1)) * Time.deltaTime;
                    break;
            }
        }
    }

    public void Move_box()
    {
        move = true;
    }

    public void Stop_box()
    {
        move = false;
    }
}
