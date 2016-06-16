using System.Web.UI;
using System.Web.UI.Design;

namespace ConSol.Data
{
    public class CustomControlDesigner : ControlDesigner
    {
        public override string GetDesignTimeHtml()
        {
            ControlCollection customControls = ((Control)Component).Controls;
            return base.GetDesignTimeHtml();
        }
    }
}