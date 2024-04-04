
using System.ComponentModel;
using System.Xml;
using Scada.Web.Plugins.PlgScheme.Model.DataTypes;
using Scada.Web.Plugins.PlgScheme.Model.PropertyGrid;
using static Scada.Web.Plugins.PlgSchShapeComp.Code.PropertyGrid.AdvancedConditions;

namespace Scada.Web.Plugins.PlgSchShapeComp.Code.PropertyGrid
{
	public class PictureConditions : ImageCondition
	{
		public PictureConditions()
			: base()
		{
			IsVisible = true;
			Blinking = BlinkingSpeed.None;
			Rotation = null;
		}

		[DisplayName("Rotation"), Category(Categories.Appearance)]
		[Description("The rotation angle of the shape in degrees.")]
		public int? Rotation { get; set; }



		[DisplayName("Blinking Speed"), Category(Categories.Appearance)]
		public BlinkingSpeed Blinking { get; set; }

		[DisplayName("Visible"), Category(Categories.Appearance)]
		public bool IsVisible { get; set; }


		public override void LoadFromXml(XmlNode xmlNode)
		{
			base.LoadFromXml(xmlNode);
			IsVisible = xmlNode.GetChildAsBool("IsVisible");
			Blinking = xmlNode.GetChildAsEnum<BlinkingSpeed>("Blinking");

			XmlNode rotationNode = xmlNode.SelectSingleNode("Rotation");
			Rotation = rotationNode != null ? (int?)int.Parse(rotationNode.InnerText) : null;
		}

		public override void SaveToXml(XmlElement xmlElem)
		{
			base.SaveToXml(xmlElem);
			if (Rotation.HasValue)
			{
				xmlElem.AppendElem("Rotation", Rotation.Value);
			}
			xmlElem.AppendElem("IsVisible", IsVisible);
			xmlElem.AppendElem("Blinking", Blinking);
		}

		public override object Clone()
		{
			Condition clonedCondition = this.DeepClone();
			clonedCondition.SchemeView = SchemeView;
			return clonedCondition;
		}
	}
}
