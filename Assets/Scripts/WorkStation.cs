using UnityEngine;

public class WorkStation : MonoBehaviour, IInteractable
{
    [SerializeField] private float interactionRadius;
    [SerializeField] private float objectRange;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInteract()
    {
        Debug.Log("Interacted with: " + name);
    }
}
