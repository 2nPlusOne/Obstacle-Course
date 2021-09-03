using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Shapes;

public class CirclesDancing: ImmediateModeShapeDrawer
{
    [SerializeField] float discRadius;
    [SerializeField] Color color;
    [SerializeField] float movementRadius;
    [SerializeField] int numberOfCircles;

    float TAU = 2 * Mathf.PI;

    void Awake()
    {

    }
    public override void DrawShapes(Camera cam)
    {
        using (Draw.Command(cam))
        {
            Draw.ZTest = CompareFunction.Always; // Makes sure it draws on top of everything like a HUD
            Draw.BlendMode = ShapesBlendMode.Transparent; // Make overlapping shapes blend using the transparent blend mode
            Draw.LineGeometry = LineGeometry.Flat2D; // This setting depends on the shapes you are drawing
            Draw.Matrix = transform.localToWorldMatrix; // Draw relative to parent transform

            for (int i = 0; i < numberOfCircles; i++)
            {
                float angle = (TAU / (float)numberOfCircles) * i;
                float x = Mathf.Cos(angle) * movementRadius;
                float z = Mathf.Sin(angle) * movementRadius;

                Draw.Sphere(new Vector3(x, 0, z), discRadius, color);
            }
        }
    }
}
