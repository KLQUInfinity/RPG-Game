using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask movementMask;

    private Interactable focus;
    private Camera cam;
    private NavMeshAgent agent;
    private Transform target;

    void Start()
    {
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        //if we press left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                //move our player to what we hit
                MoveToPoint(hit.point);

                //stop focusing any objects
                RemoveFocus();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                //check if we hit an interactable
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                //if we did set it as our focus
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
        if (target != null)
        {
            agent.SetDestination(target.position);
            FaceTarget();
        }
    }

    void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }

    void FollowTarget(Interactable newTarget)
    {
        agent.stoppingDistance = newTarget.Radius * 0.8f;
        agent.updateRotation = false;

        target = newTarget.InteractionTransform;
    }

    void StopFollowingTarget()
    {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;

        target = null;
    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
            {
                focus.OnDefocused();
            }

            focus = newFocus;
            FollowTarget(newFocus);
        }


        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDefocused();
        }

        focus = null;
        StopFollowingTarget();
    }

    void FaceTarget()
    {
        Vector3 dir = (target.position - transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, 5f);
    }
}
