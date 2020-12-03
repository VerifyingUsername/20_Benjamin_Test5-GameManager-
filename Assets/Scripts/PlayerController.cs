using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    float speed = 10.0f;
    public Animator playerAnim;

    public GameObject EnergyLeft;

    private int Energy;

    public int numberOfSpawn = 4;

    public GameObject addEnergyPrefab;
    public GameObject minusEnergyPrefab;
    bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canMove == true)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                Run();
            }
            else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
            {
                playerAnim.SetBool("isRun", false);
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
                Run();
            }
            else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                playerAnim.SetBool("isRun", false);
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                Run();
            }
            else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                playerAnim.SetBool("isRun", false);
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
                Run();
            }
            else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                playerAnim.SetBool("isRun", false);
            }
        }

        if (GMController.instance.levelTime <= 0)
        {
            canMove = false;
            SceneManager.LoadScene("LoseScene");
        }

        if (Energy < 0)
        {
            SceneManager.LoadScene("LoseScene");
        }

        if (Energy >= 50)
        {
            SceneManager.LoadScene("WinScene");
        }

    }
    // Run
    void Run()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        playerAnim.SetBool("isRun", true);
        playerAnim.SetFloat("StartRun", 0.26f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("AddEnergy"))
        {
            Energy += 5;
            EnergyLeft.GetComponent<Text>().text = "Energy: " + Energy;
            Destroy(collision.gameObject);
            GMController.instance.levelTime += 5;
            AddPrefabs();

        }
        else if (collision.gameObject.CompareTag("MinusEnergy"))
        {
            Energy -= 25;
            EnergyLeft.GetComponent<Text>().text = "Energy: " + Energy;
            Destroy(collision.gameObject);
            GMController.instance.levelTime -= 5;
        }
    }

    private void AddPrefabs()
    {
        for (int i = 0; i < numberOfSpawn; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(-10, 10), 0.5f, Random.Range(-10, 10));

            if (Random.Range(0, 2) < 1)
            {
                Instantiate(addEnergyPrefab, randomPos, Quaternion.identity);
            }
            else
            {
                Instantiate(minusEnergyPrefab, randomPos, Quaternion.identity);
            }
        }
    }
        
}
