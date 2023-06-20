using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeypressTrigger : MonoBehaviour
{
    [SerializeField] KeyCode activatingKey = KeyCode.B;
    [SerializeField] bool destroyOnPress = false;
    [SerializeField] UnityEvent onKeyPress = null;
    [SerializeField] private float holdForTime = 0f;
    float timeHeld = 0f;

    float timeBetweenTriggers = .1f;
    float nextTrigger = 0f;

    void Update()
    {
        if (Input.GetKey(activatingKey))
            timeHeld += Time.deltaTime;
        if (Input.GetKey(activatingKey) && timeHeld >= holdForTime && Time.time >= nextTrigger) 
        {
            timeHeld = 0;

            nextTrigger = Time.time + timeBetweenTriggers;

            onKeyPress?.Invoke();

            if (destroyOnPress)
                Destroy(gameObject);

        }
        if (Input.GetKeyUp(activatingKey))
            timeHeld = 0f;
    }
}
