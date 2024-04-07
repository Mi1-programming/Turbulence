using System.Collections;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.Events;

public class River : MonoBehaviour
{
    public static River instance;
    public static Spline spline;

    [SerializeField] private SpriteShapeController ssc;
    [SerializeField] private int maxLength = 12;
    [SerializeField] private float maxTurnAngle = 90.0f;
    [SerializeField] private float creativity = 0.5f;
    [SerializeField] private float sharpness = 2.0f;
    [SerializeField] private float scale = 4.0f;
    [SerializeField] private float thickness = 4.0f;
    [SerializeField] private float jumpTime = 10.0f;
    [SerializeField] private int step = 4;
    [SerializeField] private int deathStep = 1;
    [SerializeField] private UnityEvent<Vector3> onJump;

    private float lastNormal = 0.0f;
    private Vector2 currentPoint = Vector2.zero;

    private void Awake()
    {
        instance = this;
    }
    public Vector2 GetWorldPos()
    {
        Spline spline = ssc.spline;
        return spline.GetPosition(0);
    }
    private Vector2 GetDir(float dir)
    {
        return Quaternion.AngleAxis(dir, Vector3.forward) * Vector2.right * scale;
    }
    private void BuildPath()
    {
        int i = spline.GetPointCount();
        if (i + 1 > maxLength) return;

        spline.InsertPointAt(i, currentPoint);
        spline.SetHeight(i, thickness);
        spline.SetTangentMode(i, ShapeTangentMode.Continuous);

        float dir = lastNormal + Random.Range(-maxTurnAngle, maxTurnAngle);
        float softDir = Mathf.Lerp(lastNormal, dir, creativity);
        Vector2 vecDir = GetDir(dir);
        Vector2 softVecDir = GetDir(softDir);

        spline.SetLeftTangent(i, -softVecDir / sharpness);
        spline.SetRightTangent(i, softVecDir / sharpness);

        currentPoint += vecDir;
        lastNormal = dir;
    }
    private void SendJumpEvent()
    {
        int pc = spline.GetPointCount();
        Vector3 curPos = spline.GetPosition(pc - 1);
        onJump.Invoke(curPos);
    }
    public void ContinuePath(int amount)
    {
        for (int i = 0;i < amount; i++) BuildPath();
        SendJumpEvent();
    }
    private IEnumerator Start()
    {
        spline = ssc.spline;
        spline.Clear();

        for (int i = 0; i < maxLength; i++) BuildPath();

        SendJumpEvent();

        while (true)
        {
            yield return new WaitForSeconds(jumpTime);
            for (int i = 0; i < step; i++)
            {
                BuildPath();
                spline.RemovePointAt(0);
            }
            for (int i = 0; i < deathStep; i++) spline.RemovePointAt(0);

            SendJumpEvent();
        }
    }
}
