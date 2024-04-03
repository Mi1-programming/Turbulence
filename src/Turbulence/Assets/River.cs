using System.Collections;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.Events;

public class River : MonoBehaviour
{
    public static River instance;

    [SerializeField] private SpriteShapeController ssc;
    [SerializeField] private int maxLength = 12;
    [SerializeField] private float maxTurnAngle = 90.0f;
    [SerializeField] private float creativity = 0.5f;
    [SerializeField] private float sharpness = 2.0f;
    [SerializeField] private float scale = 4.0f;
    [SerializeField] private float thickness = 4.0f;
    [SerializeField] private float jumpTime = 10.0f;
    [SerializeField] private int step = 4;
    [SerializeField] private UnityEvent onJump;

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
    private void ContinuePath(Spline currentSpline)
    {
        int i = currentSpline.GetPointCount();
        if (i > maxLength)
        {
            currentSpline.RemovePointAt(0);
            i--;
        }

        currentSpline.InsertPointAt(i, currentPoint);
        currentSpline.SetHeight(i, thickness);
        currentSpline.SetTangentMode(i, ShapeTangentMode.Continuous);

        float dir = lastNormal + Random.Range(-maxTurnAngle, maxTurnAngle);
        float softDir = Mathf.Lerp(lastNormal, dir, creativity);
        Vector2 vecDir = GetDir(dir);
        Vector2 softVecDir = GetDir(softDir);

        currentSpline.SetLeftTangent(i, -softVecDir / sharpness);
        currentSpline.SetRightTangent(i, softVecDir / sharpness);

        currentPoint += vecDir;
        lastNormal = dir;
    }
    private IEnumerator Start()
    {
        Spline spline = ssc.spline;
        spline.Clear();

        for (int i = 0; i < maxLength; i++) ContinuePath(spline);

        while (true)
        {
            yield return new WaitForSeconds(jumpTime);
            for (int i = 0; i < step; i++) ContinuePath(spline);
            onJump.Invoke();
        }
    }
}
