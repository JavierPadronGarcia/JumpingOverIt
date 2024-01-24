using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public GameObject objectToActivate;
    public Material brightButtonMaterial;

    private float moveDistance = 0.1f;
    private bool isActivated = false;

    private Quaternion doorOpenGrades;
    private Quaternion doorCloseGrades;

    private void Start()
    {
        doorCloseGrades = objectToActivate.transform.rotation;
        doorOpenGrades = Quaternion.Euler(0f, 90f, 0f) * doorCloseGrades;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isActivated)
        {
            StartCoroutine(ActivateButton());
            StartCoroutine(OpenDoor());
            isActivated = true;
        }
    }

    private IEnumerator ActivateButton()
    {
        Vector3 startPosition = transform.position;

        GetComponent<Renderer>().material = brightButtonMaterial;

        Vector3 finishPosition = startPosition - Vector3.up * moveDistance;

        float time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime * 0.7f;
            transform.position = Vector3.Lerp(startPosition, finishPosition, time);
            yield return null;
        }
    }

    private IEnumerator OpenDoor()
    {
        float time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime * 1f;
            objectToActivate.transform.rotation = Quaternion.Lerp(doorCloseGrades, doorOpenGrades, time);
            yield return null;
        }
    }

}
