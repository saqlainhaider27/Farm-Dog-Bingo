using UnityEngine;
using UnityEngine.AI;

public class DogAI : MonoBehaviour {

    private MousePosition3D mousePosition3D;
    private NavMeshAgent agent;
    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        mousePosition3D = MousePosition3D.Instance;
    }
    private void Update() {
        agent.SetDestination(mousePosition3D.GetMovePosition());
    }
}
