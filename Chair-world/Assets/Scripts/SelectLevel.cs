using UnityEngine;
using UnityEngine.Events;

public class SelectLevel : MonoBehaviour {
    public FixedTouchField touchField;
	public Transform target;
	public Vector3 offset;
	public float sensitivity = 0.25f;
	public float limit = 80;
	public float zoom = 0.25f;

	public UnityEvent onClick;

	private float X, Y;

	private Camera _camera;

	private void Start () 
	{
		_camera = GetComponent<Camera>();
		limit = Mathf.Abs(limit);
		if(limit > 90) limit = 90;
		offset = new Vector3(offset.x, offset.y, -zoom);
		transform.position = target.position + offset;
	}

	private void Update ()
	{
		X = transform.localEulerAngles.y + touchField.TouchDist.x * sensitivity;
		Y += touchField.TouchDist.y * sensitivity;

		Y = Mathf.Clamp(Y, -limit, limit);
		transform.localEulerAngles = new Vector3(-Y, X, 0);
		transform.position = transform.localRotation * offset + target.position;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
				onClick.Invoke();
            #region badcode
            /*if (hit.collider.tag == "Russia" || hit.collider.tag == "USA" || hit.collider.tag == "France")

			else if(hit.collider == null)
			{
				onClick.Invoke();
			}*/
            #endregion
        }
    }
}