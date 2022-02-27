using UnityEngine;

public class data_util : MonoBehaviour
{
    [SerializeField] private string subject;
    [SerializeField] private bool test;
    [SerializeField] private bool sound_flg;
    [SerializeField] private bool visual_stimuli; //視覚刺激フラグ
    [SerializeField] private VehicleMovement vehicle_movement = VehicleMovement.acc;
    [SerializeField] private AudioClip[] sound;

    public static string _subject; //被験者名
    public static bool test_flg; //基準走行フラグ
    public static bool _sound_flg; //聴覚刺激フラグ
    public static AudioClip _sound;
    public static float mag; //視覚刺激倍率
    public static VehicleMovement _vehicle_movement; //実際のWHILLの走行タイプ

    public const float eq_whillspeed = 1.0f; //等速条件のWHILLの速度
    public const float whill_acc = 0.05367f; //WHILLの加速度
    public const float destination = 9.0f; //走行距離(m)

    public enum VehicleMovement
    {
        acc,
        dec,
        eq_acc,
        eq_dec
    }

    // Start is called before the first frame update
    void Awake()
    {
        _subject = subject;
        test_flg = test;
        _vehicle_movement = vehicle_movement;
        _sound_flg = sound_flg;
        if (visual_stimuli) mag = 1.5f;
        else mag = 1.0f;

        // sound set
        if (!_sound_flg) _sound = sound[4];
        else
        {
            switch (_vehicle_movement)
            {
                case VehicleMovement.acc:
                    _sound = sound[0];
                    break;
                case VehicleMovement.dec:
                    _sound = sound[1];
                    break;
                case VehicleMovement.eq_acc:
                    _sound = sound[2];
                    break;
                case VehicleMovement.eq_dec:
                    _sound = sound[3];
                    break;
            }
        }
    }
}
