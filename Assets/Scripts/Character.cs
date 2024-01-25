using System.Linq;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Rigidbody2D Rigidbody2D;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject[] organHolder;
    [SerializeField] private float pickingRange;
    [SerializeField] private float speedMin;
    [SerializeField] private float speedMax;
    private int _messedUp;
    private bool _hasAnOrgan;
    private Organ _carryingOrgan;

    private Vector3 _offset;

    private void Update()
    {
        _offset = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            _offset += Vector3.up;
            transform.localScale = new(1, 1, 1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            offset += Vector3.left * speed * Time.deltaTime;
            transform.rotation = new Quaternion(0, 0, 0, 0);
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
            animator.SetBool("Side", true);
            _offset += Vector3.left;
            transform.localScale = new(-1, 1, 1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            offset += Vector3.right * speed * Time.deltaTime;
            transform.rotation = new Quaternion(0, 180, 0, 0);
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
            animator.SetBool("Side", true);
            _offset += Vector3.right;
            transform.localScale = new(1, 1, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            offset += Vector3.down * speed * Time.deltaTime;
            transform.rotation = new Quaternion();
            animator.SetBool("Up", true);
            animator.SetBool("Down", true);
            animator.SetBool("Side", false);
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W) ||
            Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
            animator.SetBool("Side", false);
            _offset += Vector3.down;
            transform.localScale = new(1, 1, 1);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PickUpOrgan();
            TransplantOrgan();
        }
    }
    private void FixedUpdate()
    {
        Rigidbody2D.MovePosition(transform.position + _offset * GetSpeed() * Time.fixedDeltaTime);

        //Debug
        Debug.DrawLine(new Vector3(transform.position.x, transform.position.y - 2.9f, 0),
            new Vector2(transform.position.x, transform.position.y - 2.9f + pickingRange));
        Debug.DrawLine(new Vector3(transform.position.x, transform.position.y + 2.9f, 0),
            new Vector2(transform.position.x, transform.position.y + 2.9f + pickingRange));
    }

    private void PickUpOrgan()
    {
        var charPos = transform.position;
        var ray = Physics2D.BoxCastAll(new Vector2(charPos.x, charPos.y /* - 2.9f*/), new Vector2(1.0f, pickingRange), 0,
            Vector2.down);
        
        
        foreach (var hit in ray.OrderBy(r => Vector2.Distance(r.transform.position, transform.position)))
        {
            if (hit.transform.gameObject.GetComponent<Organ>() == null)
                continue;
            if (Vector2.Distance(transform.position, hit.transform.position) >= pickingRange)
                continue;
            if (hit.transform.gameObject.GetComponent<Organ>().isInPlayerHands)
                continue;

            if (_hasAnOrgan)
            {
                //Swap Organs
                organHolder[_messedUp].transform.GetChild(0).transform.position = hit.transform.position;
                organHolder[_messedUp].transform.GetChild(0).GetComponent<Organ>().isInPlayerHands = false;
                organHolder[_messedUp].GetComponentInChildren<Organ>().transform.parent = null;
            }

            // Pick an Organ
            print(hit.transform.gameObject.name);
            hit.transform.parent = organHolder[_messedUp].transform;
            hit.transform.localPosition = new Vector3();
            hit.transform.gameObject.GetComponent<Organ>().isInPlayerHands = true;
            _hasAnOrgan = true;
            return;
        }
    }

    private void TransplantOrgan()
    {
        if (!_hasAnOrgan)
            return;
        var charPos = transform.position;
        var ray = Physics2D.BoxCastAll(new Vector2(charPos.x, charPos.y + 1), new Vector2(2, pickingRange), 0,
            Vector2.up);
        foreach (var hit in ray)
        {
            if (hit.transform.gameObject.GetComponent<Customer>() == null)
            {
                print("Null");
                continue;
            }


            if (Vector2.Distance(transform.position, hit.transform.position) >= pickingRange)
            {
                print("Dist: " + Vector2.Distance(transform.position, hit.transform.position));
                print("Range: " + pickingRange);
                continue;
            }


            hit.transform.gameObject.GetComponent<Customer>()
                .ImplantOrgan(organHolder[_messedUp].transform.GetChild(0).GetComponent<Organ>());
            Destroy(organHolder[_messedUp].transform.GetChild(0).GetComponent<Organ>().gameObject);
            _hasAnOrgan = false;
        }
    }

    private void PlayerMessedUp()
    {
        _messedUp++;
        if (_messedUp >= 3)
        {
            //GameOver
        }
    }

    private float GetSpeed()
    {
        return Mathf.Lerp(speedMin, speedMax, GameManager.Instance.GetDifficulty());
    }
}