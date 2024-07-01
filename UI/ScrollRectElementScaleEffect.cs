using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 스크롤 했을 때 element가 뷰포트 중앙에서 멀어질수록 스케일이 작아지는 효과 클래스
public class ScrollRectElementScaleEffect : MonoBehaviour
{
    [SerializeField]
    private ScrollRect myScrollRect;

    public float distanceFactor = 0.01f;

    private void Start()
    {
        OnValueChanged(Vector2.zero);
    }

    public void OnValueChanged(Vector2 value)
    {
        for (int i = 0; i < myScrollRect.content.childCount; i++)
        {
            Vector2 pos = myScrollRect.content.GetChild(i).position;
            // 전제 조건: 뷰포트의 피봇이 (0.5 ,0.5)여야 함
            Vector2 viewportCenter = myScrollRect.viewport.position;

            float distance = Vector2.Distance(pos, viewportCenter);
            float scale = Mathf.Lerp(1f, 0.1f, distance * distanceFactor);
            myScrollRect.content.GetChild(i).transform.localScale = Vector3.one * scale;
        }
    }
}
