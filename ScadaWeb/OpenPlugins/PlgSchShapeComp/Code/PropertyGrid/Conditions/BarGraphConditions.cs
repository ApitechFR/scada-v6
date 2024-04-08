using Scada.Web.Plugins.PlgScheme.Model.DataTypes;
using Scada.Web.Plugins.PlgScheme.Model.PropertyGrid;
using System.ComponentModel;
using System.Xml;

namespace Scada.Web.Plugins.PlgSchShapeComp.Code.PropertyGrid
{
	[Serializable]
	public class BarGraphConditions : AdvancedConditions
	{
		
		public BarGraphConditions() : base()
		{
			FillColor = "None";
		}

		[DisplayName("Fill Color"), Category(Categories.Appearance)]
		public string FillColor { get; set; }

		public override void LoadFromXml(XmlNode xmlNode)
		{
			base.LoadFromXml(xmlNode);
			FillColor = xmlNode.GetChildAsString("FillColor");
		}

		public override void SaveToXml(XmlElement xmlElem)
		{
			base.SaveToXml(xmlElem);
			xmlElem.AppendElem("FillColor", string.IsNullOrEmpty(FillColor) ? "None" : FillColor);
		}

		public override object Clone()
		{
			Condition clonedCondition = this.DeepClone();
			clonedCondition.SchemeView = SchemeView;
			return clonedCondition;
		}
	}
}
