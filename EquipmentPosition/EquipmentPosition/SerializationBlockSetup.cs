﻿using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using JsonFindKey;
using JsonParse;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace EquipmentPosition
{
  public class JsonBlockSetup
  {
    public JsonBlockProperty SetupBlockProperty (BlockTableRecord btr, Transaction tr,  BlockReference blockReference, JsonBlockProperty jsonProperty)
    {
      jsonProperty.NextEquipment = blockReference.ObjectId;

      var setupAttributeProperty = new SerializationAttributeSetup();
      setupAttributeProperty.SetupAttributeProperty(tr, blockReference, jsonProperty);

      if (!btr.IsAnonymous && !btr.IsLayout)
      jsonProperty.Misc.BlockName = btr.Name;

      jsonProperty.Geometry.X = blockReference.Position.X;
      jsonProperty.Geometry.Y = blockReference.Position.Y;

      jsonProperty.Misc.Rotation = blockReference.Rotation;
      jsonProperty.General.Layer = blockReference.Layer;

      foreach (DynamicBlockReferenceProperty dbrProp in blockReference.DynamicBlockReferencePropertyCollection)
      {
        if (dbrProp.PropertyName == "Position X") { jsonProperty.Custom.TagX = DoubleConverter(dbrProp.Value); continue; }
        if (dbrProp.PropertyName == "Position Y") { jsonProperty.Custom.TagY = DoubleConverter(dbrProp.Value); continue; }
        if (dbrProp.PropertyName == "Position1 X") { jsonProperty.Custom.TagX1 = DoubleConverter(dbrProp.Value); continue; }
        if (dbrProp.PropertyName == "Position1 Y") { jsonProperty.Custom.TagY1 = DoubleConverter(dbrProp.Value); continue; }
        if (dbrProp.PropertyName == "Angle") { jsonProperty.Custom.Angle = DoubleConverter(dbrProp.Value); continue; }
        if (dbrProp.PropertyName == "Angle1") { jsonProperty.Custom.Angle1 = DoubleConverter(dbrProp.Value); continue; }
        if (dbrProp.PropertyName == "Angle2") { jsonProperty.Custom.Angle2 = DoubleConverter(dbrProp.Value); continue; }
        if (dbrProp.PropertyName == "Distance") { jsonProperty.Custom.Distance = DoubleConverter(dbrProp.Value); continue; }
        if (dbrProp.PropertyName == "Distance1") { jsonProperty.Custom.Distance1 = DoubleConverter(dbrProp.Value); continue; }
        if (dbrProp.PropertyName == "Distance2") { jsonProperty.Custom.Distance2 = DoubleConverter(dbrProp.Value); continue; }
        if (dbrProp.PropertyName == "Distance3") { jsonProperty.Custom.Distance3 = DoubleConverter(dbrProp.Value); continue; }
        if (dbrProp.PropertyName == "Distance4") { jsonProperty.Custom.Distance4 = DoubleConverter(dbrProp.Value); continue; }
        if (dbrProp.PropertyName == "Distance5") { jsonProperty.Custom.Distance5 = DoubleConverter(dbrProp.Value); continue; }
        if (dbrProp.PropertyName == "Flip state") { jsonProperty.Custom.FlipState = DoubleConverter(dbrProp.Value); continue; }
        if (dbrProp.PropertyName == "Flip state1") { jsonProperty.Custom.FlipState1 = DoubleConverter(dbrProp.Value); continue; }
        if (dbrProp.PropertyName == "Try1") { jsonProperty.Custom.Try1 = DoubleConverter(dbrProp.Value); continue; }
        if (dbrProp.PropertyName == "Try") { jsonProperty.Custom.Try = DoubleConverter(dbrProp.Value); continue; }
        if (dbrProp.PropertyName == "Housing") { jsonProperty.Custom.Housing = DoubleConverter(dbrProp.Value); continue; }
        if (dbrProp.PropertyName == "TTRY") { jsonProperty.Custom.TTRY = DoubleConverter(dbrProp.Value); continue; }

        //if (dbrProp.PropertyName == "Visibility1") { jsonProperty.Custom.Visibility1 = DoubleConverter(dbrProp.Value); continue; }
        //if (dbrProp.PropertyName == "Centrifugal Pump") { jsonProperty.Custom.PumpTableValue = DoubleConverter(dbrProp.Value); continue; }
        //if (jsonProperty.Misc.BlockName == "chamber" && dbrProp.PropertyName == "Visibility") 
        //{ jsonProperty.Custom.VisibilityValue = DoubleConverter(dbrProp.Value); continue; }
        //if (dbrProp.PropertyName == "Block Table1") { jsonProperty.Custom.BlockTableValue = DoubleConverter(dbrProp.Value); continue; }

        if (dbrProp.PropertyName == "Centrifugal Pump")
          jsonProperty.Custom.PumpTableValue = dbrProp.Value;

        else if (jsonProperty.Misc.BlockName == "chamber" && dbrProp.PropertyName == "Visibility")
          jsonProperty.Custom.VisibilityValue = dbrProp.Value;

        else if (dbrProp.PropertyName == "Block Table1")
          jsonProperty.Custom.BlockTableValue = dbrProp.Value;
      }
      return jsonProperty;
    }

    public double? DoubleConverter(object value)
    {
      if (value.GetType() != typeof(string))
      {
        double doubleValue = Convert.ToDouble(value);

        return doubleValue;
      }
      return null;
    }
  }
}
