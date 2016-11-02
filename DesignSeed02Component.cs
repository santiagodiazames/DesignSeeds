using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;

namespace DesignSeed02
{
    public class DesignSeed02Component : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public DesignSeed02Component()
          : base("DesignSeed02", "DS2", "First version of the DesignSeeds components", "Design Seeds", "Sprout")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Cells", "C", "Cell geometry and type descriptions", GH_ParamAccess.tree);
            pManager.AddTextParameter("Cell Description", "Cd", "A textual description of the Cell type", GH_ParamAccess.tree);
            pManager.AddTextParameter("System Description", "Sd", "A textual description of the System type", GH_ParamAccess.tree);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            //pManager.AddMeshParameter("Mesh", "M", "3D geometry", GH_ParamAccess.tree);
            //pManager.AddColourParameter("Colour", "C", "Mesh colours", GH_ParamAccess.tree);
            pManager.AddGenericParameter("Cells", "cells", "test cells", GH_ParamAccess.tree);
            pManager.AddIntegerParameter("Index of matched System", "i", "The index of the System that was matched", GH_ParamAccess.tree);

        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Get the curve tree (although the fact that it is curves is irrelevant).
            // I switched to IGH_Goo, but the generic constraint depends on the type of parameter you've picked.
            GH_Structure<IGH_Goo> treeData;
            DA.GetDataTree(0, out treeData);
            if (treeData.IsEmpty)
                return;

            // Get list of type descriptions (ie. must be one for each item in treeData).
            GH_Structure<GH_String> treeTypes;
            DA.GetDataTree(1, out treeTypes);
            if (treeTypes.PathCount != treeData.PathCount)
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Data and type trees must have the same layout");
                return;
            }

            // Get the category layout.
            GH_Structure<GH_String> treeCategories;
            DA.GetDataTree(2, out treeCategories);

            // Build category dictionary for easy lookup.
            Dictionary<string, GH_Path> table = new Dictionary<string, GH_Path>();
            for (int p = 0; p < treeCategories.PathCount; p++)
            {
                GH_Path path = treeCategories.Paths[p];
                List<GH_String> list = treeCategories.Branches[p];
                foreach (GH_String category in list)
                    table.Add(category.Value, path);
                // The above will throw exceptions if the same category name occurs more than once,
                // or if the category tree contains null items.
            }

            // Build a new tree. NEVER MODIFY THE OLD TREE, IT IS SHARED!
            GH_Structure<GH_Integer> treeIndices = new GH_Structure<GH_Integer>();
            GH_Structure<IGH_Goo> treeResult = new GH_Structure<IGH_Goo>();
            for (int p = 0; p < treeData.PathCount; p++)
            {
                List<IGH_Goo> listData = treeData.Branches[p];
                List<GH_String> listTypes = treeTypes.Branches[p];
                for (int i = 0; i < listData.Count; i++)
                {
                    string type = listTypes[i].Value;
                    GH_Path path = table[type];
                    treeResult.Append(listData[i], path);
                    treeIndices.Append(new GH_Integer(-1), path); // Sorry, what index?
                }
            }
            DA.SetDataTree(0, treeResult);
            DA.SetDataTree(1, treeIndices);
        }





        ////Declare a new List(Of T) to hold the input text data.
        //string cell_name = "nothing yet"; 
        //    List<string> system_names = new List<string>();
        //    int index;
            
        //    GH_Structure<GH_Curve> crvTree = new
        //    GH_Structure<GH_Curve>();

        //    //Retrieve the whole list of System Names using DA.GetDataList().
        //    if ((!DA.GetDataTree(0, out crvTree))) { return; }
        //    if ((!DA.GetData(1, ref cell_name))) { return; }
        //    if ((!DA.GetDataList(2, system_names))) { return; }

        //    index = system_names.IndexOf(cell_name);

        //    int index2 = -1;
        //    for(int i = 0; i < system_names.Count; i++)
        //    {
        //        if(String.Equals(system_names[i], cell_name, StringComparison.OrdinalIgnoreCase))
        //        {
        //            index2 = i;
        //            break;
        //        }
        //    }
            
        //    DA.SetData(0, index2);
        //    DA.SetDataTree(1, crvTree);

        //}

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{41fd022b-60bd-49b8-b2f4-3d64a088b0d8}"); }
        }
    }
}
