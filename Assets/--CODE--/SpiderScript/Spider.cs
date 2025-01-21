using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Character
{
    private HingeJoint2D hingeJoint;
    private LineRenderer lineRenderer;
    public float lineSpeed = 100f; // �izginin ilerleme h�z�
    private bool isDrawingLine = false;
    private Vector2 targetPoint;

    void Awake()
    {

    }

    void Start()
    {
        hingeJoint = gameObject.AddComponent<HingeJoint2D>();
        hingeJoint.enabled = false;
        hingeJoint.enableCollision = true;

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.enabled = false;
    }

    void Update()
    {
        Jump();
        Move();
        //Flip();
        Sliding();
    }

    private void Sliding()
    {
        if (Input.GetMouseButtonDown(0)) // Sol fare tu�una bas�ld�ysa
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

            int layerMask = LayerMask.GetMask("WebItem"); // Sadece "WebItem" katman�na �arpmak i�in

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, layerMask);

            if (hit.collider != null)
            {
                

                hingeJoint.enabled = true;
                hingeJoint.anchor = hingeJoint.transform.InverseTransformPoint(hit.point);

                targetPoint = hit.point;
                StartCoroutine(AnimateLine());
            }
            else
            {
                targetPoint = (Vector2)transform.position + direction * 100f;
                StartCoroutine(AnimateLine());
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            canMove = true;
            hingeJoint.enabled = false;
            isDrawingLine = false;
            lineRenderer.enabled = false; // Fare tu�u b�rak�ld���nda �izgiyi gizle
            StopCoroutine(AnimateLine()); // Coroutine'i durdur
        }

        if (isDrawingLine)
        {
            lineRenderer.SetPosition(0, transform.position); // Ba�lang�� pozisyonunu karakterin pozisyonuna g�re s�rekli g�ncelle
        }
    }

    IEnumerator AnimateLine()
    {
        lineRenderer.enabled = true;
        isDrawingLine = true; // �izginin �izilme durumunu aktif et
        lineRenderer.SetPosition(0, transform.position); // �izginin ba�lang�� noktas� karakter pozisyonu
        float distance = Vector2.Distance(transform.position, targetPoint);
        float traveled = 0f;

        // �izgi biti� noktas�n� hedefe kadar ad�m ad�m ta��
        while (traveled < distance && isDrawingLine)
        {
            traveled += Time.deltaTime * lineSpeed;
            Vector2 currentPoint = Vector2.Lerp(transform.position, targetPoint, traveled / distance);
            lineRenderer.SetPosition(1, currentPoint); // �izginin biti� noktas�n� g�ncelle
            yield return null; // Bir sonraki kareye ge�
        }

        lineRenderer.SetPosition(1, targetPoint); // Hedefe ula�t���nda tam konumu ayarla
        canMove = false; // Hedefe ula�t���nda canMove'u false yap
        hingeJoint.enabled = true; // HingeJoint'i aktif et
    }
}
