using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public PlayerMovement playerMovement;
    public float cameraShiftDuration;
    public float cameraLookaheadDistance;

    Vector3 desiredDir;
    Vector3 dir = Vector3.zero;
    public Vector3 offset;

    /*private void Update()
    {
        //if (!endGame)
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) ChangeDirection(new Vector2(Mathf.RoundToInt(playerMovement.movement.x), Mathf.RoundToInt(playerMovement.movement.y)));
        
    }

    void ChangeDirection(Vector3 direction)
    {
        if (desiredDir != direction)
        {
            //prevDir = dir;

            desiredDir = direction;

            //StopCoroutine(FocusCamera());
            //StartCoroutine(FocusCamera());
        }
    }*/

    /*IEnumerator FocusCamera()
    {
        float time = 0;

        while (time < cameraShiftDuration)
        {
            dir = Vector3.Lerp(prevDir, desiredDir, time / cameraShiftDuration);

            time += Time.deltaTime;

            yield return null;
        }
        dir = desiredDir;
    }*/


    private void LateUpdate()
    {
        //if (!endGame)
        //transform.position = Vector3.SmoothDamp(player.position + offset, player.position + offset - desiredDir * cameraLookaheadDistance, ref dir, cameraShiftDuration);
        //transform.position = new Vector3(player.position.x, player.position.y, transform.position.z) + (dir * cameraLookaheadDistance);

        //transform.position = player.position + offset;

        desiredDir = player.position + new Vector3(0, -0.25f, offset.z);

        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredDir, ref dir, cameraShiftDuration);

        transform.position = smoothedPosition;
    }
}


/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Rigidbody2D targetRb;

    public Rigidbody2D rb;

    public float smoothSpeed;
    public Vector3 offset;

    private void FixedUpdate()
    {
        Vector3 desiredPosition = new Vector3(targetRb.position.x + offset.x, targetRb.position.y + offset.y, offset.z);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        rb.position = smoothedPosition;
    }
}
*/
