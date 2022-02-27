using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class calc_speed : MonoBehaviour
{
    [SerializeField] private AudioClip music;
    [SerializeField] UnityEvent stop_event;

    private double? start_x = null;
    private double? start_y = null;
    private double? start_z = null;
    private double dist_x;
    private double dist_y;
    private double dist_z;
    private double distance_3;
    private double act_dist;
    private string fold_path = "C:\\data_kanayama\\";
    private bool flg = true;
    private System.Diagnostics.Stopwatch time_sw;

    // Start is called before the first frame update
    void Start()
    {
        flg = data_util.test_flg;
        time_sw = new System.Diagnostics.Stopwatch();
    }

    // Update is called once per frame
    void Update()
    {
        if (start_x != null && start_y != null && start_z != null)
        {
            dist_x = transform.position.x - (double)start_x;
            dist_y = transform.position.y - (double)start_y;
            dist_z = transform.position.z - (double)start_z;

            distance_3 = Math.Sqrt(Math.Pow(dist_x, 2) + Math.Pow(dist_y, 2) + Math.Pow(dist_z, 2));
            act_dist = distance_3;
            var milli_sec = time_sw.ElapsedMilliseconds;
            double sec = (double)milli_sec / 1000;

            switch (data_util._vehicle_movement)
            {
                case data_util.VehicleMovement.acc:
                    act_dist -= 0.5 * (data_util.whill_acc * (data_util.mag - 1)) * Math.Pow(sec, 2);
                    break;
                case data_util.VehicleMovement.dec:
                    act_dist += 0.5 * (data_util.whill_acc * (data_util.mag - 1)) * Math.Pow(sec, 2);
                    break;
                case data_util.VehicleMovement.eq_acc:
                    act_dist -= (data_util.eq_whillspeed * (data_util.mag - 1)) * sec;
                    break;
                case data_util.VehicleMovement.eq_dec:
                    act_dist += (data_util.eq_whillspeed * (data_util.mag - 1)) * sec;
                    break;
            }
        }

        #region 目的距離通知
        if (distance_3 >= data_util.destination & flg)
        {
            GetComponent<AudioSource>().PlayOneShot(music);
            Debug.LogWarning(data_util.destination + "m到達");
            flg = false;
            stop_event.Invoke();
        }
        #endregion

        #region ボタン処理
        if (OVRInput.GetDown(OVRInput.RawButton.A) && !data_util.test_flg)
        {
                try
            {
                time_sw.Stop();
                DateTime dt = DateTime.Now;
                string file_info = data_util._subject + "\\" + data_util._vehicle_movement.ToString();

                Output_text(fold_path + file_info + ".txt");
                GetComponent<AudioSource>().PlayOneShot(music);
                stop_event.Invoke();
                Debug.LogWarning("Finish!!");
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            };
        }
        #endregion
    }

    //計測開始時イベント　スタート位置取得
    public void Get_start_pos()
    {
        start_x = transform.position.x;
        start_y = transform.position.y;
        start_z = transform.position.z;

        time_sw.Start();
    }

    //計測終了時データをファイル出力
    private void Output_text(string filename)
    {
        TimeSpan ts = time_sw.Elapsed;
        var milli_sec = time_sw.ElapsedMilliseconds;
        StringBuilder sb = new StringBuilder();
        DateTime dt = DateTime.Now;

        if (!Directory.Exists(fold_path + data_util._subject))
        {
            Directory.CreateDirectory(fold_path + data_util._subject);
            Debug.Log("make directory");
        }

        using (StreamWriter sw = new StreamWriter(filename, true, Encoding.GetEncoding("Shift_JIS")))
        {
            sb.Append(dt.ToString("g") + "\n");

            string sound_filename = "no_sound";
            if (data_util._sound_flg)
            {
                sound_filename = data_util._sound.ToString();
            }
            sb.Append("sound file: " + sound_filename + "  倍率: " + data_util.mag + "\n");

            double sec = (double)milli_sec / 1000;
            sb.Append("実走行距離: " + act_dist.ToString() + "\n");
            sb.Append("経過時間(s)：　" + ts.Seconds + "秒" + ts.Milliseconds + "\n");

            sw.WriteLine(sb.ToString());
        }
    }
}
