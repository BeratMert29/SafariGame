using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Character
{
    private HingeJoint2D hingeJoint;
    private LineRenderer lineRenderer;
    public float lineSpeed = 100f; // Çizginin ilerleme hýzý
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
        if (Input.GetMouseButtonDown(0)) // Sol fare tuþuna basýldýysa
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

            int layerMask = LayerMask.GetMask("WebItem"); // Sadece "WebItem" katmanýna çarpmak için

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
            lineRenderer.enabled = false; // Fare tuþu býrakýldýðýnda çizgiyi gizle
            StopCoroutine(AnimateLine()); // Coroutine'i durdur
        }

        if (isDrawingLine)
        {
            lineRenderer.SetPosition(0, transform.position); // Baþlangýç pozisyonunu karakterin pozisyonuna göre sürekli güncelle
        }
    }

    IEnumerator AnimateLine()
    {
        lineRenderer.enabled = true;
        isDrawingLine = true; // Çizginin çizilme durumunu aktif et
        lineRenderer.SetPosition(0, transform.position); // Çizginin baþlangýç noktasý karakter pozisyonu
        float distance = Vector2.Distance(transform.position, targetPoint);
        float traveled = 0f;

        // Çizgi bitiþ noktasýný hedefe kadar adým adým taþý
        while (traveled < distance && isDrawingLine)
        {
            traveled += Time.deltaTime * lineSpeed;
            Vector2 currentPoint = Vector2.Lerp(transform.position, targetPoint, traveled / distance);
            lineRenderer.SetPosition(1, currentPoint); // Çizginin bitiþ noktasýný güncelle
            yield return null; // Bir sonraki kareye geç
        }

        lineRenderer.SetPosition(1, targetPoint); // Hedefe ulaþtýðýnda tam konumu ayarla
        canMove = false; // Hedefe ulaþtýðýnda canMove'u false yap
        hingeJoint.enabled = true; // HingeJoint'i aktif et
    }
}
