﻿/*
 * SVG components rendering
 *
 * Author   : Messie MOUKIMOU
 * Created  : 2023
 * Modified : 
 *
 * Requires:
 * - jquery
 * - schemecommon.js
 * - schemerender.js
 */

<<<<<<< HEAD
/********** Shape Renderer **********/
scada.scheme.addInfoTooltipToDiv = function (targetDiv, text) {
	if (targetDiv instanceof jQuery) {
		targetDiv = targetDiv[0];
	}

	if (!targetDiv) return;
	if (!targetDiv) return;

	// Create tooltip
	const tooltip = document.createElement("div");
	tooltip.style.position = "absolute";
	tooltip.style.bottom = "45%";
	tooltip.style.left = "50%";
	tooltip.style.transform = "translateX(-50%)";
	tooltip.style.padding = "10px";
	tooltip.style.backgroundColor = "black";
	tooltip.style.color = "white";
	tooltip.style.borderRadius = "5px";
	tooltip.style.zIndex = "10";
	tooltip.style.whiteSpace = "nowrap";
	tooltip.style.marginBottom = "5px";

	tooltip.textContent = text;

	targetDiv.style.position = "relative";
	targetDiv.appendChild(tooltip);
=======
/********** Static SVG Shape Renderer **********/

scada.scheme.SvgShapeRenderer = function () {
	scada.scheme.ComponentRenderer.call(this);
>>>>>>> parent of b80ce931 (Merge pull request #7 from moukmessie/sr9-scadaweb-compnent)
};

scada.scheme.handleBlinking = function (divComp, blinking) {
	divComp.removeClass("no-blink slow-blink fast-blink");
	switch (blinking) {
		case 0:
			break;
		case 1:
			divComp.addClass("slow-blink");
			break;
		case 2:
			divComp.addClass("fast-blink");
			break;
	}
<<<<<<< HEAD
};
scada.scheme.updateStyles = function (divComp, cond) {
	if (cond.color) divComp.css("color", cond.color);
	if (cond.backgroundColor)
		divComp.css("background-color", cond.backgroundColor);
	if (cond.textContent)
		scada.scheme.addInfoTooltipToDiv(divComp[0], cond.textContent);
	if (cond.rotation)
		divComp.css("transform", "rotate(" + cond.rotation + "deg)");
	if (cond.isVisible !== undefined)
		divComp.css("visibility", cond.isVisible ? "visible" : "hidden");
	if (cond.width) divComp.css("width", cond.width);
	if (cond.height) divComp.css("height", cond.height);
};

scada.scheme.applyRotation = function (divComp, props) {
	if (props.rotation && props.rotation > 0) {
		divComp.css({
			transform: "rotate(" + props.rotation + "deg)",
		});
	}
};
scada.scheme.updateColors = function (divComp, cnlDataExt, isHovered, props) {
	var statusColor = cnlDataExt.color;

	var backColor = chooseColor(
		isHovered,
		props.backColor,
		props.backColorOnHover,
	);
	var borderColor = chooseColor(
		isHovered,
		props.borderColor,
		props.borderColorOnHover,
	);

	setBackColor(divComp, backColor, true, statusColor);
	setBorderColor(divComp, borderColor, true, statusColor);
};

scada.scheme.updateComponentData = function (component, renderContext) {
=======

	// Set SVG attributes for color and stroke width
	svgElement.setAttribute('fill', props.backColor);
	svgElement.setAttribute('stroke', props.borderColor); props.borderColor
	svgElement.setAttribute('stroke-width', props.borderWidth);

	return svgElement;
};

scada.scheme.SvgShapeRenderer.prototype.createDom = function (
	component,
	renderContext,
) {
	var props = component.props;
	var shapeType = props.shapeType;

	var divComp = $("<div id='comp" + component.id + "'></div>");
	this.prepareComponent(divComp, component, false, true);

	var svgElement = this.createSvgElement(shapeType, props);

	var svgNamespace = "http://www.w3.org/2000/svg";
	var svgContainer = document.createElementNS(svgNamespace, "svg");
	svgContainer.appendChild(svgElement);
	svgContainer.style.width = "100%";
	svgContainer.style.height = "100%";

	divComp.append(svgContainer);
	component.dom = divComp;
};
scada.scheme.SvgShapeRenderer.prototype.updateData = function (
	component,
	renderContext,
) {
>>>>>>> parent of b80ce931 (Merge pull request #7 from moukmessie/sr9-scadaweb-compnent)
	var props = component.props;

	if (props.inCnlNum <= 0) {
		return;
	}

	var divComp = component.dom;
	var cnlDataExt = renderContext.getCnlDataExt(props.inCnlNum);

	if (props.conditions && cnlDataExt.d.stat > 0) {
		var cnlVal = cnlDataExt.d.val;

		for (var cond of props.conditions) {
			if (scada.scheme.calc.conditionSatisfied(cond, cnlVal)) {
				scada.scheme.updateStyles(divComp, cond);
				scada.scheme.handleBlinking(divComp, cond.blinking);

<<<<<<< HEAD
				if (cond.rotation !== -1 && cond.rotation !== props.rotation) {
					scada.scheme.applyRotation(divComp, cond);
=======
		var svgElement = divComp.find("svg > *");
		svgElement.attr("fill", backColor);
		svgElement.attr("stroke", borderColor);

		this.setBackColor(divComp, backColor, true, statusColor);
		this.setBorderColor(divComp, borderColor, true, statusColor);

		if (props.conditions && cnlDataExt.d.stat > 0) {
			var cnlVal = cnlDataExt.d.val;

			for (var cond of props.conditions) {
				if (scada.scheme.calc.conditionSatisfied(cond, cnlVal)) {
					// Set CSS properties based on Condition
					if (cond.color) {
						divComp.css("color", cond.color);
					}
					if (cond.backgroundColor) {
						divComp.css("background-color", cond.backgroundColor);
					}
					if (cond.textContent) {
						divComp.text(cond.textContent);
					}
					divComp.css("visibility", cond.isVisible ? "visible" : "hidden");
					divComp.css("width", cond.width);
					divComp.css("height", cond.height);

					// Handle Blinking
					if (cond.blinking == 1) {
						divComp.addClass("slow-blink");
					} else if (cond.blinking == 2) {
						divComp.addClass("fast-blink");
					} else {
						divComp.removeClass("slow-blink fast-blink");
					}

					break;
>>>>>>> parent of b80ce931 (Merge pull request #7 from moukmessie/sr9-scadaweb-compnent)
				}
				break;
			}
		}
	}
};
<<<<<<< HEAD


=======

/******* Polygon shape */

scada.scheme.PolygonRenderer = function () {
	scada.scheme.ComponentRenderer.call(this);
};

scada.scheme.PolygonRenderer.prototype = Object.create(
	scada.scheme.ComponentRenderer.prototype,
);
scada.scheme.PolygonRenderer.constructor =
	scada.scheme.PolygonRenderer;

scada.scheme.PolygonRenderer.prototype.generatePolygonPath = function (
	numPoints,
) {

	// Check that numPoints is a valid value
	var validPoints = [3, 4, 5, 6, 8, 10];
	if (!validPoints.includes(numPoints)) {
		return "";
	}

	// Generate the points of the polygon
	var path = "";
	for (var i = 0; i < numPoints; i++) {
		var angle = (2 * Math.PI * i) / numPoints;
		var x = 50 + 50 * Math.cos(angle);
		var y = 50 + 50 * Math.sin(angle);
		path += x + "% " + y + "%, ";
	}

	// Remove trailing comma and space
	path = path.slice(0, -2);

	return "polygon(" + path + ")";
};

scada.scheme.PolygonRenderer.prototype.createDom = function (
	component,
	renderContext,
) {
	var props = component.props;

	var divComp = $("<div id='comp" + component.id + "'></div>");
	this.prepareComponent(divComp, component);

	var polygonPath = this.generatePolygonPath(props.numberOfSides);

	divComp.css({
		width: "200px",
		height: "200px",
		background: props.backColor,
		"clip-path": this.generatePolygonPath(props.numberOfSides),
		"border-width": props.borderWidth + "px",
		"border-color": props.borderColor,
		"border-radius": props.boundedCorners ? props.bornerRadius + "%" : "0%",
	});

	component.dom = divComp;
};

scada.scheme.PolygonRenderer.prototype.updateData = function (
	component,
	renderContext,
) {
	var props = component.props;

	if (props.inCnlNum > 0) {
		var divComp = component.dom;
		var cnlDataExt = renderContext.getCnlDataExt(props.inCnlNum);

		// choose and set colors of the component
		var statusColor = cnlDataExt.Color;
		var isHovered = divComp.is(":hover");

		var backColor = this.chooseColor(
			isHovered,
			props.backColor,
			props.backColorOnHover,
		);
		var borderColor = this.chooseColor(
			isHovered,
			props.borderColor,
			props.borderColorOnHover,
		);

		this.setBackColor(divComp, backColor, true, statusColor);
		this.setBorderColor(divComp, borderColor, true, statusColor);

		// Advanced Conditions
		if (props.conditions && cnlDataExt.d.stat > 0) {
			var cnlVal = cnlDataExt.d.val;

			for (var cond of props.conditions) {
				if (scada.scheme.calc.conditionSatisfied(cond, cnlVal)) {
					// Set CSS properties based on Condition
					if (cond.color) {
						divComp.css("color", cond.color);
					}
					if (cond.backgroundColor) {
						divComp.css("background-color", cond.backgroundColor);
					}
					if (cond.textContent) {
						divComp.text(cond.textContent);
					}
					divComp.css("visibility", cond.isVisible ? "visible" : "hidden");
					divComp.css("width", cond.width);
					divComp.css("height", cond.height);

					// Handle Blinking
					if (cond.blinking == 1) {
						divComp.addClass("slow-blink");
					} else if (cond.blinking == 2) {
						divComp.addClass("fast-blink");
					} else {
						divComp.removeClass("slow-blink fast-blink");
					}

					break;
				}
			}
		}
	}
};
>>>>>>> parent of b80ce931 (Merge pull request #7 from moukmessie/sr9-scadaweb-compnent)
/**************** Custom SVG *********************/
scada.scheme.CustomSVGRenderer = function () {
	scada.scheme.ComponentRenderer.call(this);
};

scada.scheme.CustomSVGRenderer.prototype = Object.create(
	scada.scheme.ComponentRenderer.prototype,
);
scada.scheme.CustomSVGRenderer.constructor =
	scada.scheme.CustomSVGRenderer;

scada.scheme.CustomSVGRenderer.prototype.createDom = function (
	component,
	renderContext,
) {
	var props = component.props;

	var divComp = $("<div id='comp" + component.id + "'></div>");
	this.prepareComponent(divComp, component, false, true);
	this.setBackColor(divComp, props.backColor);
	scada.scheme.applyRotation(divComp, props);

<<<<<<< HEAD
	if (props.svgCode && props.svgCode.includes("width") || props.svgCode.includes("height")) {
		props.svgCode = props.svgCode.replace(
			/<svg[^>]*?(\s+width\s*=\s*["'][^"']*["'])/g,
			"<svg",
		);
		props.svgCode = props.svgCode.replace(
			/<svg[^>]*?(\s+height\s*=\s*["'][^"']*["'])/g,
			"<svg",
		);
	}
	divComp.append(props.svgCode);
=======
	var svg = this.generateSVG(
		props.svgCode,
		props.borderColor,
		props.backColor,
		props.borderWidth,
		props.viewBoxX,
		props.viewBoxY,
		props.viewBoxWidth,
		props.viewBoxHeight,
		props.width,
		props.height);

	divComp.append(svg);
>>>>>>> parent of b80ce931 (Merge pull request #7 from moukmessie/sr9-scadaweb-compnent)
	component.dom = divComp;
};

scada.scheme.CustomSVGRenderer.prototype.updateData = function (
	component,
	renderContext,
) {
<<<<<<< HEAD
	scada.scheme.applyRotation(component.dom, component.props);
	scada.scheme.updateComponentData(component, renderContext);
};

/**
 * Basic shape renderer
 */
scada.scheme.BasicShapeRenderer = function () {
	scada.scheme.ComponentRenderer.call(this);
};

scada.scheme.BasicShapeRenderer.prototype = Object.create(
	scada.scheme.ComponentRenderer.prototype
);

scada.scheme.BasicShapeRenderer.constructor =
	scada.scheme.BasicShapeRenderer;


scada.scheme.BasicShapeRenderer.prototype.createDom = function (
	component,
	renderContext,
) {
	var props = component.props;
	var shapeType = props.shapeType;

	var divComp = $("<div id='comp" + component.id + "'></div>");
	var shape = $("<div class='shape '></div>");
	this.prepareComponent(divComp, component, false, true);
	if (shapeType == "Line") {
		shape.addClass(shapeType.toLowerCase());
		shape.css({
			"border-color": props.borderColor,
			"border-width": props.borderWidth,
			"border-style": "solid",
			"background-color": props.backColor,
		});

		divComp.css({
			display: "flex",
			"align-items": "center",
			"justify-content": "center",
		});

		divComp.append(shape);
	} else {
		divComp.addClass(shapeType.toLowerCase());
		this.setBackColor(divComp, props.backColor);
		this.setBorderColor(divComp, props.borderColor);
		if (props.borderWidth > 0) {
			this.setBorderWidth(divComp, props.borderWidth);
		}
	}

	scada.scheme.applyRotation(divComp, props);

	component.dom = divComp;
};

scada.scheme.BasicShapeRenderer.prototype.updateData = function (
	component,
	renderContext,
) {
	scada.scheme.applyRotation(component.dom, component.props);
	scada.scheme.updateComponentData(component, renderContext);
};


/**** 
 * BARGRAPH
 *  */
scada.scheme.BarGraphRenderer = function () {
	scada.scheme.ComponentRenderer.call(this);
};

scada.scheme.BarGraphRenderer.prototype = Object.create(
	scada.scheme.ComponentRenderer.prototype
);
scada.scheme.BarGraphRenderer.constructor = scada.scheme.BarGraphRenderer;

scada.scheme.BarGraphRenderer.prototype.createDom = function (component, renderContext) {
	var props = component.props;

	var divComp = $("<div id='comp" + component.id + "'></div>");

	var bar = $("<div class='bar' style='height:" + props.value + "%" + ";background-color:" + props.barColor + "' data-value='" + parseInt(props.Value) + "'></div>");

	divComp.append(bar);

	this.prepareComponent(divComp, component);

	divComp.css({
		"border": props.borderWidth + "px solid " + props.borderColor,
		"display": "flex",
		"align-items": "flex-end",
		"justify-content": "center"
	});

	component.dom = divComp;
};


scada.scheme.BarGraphRenderer.prototype.updateData = function (component, renderContext) {
	var props = component.props;
	if (props.inCnlNum > 0) {
		var divComp = component.dom;
		var cnlDataExt = renderContext.getCnlDataExt(props.inCnlNum);

		divComp.css({
			"border": props.borderWidth + "px solid " + props.borderColor,
			"background-color": props.backColor,
		})
		divComp.find('.bar').css({
			"background-color": props.barColor,
			"height": props.value + "%",
		});
		divComp.find('.bar').attr('data-value', parseInt(props.value));

	}

	if (props.conditions && cnlDataExt.d.stat > 0) {
		var cnlVal = cnlDataExt.d.val;

		for (var condition of props.conditions) {
			if (scada.scheme.calc.conditionSatisfied(condition, cnlVal)) {
				if (scada.scheme.calc.conditionSatisfied(condition, cnlVal)) {
					var barStyles = {};

					if (condition.level === "Min") {
						barStyles.height = "10%";
					} else if (condition.Level === "Low") {
						barStyles.height = "30%";
					} else if (condition.level === "Medium") {
						barStyles.height = "50%";
					} else if (condition.level === "High") {
						barStyles.height = "70%";
					} else if (condition.level === "Max") {
						barStyles.height = "100%";
					}
					if (condition.fillColor) {
						barStyles['background-color'] = condition.fillColor;
					}

					divComp.find('.bar').css(barStyles);

					if (condition.textContent) {
						scada.scheme.addInfoTooltipToDiv(divComp[0], condition.textContent);
					}
					// Set other CSS properties based on Condition
					if (condition.color) {
						divComp.css("color", condition.color);
					}
					if (condition.backgroundColor) {
						divComp.css("background-color", condition.backgroundColor);
					}
					if (condition.textContent) {
						divComp.text(condition.textContent);
					}
					divComp.css("visibility", condition.isVisible ? "visible" : "hidden");
					if (condition.width) {
						divComp.css("width", condition.width);
					}
					if (condition.height) {
						divComp.css("height", condition.height);
					}

					// Handle Blinking
					if (condition.blinking == 1) {
						divComp.addClass("slow-blink");
					} else if (condition.blinking == 2) {
						divComp.addClass("fast-blink");
					} else {
						divComp.removeClass("slow-blink fast-blink");
					}
				}
			}
		}
	}

};


=======
	var props = component.props;

	if (props.inCnlNum > 0) {
		var divComp = component.dom;
		var cnlDataExt = renderContext.getCnlDataExt(props.inCnlNum);

		// choose and set colors of the component
		var statusColor = cnlDataExt.Color;
		var isHovered = divComp.is(":hover");

		var backColor = this.chooseColor(
			isHovered,
			props.backColor,
			props.backColorOnHover,
		);
		var borderColor = this.chooseColor(
			isHovered,
			props.borderColor,
			props.borderColorOnHover,
		);

		this.setBackColor(divComp, backColor, true, statusColor);
		this.setBorderColor(divComp, borderColor, true, statusColor);

		divComp.removeClass("no-blink slow-blink fast-blink");
		if (props.conditions && cnlDataExt.d.stat > 0) {
			var cnlVal = cnlDataExt.d.val;
			for (var cond of props.conditions) {
				if (scada.scheme.calc.conditionSatisfied(cond, cnlVal)) {
					divComp.css("background-color", cond.color);
					switch (cond.blinking) {
						case 0:
							divComp.addClass("no-blink");
							break;
						case 1:
							divComp.addClass("slow-blink");
							break;
						case 2:
							divComp.addClass("fast-blink");
							break;
					}
					break;
				}
			}
		}
	}
};

>>>>>>> parent of b80ce931 (Merge pull request #7 from moukmessie/sr9-scadaweb-compnent)
/********** Renderer Map **********/

// Add components to the renderer map
scada.scheme.rendererMap.set("Scada.Web.Plugins.PlgSchShapeComp.Code.BasicShape", new scada.scheme.BasicShapeRenderer);
scada.scheme.rendererMap.set("Scada.Web.Plugins.PlgSchShapeComp.Code.CustomSVG", new scada.scheme.CustomSVGRenderer);
<<<<<<< HEAD
scada.scheme.rendererMap.set("Scada.Web.Plugins.PlgSchShapeComp.Code.BarGraph", new scada.scheme.BarGraphRenderer);
=======
scada.scheme.rendererMap.set("Scada.Web.Plugins.PlgSchShapeComp.Code.Polygon", new scada.scheme.PolygonRenderer);
>>>>>>> parent of b80ce931 (Merge pull request #7 from moukmessie/sr9-scadaweb-compnent)
