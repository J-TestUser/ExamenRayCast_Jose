using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Raycast : MonoBehaviour
{
    private Vector2 _mouseInput;
    private InputAction _clickAction;
    private InputAction _mousePosition;

    [SerializeField] private float _timer = 5f;
    [SerializeField] private Text _timerText;

    private string _sceneName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        _clickAction = InputSystem.actions["Attack"];
        _mousePosition = InputSystem.actions["MousePosition"];        
    }
    void Start()
    {
        _timerText.text = _timer.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(_clickAction.WasPressedThisFrame())
        {
            ShootRaycast();
        }
    }

    void ShootRaycast()
    {
        _mouseInput = _mousePosition.ReadValue<Vector2>();

        Ray ray = Camera.main.ScreenPointToRay(_mouseInput);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if(hit.collider.CompareTag("Escena1"))
            {
                StartCoroutine(Timer());
                _sceneName = "Scene1";
                Debug.Log(_sceneName);


            }
            if(hit.collider.CompareTag("Escena2"))
            {
                StartCoroutine(Timer());
                _sceneName = "Scene2";
                Debug.Log(_sceneName);

            }

            if(hit.collider.CompareTag("Escena3"))
            {
                StartCoroutine(Timer());
                _sceneName = "Scene3";
                Debug.Log(_sceneName);                 
            }
        }
    }


    IEnumerator Timer()
    {
        _timer = 5;
    
        while(_timer >= 0)
        {
            _timer -= 0.1f;
            _timerText.text = Mathf.Max(0, _timer).ToString("F1");
            yield return new WaitForSeconds(0.1f);
        }
        if(_timer <= 0)
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}
