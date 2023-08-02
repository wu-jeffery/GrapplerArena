using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGunScript : MonoBehaviour
{
    [SerializeField] float maxDistance;
    private LineRenderer lr;
    private Vector3 grapplePoint; 
    public LayerMask whatIsGrappleable;
    public Transform GunTip, camera, player;
    private SpringJoint joint;
    
    void Awake(){
        lr = GetComponent<LineRenderer>();
    }
    void Update(){
        if(Input.GetMouseButtonDown(0)){
            StartGrapple();
        }
        else if(Input.GetMouseButtonUp(0)){
            StopGrapple();
        }
    }
    
    //Called after Update
    void LateUpdate(){
        drawRope();
    }

    //call whenever we want to start a grapple
    void StartGrapple(){
        RaycastHit hit;
        if(Physics.Raycast(camera.position,camera.forward, out hit, maxDistance, whatIsGrappleable)){
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            //The distance grapple will try to keep from grapple point
            joint.maxDistance = distanceFromPoint = 0.8f;
            joint.minDistance = distanceFromPoint = 0.25f;

            //Change these values for preference
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
        }
    }
    void drawRope(){
        //If not grappling, don't draw rope
        if(!joint) return;
        lr.SetPosition(0, GunTip.position);
        lr.SetPosition(1, grapplePoint);
    }
    
    void StopGrapple(){
        lr.positionCount = 0;
        Destroy(joint);
    }
    public bool IsGrappling(){
        return joint != null;
    }
    public Vector3 GetGrapplePoint(){
        return grapplePoint;
    }
}
