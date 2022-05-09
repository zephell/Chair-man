using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour {
    public FixedTouchField touchField;
	public Transform target;
	public Vector3 offset;
	public float sensitivity = 0.25f;
	public float limit = 80;
	public float zoom = 0.25f;

	public UnityEvent onClick;

	private float X, Y;

	void Start () 
	{
		limit = Mathf.Abs(limit);
		if(limit > 90) limit = 90;
		offset = new Vector3(offset.x, offset.y, -zoom);
		transform.position = target.position + offset;
	}

	void Update ()
	{
		X = transform.localEulerAngles.y + touchField.TouchDist.x * sensitivity;
		Y += touchField.TouchDist.y * sensitivity;
		Y = Mathf.Clamp (Y, -limit, limit);
		transform.localEulerAngles = new Vector3(-Y, X, 0);
		transform.position = transform.localRotation * offset + target.position;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "The best country - Russia")
				{
					onClick.Invoke();
				}
                if (hit.collider.tag == "USA")
				{
					onClick.Invoke();
				}
				if (hit.collider.tag == "France")
				{
					onClick.Invoke();
				}
				else if(hit.collider == null)
                {
					onClick.Invoke();
				}
            }
        }
	}
}