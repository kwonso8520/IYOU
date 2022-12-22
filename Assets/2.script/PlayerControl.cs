using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class PlayerControl : MonoBehaviour
{
    public GameObject[] Soul;

    float speed;

    int score;
    public TextMeshProUGUI tmp;
    // Start is called before the first frame update
    void Start()
    {
        speed = 5;
        StartCoroutine("SoulCreate");
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, v, 0);
        GetComponent<Rigidbody2D>().velocity = dir * speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("WALL"))
        {
            GetComponent<SpriteRenderer>().color = collision.collider.GetComponent<SpriteRenderer>().color;

            StopCoroutine("ColorBack");
            StartCoroutine("ColorBack");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SOUL")) {
            //Player와 색이 같다면 = 사라짐
            if (GetComponent<SpriteRenderer>().color == collision.GetComponent <SpriteRenderer>().color)
            {
                speed += 0.5f;
                score += 2;
                tmp.text = "score : " + score;
                Destroy(collision.gameObject);
                StopCoroutine("SpeedBack");
                StartCoroutine("SpeedBack");
            }
            //Player와 색이 다르다면 = 도망감
            else if(GetComponent<SpriteRenderer>().color != collision.GetComponent<SpriteRenderer>().color)
            {
                float x = Random.Range(-8f, 8f);
                float y = Random.Range(-3f, 4f);
                int idx = Random.Range(0, 3 + 1);
                Vector3 pos = new Vector3(x, y, 0);
                collision.transform.position = pos;

                speed -= 0.5f;
                score -= 1;
                tmp.text = "score : " + score;
                StopCoroutine("SpeedBack");
                StartCoroutine("SpeedBack");
            }
        }
    }
    IEnumerator ColorBack()
    {
        yield return new WaitForSeconds(3.5f);
        GetComponent<SpriteRenderer>().color = Color.white;
        //Color color = new Color32(255, 255, 255, 255);
    }
    IEnumerator SoulCreate()
    {
        yield return new WaitForSeconds(3f);
        //생성코드
        
        float x = Random.Range(-8f, 8f);
        float y = Random.Range(-3f, 4f);
        int idx = Random.Range(0,3+1);
        Vector3 pos = new Vector3(x, y, 0);

        Instantiate(Soul[idx],pos,Quaternion.identity);
        StartCoroutine("SoulCreate");
    }
    IEnumerator SpeedBack()
    {
        yield return new WaitForSeconds(3f);
        speed = 5;
    }
    public void Left()
    {
        transform.position += new Vector3(-1, 0, 0) * speed * 0.1f;
    }
    public void Right()
    {
        transform.position += new Vector3(1, 0, 0) * speed * 0.1f;
    }
    public void Up()
    {
        transform.position += new Vector3(0, 1, 0) * speed * 0.1f;
    }
    public void Down()
    {
        transform.position += new Vector3(0, -1, 0) * speed * 0.1f;
    }
}
