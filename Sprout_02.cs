using System;
using Grasshopper.Kernel;

namespace DesignSeed02
{
    public class Sprout_02 : GH_Component
    {
        public Sprout_02() : base("Sprout_02", "S2", "The first Sprout component", "Extra", "Simple")
        {
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("b1e3b04b-98df-4d4c-9a7f-e08baa5436c4"); }
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("String", "S", "String to reverse", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Reverse", "R", "Reversed string", GH_ParamAccess.item);
        }
        
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Declare a variable for the input String
            string data = null;

            // Use the DA object to retrieve the data inside the first input parameter.
            // If the retieval fails (for example if there is no data) we need to abort.
            if (!DA.GetData(0, ref data)) { return; }

            // If the retrieved data is Nothing, we need to abort.
            // We're also going to abort on a zero-length String.
            if (data == null) { return; }
            if (data.Length == 0) { return; }

            // Convert the String to a character array.
            char[] chars = data.ToCharArray();

            // Reverse the array of character.
            System.Array.Reverse(chars);

            // Use the DA object to assign a new String to the first output parameter.
            DA.SetData(0, new string(chars));
        }

    }

}
