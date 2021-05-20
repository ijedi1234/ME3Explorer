﻿//This class was generated by ME3Explorer
//Author: Warranty Voider
//URL: http://sourceforge.net/projects/me3explorer/
//URL: http://me3explorer.freeforums.org/
//URL: http://www.facebook.com/pages/Creating-new-end-for-Mass-Effect-3/145902408865659
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using ME3Explorer.Unreal;
using ME3Explorer.Packages;
using KFreonLib.Debugging;
using SlimDX;
using SlimDX.Direct3D9;

namespace ME3Explorer.Unreal.Classes
{
    public class CoverLink
    {
        #region Unreal Props

        //Byte Properties

        public int Physics;
        public int LocationDescription;
        //Bool Properties

        public bool bHasCrossLevelPaths = false;
        public bool bAutoAdjust = false;
        public bool bDisabled = false;
        public bool bCanStepUpOn = false;
        public bool bPlayerOnly = false;
        public bool bLooped = false;
        public bool bBlockActors = false;
        public bool bCollideActors = false;
        public bool bPathColliding = false;
        public bool bPathsChanged = false;
        //Name Properties

        public int Tag;
        public int Group;
        //Object Properties

        public int nextNavigationPoint;
        public int CylinderComponent;
        public int CollisionComponent;
        public int NextCoverLink;
        public int Base;
        //Float Properties

        public float CircularRadius;
        public float MaxFireLinkDist;
        //Integer Properties

        public int visitedWeight;
        public int bestPathWeight;
        public int NetworkID;
        public int ApproximateLineOfFire;
        public int Cost;
        public int ExtraCost;
        //Vector3 Properties

        public Vector3 location;

        #endregion

        public int MyIndex;
        public ME3Package pcc;
        public byte[] data;
        public List<PropertyReader.Property> Props;
        public Matrix MyMatrix;
        public CustomVertex.PositionColored[] points;
        public CustomVertex.PositionColored[] points_sel;
        public bool isSelected = false;
        public bool isEdited = false;

        public CoverLink(ME3Package Pcc, int Index)
        {
            pcc = Pcc;
            MyIndex = Index;
            if (pcc.isExport(Index))
                data = pcc.Exports[Index].Data;
            Props = PropertyReader.getPropList(pcc.Exports[Index]);
            
            foreach (PropertyReader.Property p in Props)
                switch (pcc.getNameEntry(p.Name))
                {
                    #region
                    case "Physics":
                        Physics = p.Value.IntValue;
                        break;
                    case "LocationDescription":
                        LocationDescription = p.Value.IntValue;
                        break;
                    case "bHasCrossLevelPaths":
                        if (p.raw[p.raw.Length - 1] == 1)
                            bHasCrossLevelPaths = true;
                        break;
                    case "bAutoAdjust":
                        if (p.raw[p.raw.Length - 1] == 1)
                            bAutoAdjust = true;
                        break;
                    case "bDisabled":
                        if (p.raw[p.raw.Length - 1] == 1)
                            bDisabled = true;
                        break;
                    case "bCanStepUpOn":
                        if (p.raw[p.raw.Length - 1] == 1)
                            bCanStepUpOn = true;
                        break;
                    case "bPlayerOnly":
                        if (p.raw[p.raw.Length - 1] == 1)
                            bPlayerOnly = true;
                        break;
                    case "bLooped":
                        if (p.raw[p.raw.Length - 1] == 1)
                            bLooped = true;
                        break;
                    case "bBlockActors":
                        if (p.raw[p.raw.Length - 1] == 1)
                            bBlockActors = true;
                        break;
                    case "bCollideActors":
                        if (p.raw[p.raw.Length - 1] == 1)
                            bCollideActors = true;
                        break;
                    case "bPathColliding":
                        if (p.raw[p.raw.Length - 1] == 1)
                            bPathColliding = true;
                        break;
                    case "bPathsChanged":
                        if (p.raw[p.raw.Length - 1] == 1)
                            bPathsChanged = true;
                        break;
                    case "Tag":
                        Tag = p.Value.IntValue;
                        break;
                    case "Group":
                        Group = p.Value.IntValue;
                        break;
                    case "nextNavigationPoint":
                        nextNavigationPoint = p.Value.IntValue;
                        break;
                    case "CylinderComponent":
                        CylinderComponent = p.Value.IntValue;
                        break;
                    case "CollisionComponent":
                        CollisionComponent = p.Value.IntValue;
                        break;
                    case "NextCoverLink":
                        NextCoverLink = p.Value.IntValue;
                        break;
                    case "Base":
                        Base = p.Value.IntValue;
                        break;
                    case "CircularRadius":
                        CircularRadius = BitConverter.ToSingle(p.raw, p.raw.Length - 4);
                        break;
                    case "MaxFireLinkDist":
                        MaxFireLinkDist = BitConverter.ToSingle(p.raw, p.raw.Length - 4);
                        break;
                    case "visitedWeight":
                        visitedWeight = p.Value.IntValue;
                        break;
                    case "bestPathWeight":
                        bestPathWeight = p.Value.IntValue;
                        break;
                    case "NetworkID":
                        NetworkID = p.Value.IntValue;
                        break;
                    case "ApproximateLineOfFire":
                        ApproximateLineOfFire = p.Value.IntValue;
                        break;
                    case "Cost":
                        Cost = p.Value.IntValue;
                        break;
                    case "ExtraCost":
                        ExtraCost = p.Value.IntValue;
                        break;
                    case "location":
                        location = new Vector3(BitConverter.ToSingle(p.raw, p.raw.Length - 12),
                                              BitConverter.ToSingle(p.raw, p.raw.Length - 8),
                                              BitConverter.ToSingle(p.raw, p.raw.Length - 4));
                        break;
                    #endregion
                }
            MyMatrix = Matrix.Translation(location);
            GenerateMesh();
        }

        public void GenerateMesh()
        {
            List<CustomVertex.PositionColored> list = new List<CustomVertex.PositionColored>();

            float w = 20;
            float h = 100;
            Vector3 z = new Vector3();
            int c = Color.Orange.ToArgb();
            list.Add(new CustomVertex.PositionColored(z, c));
            list.Add(new CustomVertex.PositionColored(z + new Vector3(-w, -w, h), c));

            list.Add(new CustomVertex.PositionColored(z, c));
            list.Add(new CustomVertex.PositionColored(z + new Vector3(w, -w, h), c));

            list.Add(new CustomVertex.PositionColored(z, c));
            list.Add(new CustomVertex.PositionColored(z + new Vector3(w, w, h), c));

            list.Add(new CustomVertex.PositionColored(z, c));
            list.Add(new CustomVertex.PositionColored(z + new Vector3(-w, w, h), c));

            list.Add(new CustomVertex.PositionColored(z + new Vector3(-w, -w, h), c));
            list.Add(new CustomVertex.PositionColored(z + new Vector3(w, -w, h), c));

            list.Add(new CustomVertex.PositionColored(z + new Vector3(w, -w, h), c));
            list.Add(new CustomVertex.PositionColored(z + new Vector3(w, w, h), c));

            list.Add(new CustomVertex.PositionColored(z + new Vector3(w, w, h), c));
            list.Add(new CustomVertex.PositionColored(z + new Vector3(-w, w, h), c));

            list.Add(new CustomVertex.PositionColored(z + new Vector3(-w, w, h), c));
            list.Add(new CustomVertex.PositionColored(z + new Vector3(-w, -w, h), c));
            points = list.ToArray();
            points_sel = list.ToArray();
            for (int i = 0; i < points_sel.Length; i++)
                points_sel[i].Color = Color.Red.ToArgb();
        }

        public void SaveChanges()
        {
            if (isEdited)
            {
                byte[] buff = Vector3ToBuff(location);
                int f = -1;
                for (int i = 0; i < Props.Count; i++)
                    if (pcc.getNameEntry(Props[i].Name) == "location")
                    {
                        f = i;
                        break;
                    };
                if (f != -1)//has prop
                {
                    int off = Props[f].offend - 12;
                    for (int i = 0; i < 12; i++)
                        data[off + i] = buff[i];
                }
                else//have to add prop
                {
                    DebugOutput.PrintLn(MyIndex + " : cant find location property");
                }
                pcc.Exports[MyIndex].Data = data;
            }
        }

        public byte[] Vector3ToBuff(Vector3 v)
        {
            MemoryStream m = new MemoryStream();
            
            m.Write(BitConverter.GetBytes(v.X), 0, 4);
            m.Write(BitConverter.GetBytes(v.Y), 0, 4);
            m.Write(BitConverter.GetBytes(v.Z), 0, 4);
            return m.ToArray();
        }

        public void CreateModJobs()
        {
            if (isEdited)
            {
                byte[] buff = Vector3ToBuff(location);
                int f = -1;
                for (int i = 0; i < Props.Count; i++)
                    if (pcc.getNameEntry(Props[i].Name) == "location")
                    {
                        f = i;
                        break;
                    };
                if (f != -1)//has prop
                {
                    int off = Props[f].offend - 12;
                    for (int i = 0; i < 12; i++)
                        data[off + i] = buff[i];
                }
                else//have to add prop
                {
                    DebugOutput.PrintLn(MyIndex + " : cant find location property");
                }
                KFreonLib.Scripting.ModMaker.ModJob mj = new KFreonLib.Scripting.ModMaker.ModJob();
                string currfile = Path.GetFileName(pcc.FileName);
                mj.data = data;
                mj.Name = "Binary Replacement for file \"" + currfile + "\" in Object #" + MyIndex + " with " + data.Length + " bytes of data";
                string lc = Path.GetDirectoryName(Application.ExecutablePath);
                string template = System.IO.File.ReadAllText(lc + "\\exec\\JobTemplate_Binary2.txt");
                template = template.Replace("**m1**", MyIndex.ToString());
                template = template.Replace("**m2**", currfile);
                mj.Script = template;
                KFreonLib.Scripting.ModMaker.JobList.Add(mj);
                DebugOutput.PrintLn("Created Mod job : " + mj.Name);
            }
        }

        public void ProcessTreeClick(int[] path, bool AutoFocus)
        {
            isSelected = true;
        }

        public void ApplyTransform(Matrix m)
        {
            if (isSelected)
            {
                location += new Vector3(m.M41, m.M42, m.M43);
                MyMatrix = Matrix.Translation(location);
                isEdited = true;
            }
        }

        public void SetSelection(bool Selected)
        {
            isSelected = Selected;
        }

        public void Render(Device device)
        {
            device.SetRenderState(RenderState.Lighting, false);
            device.SetTransform(TransformState.World, MyMatrix);
            device.VertexFormat = CustomVertex.PositionColored.Format;
            if (points.Length != 0 && !isSelected)
                device.DrawUserPrimitives(PrimitiveType.LineList, points.Length / 2, points);
            if (points_sel.Length != 0 && isSelected)
                device.DrawUserPrimitives(PrimitiveType.LineList, points_sel.Length / 2, points_sel);
        }

        public TreeNode ToTree()
        {
            TreeNode res = new TreeNode(pcc.Exports[MyIndex].ObjectName + "(#" + MyIndex + ")");
            res.Nodes.Add("Physics : " + pcc.getNameEntry(Physics));
            res.Nodes.Add("LocationDescription : " + pcc.getNameEntry(LocationDescription));
            res.Nodes.Add("bHasCrossLevelPaths : " + bHasCrossLevelPaths);
            res.Nodes.Add("bAutoAdjust : " + bAutoAdjust);
            res.Nodes.Add("bDisabled : " + bDisabled);
            res.Nodes.Add("bCanStepUpOn : " + bCanStepUpOn);
            res.Nodes.Add("bPlayerOnly : " + bPlayerOnly);
            res.Nodes.Add("bLooped : " + bLooped);
            res.Nodes.Add("bBlockActors : " + bBlockActors);
            res.Nodes.Add("bCollideActors : " + bCollideActors);
            res.Nodes.Add("bPathColliding : " + bPathColliding);
            res.Nodes.Add("bPathsChanged : " + bPathsChanged);
            res.Nodes.Add("Tag : " + pcc.getNameEntry(Tag));
            res.Nodes.Add("Group : " + pcc.getNameEntry(Group));
            res.Nodes.Add("nextNavigationPoint : " + nextNavigationPoint);
            res.Nodes.Add("CylinderComponent : " + CylinderComponent);
            res.Nodes.Add("CollisionComponent : " + CollisionComponent);
            res.Nodes.Add("NextCoverLink : " + NextCoverLink);
            res.Nodes.Add("Base : " + Base);
            res.Nodes.Add("CircularRadius : " + CircularRadius);
            res.Nodes.Add("MaxFireLinkDist : " + MaxFireLinkDist);
            res.Nodes.Add("visitedWeight : " + visitedWeight);
            res.Nodes.Add("bestPathWeight : " + bestPathWeight);
            res.Nodes.Add("NetworkID : " + NetworkID);
            res.Nodes.Add("ApproximateLineOfFire : " + ApproximateLineOfFire);
            res.Nodes.Add("Cost : " + Cost);
            res.Nodes.Add("ExtraCost : " + ExtraCost);
            return res;
        }

    }
}