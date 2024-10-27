using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Oni.Message;
using MessageType = Oni.Message.MessageType;
using Sirenix.OdinInspector;

public class Damageable : MonoBehaviour
{
    // HP
    public int maxHitPoints;
    [Tooltip("무적 유지 시간")]
    public float invulnerabiltyTime;


    [Tooltip("데미지를 입을 수 있는 각도를 제한 hitForwardRotation을 기준으로 설정됨")]
    [Range(0.0f, 360.0f)]
    public float hitAngle = 360.0f;
    [Tooltip("hitAngleZone을 설정할 월드 forward방향")]
    [Range(0.0f, 360.0f)]
    public float hitForwardRotation = 360.0f;

    // 지금 무적상태인지 여부
    [ReadOnly]
    [ShowInInspector]
    public bool isInvulnerable { get; set; }
    // currentHP
    public int currentHitPoints { get; private set; }

    public UnityEvent OnReceiveDamage, OnDeath, OnHitWhileInvulnerable, OnBecomeVulnerable, OnResetDamage;

    [Tooltip("이 오브젝트가 데미지를 입을 때 알림받을 오브젝트들")]
    public List<MonoBehaviour> onDamageMessageReceivers;

    protected float m_timeSinceLastHit = 0.0f;
    protected Collider m_Collider;

    System.Action schedule;

    void Start()
    {
        ResetDamage();
        m_Collider = GetComponent<Collider>();
    }

    void Update()
    {
        if (isInvulnerable)
        {
            m_timeSinceLastHit += Time.deltaTime;
            if (m_timeSinceLastHit > invulnerabiltyTime)
            {
                m_timeSinceLastHit = 0.0f;
                isInvulnerable = false;
                OnBecomeVulnerable.Invoke();
            }
        }
    }

    public void ResetDamage()
    {
        currentHitPoints = maxHitPoints;
        isInvulnerable = false;
        m_timeSinceLastHit = 0.0f;
        OnResetDamage.Invoke();
    }

    public void SetColliderState(bool enabled)
    {
        m_Collider.enabled = enabled;
    }

    public void ApplyDamage(DamageMessage data)
    {
        if (currentHitPoints <= 0)
        {
            //이미 죽은 상태라면 리턴. TODO : 죽은 이후에도 데미지 판정을 체크해야 할 경우 처리...
            return;
        }

        if (isInvulnerable)
        {
            OnHitWhileInvulnerable.Invoke();
            return;
        }

        Vector3 forward = transform.forward;
        forward = Quaternion.AngleAxis(hitForwardRotation, transform.up) * forward;

        // 데미지를 가한 오브젝트와의 각도를 구하기 위한 dot 연산
        Vector3 positionToDamager = data.damageSource - transform.position;
        positionToDamager -= transform.up * Vector3.Dot(transform.up, positionToDamager);

        if (Vector3.Angle(forward, positionToDamager) > hitAngle * 0.5f)
            return;

        isInvulnerable = true;
        currentHitPoints -= data.amount;

        if (currentHitPoints <= 0)
            schedule += OnDeath.Invoke; // 서로 동시에 죽이는 상황이 왔을 때 발생할 문제를 대비하여 LateUpdate로 사망 이벤트를 넘김
        else
            OnReceiveDamage.Invoke();

        var messageType = currentHitPoints <= 0 ? MessageType.DEAD : MessageType.DAMAGED;

        for (var i = 0; i < onDamageMessageReceivers.Count; ++i)
        {
            var receiver = onDamageMessageReceivers[i] as IMessageReceiver;
            receiver.OnReceiveMessage(messageType, this, data);
        }
    }

    void LateUpdate()
    {
        if (schedule != null)
        {
            schedule();
            schedule = null;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Vector3 forward = transform.forward;
        forward = Quaternion.AngleAxis(hitForwardRotation, transform.up) * forward;

        if (Event.current.type == EventType.Repaint)
        {
            UnityEditor.Handles.color = Color.blue;
            UnityEditor.Handles.ArrowHandleCap(0, transform.position, Quaternion.LookRotation(forward), 1.0f,
                EventType.Repaint);
        }


        UnityEditor.Handles.color = new Color(1.0f, 0.0f, 0.0f, 0.5f);
        forward = Quaternion.AngleAxis(-hitAngle * 0.5f, transform.up) * forward;
        UnityEditor.Handles.DrawSolidArc(transform.position, transform.up, forward, hitAngle, 1.0f);
    }
#endif

    // 파라미터로 넘기는 DamageMessage구조체. 데미지 이벤트에 필요한 정보들을 추가
    public class DamageMessage
    {
        public MonoBehaviour damager;
        public int amount;
        public Vector3 direction;
        public Vector3 damageSource;
        public bool throwing;
        public bool isUpperAttack;
        public float verticalPower;
        public bool stopCamera;
        public bool isDownAttack;

        public class Builder
        {
            public MonoBehaviour Damager { get; private set; }
            public int Amount { get; private set; }
            public Vector3 Direction { get; private set; }
            public Vector3 DamageSource { get; private set; }
            public bool Throwing { get; private set; }
            public bool IsUpperAttack { get; private set; }
            public float VerticalPower { get; private set; }
            public bool StopCamera { get; private set; }
            public bool IsDownAttack { get; private set; }

            public Builder()
            {
                Damager = null;
                Amount = 0;
                Direction = Vector3.zero;
                DamageSource = Vector3.zero;
                Throwing = false;
                IsUpperAttack = false;
                VerticalPower = 0f;
                StopCamera = false;
                IsDownAttack = false;
            }

            public Builder SetDamager(MonoBehaviour damager)
            {
                this.Damager = damager;
                return this;
            }

            public Builder SetDamage(int amount, Vector3 direction, Vector3 damageSource)
            {
                this.Amount = amount;
                this.Direction = direction;
                this.DamageSource = damageSource;
                return this;
            }

            public Builder SetThrowing(bool throwing)
            {
                this.Throwing = throwing;
                return this;
            }

            public Builder SetUpperAttack(bool isUpperAttack, float verticalPower)
            {
                this.IsUpperAttack = isUpperAttack;
                this.VerticalPower = verticalPower;
                return this;
            }

            public Builder SetStopCamera(bool stopCamera)
            {
                this.StopCamera = stopCamera;
                return this;
            }

            public Builder SetIsDownAttack(bool isDownAttack, float verticalPower)
            {
                this.IsDownAttack = isDownAttack;
                this.VerticalPower = verticalPower;
                return this;
            }

            public DamageMessage Build()
            {
                DamageMessage msg = new DamageMessage();
                
                msg.damager = this.Damager;
                msg.amount = this.Amount;
                msg.direction = this.Direction;
                msg.damageSource = this.DamageSource;
                msg.throwing = this.Throwing;
                msg.isUpperAttack = this.IsUpperAttack;
                msg.verticalPower = this.VerticalPower;
                msg.stopCamera = this.StopCamera;
                msg.isDownAttack = this.IsDownAttack;

                return msg;
            }
        }
    }
}
