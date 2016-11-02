using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace DesignSeed02
{
    public class DesignSeed02Info : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "DesignSeed02";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return null;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("9f4492a3-5ad5-4c27-9fef-4178468bb72e");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "Perkins+Will";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "";
            }
        }
    }
}
