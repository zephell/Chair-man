using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("-----Player Settings-----")]
    [SerializeField] private float speed;
    [SerializeField] private float forwardForce;
    [SerializeField] private float forwardBoostMult; //Умножитель скорости при бусте
    [SerializeField] private float boostTime; //Сколько длится буст

    [SerializeField] private GameObject[] speedParticles;

    [Header("-----UI links-----")]
    [SerializeField] private FixedTouchField touchField;
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private Canvas gameCanvas;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOver;

    [Header("-----Camera-----")]
    [SerializeField] private float standartFov; //Стандартное поле зрения
    [SerializeField] private float BoostFov; //Ускоренное поле зрения
    [SerializeField] private float fovChangeVelocity = 5f; //

    [Header("-----Other-----")]
    [SerializeField] private Transform skinHolder;
    [SerializeField] private GameObject gameOverParticle;
    [SerializeField] private GameObject coinsParticles;


    // only private
    private Rigidbody rb;
    private bool boosted; 
    private bool cameraFOVscaled; 
    private float boostTimer;
    private float currentForce;
    private Camera mainCamera;
    private float forwardForcePrivate;
    private float forwardForceForPause;
    private bool isPaused = false;
    private bool isVibrate = false;


    private void Awake()
    {
        PlayerPrefs.SetInt("Equipped_Skin_Id", 0);
        PlayerPrefs.SetInt("Last_lvl", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.Save();
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>(); //get rigidbody

        forwardForcePrivate = forwardForce;
        forwardForce = 0;
    }


    public void StartGame()
    {
        mainCanvas.gameObject.SetActive(false);
        gameCanvas.gameObject.SetActive(true);
        Time.timeScale = 1;
        forwardForce = forwardForcePrivate;
        forwardForceForPause = forwardForcePrivate;
    }

    private void FixedUpdate()
    {
        Vector3 touchMoveVector = new Vector3(touchField.TouchDist.x * speed, 0, 0); //create touch Vector
        rb.AddForce(touchMoveVector.x, 0, 0, ForceMode.Impulse); // move x player
        Vector3 movePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + currentForce / 20); //create move Vector
        rb.MovePosition(movePosition); // move z player


        if(boostTimer > 0f) // Пока время больше 0 мы имеем буст
        {
            boostTimer -= Time.fixedDeltaTime; //отнимаем дельту времени для таймера
            currentForce = forwardForce * forwardBoostMult; //Текущая сила будет передня сила умноженая на умножитель буста 

            foreach (var item in speedParticles)
                item.SetActive(true);
        }
        else
        {
            boosted = false; //Таймер меньше 0 выключаем буст
            currentForce = forwardForce; //Делаем стандартною скорость

            foreach (var item in speedParticles)
                item.SetActive(false);
        }
    }


    private void Update()
    {
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        forwardForce = forwardForceForPause * Time.timeScale;
        
        if (boosted)
            mainCamera.fieldOfView = Mathf.MoveTowards(mainCamera.fieldOfView, BoostFov, Time.deltaTime * fovChangeVelocity);
        else
            mainCamera.fieldOfView = Mathf.MoveTowards(mainCamera.fieldOfView, standartFov, Time.deltaTime * fovChangeVelocity);
        
        if (cameraFOVscaled)
            mainCamera.fieldOfView = Mathf.MoveTowards(mainCamera.fieldOfView, 75, Time.deltaTime * 100);
        else
            mainCamera.fieldOfView = Mathf.MoveTowards(mainCamera.fieldOfView, standartFov, Time.deltaTime * 120);
        
        if(isPaused)
            Time.timeScale = Mathf.MoveTowards(Time.timeScale, 0.01f, Time.deltaTime * 10);
        else
            Time.timeScale = Mathf.MoveTowards(Time.timeScale, 1f, Time.deltaTime * 10);

        if(isVibrate) Handheld.Vibrate();

        IsFall();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Speed Boost")
        {
            SpeedBoost();
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.tag == "Coin")
            CoinEquip(collision.gameObject);
        
        if(collision.gameObject.tag == "Paper")
            PaperEquip(collision);

        if(collision.gameObject.tag == "Coffee")
            StartCoroutine(CoffeeEquip(collision.gameObject));
    }


    private void CoinEquip(GameObject other)
    {
        StartCoroutine(Vibrate(0.2f));
        Destroy(other);
        rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
        rb.AddRelativeTorque(0, 0, 2, ForceMode.Impulse);

        Instantiate(coinsParticles, transform.position, Quaternion.identity);

        CoinsData.IncreaseCoinsCount(1);
    }


    private void PaperEquip(Collider other)
    {
        other.isTrigger = false;
        rb.freezeRotation = false;
        rb.AddForce(0, 5, 0, ForceMode.Impulse);
        rb.AddRelativeTorque(0, 0, 2, ForceMode.Impulse);
        forwardForce = 0;
        speed = 0;
        gameCanvas.gameObject.SetActive(false);
        var particle = Instantiate(gameOverParticle, transform.position, Quaternion.identity);
        StartCoroutine(Vibrate(2f));
    }


    private IEnumerator CoffeeEquip(GameObject other)
    {
        StartCoroutine(Vibrate(0.2f));
        Destroy(other);
        float b = speed;
        speed = 0;
        forwardForce += 5;
        rb.AddRelativeTorque(0, 0, 10, ForceMode.Impulse);
        rb.AddForce(new Vector3(0, 10, -5), ForceMode.Impulse);
        cameraFOVscaled = true;
        yield return new WaitForSeconds(3f);
        cameraFOVscaled = false;
        speed = b;
        forwardForce -= 5;
    }


    private void SpeedBoost()
    {
        StartCoroutine(Vibrate(0.2f));
        rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
        rb.AddRelativeTorque(0, 0, 2, ForceMode.Impulse);
        boostTimer = boostTime;
        boosted = true;
    }


    public void Paused()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
    }

    private IEnumerator Vibrate(float time)
    {
        isVibrate = true;
        yield return new WaitForSeconds(time);
        isVibrate = false;
    }

    private void IsFall()
    {
        if (transform.position.y <= -1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}