﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ME3Explorer.Packages;
using ME3Explorer.Unreal;

namespace ME3Explorer.Pathfinding_Editor
{
    public partial class PathfindingNodeInfoPanel : UserControl
    {

        private IExportEntry export;
        private List<int> combatZones;

        public PathfindingEditor PathfindingEditorInstance { get; private set; }

        public PathfindingNodeInfoPanel()
        {
            InitializeComponent();
        }

        public void PassPathfindingNodeEditorIn(PathfindingEditor PathfindingEditorInstance)
        {
            this.PathfindingEditorInstance = PathfindingEditorInstance;
        }

        public void LoadExport(IExportEntry export)
        {
            this.export = export;
            reachableNodesList.Items.Clear();
            sfxCombatZoneList.Items.Clear();

            combatZones = new List<int>();

            var props = export.GetProperties();

            exportTitleLabel.Text = export.ObjectName + "_" + export.indexValue;

            //Calculate reachspecs
            ArrayProperty<ObjectProperty> PathList = export.GetProperty<ArrayProperty<ObjectProperty>>("PathList");
            if (PathList != null)
            {
                foreach (ObjectProperty prop in PathList)
                {
                    IExportEntry outgoingSpec = export.FileRef.Exports[prop.Value - 1];
                    StructProperty outgoingEndStructProp = outgoingSpec.GetProperty<StructProperty>("End"); //Embeds END
                    ObjectProperty outgoingSpecEndProp = outgoingEndStructProp.Properties.GetProp<ObjectProperty>("Actor"); //END                    
                    if (outgoingSpecEndProp.Value - 1 > 0)
                    {

                        IExportEntry endNode = export.FileRef.Exports[outgoingSpecEndProp.Value - 1];
                        string targetNodeName = endNode.ObjectName + "_" + endNode.indexValue;
                        reachableNodesList.Items.Add(targetNodeName + "(" + endNode.Index + ") via " + outgoingSpec.ObjectName + "_" + outgoingSpec.indexValue + "(" + outgoingSpec.Index + ")");
                    }
                    else
                    {
                        reachableNodesList.Items.Add("External File Node via " + outgoingSpec.ObjectName + "_" + outgoingSpec.indexValue + "(" + outgoingSpec.Index + ")");
                    }
                }
            }

            //Calculate SFXCombatZones
            ArrayProperty<StructProperty> volumes = props.GetProp<ArrayProperty<StructProperty>>("Volumes");
            if (volumes != null)
            {
                foreach (StructProperty volume in volumes)
                {
                    ObjectProperty actorRef = volume.GetProp<ObjectProperty>("Actor");
                    if (actorRef != null && actorRef.Value > 0)
                    {
                        IExportEntry combatZoneExport = export.FileRef.Exports[actorRef.Value - 1];
                        combatZones.Add(combatZoneExport.Index);
                        sfxCombatZoneList.Items.Add(combatZoneExport.ObjectName + "_" + combatZoneExport.indexValue + "(" + combatZoneExport.Index + ")");

                    }
                }
            }
        }

        private void sfxCombatZoneSelectionChanged(object sender, EventArgs e)
        {
            int n = sfxCombatZoneList.SelectedIndex;
            if (n == -1 || n < 0 || n >= combatZones.Count)
                return;

            if (PathfindingEditorInstance != null)
            {
                PathfindingEditorInstance.ActiveCombatZoneExportIndex = combatZones[n];
                PathfindingEditorInstance.RefreshView();
                PathfindingEditorInstance.graphEditor.Invalidate(); //force repaint
            }

        }
    }
}
