﻿using System.Xml;
using System.ComponentModel;
using Scada.Web.Plugins.PlgScheme.Model;
using Scada.Web.Plugins.PlgScheme.Model.DataTypes;
using Scada.Log;


using Scada.Web.Plugins.PlgScheme.Model.PropertyGrid;
using Scada.Web.Plugins.PlgSchShapeComp.Code.PropertyGrid;


namespace Scada.Web.Plugins.PlgSchShapeComp.Code
{
	public class BarGraph : ComponentBase, IDynamicComponent
	{
		public BarGraph()
		{
			FillColor = "Blue";
			Conditions = new List<BarGraphConditions>();
			InCnlNum = 0;
			CtrlCnlNum = 0;
			InCnlNumCustom = "NA (0)";
			CtrlCnlNumCustom = "NA (0)";
			BorderWidth = 1;
			BorderColor = "Black";
			Rotation = 0;
		}

		/// <summary>
		/// Get or set the border width
		/// </summary>
		[DisplayName("Conditions"), Category(Categories.Behavior)]
		[Description("The conditions for Bar Graph output depending on the value of the input channel.")]
		[DefaultValue(null), TypeConverter(typeof(CollectionConverter))]
		public List<BarGraphConditions> Conditions { get; protected set; }

		/// <summary>
		/// Get or set the border width
		/// </summary>
		[DisplayName("Bar Color"), Category(Categories.Appearance)]
		[Description("The color of the Bar Graph.")]
		[DefaultValue("Blue")]
		public string FillColor { get; set; }
		
		/// <summary>
		/// Get or set the max value
		/// </summary>
		[DisplayName("Bar Max Value"), Category(Categories.Appearance)]
		[Description("The max value of the Bar Graph.")]
		[DefaultValue(100)]
		public double MaxValue { get; set; }

		//add min value property for bar graph
		[DisplayName("Bar Min Value"), Category(Categories.Appearance)]
		[Description("The min value of the Bar Graph.")]
		[DefaultValue(0)]
		public double MinValue { get; set; }

		/// <summary>
		/// Get or set the action
		/// </summary>
		[DisplayName("Action"), Category(Categories.Behavior)]
		[Description("The action executed by clicking the left mouse button on the component.")]
		[DefaultValue(Actions.None)]
		public Actions Action { get; set; }

		[DisplayName("Rotation"), Category(Categories.Appearance)]
		[Description("The rotation of the component.")]
		[DefaultValue(0)]
		public int Rotation { get; set; }


		/// <summary>
		/// Get or set the input channel number
		/// </summary>
		[DisplayName("Input channel"), Category(Categories.Data)]
		[Description("The input channel number associated with the component.")]
		public int InCnlNum { get; set; }

		/// <summary>
		/// Get or set the input channel number 
		/// </summary>
		[DisplayName("Input channel"), Category(Categories.Data)]
		[Description("The input channel number associated with the component.")]
		public string InCnlNumCustom { get; set; }

		/// <summary>
		/// Get or set the control channel number
		/// </summary>
		[DisplayName("Output channel"), Category(Categories.Data)]
		[Description("The output channel number associated with the component.")]
		public int CtrlCnlNum { get; set; }

		/// <summary>
		/// Get or set the control channel number custom
		/// </summary>
		[DisplayName("Output channel"), Category(Categories.Data)]
		[Description("The output channel number associated with the component.")]
		public string CtrlCnlNumCustom { get; set; }

		public override void LoadFromXml(XmlNode xmlNode)
		{
			base.LoadFromXml(xmlNode);
			FillColor = xmlNode.GetChildAsString("FillColor");
			Action = xmlNode.GetChildAsEnum<Actions>("Action");
			InCnlNum = xmlNode.GetChildAsInt("InCnlNum");
			CtrlCnlNum = xmlNode.GetChildAsInt("CtrlCnlNum");
			InCnlNumCustom = xmlNode.GetChildAsString("InCnlNumCustom");
			CtrlCnlNumCustom = xmlNode.GetChildAsString("CtrlCnlNumCustom");
			MaxValue = xmlNode.GetChildAsDouble("MaxValue");
			MinValue = xmlNode.GetChildAsDouble("MinValue");
			Rotation = xmlNode.GetChildAsInt("Rotation");
			XmlNode conditionsNode = xmlNode.SelectSingleNode("Conditions");

			if (conditionsNode != null)
			{
				Conditions = new List<BarGraphConditions>();
				XmlNodeList conditionNodes = conditionsNode.SelectNodes("Condition");
				foreach (XmlNode conditionNode in conditionNodes)
				{
					BarGraphConditions condition = new BarGraphConditions { SchemeView = SchemeView };
					condition.LoadFromXml(conditionNode);
					Conditions.Add(condition);
				}
			}
		}

		public override void SaveToXml(XmlElement xmlElem)
		{
			base.SaveToXml(xmlElem);

			xmlElem.AppendElem("Rotation", Rotation);
			XmlElement conditionsElem = xmlElem.AppendElem("Conditions");
			foreach (BarGraphConditions condition in Conditions)
			{
				XmlElement conditionElem = conditionsElem.AppendElem("Condition");
				condition.SaveToXml(conditionElem);
			}
			xmlElem.AppendElem("FillColor", FillColor);
			xmlElem.AppendElem("InCnlNum", InCnlNum);
			xmlElem.AppendElem("CtrlCnlNum", CtrlCnlNum);
			xmlElem.AppendElem("InCnlNumCustom", InCnlNumCustom);
			xmlElem.AppendElem("CtrlCnlNumCustom", CtrlCnlNumCustom);
			xmlElem.AppendElem("MaxValue", MaxValue);
			xmlElem.AppendElem("MinValue", MinValue);
			xmlElem.AppendElem("Action", Action.ToString());
		}

		public override ComponentBase Clone()
		{
			BarGraph cloneComponent = (BarGraph)base.Clone();

			foreach (BarGraphConditions condition in cloneComponent.Conditions)
			{
				condition.SchemeView = schemeView;
			}

			return cloneComponent;
		}
	}

}
