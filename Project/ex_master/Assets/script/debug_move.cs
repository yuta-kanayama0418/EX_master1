//WHILLを動かさずにVR上で走らせたいときのデバッグ用
using UnityEngine;

public class debug_move : MonoBehaviour
{
    [SerializeField]　private float distance;
    [SerializeField]　private float time;
    private Vector3 dest;
    private bool move_flg;

    public void Move()
    {
        move_flg = true;
    }

    private void Update()
    {
        if (move_flg) this.transform.position += new Vector3(0, 0, distance / time) * Time.deltaTime;
    }
}
