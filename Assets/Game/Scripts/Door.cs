using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject openText;
    public GameObject openDoor;
    public GameObject closeDoor;
    public AudioSource open;
    public bool inReach;
    public bool isOpen;

    void Start()
    {
        inReach = isOpen = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            openText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            openText.SetActive(false);
        }
    }

    void Update()
    {
        if (inReach && Input.GetButtonDown("Interact") && !isOpen)
            DoorOpen();
        else if (inReach && Input.GetButtonDown("Interact") && isOpen)
            DoorClose();
    }

    private void DoorOpen()
    {
        open.Play();
        openDoor.SetActive(true);
        closeDoor.SetActive(false);

        StartCoroutine(WaitOpen());
    }

    private void DoorClose()
    {
        openDoor.SetActive(false);
        closeDoor.SetActive(true);

        StartCoroutine(WaitClose());
    }

    IEnumerator WaitOpen()
    {
        yield return new WaitForSeconds(1f);
        isOpen = true;
    }

    IEnumerator WaitClose()
    {
        yield return new WaitForSeconds(1f);
        isOpen = false;
    }
}
