using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    public Order order;

    private SpriteRenderer sprite;
    private System.Action onArrive;
    private Vector3 target;
    private bool moving;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void WalkTo(Vector3 position, System.Action arrived)
    {
        target = new Vector3(position.x, position.y);
        onArrive = arrived;
        moving = true;

        if (sprite != null)
        {
            sprite.flipX = target.x < transform.position.x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!moving)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        
        if(Vector3.Distance(transform.position, target) < 0.05f)
        {
            moving = false;
            System.Action callback = onArrive;
            onArrive = null;
            callback?.Invoke();
        }
    }
}
