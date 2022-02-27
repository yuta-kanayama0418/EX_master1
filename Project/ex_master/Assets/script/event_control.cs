using UnityEngine;
using UnityEngine.Events;

public class event_control : MonoBehaviour
{
    [SerializeField] UnityEvent start_event;
    [SerializeField] UnityEvent debug_event;
    [SerializeField] UnityEvent sight_reset;

    private bool start_flg = true;

    void OnTriggerEnter()
    {
        if (start_flg)
        {
            start_event.Invoke();
            start_flg = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            debug_event.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sight_reset.Invoke();
        }
    }
}
