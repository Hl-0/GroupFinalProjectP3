using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class RoomTeleport : MonoBehaviour
{
    public Transform roomAreaTeleport;
    public XROrigin origin;
    public Canvas sceneTransitioner;
    private Animator canvasAnimator;


    // Start is called before the first frame update
    void Start()
    {
        Instantiate(sceneTransitioner);
        canvasAnimator = sceneTransitioner.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TeleportToRoom()
    {
        StartCoroutine(FadeInCanvas());
        origin.transform.position = roomAreaTeleport.position;
        StartCoroutine(FadeOutCanvas());
    }

    IEnumerator FadeInCanvas()
    {
        canvasAnimator.Play("CanvasFadeIn");
        yield return new WaitForSeconds(0.75f);
    }

    IEnumerator FadeOutCanvas()
    {
        canvasAnimator.Play("CanvasFadeOut");
        yield return new WaitForSeconds(0.75f);
        sceneTransitioner.gameObject.SetActive(false);
    }
}
