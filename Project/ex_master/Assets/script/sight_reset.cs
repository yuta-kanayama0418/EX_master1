using UnityEngine;
using UnityEngine.SpatialTracking;

public class sight_reset : MonoBehaviour
{
    [SerializeField] GameObject vr_camera;
    [SerializeField] GameObject event_wall;
    [SerializeField] int wait_cnt = 100;
    [SerializeField] float event_dist = 2.0f;
    [SerializeField] float player_height = 2.0f;

    private bool set_flg = true;
    private int count = 0;
    public static float random = 0.0f;

    // Start is called before the first frame update
    void Awake()
    {
        //スタート地点をランダムにする
        random = Random.value * 100;
    }

    // Update is called once per frame
    void Update()
    {
        //initialization
        count++;
        if (set_flg && count > wait_cnt)
        {
            Reset();
            set_flg = false;
        }
    }

    //視点のリセット
    public void Reset()
    {
        event_wall.SetActive(false);

        TrackedPoseDriver tpd = vr_camera.GetComponent<TrackedPoseDriver>();
        tpd.trackingType = TrackedPoseDriver.TrackingType.PositionOnly;

        correct_rotation();
        correct_position();

        tpd.trackingType = TrackedPoseDriver.TrackingType.RotationAndPosition;

        Vector3 pos = vr_camera.transform.position;
        pos.z += event_dist;
        event_wall.transform.position = pos;

        event_wall.SetActive(true);
    }

    private void correct_position()
    {
        float pos_x = vr_camera.transform.position.x;
        float pos_y = vr_camera.transform.position.y;
        float pos_z = vr_camera.transform.position.z;

        Vector3 pos = this.transform.position;

        this.transform.position = new Vector3(pos.x - pos_x, pos.y - pos_y + player_height, pos.z - pos_z + random);
    }

    private void correct_rotation()
    {
        Quaternion rot_vr_Q = vr_camera.transform.localRotation;
        Vector3 rot_vr = rot_vr_Q.eulerAngles;

        rot_vr.x = 0;
        rot_vr.z = 0;
        rot_vr.y = -rot_vr.y;

        this.transform.rotation = Quaternion.Euler(rot_vr);
    }
}
