using System.Xml;
using System.ComponentModel;
using Scada.Web.Plugins.PlgScheme.Model.PropertyGrid;

namespace Scada.Web.Plugins.PlgSchShapeComp.Code.PropertyGrid
{
	[Serializable]
	public class BasicShapeConditions : AdvancedConditions
	{
		public BasicShapeConditions()
					: base()
		{
			
			Height = null;
			Width = null;
		}

		[DisplayName("Height"), Category(Categories.Appearance)]
		public int? Height { get; set; }

		[DisplayName("Width"), Category(Categories.Appearance)]
		public int? Width { get; set; }

		

		public override void LoadFromXml(XmlNode xmlNode)
		{
			base.LoadFromXml(xmlNode);
			XmlNode heightNode = xmlNode.SelectSingleNode("Height");
			Height = heightNode != null ? (int?)int.Parse(heightNode.InnerText) : null;

			XmlNode widthNode = xmlNode.SelectSingleNode("Width");
			Width = widthNode != null ? (int?)int.Parse(widthNode.InnerText) : null;
		}

		public override void SaveToXml(XmlElement xmlElem)
		{
			base.SaveToXml(xmlElem);
			if (Width.HasValue)
			{
				xmlElem.AppendElem("Width", Width.Value);
			}
			if (Height.HasValue)
			{
				xmlElem.AppendElem("Height", Height.Value);
			}
		}

	}
}